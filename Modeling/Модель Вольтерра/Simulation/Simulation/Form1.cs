using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //Убираем мешающее название серии
            chartTime.Series[0].IsVisibleInLegend = false;
            chartTime.ChartAreas[0].AxisX.Maximum = 10;
            chartTime.ChartAreas[0].AxisY.Maximum = 10;
            chartDifferences.ChartAreas[0].AxisX.Maximum = 10;
            chartDifferences.ChartAreas[0].AxisY.Maximum = 10;
            comboBox1.SelectedIndex = 0;
        }

        public static double a1, a2, a3, b1, b2, b3;

        private void buttonScen_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                textBoxA1.Text = "4";
                textBoxA2.Text = "0";
                textBoxA3.Text = "-2";
                textBoxB1.Text = "-6";
                textBoxB2.Text = "0";
                textBoxB3.Text = "2";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                textBoxA1.Text = "-2";
                textBoxA2.Text = "-1";
                textBoxA3.Text = "-1";
                textBoxB1.Text = "-3";
                textBoxB2.Text = "-4";
                textBoxB3.Text = "-3";
            }
            else if(comboBox1.SelectedIndex == 2)
            {
                textBoxA1.Text = "0.02";
                textBoxA2.Text = "-2";
                textBoxA3.Text = "2";
                textBoxB1.Text = "0.1";
                textBoxB2.Text = "-1";
                textBoxB3.Text = "2";
            }
            else if(comboBox1.SelectedIndex == 3)
            {
                textBoxA1.Text = "0.02";
                textBoxA2.Text = "-2";
                textBoxA3.Text = "2";
                textBoxB1.Text = "1";
                textBoxB2.Text = "1";
                textBoxB3.Text = "-2";
            }
            else if(comboBox1.SelectedIndex == 4)
            {
                textBoxA1.Text = "1";
                textBoxA2.Text = "1";
                textBoxA3.Text = "-3";
                textBoxB1.Text = "1";
                textBoxB2.Text = "-1";
                textBoxB3.Text = "3";
            }
            else
            {
                textBoxA1.Text = "2";
                textBoxA2.Text = "1";
                textBoxA3.Text = "-1";
                textBoxB1.Text = "-1";
                textBoxB2.Text = "1";
                textBoxB3.Text = "-1";
            }
        }

        //Prey
        public static double fx(double x, double y)
        {
            return (a1 * x + a2 * Math.Pow(x, 2) + a3 * x * y);
        }

        //Predator
        public static double fy(double x, double y)
        {
            return (b1 * y + b2 * Math.Pow(y, 2) + b3 * x * y);
        }

        public static Data methodRunge(double x, double y, double h)
        {
            double dx1, dx2, dx3, dx4, dy1, dy2, dy3, dy4;
            dx1 = fx(x, y);
            dy1 = fy(x, y);
            dx2 = fx(x + (h / 2.0) * dx1, y + (h / 2.0) * dy1);
            dy2 = fy(x + (h / 2.0) * dx1, y + (h / 2.0) * dy1);
            dx3 = fx(x + (h / 2.0) * dx2, y + (h / 2.0) * dy2);
            dy3 = fy(x + (h / 2.0) * dx2, y + (h / 2.0) * dy2);
            dx4 = fx(x + h * dx3, y + h * dy3);
            dy4 = fy(x + h * dx3, y + h * dy3);

            x += h * (dx1 + 2.0 * dx2 + 2.0 * dx3 + dx4) / 6.0;
            y += h * (dy1 + 2.0 * dy2 + 2.0 * dy3 + dy4) / 6.0;

            return new Data(x, y);
        }

        private void buttonWork_Click(object sender, EventArgs e)
        {
            a1 = Convert.ToDouble(textBoxA1.Text);
            a2 = Convert.ToDouble(textBoxA2.Text);
            a3 = Convert.ToDouble(textBoxA3.Text);
            b1 = Convert.ToDouble(textBoxB1.Text);
            b2 = Convert.ToDouble(textBoxB2.Text);
            b3 = Convert.ToDouble(textBoxB3.Text);

            double X_init = 1.0;
            double Y_init = 1.0;
            double H = Convert.ToDouble(textBoxH.Text);
            double T_start = 0.0;
            double T_finish = 10;

            chartTime.Series[0].Points.Clear();
            chartDifferences.Series[0].Points.Clear();
            chartDifferences.Series[1].Points.Clear();

            double x, y;
            double i = T_start;
            x = X_init;
            y = Y_init;

            do
            {
                Data curr = methodRunge(x, y, H);
                if (!(curr.X > 1000 || curr.Y > 1000))
                { chartTime.Series[0].Points.AddXY(curr.X, curr.Y);
                    chartDifferences.Series[0].Points.AddXY(i, curr.X);
                    chartDifferences.Series[1].Points.AddXY(i, curr.Y);
                    x = curr.X;
                    y = curr.Y;
                }
                i += H;
            } while (i <= T_finish);
        }
    }

    public class Data
    {
        public double X;
        public double Y;
        public Data(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
