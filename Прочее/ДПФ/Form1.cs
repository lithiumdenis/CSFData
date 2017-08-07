using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Numerics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Dictionary<int, Complex> fm, ck, fmres;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            int n = Convert.ToInt32(textBox1.Text);
            int N = 2 * n + 1;
            Random rand = new Random();
           
            fm = new Dictionary<int, Complex>();
            ck = new Dictionary<int, Complex>();
            fmres = new Dictionary<int, Complex>();

            for (int i = - n; i <= n; i++)
                fm[i] = rand.NextDouble();

            for (int i = -n; i <= n; i++)
                ck[i] = dft(i, n);

            for (int i = -n; i <= n; i++)
                fmres[i] = dftO(i, n);

            for (int i = -n; i <= n; i++)
                chart1.Series[1].Points.AddXY(i, fm[i].Real);

            for (int i = -n; i <= n; i++)
                chart1.Series[0].Points.AddXY(i, fmres[i].Real);

            // Среднеквадратическое отклонение
            Complex avg = avgNorm(n);
            textBox2.Text = avg.Real.ToString();

            // Проверка равенства Парсеваля
            Complex parsLeft = parsevLeft(n);
            textBox3.Text = parsLeft.Real.ToString();

            Complex parsRight = parsevRight(n);
            textBox4.Text = parsRight.Real.ToString();
        }

        public Complex dft(int k, int n)
        {
            Complex sum = 0;
            for (int i = -n; i <= n; i++)
                sum += fm[i] * Complex.Exp(-Complex.ImaginaryOne * 2 * Math.PI * k * i / (2 * n + 1));

            return sum / (2 * n + 1);
        }

        public Complex dftO(int m, int n)
        {
            Complex sum = 0;
            for (int i = -n; i <= n; i++)
                sum += ck[i] * Complex.Exp(Complex.ImaginaryOne * 2 * Math.PI * m * i / (2 * n + 1));

            return sum;
        }

        public Complex avgNorm(int n)
        {
            Complex sum = 0;
            Complex curr = 0;
            for (int i = -n; i <= n; i++)
            {
                curr = fm[i] - fmres[i];
                sum += Math.Pow(curr.Real, 2) + Math.Pow(curr.Imaginary, 2);
            }
            return sum / (2 * n + 1);
        }

        public Complex parsevLeft(int n)
        {
            Complex sum = 0;
            Complex curr = 0;
            for (int i = -n; i <= n; i++)
            {
                curr = fm[i];
                sum += Math.Pow(curr.Real, 2) + Math.Pow(curr.Imaginary, 2);
            }
            return sum;
        }

        public Complex parsevRight(int n)
        {
            Complex sum = 0;
            Complex curr = 0;
            for (int i = -n; i <= n; i++)
            {
                curr = ck[i];
                sum += Math.Pow(curr.Real, 2) + Math.Pow(curr.Imaginary, 2);
            }
            return sum * (2 * n + 1);
        }
    }
}
