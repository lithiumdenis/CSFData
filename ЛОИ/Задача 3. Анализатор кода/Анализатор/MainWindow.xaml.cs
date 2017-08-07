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

namespace Анализатор
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            analysis = new AnalysisCode();
        }

        private AnalysisCode analysis;

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            String filename = "алфавит.cpp";
            try
            {
                TextRange textRange;
                string[] str = File.ReadAllLines(filename);
                System.IO.FileStream fileStream;

                if (System.IO.File.Exists(filename))
                {
                    textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                    using (fileStream = new System.IO.FileStream(filename, System.IO.FileMode.OpenOrCreate))
                    {
                        textRange.Load(fileStream, System.Windows.DataFormats.Text);
                    }
                }
                listBox.Items.Clear();
                listBox.Items.Add("Наименование \t\t Тип \t\t Количество");

                analysis.Analysis(str);
                ShowElements(analysis.GetListElements());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Read File");
            }
            

        }

        private void ShowElements(List<ElmentLanguage> list)
        {
            String[] str;
            for (int i = 0; i < list.Count; i++)
            {
                str = new String[3];
                str[0] = list[i].Name;
                str[1] = TypeName(list[i].GetType());
                str[2] = list[i].Count.ToString();
                listBox.Items.Add(str[0] + "\t\t" + str[1] + "\t\t" + str[2] + "\t\t");
            }
        }

        private String TypeName(ElementType type)
        {
            if (type == ElementType.BaseType)
                return "Base type";
            if (type == ElementType.Cycle)
                return "Cycle";
            if (type == ElementType.OfficialWord)
                return "Official Word";
            if (type == ElementType.Variable)
                return "Variable";
            return "None";
        }
    }
}
