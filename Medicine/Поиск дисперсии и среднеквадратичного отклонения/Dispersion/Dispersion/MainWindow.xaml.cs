using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Dispersion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static List<string> Results;

        public static double dispersia(double[] x)
        {
            //Находим х среднее
            double avgX = 0;
            for (int i = 0; i < x.Length; i++)
                avgX += x[i];
            avgX /= x.Length;
            //Находим дисперсию
            double disp = 0;
            for (int i = 0; i < x.Length; i++)
                disp += Math.Pow(x[i] - avgX, 2);
            disp /= x.Length;
            return disp;
        }

        public static void SaveResultsToFile()
        {
            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                for (int i = 0; i < Results.Count; i++)
                {
                    sw.WriteLine(Results[i]);
                }
            }
        }

        public static void WorkWithOneFile(string FileName, string FileDirectory, string Type)
        {
            List<string> Data = new List<string>();
            try
            {
                string[] lines = System.IO.File.ReadAllLines(FileDirectory + "\\" + FileName);
                string buf;
                for (int i = 0; i < lines.Length; i++)
                {
                    buf = lines[i];
                    buf = buf.Trim();
                    //Если больше одного пробела между строками
                    buf = System.Text.RegularExpressions.Regex.Replace(buf, " +", " ");
                    Data.Add(buf);
                }

                if (Type == "Строки")
                {
                    for (int j = 0; j < Data.Count; j++)
                    {
                        string[] buf2 = Data[j].Split(' ', '\t');
                        double[] doublebuf = new double[buf2.Length];
                        for (int i = 0; i < buf2.Length; i++)
                            doublebuf[i] = Convert.ToDouble(buf2[i]);
                        double dispResult = dispersia(doublebuf);
                        Results.Add("строка: " + j + " дисперсия: " + dispResult + " отклонение: " + Math.Sqrt(dispResult));
                    }
                }
                if (Type == "Столбцы")
                {
                    string[][] allData = new string[Data.Count][];
                    double[][] allDoubleData = new double[Data.Count][];
                    for (int j = 0; j < Data.Count; j++)
                    {
                        allData[j] = Data[j].Split(' ', '\t');
                    }
                    for (int i = 0; i < Data.Count; i++)
                    {
                        allDoubleData[i] = new double[allData[0].Length];
                        for (int j = 0; j < allData[0].Length; j++)
                        {
                            allDoubleData[i][j] = Convert.ToDouble(allData[i][j]);
                        }
                    }

                    for(int i = 0; i < allDoubleData[0].Length; i++)
                    {
                        double[] doublebuf = new double[allDoubleData.Length];
                        for (int j = 0; j < allDoubleData.Length; j++)
                            doublebuf[j] = allDoubleData[j][i];
                        double dispResult = dispersia(doublebuf);
                        Results.Add("столбец: " + i + " дисперсия: " + dispResult + " отклонение: " + Math.Sqrt(dispResult));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonGetFiles_Click(object sender, RoutedEventArgs e)
        {
            Results = new List<string>();
            ListViewFileSystem.Items.Clear();
            DirectoryInfo dir = new DirectoryInfo(textboxDirectory.Text);
            foreach (var item in dir.GetFiles(textboxMask.Text))
            {
                ListViewFileSystem.Items.Add(item.Name);
                Results.Add("Начало работы с файлом: " + item.Name);
                WorkWithOneFile(item.Name, textboxDirectory.Text, ComboBoxType.Text);
                Results.Add("Конец работы с файлом: " + item.Name);
                Results.Add("");
            }
            SaveResultsToFile();
        }
    }
}
