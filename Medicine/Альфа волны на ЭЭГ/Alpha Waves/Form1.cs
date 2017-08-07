using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
using System.Drawing;

namespace Alpha_Waves
{
    public partial class Form1 : Form
    {
        public static List<CoreFunctions.Signal> AscData;
        public static List<double> FilteredData;

        public Form1()
        {
            InitializeComponent();
            visualProperties();
        }

        private void visualProperties()
        {
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            groupBox1.BackColor = Color.FromArgb(200, 255, 255, 255);
            chart1.BackColor = Color.FromArgb(200, 255, 255, 255);
            chart2.BackColor = Color.FromArgb(200, 255, 255, 255);
            label1.BackColor = Color.FromArgb(255, 255, 255, 255);
            label2.BackColor = Color.FromArgb(0, 255, 255, 255);
            label3.BackColor = Color.FromArgb(0, 255, 255, 255);
            label4.BackColor = Color.FromArgb(0, 255, 255, 255);
            label5.BackColor = Color.FromArgb(0, 255, 255, 255);
            label6.BackColor = Color.FromArgb(0, 255, 255, 255);
            label7.BackColor = Color.FromArgb(0, 255, 255, 255);
            label8.BackColor = Color.FromArgb(0, 255, 255, 255);
            label9.BackColor = Color.FromArgb(0, 255, 255, 255);
            label10.BackColor = Color.FromArgb(0, 255, 255, 255);
            label11.BackColor = Color.FromArgb(0, 255, 255, 255);
        }

        public void MoneyChangerChart(int USDchanger, int EURchanger)
        {
            Random rand = new Random();
            textBoxUSDtoRUR.Text = Convert.ToString(Convert.ToDouble(textBoxUSDtoRUR.Text) + USDchanger + rand.NextDouble());
            textBoxEURtoRUR.Text = Convert.ToString(Convert.ToDouble(textBoxEURtoRUR.Text) + EURchanger + rand.NextDouble());
            textBoxUSDtoRUR2.Text = Convert.ToString(Convert.ToDouble(textBoxUSDtoRUR2.Text) + USDchanger + rand.NextDouble());
            textBoxEURtoRUR2.Text = Convert.ToString(Convert.ToDouble(textBoxEURtoRUR2.Text) + EURchanger + rand.NextDouble());
            chart2.Series[0].Points.AddY(Convert.ToDouble(textBoxUSDtoRUR.Text));
            chart2.Series[1].Points.AddY(Convert.ToDouble(textBoxEURtoRUR.Text));
            chart2.Series[2].Points.AddY(Convert.ToDouble(textBoxUSDtoRUR2.Text));
            chart2.Series[3].Points.AddY(Convert.ToDouble(textBoxEURtoRUR2.Text));
            
        }

        private void drawKurs(double maxDensity)
        {
            if (maxDensity < 10)
                MoneyChangerChart(-4, -2);
            else if (maxDensity > 10 && maxDensity < 15)
                MoneyChangerChart(-2, -1);
            else if (maxDensity > 15 && maxDensity < 25)
                MoneyChangerChart(0, 0);
            else if (maxDensity > 25 && maxDensity < 30)
                MoneyChangerChart(1, 3);
            else if (maxDensity > 30 && maxDensity < 40)
                MoneyChangerChart(3, 5);
            else if (maxDensity > 40 && maxDensity < 60)
                MoneyChangerChart(5, 6);
            else
                MoneyChangerChart(6, 8);
        }

        private void drawWaves(int from, int to)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();

            Dictionary<int, Complex> Density = CoreFunctions.takePowerDensity(from, to, FilteredData);
            double maxDensity = CoreFunctions.getMaximumPower(from, to, Density);
            textBoxSPMMax.Text = maxDensity.ToString();
            drawKurs(maxDensity);

            for (int i = from; i < to; i++)
            {
                chart1.Series[0].Points.AddXY(i / 200.0, AscData[i].Oz);
                chart1.Series[1].Points.AddXY(i / 200.0, FilteredData[i]);
                chart1.Series[2].Points.AddXY(i / 200.0, Density[i].Real);
            }
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            AscData = new List<CoreFunctions.Signal>();
            CoreFunctions.AddData(AscData, openFileDialog1);
            FilteredData = CoreFunctions.ButterworthFilter(AscData);

            chart2.Series[0].Points.Clear();
            chart2.Series[1].Points.Clear();
            chart2.Series[2].Points.Clear();
            chart2.Series[3].Points.Clear();

            textBoxUSDtoRUR.Text = Convert.ToString(30.1919999564193);
            textBoxEURtoRUR.Text = Convert.ToString(39.6890575797368);
            textBoxUSDtoRUR2.Text = Convert.ToString(32.2395348542681);
            textBoxEURtoRUR2.Text = Convert.ToString(42.2216089941996);

            for (int i = 0; (i + 200) < FilteredData.Count; i += 200)
            {
                if (chart2.Series[0].Points.Count == 10)
                {
                    chart2.Series[0].Points.Clear();
                    chart2.Series[1].Points.Clear();
                    chart2.Series[2].Points.Clear();
                    chart2.Series[3].Points.Clear();
                    drawWaves(i, i + 200);
                    Application.DoEvents();
                    Thread.Sleep(2000);
                }
                else
                {
                    drawWaves(i, i + 200);
                    Application.DoEvents();
                    Thread.Sleep(2000);
                }
            }
        }
    }
}