using System;
using System.Collections.Generic;
using System.Numerics;
using System.Windows.Forms;

namespace Alpha_Waves
{
    public class CoreFunctions
    {
        public struct Signal
        {
            public double Stim, Fp1, Fp2, Fpz, F3, F4, Fz, C3, C4, Cz, P3, P4, Pz, O1, O2, Oz, F7, F8, T3, T4, T5, T6;
            public Signal(double stim, double fp1, double fp2, double fpz, double f3, double f4, double fz, double c3, double c4,
                double cz, double p3, double p4, double pz, double o1, double o2, double oz, double f7, double f8,
                double t3, double t4, double t5, double t6)
            {
                Stim = stim;
                Fp1 = fp1;
                Fp2 = fp2;
                Fpz = fpz;
                F3 = f3;
                F4 = f4;
                Fz = fz;
                C3 = c3;
                C4 = c4;
                Cz = cz;
                P3 = p3;
                P4 = p4;
                Pz = pz;
                O1 = o1;
                O2 = o2;
                Oz = oz;
                F7 = f7;
                F8 = f8;
                T3 = t3;
                T4 = t4;
                T5 = t5;
                T6 = t6;
            }
        }

        public static double getMaximumPower(int from, int to, Dictionary<int, Complex> Density)
        {
            double max = -1;
            for (int i = from; i < to; i++)
            {
                if (Density[i].Real > max)
                    max = Density[i].Real;
            }
            return max;
        }

        public static Dictionary<int, Complex> takePowerDensity(int from, int to, List<double> FilteredData)
        {
            Dictionary<int, Complex> Density = new Dictionary<int, Complex>();
            Dictionary<int, Complex> fm = new Dictionary<int, Complex>();
            Dictionary<int, Complex> ck = new Dictionary<int, Complex>();
            Complex[] RESCK;

            for (int i = -100, k = from; i <= 100 && k < to; i++, k++)
                fm[i] = new Complex(FilteredData[k], 0);

            for (int i = -100; i <= 100; i++)
                ck[i] = dft(i, 100, fm);

            RESCK = new Complex[ck.Count - 1];

            for (int i = 0; i < ck.Count - 1; i++)
                RESCK[i] = ck[i - 100];

            for (int i = from, k = 0; i < to; i++, k++)
                Density[i] = Math.Pow(Math.Abs(RESCK[k].Real), 2) + Math.Pow(Math.Abs(RESCK[k].Imaginary), 2);

            return Density;
        }

        public static void AddData(List<CoreFunctions.Signal> AscData, OpenFileDialog openFileDialog1)
        {
            openFileDialog1.FileName = null;
            openFileDialog1.InitialDirectory = "C:\\Users\\1\\Desktop";
            openFileDialog1.Filter = "Asc files (*.asc)|*.asc";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                    string[] buf;
                    for (int i = 4; i < lines.Length; i++) // Учитываем первые 4 строчки с мусором из .asc
                    {
                        buf = lines[i].Split(' ');
                        AscData.Add(new CoreFunctions.Signal(Convert.ToDouble(buf[0]), Convert.ToDouble(buf[1]), Convert.ToDouble(buf[2]),
                            Convert.ToDouble(buf[3]), Convert.ToDouble(buf[4]), Convert.ToDouble(buf[5]),
                            Convert.ToDouble(buf[6]), Convert.ToDouble(buf[7]), Convert.ToDouble(buf[8]),
                            Convert.ToDouble(buf[9]), Convert.ToDouble(buf[10]), Convert.ToDouble(buf[11]),
                            Convert.ToDouble(buf[12]), Convert.ToDouble(buf[13]), Convert.ToDouble(buf[14]),
                            Convert.ToDouble(buf[15]), Convert.ToDouble(buf[16]), Convert.ToDouble(buf[17]),
                            Convert.ToDouble(buf[18]), Convert.ToDouble(buf[19]), Convert.ToDouble(buf[20]),
                            Convert.ToDouble(buf[21])));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public static Complex dft(int k, int n, Dictionary<int, Complex> data)
        {
            Complex sum = 0;
            for (int i = -n; i < n; i++)
                sum += data[i] * Complex.Exp(-Complex.ImaginaryOne * 2 * Math.PI * k * i / (2 * n + 1));
            return sum / (2 * n + 1);
        }

        public static List<double> ButterworthFilter(List<CoreFunctions.Signal> AscData)
        {
            int NZEROS = 4;
            int NPOLES = 4;
            double GAIN = 2.761027558e+02;
            double[] xv, yv;
            List<double> Result = new List<double>();
            xv = new double[NZEROS + 1];
            yv = new double[NPOLES + 1];

            for (int i = 0; i < AscData.Count; i++)
            {
                xv[0] = xv[1];
                xv[1] = xv[2];
                xv[2] = xv[3];
                xv[3] = xv[4];
                xv[4] = AscData[i].Oz / GAIN;
                yv[0] = yv[1];
                yv[1] = yv[2];
                yv[2] = yv[3];
                yv[3] = yv[4];
                yv[4] = (xv[0] + xv[4]) - 2 * xv[2] + (-0.8371816513 * yv[0]) + (3.3324758953 * yv[1]) + (-5.1461877541 * yv[2]) + (3.6427871267 * yv[3]);
                Result.Add(yv[4]);
            }
            return Result;
        }
    }
}
