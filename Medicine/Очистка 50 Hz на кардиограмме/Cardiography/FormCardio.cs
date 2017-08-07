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
        public static List<double> FilteredData;
        public FormCardio()
        {
            InitializeComponent();
        }

        private void drawWaves(int from, int to)
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
         
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            chart2.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart2.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chart2.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;

            if (comboBox1.Text == "Butterworth")
                FilteredData = Core.ButterworthFilter(Data);
            else if (comboBox1.Text == "Bessel")
                FilteredData = Core.BesselFilter(Data);
            else if (comboBox1.Text == "Chebyshev")
                FilteredData = Core.ChebyshevFilter(Data);
            else
                MessageBox.Show("Choose filter!");
          
            for (int i = from; i < to; i++)
            {
                chart1.Series[0].Points.AddXY(Math.Round(i / 1000.0, 3), Data[i].O2);
                chart2.Series[0].Points.AddXY(Math.Round(i / 1000.0, 3), FilteredData[i]);
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
