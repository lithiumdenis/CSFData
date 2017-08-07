using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace Evoked_potentials
{
    public partial class Form1 : Form
    {
        public struct Signal
        {
            public double s1;
            public double s2;
            public double s3;
            public double s4;
            public double s5;
            public double s6;
            public double s7;
            public double s8;
            public double s9;
            public double s10;
            public double s11;
            public double s12;
            public double s13;
            public double s14;
            public double s15;
            public double s16;
            public double s17;
            public double s18;
            public double s19;
            public double s20;
            public double s21;
            public double s22;
            public Signal(double S1, double S2, double S3, double S4, double S5, double S6, double S7, double S8, double S9,
                double S10, double S11, double S12, double S13, double S14, double S15, double S16, double S17, double S18,
                double S19, double S20, double S21, double S22)
            {
                s1 = S1;
                s2 = S2;
                s3 = S3;
                s4 = S4;
                s5 = S5;
                s6 = S6;
                s7 = S7;
                s8 = S8;
                s9 = S9;
                s10 = S10;
                s11 = S11;
                s12 = S12;
                s13 = S13;
                s14 = S14;
                s15 = S15;
                s16 = S16;
                s17 = S17;
                s18 = S18;
                s19 = S19;
                s20 = S20;
                s21 = S21;
                s22 = S22;
            }
        }

        public static List<Signal> Data;
        public static List<Signal> DataFinalAll;
        public static List<Signal> DataCopy;
        List<double> potentials;
        List<double> potentials2;
        List<double> skolAvg;   //Фильтр скользящего среднего
        Dictionary<int, Complex> fm, ck, Fourier;

        public Form1()
        {
            InitializeComponent();
            
            chart1.Series[0].Color = Color.Red; // цвет  линий
            chart1.Series[1].Color = Color.DarkCyan;

            chart2.Series[0].Color = Color.Blue; // цвет  линий

            chart1.Series[0].BorderWidth = 1; // толщина линий
            chart1.Series[1].BorderWidth = 1;
            
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart2.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart3.ChartAreas[0].CursorX.IsUserEnabled = true; // Для прокрутки графика по оси Х
            chart3.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart3.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart3.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine; //Типы графиков
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[4].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[5].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[6].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[7].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[8].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[9].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[10].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[11].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[12].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[13].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[14].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[15].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[16].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[17].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[18].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[19].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[20].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart1.Series[21].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart3.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart3.Series[2].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            chart3.Series[3].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;

            //chart1.ChartAreas[0].AxisX.Maximum = 100; // Max и Min значения координат
            //chart1.ChartAreas[0].AxisY.Maximum = 800;
            //chart1.ChartAreas[0].AxisY.Minimum = -800;

            //chart2.ChartAreas[0].AxisY.Maximum = 800;
            //chart2.ChartAreas[0].AxisY.Minimum = -800;

            //chart1.ChartAreas[0].AxisX.Interval = 400; // Интервалы
            //chart2.ChartAreas[0].AxisX.Interval = 400;
        }

        private void drawPotentials(int Left, int Right)
        {
            if (Left >= 0 && Right <= potentials.Count)
            {
                chart1.Series[0].Points.Clear(); // Очистка графиков
                chart1.Series[1].Points.Clear();
                chart1.Series[2].Points.Clear();
                chart1.Series[3].Points.Clear();
                chart1.Series[4].Points.Clear();
                chart1.Series[5].Points.Clear();
                chart1.Series[6].Points.Clear();
                chart1.Series[7].Points.Clear();
                chart1.Series[8].Points.Clear();
                chart1.Series[9].Points.Clear();
                chart1.Series[10].Points.Clear();
                chart1.Series[11].Points.Clear();
                chart1.Series[12].Points.Clear();
                chart1.Series[13].Points.Clear();
                chart1.Series[14].Points.Clear();
                chart1.Series[15].Points.Clear();
                chart1.Series[16].Points.Clear();
                chart1.Series[17].Points.Clear();
                chart1.Series[18].Points.Clear();
                chart1.Series[19].Points.Clear();
                chart1.Series[20].Points.Clear();
                chart1.Series[21].Points.Clear();
                
                //chart2.Series[0].Points.Clear();
                chart2.Series[0].Points.Clear();
                
                chart3.Series[0].Points.Clear(); // Очистка графиков
                chart3.Series[1].Points.Clear();
                chart3.Series[2].Points.Clear();
                chart3.Series[3].Points.Clear();

                for (int i = Left; i < Right; i++)
                {
                    chart3.Series[0].Points.AddXY(i, Fourier[i].Real);
                    chart3.Series[1].Points.AddXY(i, Fourier[i].Imaginary);
                    chart3.Series[2].Points.AddXY(i, Fourier[i].Phase);
                    chart3.Series[3].Points.AddXY(i, Fourier[i].Magnitude);

                    //chart2.Series[0].Points.AddXY(i, potentials[i]);
                    chart2.Series[0].Points.AddXY(i, skolAvg[i]);

                    //chart1.Series[0].Points.AddXY(i, DataFinalAll[i].s1);
                    //chart1.Series[1].Points.AddXY(i, DataFinalAll[i].s2);//fp1aa
                    //chart1.Series[2].Points.AddXY(i, DataFinalAll[i].s3);
                    //chart1.Series[3].Points.AddXY(i, DataFinalAll[i].s4);
                    //chart1.Series[4].Points.AddXY(i, DataFinalAll[i].s5);
                    //chart1.Series[5].Points.AddXY(i, DataFinalAll[i].s6);
                    //chart1.Series[6].Points.AddXY(i, DataFinalAll[i].s7);
                    //chart1.Series[7].Points.AddXY(i, DataFinalAll[i].s8);
                    //chart1.Series[8].Points.AddXY(i, DataFinalAll[i].s9);
                    //chart1.Series[9].Points.AddXY(i, DataFinalAll[i].s10);
                    //chart1.Series[10].Points.AddXY(i, DataFinalAll[i].s11);
                    chart1.Series[11].Points.AddXY(i, DataFinalAll[i].s12);
                    chart1.Series[12].Points.AddXY(i, DataFinalAll[i].s13);
                    chart1.Series[13].Points.AddXY(i, DataFinalAll[i].s14);
                    chart1.Series[14].Points.AddXY(i, DataFinalAll[i].s15);
                    chart1.Series[15].Points.AddXY(i, DataFinalAll[i].s16); //oza2
                    //chart1.Series[16].Points.AddXY(i, DataFinalAll[i].s17);
                    //chart1.Series[17].Points.AddXY(i, DataFinalAll[i].s18);
                    //chart1.Series[18].Points.AddXY(i, DataFinalAll[i].s19); //t3a1
                    //chart1.Series[19].Points.AddXY(i, DataFinalAll[i].s20); //t4a2
                    chart1.Series[20].Points.AddXY(i, DataFinalAll[i].s21);
                    chart1.Series[21].Points.AddXY(i, DataFinalAll[i].s22);
                }
            }
            else
            {
                MessageBox.Show("Неправильный интервал для маркеров");
            }
        }

        private void открытьФайлToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.FileName = null;
            openFileDialog1.InitialDirectory = "C:\\Users\\1\\Desktop";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Data = new List<Signal>();
                    DataCopy = new List<Signal>();
                    potentials = new List<double>();
                    potentials2 = new List<double>();
                    skolAvg = new List<double>();
                    DataFinalAll = new List<Signal>();

                    string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                    string[] buf;
                    for (int i = 4; i < lines.Length; i++) // Учитываем первые 4 строчки с мусором из .asc
                    {
                        buf = lines[i].Split(' ');
                        Data.Add(new Signal(Convert.ToInt32(buf[0]), Convert.ToInt32(buf[1]), Convert.ToInt32(buf[2]),
                            Convert.ToInt32(buf[3]), Convert.ToInt32(buf[4]), Convert.ToInt32(buf[5]),
                            Convert.ToInt32(buf[6]), Convert.ToInt32(buf[7]), Convert.ToInt32(buf[8]), 
                            Convert.ToInt32(buf[9]), Convert.ToInt32(buf[10]), Convert.ToInt32(buf[11]),
                            Convert.ToInt32(buf[12]), Convert.ToInt32(buf[13]), Convert.ToInt32(buf[14]),
                            Convert.ToInt32(buf[15]), Convert.ToInt32(buf[16]), Convert.ToInt32(buf[17]),
                            Convert.ToInt32(buf[18]), Convert.ToInt32(buf[19]), Convert.ToInt32(buf[20]),
                            Convert.ToInt32(buf[21])));      
                    }

                    while(Data[0].s1 != 1)
                    {
                        Data.RemoveAt(0);
                    }

                    //using (System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"C:\Users\1\Desktop\Stim.txt", true))
                    //{
                    //    for (int i = 0; i < Data.Count; i++)
                    //        file1.WriteLine(Data[i].s1);
                    //}

                    //using (System.IO.StreamWriter file2 = new System.IO.StreamWriter(@"C:\Users\1\Desktop\S16.txt", true))
                    //{
                    //    for (int i = 0; i < Data.Count; i++)
                    //        file2.WriteLine(Data[i].s16);
                    //}

                    int countOfSignals = 0;
                    for (int i = 0; i < Data.Count; i++)
                    {
                        if (Data[i].s1 == 1)
                            countOfSignals++;
                    }
                    

                    DataCopy = Data;

                    int divide = 100;

                    List<Signal>[] MassiveData = new List<Signal>[countOfSignals + divide + 10];

                    int currMasDat = -1;
                    while (DataCopy.Count > 1)
                    {
                        if (DataCopy[0].s1 == 1)
                        {
                            currMasDat++;
                            MassiveData[currMasDat] = new List<Signal>();
                            MassiveData[currMasDat].Add(DataCopy[0]);
                            DataCopy.RemoveAt(0);
                        }
                        else
                        {
                            MassiveData[currMasDat].Add(DataCopy[0]);
                            DataCopy.RemoveAt(0);
                        }
                    }

                    countOfSignals--;

                    //Находим самый мелкий массив
                    int massDataMaxLength = MassiveData[0].Count;
                    for (int i = 1; i < MassiveData.Length - divide - 10 - 1; i++)
                        if (massDataMaxLength > MassiveData[i].Count)
                            massDataMaxLength = MassiveData[i].Count;

                    Signal ZeroSignal = new Signal(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

                    while(MassiveData[currMasDat].Count < massDataMaxLength)
                        MassiveData[currMasDat].Add(ZeroSignal);
                   
                    for(int i = 0; i < divide + 10; i++)
                    {
                        currMasDat++;
                        MassiveData[currMasDat] = new List<Signal>();
                        for (int j = 0; j < massDataMaxLength; j++)
                            MassiveData[currMasDat].Add(ZeroSignal);
                        
                    }

                    //Делаем, чтобы все массивы были одной длины
                    for (int i = 0; i < MassiveData.Length - 1; i++)
                        while (massDataMaxLength < MassiveData[i].Count)
                        {
                            MassiveData[i].RemoveAt(MassiveData[i].Count - 1);
                        }

                    //Костыль, если последний элемент вдруг получится == null
                    if (MassiveData[MassiveData.Length - 1] == null)
                    {
                        MassiveData[MassiveData.Length - 1] = new List<Signal>();
                        for (int j = 0; j < massDataMaxLength; j++)
                            MassiveData[MassiveData.Length - 1].Add(ZeroSignal);
                    }

                    //using (System.IO.StreamWriter file3 = new System.IO.StreamWriter(@"C:\Users\1\Desktop\MassDatStim.txt", true))
                    //{
                    //    for (int i = 0; i < MassiveData.Length; i++)
                    //        for (int j = 0; j < massDataMaxLength; j++ )
                    //            file3.WriteLine(MassiveData[i][j].s1);
                    //}

                    //using (System.IO.StreamWriter file4 = new System.IO.StreamWriter(@"C:\Users\1\Desktop\MassDatS16.txt", true))
                    //{
                    //    for (int i = 0; i < MassiveData.Length; i++)
                    //        for (int j = 0; j < massDataMaxLength; j++)
                    //            file4.WriteLine(MassiveData[i][j].s16);
                    //}

                    for (int i = 0; i < MassiveData.Length; i++)
                        for (int j = 0; j < massDataMaxLength; j++)
                            DataFinalAll.Add(MassiveData[i][j]);
                   
                    double curr = 0;
                    for (int i = 0; i < MassiveData.Length - divide - 10; i++)
                    {
                            for (int j = 0; j < massDataMaxLength; j++)
                            {
                                curr = 0;
                                for (int k = i; k < i + divide; k++)
                                {
                                    curr += MassiveData[k][j].s16;
                                }
                                curr /= divide;
                                potentials.Add(curr);
                            }
                    }

                    int P = 2;

                    for (int i = 0; i < potentials.Count; i++)
                    {
                        //если перед элементом уже есть P элементов, то
                        if (skolAvg.Count > P)
                        {
                            double sum = 0;
                            for (int m = 0; m <= P; m++)
                            {
                                sum += potentials[i - m] * (1 / P + 1);
                            }
                            skolAvg.Add(sum);
                        }
                        //если элементов меньше, чем P, то используем все, что есть
                        else
                        {
                            double sum2 = 0;
                            for (int m = 0; m < i; m++)
                            {
                                sum2 += potentials[i - m] * (1 / i + 1);
                            }
                            skolAvg.Add(sum2);
                        }
                    }

                    fm = new Dictionary<int, Complex>();
                    ck = new Dictionary<int, Complex>();
                    Fourier = new Dictionary<int, Complex>();

                    if ((potentials.Count % 2) == 0)
                        potentials.Add(0);

                    for (int i = -potentials.Count / 2, k = 0; i <= potentials.Count / 2; i++, k++)
                        fm[i] = potentials[k];

                    for (int i = -potentials.Count / 2; i <= potentials.Count / 2; i++)
                        ck[i] = dft(i, potentials.Count / 2);

                    for (int i = -potentials.Count / 2, k = 0; i <= potentials.Count / 2; i++, k++)
                        Fourier[k] = ck[i];

                    buttonMarkers.Enabled = true;
                    drawPotentials(0, potentials.Count - 1);

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void выходToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMarkers_Click(object sender, EventArgs e)
        {
            int left, right;
            try
            {
                 left = Convert.ToInt32(textBoxLeftMark.Text);
                 right = Convert.ToInt32(textBoxRightMark.Text);
            }
            catch
            {
                MessageBox.Show("В маркерах некорректная инфа");
                return;
            }

            drawPotentials(left, right);

            textBoxMarkAmplitude.Text = Convert.ToString(String.Format("{0:0.0000}", Math.Abs(potentials[left] - potentials[right]))) + " мкВ";
            textBoxMarkTime.Text = Convert.ToString(String.Format("{0:0.0000}", (right - left) / 206.0)) + " сек"; //206 в 1 секунде
        }

        public Complex dft(int k, int n)
        {
            Complex sum = 0;
            for (int i = -n; i <= n; i++)
                sum += fm[i] * Complex.Exp(-Complex.ImaginaryOne * 2 * Math.PI * k * i / (2 * n + 1));

            return sum / (2 * n + 1);
        }

    }
}