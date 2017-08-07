using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Numerics;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart1.Series[0].Color = Color.Blue; // цвет  линий
            chart1.Series[1].Color = Color.Red;

            chart1.Series[0].BorderWidth = 1; // толщина линий
            chart1.Series[1].BorderWidth = 2;

            int n = Convert.ToInt32(textBox1.Text);
            int N = 2 * n + 1;

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            Random rand = new Random();

            Complex[] F = new Complex[N];
            Complex[] G = new Complex[N];
            Complex[] Sv1 = new Complex[N];

            for (int i = 0; i < N; i++)
                F[i] = rand.NextDouble();

            for (int i = 0; i < N; i++)
                G[i] = rand.NextDouble();

            for (int i = 0; i <N; i++)
               Sv1[i] = svertkaOp(i, n, F, G);

            // Считаем ДПФ для F и G

            Complex[] FD = new Complex[N];
            Complex[] GD = new Complex[N];

            for (int i = 0; i < N; i++)
                FD[i] = dft(i, n, F);

            for (int i = 0; i < N; i++)
                GD[i] = dft(i, n, G);

            // Левая часть проверки
            
            Complex[] DPFS = new Complex[N];
            for (int i = 0; i < N; i++)
                DPFS[i] = dft(i, n, Sv1);
           
            for (int i = 0; i < N; i++)
                chart1.Series[1].Points.AddXY(i, DPFS[i].Real);

            // Правая часть проверки
            Complex[] Sv3 = new Complex[N];
            for (int i = 0; i <N; i++)
                Sv3[i] = (FD[i] * GD[i]) * N;

            for (int i = 0; i <N; i++)
                chart1.Series[0].Points.AddXY(i, Sv3[i].Real);
        }

        public Complex svertkaOp(int m, int n, Complex[] f, Complex[] g)
        {
            Complex sum = 0;
           
            for (int k = 0; k < f.Length; k++)
                sum += f[(m - k + f.Length) % (f.Length)] * g[k];

            return sum;
        }

        public Complex dft(int k, int n, Complex[] a)
        {
            Complex sum = 0;
            for (int i = 0; i < a.Length; i++)
                sum += a[i] * Complex.Exp(-Complex.ImaginaryOne * 2 * Math.PI * k * i / (2 * n + 1));

            return sum / (2 * n + 1);
        }
    }
}
