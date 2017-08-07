namespace Наноструктуры
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxCountEigenVal = new System.Windows.Forms.TextBox();
            this.buttonWork = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDepthWell = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLengthWell = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.chartSpektr = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartVoln = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartRaspr = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxC = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartSpektr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVoln)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRaspr)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(490, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Собственных значений:";
            // 
            // textBoxCountEigenVal
            // 
            this.textBoxCountEigenVal.BackColor = System.Drawing.Color.OldLace;
            this.textBoxCountEigenVal.Location = new System.Drawing.Point(661, 4);
            this.textBoxCountEigenVal.Name = "textBoxCountEigenVal";
            this.textBoxCountEigenVal.Size = new System.Drawing.Size(203, 22);
            this.textBoxCountEigenVal.TabIndex = 7;
            this.textBoxCountEigenVal.Text = "4";
            // 
            // buttonWork
            // 
            this.buttonWork.BackColor = System.Drawing.Color.DarkBlue;
            this.buttonWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonWork.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonWork.Location = new System.Drawing.Point(1111, 4);
            this.buttonWork.Name = "buttonWork";
            this.buttonWork.Size = new System.Drawing.Size(272, 24);
            this.buttonWork.TabIndex = 6;
            this.buttonWork.Text = "Выполнить";
            this.buttonWork.UseVisualStyleBackColor = false;
            this.buttonWork.Click += new System.EventHandler(this.buttonWork_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(247, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "u0:";
            // 
            // textBoxDepthWell
            // 
            this.textBoxDepthWell.BackColor = System.Drawing.Color.OldLace;
            this.textBoxDepthWell.Location = new System.Drawing.Point(281, 6);
            this.textBoxDepthWell.Name = "textBoxDepthWell";
            this.textBoxDepthWell.Size = new System.Drawing.Size(203, 22);
            this.textBoxDepthWell.TabIndex = 2;
            this.textBoxDepthWell.Text = "0.5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "a:";
            // 
            // textBoxLengthWell
            // 
            this.textBoxLengthWell.BackColor = System.Drawing.Color.OldLace;
            this.textBoxLengthWell.Location = new System.Drawing.Point(38, 6);
            this.textBoxLengthWell.Name = "textBoxLengthWell";
            this.textBoxLengthWell.Size = new System.Drawing.Size(203, 22);
            this.textBoxLengthWell.TabIndex = 0;
            this.textBoxLengthWell.Text = "5";
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.Lavender;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(866, 169);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(517, 548);
            this.listBox1.TabIndex = 1;
            // 
            // chartSpektr
            // 
            this.chartSpektr.BackColor = System.Drawing.Color.Lavender;
            chartArea1.BackColor = System.Drawing.Color.Ivory;
            chartArea1.Name = "ChartArea1";
            this.chartSpektr.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.Color.Lavender;
            legend1.Name = "Legend1";
            this.chartSpektr.Legends.Add(legend1);
            this.chartSpektr.Location = new System.Drawing.Point(15, 497);
            this.chartSpektr.Name = "chartSpektr";
            this.chartSpektr.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.Name = "№ 0";
            this.chartSpektr.Series.Add(series1);
            this.chartSpektr.Size = new System.Drawing.Size(835, 220);
            this.chartSpektr.TabIndex = 2;
            this.chartSpektr.Text = "Спектр";
            title1.Name = "Title1";
            title1.Text = "Энергетические уровни";
            this.chartSpektr.Titles.Add(title1);
            // 
            // chartVoln
            // 
            this.chartVoln.BackColor = System.Drawing.Color.Lavender;
            chartArea2.BackColor = System.Drawing.Color.Ivory;
            chartArea2.Name = "ChartArea1";
            this.chartVoln.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.Lavender;
            legend2.Name = "Legend1";
            this.chartVoln.Legends.Add(legend2);
            this.chartVoln.Location = new System.Drawing.Point(15, 45);
            this.chartVoln.Name = "chartVoln";
            this.chartVoln.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "№ 0";
            this.chartVoln.Series.Add(series2);
            this.chartVoln.Size = new System.Drawing.Size(835, 220);
            this.chartVoln.TabIndex = 0;
            this.chartVoln.Tag = "";
            this.chartVoln.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Волновые функции";
            this.chartVoln.Titles.Add(title2);
            // 
            // chartRaspr
            // 
            this.chartRaspr.BackColor = System.Drawing.Color.Lavender;
            chartArea3.BackColor = System.Drawing.Color.Ivory;
            chartArea3.Name = "ChartArea1";
            this.chartRaspr.ChartAreas.Add(chartArea3);
            legend3.BackColor = System.Drawing.Color.Lavender;
            legend3.Name = "Legend1";
            this.chartRaspr.Legends.Add(legend3);
            this.chartRaspr.Location = new System.Drawing.Point(15, 271);
            this.chartRaspr.Name = "chartRaspr";
            this.chartRaspr.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Legend1";
            series3.Name = "№ 0";
            this.chartRaspr.Series.Add(series3);
            this.chartRaspr.Size = new System.Drawing.Size(835, 220);
            this.chartRaspr.TabIndex = 0;
            this.chartRaspr.Text = "chart1";
            title3.Name = "Title1";
            title3.Text = "Плотность распределения вероятности";
            this.chartRaspr.Titles.Add(title3);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(874, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(430, 22);
            this.label4.TabIndex = 9;
            this.label4.Text = "Частица в потенциальной яме конечной глубины";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 7.6F, System.Drawing.FontStyle.Italic);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(863, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(451, 60);
            this.label5.TabIndex = 10;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // textBoxC
            // 
            this.textBoxC.BackColor = System.Drawing.Color.OldLace;
            this.textBoxC.Location = new System.Drawing.Point(902, 4);
            this.textBoxC.Name = "textBoxC";
            this.textBoxC.Size = new System.Drawing.Size(203, 22);
            this.textBoxC.TabIndex = 11;
            this.textBoxC.Text = "5";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(875, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "C:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.ClientSize = new System.Drawing.Size(1395, 737);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxC);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chartRaspr);
            this.Controls.Add(this.chartVoln);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.chartSpektr);
            this.Controls.Add(this.buttonWork);
            this.Controls.Add(this.textBoxCountEigenVal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDepthWell);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLengthWell);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ФКН, 4 курс, 6 группа. Частица в потенциальной яме конечной глубины";
            ((System.ComponentModel.ISupportInitialize)(this.chartSpektr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartVoln)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRaspr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonWork;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDepthWell;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLengthWell;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartSpektr;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartVoln;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRaspr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxCountEigenVal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxC;
        private System.Windows.Forms.Label label6;
    }
}

