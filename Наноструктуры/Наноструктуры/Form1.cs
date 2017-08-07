using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace Наноструктуры
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonWork_Click(object sender, EventArgs e)
        {
            if (2 * Convert.ToDouble(textBoxLengthWell.Text) < 0 || 2 * Convert.ToDouble(textBoxLengthWell.Text) > 50)
                MessageBox.Show("Ширина ямы возможна только в пределах от 0 до 25 нм!");
            else if (Convert.ToDouble(textBoxDepthWell.Text) < 0 || Convert.ToDouble(textBoxDepthWell.Text) > 2)
                MessageBox.Show("Глубина ямы должна быть от 0 до 2 эВ!");
            else
            {
                double wellLength = Convert.ToDouble(textBoxLengthWell.Text); //a в нанометрах
                double wellDepth = Convert.ToDouble(textBoxDepthWell.Text); //Глубина в эв
                int N_max = Convert.ToInt32(textBoxCountEigenVal.Text); //Количество вычисляемых собственных значений
                double[] EigenEnergies = new double[N_max + 1]; // Массив хранящий результаты
                List<double> Wavefunction = new List<double>();
                List<double> Energy = new List<double>();
                CoreSchrodinger.Work(wellLength, wellDepth, N_max, EigenEnergies, Wavefunction, Energy);
                DrawResults(Wavefunction, Energy, wellLength, N_max, EigenEnergies, wellDepth);
            }
        }

        public void DrawResults(List<double> Wavefunction, List<double> Energy, double wellLength, int N_max, double[] EigenEnergies, double wellDepth)
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("№ \t\t Z \t\t E\n");

            CultureInfo ci = new CultureInfo("en-us");

            SpecEquation se = new SpecEquation();
            double constant = Convert.ToDouble(textBoxC.Text);
            double[] resultNech = se.SolveFunc(0, 500, 0.001, true, constant);
            double[] resultChet = se.SolveFunc(0, 500, 0.001, false, constant);

            //Зануляем ненужные
            for (int i = 0; i < resultNech.Length; i++)
                if (i % 2 != 0)
                    resultNech[i] = 0;

            for (int i = 0; i < resultChet.Length; i++)
                if (i % 2 != 0)
                    resultChet[i] = 0;

            //Добавляем нужные в один список
            List<double> iZ = new List<double>();
            for (int i = 0; i < resultNech.Length; i++)
                if (resultNech[i] != 0)
                    iZ.Add(resultNech[i]);
            for (int i = 0; i < resultChet.Length; i++)
                if (resultChet[i] != 0)
                    iZ.Add(resultChet[i]);
            //Сортируем нужные
            iZ.Sort();

            double countofExists;
            if (N_max < iZ.Count)
                countofExists = N_max;
            else
                countofExists = iZ.Count;

            for (int i = 0; i < countofExists; i++)
            {
                Energy.Add(Math.Round(-wellDepth * (1 - Math.Pow(Math.Round(iZ[i], 5), 2) / Math.Pow(constant, 2)), 5));
                listBox1.Items.Add(i + 1 +" \t\t" + Math.Round(iZ[i], 5) + " \t\t" + Math.Round(-wellDepth * (1 - Math.Pow(Math.Round(iZ[i], 5), 2) / Math.Pow(constant, 2)), 5));

            }
            //////////////////////////////////Энергетич. спектр///////////////////////////////////////////////
            Axis ax = new Axis();
            Axis bx = new Axis();
            chartSpektr.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chartSpektr.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartSpektr.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartSpektr.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartSpektr.Series[0].Points.Clear();
            chartSpektr.Series.Clear();
            ax.Title = "E, эВ";
            chartSpektr.ChartAreas[0].AxisY = ax;

            bx.Title = "а, нм";
            chartSpektr.ChartAreas[0].AxisX = bx;

            for (int i = 0; i < countofExists; i++)
            {
                Series series1 = new Series();
                series1.Name = "" + (i + 1);
                if (i % 2 != 0)
                    series1.Color = System.Drawing.Color.Red;
                else
                    series1.Color = System.Drawing.Color.Blue;
                chartSpektr.Series.Add(series1);
                chartSpektr.Series[i].ChartType = SeriesChartType.FastLine;


            }

            for (double i = -wellLength; i < wellLength; i += 0.05)
            {
                for (int j = 0; j < countofExists; j++)
                    chartSpektr.Series[j].Points.AddXY(i, Energy[j]);

            }

            ///////////////////////////Волновые функции////////////////////////////////////////////////////////
            chartVoln.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chartVoln.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartVoln.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartVoln.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartVoln.Series[0].Points.Clear();
            chartVoln.Series.Clear();

            Axis ax1 = new Axis();
            Axis bx1 = new Axis();
            ax1.Title = "ψ[z,E]";
            chartVoln.ChartAreas[0].AxisY = ax1;
     
            bx1.Title = "а, нм";
            chartVoln.ChartAreas[0].AxisX = bx1;

            for (int i = 0; i < N_max; i++)
            {
                Series series1 = new Series();
                series1.Name = "" + (i + 1);
                chartVoln.Series.Add(series1);
                chartVoln.Series[i].ChartType = SeriesChartType.FastLine;
            }

            double lcurr = -wellLength;

            for (int i = 0, j = 0, k = 0; i < Wavefunction.Count; i++)
            {
                if (Wavefunction[i] != 99999999999999999)
                {
                    chartVoln.Series[j].Points.AddXY(lcurr, Wavefunction[i]);
                    lcurr += 0.01;
                    k++;
                }

                else
                {
                    j++;
                    lcurr = -wellLength;
                    k = 0;
                }
            }
            /////////////////////////////распределение вероятностей/////////////////////////////////////////

            chartRaspr.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chartRaspr.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartRaspr.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartRaspr.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            chartRaspr.Series[0].Points.Clear();

            Axis ax2 = new Axis();
            Axis bx2 = new Axis();
            ax2.Title = "ψ[z,E]^2";
            chartRaspr.ChartAreas[0].AxisY = ax2;

            bx2.Title = "а, нм";
            chartRaspr.ChartAreas[0].AxisX = bx2;

            chartRaspr.Series.Clear();
            for (int i = 0; i < N_max; i++)
            {
                System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
                series1.Name = "" + (i + 1);
                chartRaspr.Series.Add(series1);
                chartRaspr.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            }

            lcurr = -wellLength;

            for (int i = 0, j = 0, k = 0; i < Wavefunction.Count; i++)
            {
                if (Wavefunction[i] != 99999999999999999)
                {
                    chartRaspr.Series[j].Points.AddXY(lcurr, Math.Pow(Wavefunction[i], 2));
                    lcurr += 0.01;
                    k++;
                }

                else
                {
                    j++;
                    lcurr = -wellLength;
                    k = 0;
                }
            }
        }
    }
}
