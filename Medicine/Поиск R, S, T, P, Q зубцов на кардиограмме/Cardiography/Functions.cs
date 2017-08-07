using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cardiography
{
    public class Core
    {
        public struct Signal
        {
            public double O1, O2, O3;
            public Signal(double o1, double o2, double o3)
            {
                O1 = o1;
                O2 = o2;
                O3 = o3;
            }
        }

        public static void AddData(List<Core.Signal> Data, OpenFileDialog openFileDialog1)
        {
            openFileDialog1.FileName = null;
            openFileDialog1.InitialDirectory = "C:\\Users\\1\\Desktop";
            openFileDialog1.Filter = ".CRV files (*.crv)|*.crv";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
                    string[] buf;
                    for (int i = 5; i < lines.Length; i++) // Учитываем первые 5 строчек с мусором
                    {
                        buf = lines[i].Split('\t');
                        Data.Add(new Core.Signal(Convert.ToDouble(buf[0]), Convert.ToDouble(buf[1]), Convert.ToDouble(buf[2])));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
