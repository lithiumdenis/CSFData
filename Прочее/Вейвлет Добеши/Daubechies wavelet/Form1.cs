using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Daubechies_wavelet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Daubechies daubechies = new Daubechies();
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            int n = 256;
            List<double> Data = new List<double>();
            Random Rand = new Random();
            for (int i = 0; i < n; i++)
                Data.Add(Rand.NextDouble());

            double[] s = new double[Data.Count];
            for (int i = 0; i < Data.Count; i++)
                s[i] = Data[i];

            //Построение всплеска Добеши
            double[] sv = new double[Data.Count];
            for (int i = 0; i < Data.Count; i++)
                sv[i] = 0;

            sv[sv.Length - 30] = 1;
            daubechies.invDaubTrans(sv);
            
            daubechies.daubTrans(s);
            daubechies.invDaubTrans(s);

            listBox1.Items.Clear();
            listBox1.Items.Add("Data \t\t\t Restore Data \t\t Wavelet");

            for (int i = 0; i < Data.Count; i++)
            {
                chart1.Series[0].Points.AddXY(i, Data[i]);
                chart1.Series[1].Points.AddXY(i, s[i]);
                chart1.Series[2].Points.AddXY(i, sv[i]);
                listBox1.Items.Add(String.Format("{0:0.000000000000000}", Data[i]) + " \t " + String.Format("{0:0.000000000000000}", s[i]) + " \t " + String.Format("{0:0.000000000000000}", sv[i]));
            }
        }
    }

    class Daubechies
    {
        static double sqrt_3 = Math.Sqrt(3);
        static double denom = 4 * Math.Sqrt(2);
        //
        // forward transform scaling (smoothing) coefficients
        //
        static double h0 = (1 + sqrt_3) / denom;
        static double h1 = (3 + sqrt_3) / denom;
        static double h2 = (3 - sqrt_3) / denom;
        static double h3 = (1 - sqrt_3) / denom;
        //
        // forward transform wavelet coefficients
        //
        static double g0 = h3;
        static double g1 = -h2;
        static double g2 = h1;
        static double g3 = -h0;
        //
        // Inverse transform coefficients for smoothed values
        //
        static double Ih0 = h2;
        static double Ih1 = g2;  // h1
        static double Ih2 = h0;
        static double Ih3 = g0;  // h3
        //
        // Inverse transform for wavelet values
        //
        static double Ig0 = h3;
        static double Ig1 = g3;  // -h0
        static double Ig2 = h1;
        static double Ig3 = g1;  // -h2

        protected void transform(double[] a, int n)
        {
            if (n >= 4)
            {
                int i, j;
                int half = n >> 1;

                double[] tmp = new double[n];

                i = 0;
                for (j = 0; j < n - 3; j = j + 2)
                {
                    tmp[i] = a[j] * h0 + a[j + 1] * h1 + a[j + 2] * h2 + a[j + 3] * h3;
                    tmp[i + half] = a[j] * g0 + a[j + 1] * g1 + a[j + 2] * g2 + a[j + 3] * g3;
                    i++;
                }

                tmp[i] = a[n - 2] * h0 + a[n - 1] * h1 + a[0] * h2 + a[1] * h3;
                tmp[i + half] = a[n - 2] * g0 + a[n - 1] * g1 + a[0] * g2 + a[1] * g3;

                for (i = 0; i < n; i++)
                {
                    a[i] = tmp[i];
                }
            }
        }

        protected void invTransform(double[] a, int n)
        {
            if (n >= 4)
            {
                int i, j;
                int half = n >> 1;
                int halfPls1 = half + 1;

                double[] tmp = new double[n];

                //      last smooth val  last coef.  first smooth  first coef
                tmp[0] = a[half - 1] * Ih0 + a[n - 1] * Ih1 + a[0] * Ih2 + a[half] * Ih3;
                tmp[1] = a[half - 1] * Ig0 + a[n - 1] * Ig1 + a[0] * Ig2 + a[half] * Ig3;
                j = 2;
                for (i = 0; i < half - 1; i++)
                {
                    //     smooth val     coef. val       smooth val    coef. val
                    tmp[j++] = a[i] * Ih0 + a[i + half] * Ih1 + a[i + 1] * Ih2 + a[i + halfPls1] * Ih3;
                    tmp[j++] = a[i] * Ig0 + a[i + half] * Ig1 + a[i + 1] * Ig2 + a[i + halfPls1] * Ig3;
                }
                for (i = 0; i < n; i++)
                {
                    a[i] = tmp[i];
                }
            }
        }

        public void daubTrans(double[] s)
        {
            int N = s.Length;
            int n;
            for (n = N; n >= 4; n >>= 1)
            {
                transform(s, n);
            }
        }

        public void invDaubTrans(double[] coef)
        {
            int N = coef.Length;
            int n;
            for (n = 4; n <= N; n <<= 1)
            {
                invTransform(coef, n);
            }
        }
    }
}
