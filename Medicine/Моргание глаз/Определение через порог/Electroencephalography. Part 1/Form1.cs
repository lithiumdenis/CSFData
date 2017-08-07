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
            
            chart1.Series[0].Color = Color.Blue; // цвет  линий
            chart2.Series[0].Color = Color.Orange;
            chart1.Series[1].Color = Color.DarkCyan;
            chart2.Series[1].Color = Color.DarkCyan;

            chart1.Series[0].BorderWidth = 1; // толщина линий
            chart2.Series[0].BorderWidth = 1;
            chart1.Series[1].BorderWidth = 2;
            chart2.Series[1].BorderWidth = 2;

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

            chart1.Series[0].Points.Clear(); // Очистка графиков
            chart2.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart2.Series[1].Points.Clear();

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

                    if (Data.Count < 2)
                    {
                        MessageBox.Show("Сначала выберите файл!");
                    }

                    for (int i = 0; i < Data.Count; i++)
                    {
                        // Если амплитуда такая, что, очевидно, есть моргание
                        if (Data[i].Up > 150)
                        {
                            chart1.Series[0].Points.AddXY(i, Data[i].Up);
                            chart2.Series[0].Points.AddXY(i, Data[i].Down);
                            chart1.Series[1].Points.AddXY(i, 0);
                            chart2.Series[1].Points.AddXY(i, 0);
         
                        }
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