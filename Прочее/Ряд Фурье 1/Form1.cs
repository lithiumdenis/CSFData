using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static double series(double current, int N)
        {
            double sum = 0;

            for (int k = 1; k < N; k++)
            {
                sum += (Math.Pow(-1, k + 1) * Math.Sin(k * current)) / k;
            }
            return sum;
        }

        private static double function(double current, int N)
        {
            double sum = 0;

            sum += 2 * series(current, N);

            return sum;
        }

        private static double baseFunction(double x)
        {
            return x;
        }

        public struct Pair
        {
            public double dot, value;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            int N = Convert.ToInt32(textBox1.Text);

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            //границы
            double a = -Math.PI, b = Math.PI;
            
            //работа с рядом
            List<Pair> data = new List<Pair>();
            
            for (double i = a - 1; i < b + 1; i += 0.25)
            {
                data.Add(new Pair() { dot = i, value = function(i, N) });
            }

            for (int i = 0; i < data.Count; i++)
                chart1.Series[0].Points.AddXY(data[i].dot, data[i].value);

            //исходная функция
            List<Pair> basedata = new List<Pair>();
            for (double i = a; i < b + 1; i++)
            {
                basedata.Add(new Pair() { dot = i, value = baseFunction(i) });
            }

            for (int i = 0; i < basedata.Count; i++)
                chart1.Series[1].Points.AddXY(basedata[i].dot, basedata[i].value);
        }
    }
}
