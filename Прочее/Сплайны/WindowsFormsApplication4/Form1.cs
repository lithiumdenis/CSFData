using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AForge.Math;


namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
        class CubicSpline
        {
            SplineTuple[] splines; // Сплайн

            // Структура, описывающая сплайн на каждом сегменте сетки
            private struct SplineTuple
            {
                public double a, b, c, d, x;
            }

            // Построение сплайна
            // x - узлы сетки, должны быть упорядочены по возрастанию, кратные узлы запрещены
            // y - значения функции в узлах сетки
            // n - количество узлов сетки
            public void BuildSpline(double[] x, double[] y, int n)
            {
                // Инициализация массива сплайнов
                splines = new SplineTuple[n];
                for (int i = 0; i < n; ++i)
                {
                    splines[i].x = x[i];
                    splines[i].a = y[i];
                }
                splines[0].c = splines[n - 1].c = 0.0;

                // Решение СЛАУ относительно коэффициентов сплайнов c[i] методом прогонки для трехдиагональных матриц
                // Вычисление прогоночных коэффициентов - прямой ход метода прогонки
                double[] alpha = new double[n - 1];
                double[] beta = new double[n - 1];
                alpha[0] = beta[0] = 0.0;
                for (int i = 1; i < n - 1; ++i)
                {
                    double hi = x[i] - x[i - 1];
                    double hi1 = x[i + 1] - x[i];
                    double A = hi;
                    double C = 2.0 * (hi + hi1);
                    double B = hi1;
                    double F = 6.0 * ((y[i + 1] - y[i]) / hi1 - (y[i] - y[i - 1]) / hi);
                    double z = (A * alpha[i - 1] + C);
                    alpha[i] = -B / z;
                    beta[i] = (F - A * beta[i - 1]) / z;
                }

                // Нахождение решения - обратный ход метода прогонки
                for (int i = n - 2; i > 0; --i)
                {
                    splines[i].c = alpha[i] * splines[i + 1].c + beta[i];
                }

                // По известным коэффициентам c[i] находим значения b[i] и d[i]
                for (int i = n - 1; i > 0; --i)
                {
                    double hi = x[i] - x[i - 1];
                    splines[i].d = (splines[i].c - splines[i - 1].c) / hi;
                    splines[i].b = hi * (2.0 * splines[i].c + splines[i - 1].c) / 6.0 + (y[i] - y[i - 1]) / hi;
                }
            }

            // Вычисление значения интерполированной функции в произвольной точке
            public double Interpolate(double x)
            {
                if (splines == null)
                {
                    return double.NaN; // Если сплайны ещё не построены - возвращаем NaN
                }

                int n = splines.Length;
                SplineTuple s;

                if (x <= splines[0].x) // Если x меньше точки сетки x[0] - пользуемся первым эл-тов массива
                {
                    s = splines[0];
                }
                else if (x >= splines[n - 1].x) // Если x больше точки сетки x[n - 1] - пользуемся последним эл-том массива
                {
                    s = splines[n - 1];
                }
                else // Иначе x лежит между граничными точками сетки - производим бинарный поиск нужного эл-та массива
                {
                    int i = 0;
                    int j = n - 1;
                    while (i + 1 < j)
                    {
                        int k = i + (j - i) / 2;
                        if (x <= splines[k].x)
                        {
                            j = k;
                        }
                        else
                        {
                            i = k;
                        }
                    }
                    s = splines[j];
                }

                double dx = x - s.x;
                // Вычисляем значение сплайна в заданной точке по схеме Горнера (в принципе, "умный" компилятор применил бы схему Горнера сам, но ведь не все так умны, как кажутся)
                return s.a + (s.b + (s.c / 2.0 + s.d * dx / 6.0) * dx) * dx;
            }
        }
        void process()
        {
            int k = 5;
            double a= 0, b = 10;
            double step = (b - a )/(k-1); 
            double[] x = new double[k];
            double[] y = new double[k];
            x[0] = a;
            x[k-1] = b;
            y[0] = f(a);
            y[k-1] = f(b);
            for (int i = 1; i < k; i++)
            {
                x[i] = i * step;
                y[i] = f(x[i]);
            }
            
            CubicSpline c = new CubicSpline();
            c.BuildSpline(x, y, x.Length);
            int n = 50;
            double h = (x[x.Length-1] - x[0])/(n-1);
            double[] spline = new double[n];
            for (int i = 0; i < spline.Length; i++)
            {
                spline[i] = c.Interpolate(i * h);
            }


            double e = 0.1;
            int count = Convert.ToInt32((b - a) / e);
            double[] ff = new double[count+1];
            for (int i = 0; i < ff.Length; i++)
                ff[i] = f(i*e);
            fillChart(ff, spline, y, e, h, step, chart1);


        }
        
        public double f(double x)
        {
            return Math.Cos(x);
        }
        public Form1()
        {
            InitializeComponent();
            process();
        }

        private void fillChart(double[] array, double[] array2, double[] array3, double e, double h, double step, System.Windows.Forms.DataVisualization.Charting.Chart chart1)
        {
            int blockSize = 10;
            chart1.Series.Clear();
            // chart 1
            var series = chart1.Series.Add("Функция");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series.BorderWidth = 1;
            series.Color = Color.Purple;
            // chart 2
            var series2 = chart1.Series.Add("Сплайн");
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.BorderWidth = 1;
            series2.Color = Color.Orange;

            var series3 = chart1.Series.Add("Точки");
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series3.BorderWidth = 1;
            series3.Color = Color.Aqua;

            for (int i = 0; i < array.Length; i++) // функция
            {
                series.Points.AddXY(i * e, array[i]);
               
            }
            for (int i = 0; i < array2.Length; i++) // сплайн
            {
                series2.Points.AddXY(i * h, array2[i]);
            }
            for (int i = 0; i < array3.Length; i++) // точки
            {
                series3.Points.AddXY(i * step, array3[i]); 
            }

            var chartArea = chart1.ChartAreas[series.ChartArea];
            // set view range to [0,max]
            chartArea.AxisX.Minimum = 0;
            chartArea.AxisX.Maximum = 10;
            //chartArea.AxisY.Maximum = 3;
            //chartArea.AxisY.Minimum = -3;

            // enable autoscroll
            chartArea.CursorX.AutoScroll = true;

            // let's zoom to [0,blockSize] (e.g. [0,100])
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisX.ScaleView.SizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            int position = 0;
            int size = blockSize;
            chartArea.AxisX.ScaleView.Zoom(position, size);

            // disable zoom-reset button (only scrollbar's arrows are available)
            chartArea.AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.SmallScroll;

            // set scrollbar small change to blockSize (e.g. 100)
            chartArea.AxisX.ScaleView.SmallScrollSize = blockSize;
            chart1.Legends[0].Enabled = false;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
        }

        //UNUSED
        private void chart1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            process();
        }
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
