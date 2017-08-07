using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Electroencephalography.Part_1
{
    public partial class Form1 : Form
    {
        public struct Signal
        {
            public int Up;
            public int Down;
            public Signal(int up, int down)
            {
                Up = up;
                Down = down;
            }
        }

        public static List<Signal> Data;

        public Form1()
        {
            InitializeComponent();
            
            chart1.Series[0].Color = Color.Black; // цвет  линий
            chart2.Series[0].Color = Color.Black;
            chart1.Series[1].Color = Color.Red;
            chart2.Series[1].Color = Color.Red;
            chart1.Series[2].Color = Color.Orange;
            chart2.Series[2].Color = Color.Orange;
            chart1.Series[3].Color = Color.Yellow;
            chart2.Series[3].Color = Color.Yellow;
            chart1.Series[4].Color = Color.Green;
            chart2.Series[4].Color = Color.Green;
            chart1.Series[5].Color = Color.LightBlue;
            chart2.Series[5].Color = Color.LightBlue;
            chart1.Series[6].Color = Color.Purple;
            chart2.Series[6].Color = Color.Purple;
            chart1.Series[7].Color = Color.Blue;
            chart2.Series[7].Color = Color.Blue;
            chart1.Series[8].Color = Color.Brown;
            chart2.Series[8].Color = Color.Brown;

            chart1.Series[0].BorderWidth = 1; // толщина линий
            chart2.Series[0].BorderWidth = 1;
            chart1.Series[1].BorderWidth = 2;
            chart2.Series[1].BorderWidth = 2;
            chart1.Series[2].BorderWidth = 2;
            chart2.Series[2].BorderWidth = 2;
            chart1.Series[3].BorderWidth = 2;
            chart2.Series[3].BorderWidth = 2;
            chart1.Series[4].BorderWidth = 2;
            chart2.Series[4].BorderWidth = 2;
            chart1.Series[5].BorderWidth = 2;
            chart2.Series[5].BorderWidth = 2;
            chart1.Series[6].BorderWidth = 2;
            chart2.Series[6].BorderWidth = 2;
            chart1.Series[7].BorderWidth = 2;
            chart2.Series[7].BorderWidth = 2;
            chart1.Series[8].BorderWidth = 2;
            chart2.Series[8].BorderWidth = 2;

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart2.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline; // Типы графиков
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[5].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[5].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[6].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[6].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[7].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[7].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart1.Series[8].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chart2.Series[8].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;

            //chart1.ChartAreas[0].AxisX.Maximum = 100; // Max и Min значения координат
            chart1.ChartAreas[0].AxisY.Maximum = 800;
            chart1.ChartAreas[0].AxisY.Minimum = -800;

            chart2.ChartAreas[0].AxisY.Maximum = 800;
            chart2.ChartAreas[0].AxisY.Minimum = -800;

            //chart1.ChartAreas[0].AxisX.Interval = 400; // Интервалы
            //chart2.ChartAreas[0].AxisX.Interval = 400;
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data = new List<Signal>();

            openFileDialog1.FileName = null;
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                    string[] buf;
                    for (int i = 4; i < lines.Length; i++) // Учитываем первые 4 строчки с мусором из .asc
                    {
                        buf = lines[i].Split(' ');
                        Data.Add(new Signal(Convert.ToInt32(buf[0]), Convert.ToInt32(buf[1])));      
                    }

                    chart1.Series[0].Points.Clear(); // Очистка графиков
                    chart2.Series[0].Points.Clear();
                    chart1.Series[1].Points.Clear();
                    chart2.Series[1].Points.Clear();
                    chart1.Series[2].Points.Clear();
                    chart2.Series[2].Points.Clear();
                    chart1.Series[3].Points.Clear();
                    chart2.Series[3].Points.Clear();
                    chart1.Series[4].Points.Clear();
                    chart2.Series[4].Points.Clear();
                    chart1.Series[5].Points.Clear();
                    chart2.Series[5].Points.Clear();
                    chart1.Series[6].Points.Clear();
                    chart2.Series[6].Points.Clear();
                    chart1.Series[7].Points.Clear();
                    chart2.Series[7].Points.Clear();
                    chart1.Series[8].Points.Clear();
                    chart2.Series[8].Points.Clear();

                    for (int i = 0; i < Data.Count; i++)
                    {
                        // Вверх
                        if ((Data[i].Up > 320 && Data[i].Up < 500) && (Data[i].Down > 200 && Data[i].Down < 370))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[1].Points.AddXY(i, 0);
                            chart2.Series[1].Points.AddXY(i, 0);
                        }
                        // Влево
                        else if ((Data[i].Up > -30 && Data[i].Up < 40) && (Data[i].Down > -430 && Data[i].Down < -150))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[2].Points.AddXY(i, 0);
                            chart2.Series[2].Points.AddXY(i, 0);
                        }
                        // Вниз
                        else if ((Data[i].Up > -380 && Data[i].Up < -200) && (Data[i].Down > 20 && Data[i].Down < 60))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[3].Points.AddXY(i, 0);
                            chart2.Series[3].Points.AddXY(i, 0);
                        }
                        // Вправо
                        else if ((Data[i].Up > -80 && Data[i].Up < -35) && (Data[i].Down > 250 && Data[i].Down < 570))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[4].Points.AddXY(i, 0);
                            chart2.Series[4].Points.AddXY(i, 0);
                        }
                        // Лево-Верх
                        else if ((Data[i].Up > 100 && Data[i].Up < 350) && (Data[i].Down > -310 && Data[i].Down < -115))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[5].Points.AddXY(i, 0);
                            chart2.Series[5].Points.AddXY(i, 0);
                        }
                        // Лево-Низ
                        else if ((Data[i].Up > -350 && Data[i].Up < -205) && (Data[i].Down > 65 && Data[i].Down < 330))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[6].Points.AddXY(i, 0);
                            chart2.Series[6].Points.AddXY(i, 0);
                        }
                        // Право-Верх
                        else if ((Data[i].Up > 150 && Data[i].Up < 320) && (Data[i].Down > 375 && Data[i].Down < 850))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[7].Points.AddXY(i, 0);
                            chart2.Series[7].Points.AddXY(i, 0);
                        }
                        // Право-Низ
                        else if ((Data[i].Up > -200 && Data[i].Up < -120) && (Data[i].Down > 230 && Data[i].Down < 420))
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[8].Points.AddXY(i, 0);
                            chart2.Series[8].Points.AddXY(i, 0);
                        }
                        // Нет движений
                        else
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}