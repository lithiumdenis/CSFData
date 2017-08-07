using System;
using System.Windows.Forms;

namespace Shooting_Method
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShootingMethod();
        }

        public void Euler2(double[] x, double[] y, double[] yy, double xa, double ya, double h, double alfa)
        {
            x[0] = xa;
            y[0] = ya;
            yy[0] = Math.Tan(alfa);
            for (int i = 1; i < x.Length; i++)
            {
                x[i] = x[i - 1] + h;
                y[i] = y[i - 1] + h * yy[i - 1];
                yy[i] = yy[i - 1] + h * f(x[i - 1], y[i - 1], yy[i - 1]);
            }
        }

        private void ShootingMethod()
        {
            double xa = 0,                         // х(0)
                    xb = 5,
                    ya = 0,                         // у(0)
                    yb = 10.0 / 6.0;  //5/3

            double h = 0.1/*0*/,                       // Шаг
                    alfa = Math.Atan((yb - ya) / (xb - xa));    // Угол из tg

            int n = Convert.ToInt32((xb - xa) / h);

            double[] x = new double[n + 1];
            double[] y = new double[n + 1];
            double[] yy = new double[n + 1];

            double  fXleft,
                    fXright,
                    Xleft = alfa,
                    Xright,
                    Xmid;

            Euler2(x, y, yy, xa, ya, h, alfa);

            if (y[y.Length - 1] > yb)
            {
                fXleft = y[y.Length - 1];
                while (y[y.Length - 1] > yb)
                    Euler2(x, y, yy, xa, ya, h, alfa -= 0.9);                                                                             //1 1 2;    0.9 0.9 1.8
                fXright = y[y.Length - 1];
                Xright = alfa;
            }
            else
            {
                fXright = y[y.Length - 1];
                while (y[y.Length - 1] < yb)
                    Euler2(x, y, yy, xa, ya, h, alfa += 0.9);
                fXleft = y[y.Length - 1];
                Xright = alfa;
            }
            
            while (Math.Abs(fXleft - fXright) >= 1.8)
            {
                Xmid = (Xleft + Xright) / 2.0;
                Euler2(x, y, yy, xa, ya, h, Xmid);

                if ((y[y.Length - 1] - yb) * (fXleft - yb) > 0)
                {
                    Xleft = Xmid;
                    fXleft = y[y.Length - 1];
                }
                else
                {
                    Xright = Xmid;
                    fXright = y[y.Length - 1];  // y[y.Length - 1] == fXmid
                }
            }

            PrintData(x, y);
        }

        static double f(double x, double y, double yy)
        {
            return -yy - 2 * y;
        }

        static double f_solve(double x)
        {
            return (6 * Math.Exp(-x / 3/*2*/) * Math.Sin((2.65 * x) / 2)) / 2.65;
        }

        public void PrintData(double[] x, double[] y)
        {
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            for (int i = 0; i < x.Length; i++)
            {
                chart1.Series[0].Points.AddXY(x[i], y[i]);
                chart1.Series[1].Points.AddXY(x[i], f_solve(x[i]));
            }
        }
    }
}
