using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cardiography
{
    public partial class FormCardio : Form
    {
        public static List<Core.Signal> Data;
        public FormCardio()
        {
            InitializeComponent();
        }

        private void drawWaves(int from, int to)
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();
            chart1.Series[5].Points.Clear();

            chart1.Series[1].BorderWidth = 14;
            chart1.Series[2].BorderWidth = 14;
            chart1.Series[3].BorderWidth = 14;
            chart1.Series[4].BorderWidth = 14;
            chart1.Series[5].BorderWidth = 14;

            chart1.Series[3].Color = Color.Purple;
            chart1.Series[4].Color = Color.ForestGreen;
            chart1.Series[5].Color = Color.Black;


            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            double maximum = 0;
            int maximumI = 0;
            bool finder = false;

            for (int i = from; i < to; i++)
            {
                chart1.Series[0].Points.AddXY(Math.Round(i / 1000.0, 3), Data[i].O2);
                if (Data[i].O2 > 4000)
                {
                    finder = true;
                    if (maximum < Data[i].O2)
                    {
                        maximum = Data[i].O2;
                        maximumI = i;
                    }
                }
                else
                    if (finder == true)
                    {
                        chart1.Series[1].Points.AddXY(Math.Round(maximumI / 1000.0, 3), Data[maximumI].O2);
                        if(maximumI + 33 < Data.Count)
                            chart1.Series[4].Points.AddXY(Math.Round((maximumI + 33) / 1000.0, 3), Data[maximumI + 33].O2);
                        if (maximumI + 230 < Data.Count)
                            chart1.Series[5].Points.AddXY(Math.Round((maximumI + 230) / 1000.0, 3), Data[maximumI + 230].O2);
                        if(maximumI - 33 > 0)
                            chart1.Series[3].Points.AddXY(Math.Round((maximumI - 33) / 1000.0, 3), Data[maximumI - 33].O2);
                        if (maximumI - 133 > 0)
                            chart1.Series[2].Points.AddXY(Math.Round((maximumI - 133) / 1000.0, 3), Data[maximumI - 133].O2);
                        finder = false;
                        maximum = 0;
                    }
            }
        }

        private void buttonFile_Click(object sender, EventArgs e)
        {
            Data = new List<Core.Signal>();
            Core.AddData(Data, openFileDialog1);
            drawWaves(0, Data.Count);
        }
    }
}
