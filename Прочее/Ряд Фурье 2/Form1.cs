using System;
using System.Collections.Generic;
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

            for (int k = 0; k < N; k++)
            {
                sum += (2.0 * (Math.Pow(-1.0, k) * Math.Cos((2.0 * k + 1.0) * current))) / ((2.0 * k + 1.0) * Math.PI);
            }
            return sum;
        }

        private static double function(double current, int N)
        {
            double sum = 0;

            sum += 1.0 /2.0 + series(current, N);

            return sum;
        }

        private static double baseFunction(double x)
        {
            if(x < -Math.PI / 2)
                return 0;
            if (x > Math.PI / 2)
                return 0;
            else return 1;
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
            
            for (double i = a; i < b; i += 0.25)
            {
                data.Add(new Pair() { dot = i, value = function(i, N) });
            }

            for (int i = 0; i < data.Count; i++)
                chart1.Series[0].Points.AddXY(data[i].dot, data[i].value);

            //исходная функция
            List<Pair> basedata = new List<Pair>();
            for (double i = a; i < b; i +=0.25)
            {
                basedata.Add(new Pair() { dot = i, value = baseFunction(i) });
            }

            for (int i = 0; i < basedata.Count; i++)
                chart1.Series[1].Points.AddXY(basedata[i].dot, basedata[i].value);
        }
    }
}
