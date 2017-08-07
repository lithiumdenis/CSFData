using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cardiography
{
    public class Core
    {
        public struct Signal
        {
            public double O1, O2, O3;
            public Signal(double o1, double o2, double o3)
            {
                O1 = o1;
                O2 = o2;
                O3 = o3;
            }
        }

        public static void AddData(List<Core.Signal> Data, OpenFileDialog openFileDialog1)
        {
            openFileDialog1.FileName = null;
            openFileDialog1.InitialDirectory = "C:\\Users\\1\\Desktop";
            openFileDialog1.Filter = ".CRV files (*.crv)|*.crv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                    string[] buf;
                    for (int i = 5; i < lines.Length; i++) // Учитываем первые 5 строчек с мусором
                    {
                        buf = lines[i].Split('\t');
                        Data.Add(new Core.Signal(Convert.ToDouble(buf[0]), Convert.ToDouble(buf[1]), Convert.ToDouble(buf[2])));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Bandstop filter
        public static List<double> ButterworthFilter(List<Core.Signal> AscData)
        {
            int NZEROS = 4;
            int NPOLES = 4;
            double GAIN = 1.008925362e+00;
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
                xv[4] = AscData[i].O2 / GAIN;
                yv[0] = yv[1];
                yv[1] = yv[2];
                yv[2] = yv[3];
                yv[3] = yv[4];
                yv[4] = (xv[0] + xv[4]) - 3.8043011588 * (xv[1] + xv[3]) + 5.6181768268 * xv[2]
                     + (-0.9823854506 * yv[0]) + (3.7538940078 * yv[1])
                     + (-5.5683978994 * yv[2]) + (3.7873995331 * yv[3]);            
                Result.Add(yv[4]);
            }
            return Result;
        }

        public static List<double> BesselFilter(List<Core.Signal> AscData)
        {
            int NZEROS = 4;
            int NPOLES = 4;
            double GAIN = 1.008580037e+00;
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
                xv[4] = AscData[i].O2 / GAIN;
                yv[0] = yv[1];
                yv[1] = yv[2];
                yv[2] = yv[3];
                yv[3] = yv[4];
                yv[4] = (xv[0] + xv[4]) - 3.8043011588 * (xv[1] + xv[3]) + 5.6181768268 * xv[2]
                     + (-0.9830342908 * yv[0]) + (3.7558021245 * yv[1])
                     + (-5.5703343501 * yv[2]) + (3.7880734581 * yv[3]);
                Result.Add(yv[4]);
            }
            return Result;
        }

        public static List<double> ChebyshevFilter(List<Core.Signal> AscData)
        {
            int NZEROS = 4;
            int NPOLES = 4;
            double GAIN = 1.005933943e+00;
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
                xv[4] = AscData[i].O2 / GAIN;
                yv[0] = yv[1];
                yv[1] = yv[2];
                yv[2] = yv[3];
                yv[3] = yv[4];
                yv[4] = (xv[0] + xv[4]) - 3.8043011588 * (xv[1] + xv[3]) + 5.6181768268 * xv[2]
                     + (-0.9882538910 * yv[0]) + (3.7706883833 * yv[1])
                     + (-5.5849837733 * yv[2]) + (3.7930312513 * yv[3]);
                Result.Add(yv[4]);
            }
            return Result;
        }
    }
}
