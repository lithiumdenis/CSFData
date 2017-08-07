using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Wavelets
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static void HaarTransform(ref double[] array, int level)
        {
            int SIZE = array.GetLength(0);    // Количество элементов
            double[] avg = new double[SIZE];    // Суммы
            double[] diff = new double[SIZE];   // Разности

            // count - количество повторов
            for (int count = SIZE; count > array.Length / (2 * level); count /= 2)
            {
                // Рассчитать полусуммы и разности, занести во временные массивы сумм и разностей
                for (int i = 0; i < count / 2; i++)
                {
                    avg[i] = (array[2 * i] + array[2 * i + 1]) / Math.Sqrt(2);
                    diff[i] = ((array[2 * i + 1]) * Math.Sqrt(2)) - avg[i];
                }

                // Перенести суммы и разности в итоговый массив
                for (int i = 0; i < count / 2; i++)
                {
                    array[i] = avg[i]; // В первую половину
                    array[i + count / 2] = diff[i]; // Во вторую половину
                }
            }
        }

        public static List<Double> DirectTransform(List<Double> SourceList)
        {
            if (SourceList.Count == 1)
                return SourceList;

            List<Double> RetVal = new List<Double>();
            List<Double> TmpArr = new List<Double>();

            for (int j = 0; j < SourceList.Count - 1; j += 2)
            {
                RetVal.Add((SourceList[j] - SourceList[j + 1]) / 2.0);
                TmpArr.Add((SourceList[j] + SourceList[j + 1]) / 2.0);
            }

            RetVal.AddRange(DirectTransform(TmpArr));

            return RetVal;
        }

        public static List<Double> InverseTransform(List<Double> array)
        {
            if (array.Count == 1)
                return array;

            List<Double> RetVal = new List<Double>();
            List<Double> TmpPart = new List<Double>();

            for (int i = array.Count / 2; i < array.Count; i++)
                TmpPart.Add(array[i]);

            List<Double> Part2 = InverseTransform(TmpPart);

            for (int i = 0; i < array.Count / 2; i++)
            {
                RetVal.Add(Part2[i] + array[i]);
                RetVal.Add(Part2[i] - array[i]);
            }

            return RetVal;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = 256;
            List<double> Data = new List<double>();
            Random Rand = new Random();
            for (int i = 0; i < n; i++)
                Data.Add(Rand.NextDouble());

            double[] haar = new double[Data.Count];
            for (int i = 0; i < Data.Count; i++)
                haar[i] = Data[i];

            HaarTransform(ref haar, 1);

            double sumLeft = 0;
            double sumRight = 0;
            for (int i = 0; i < Data.Count; i++)
            {
                sumLeft += Math.Pow(Data[i], 2);
                sumRight += Math.Pow(haar[i], 2);
            }

            textBox1.Text = sumLeft.ToString();
            textBox2.Text = sumRight.ToString();

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();

            chart1.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            List<Double> Haar = DirectTransform(Data);
            
            
            List<Double> RestoreData = InverseTransform(Haar);

            for (int i = 0; i < Data.Count; i++)
            {
                chart1.Series[0].Points.AddXY(i, Data[i]);
                chart1.Series[1].Points.AddXY(i, RestoreData[i]);
            }
        }
    }
}
