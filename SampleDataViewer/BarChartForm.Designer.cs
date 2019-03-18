namespace DataTableDataViewer
{
    partial class BarChartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.barChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.characteristic1 = new System.Windows.Forms.RadioButton();
            this.characteristic2 = new System.Windows.Forms.RadioButton();
            this.characteristic3 = new System.Windows.Forms.RadioButton();
            this.characteristic4 = new System.Windows.Forms.RadioButton();
            this.radioBtnGB = new System.Windows.Forms.Panel();
            this.addToRaportBtn = new System.Windows.Forms.Button();
            this.errorLbl = new System.Windows.Forms.Label();
            this.addAllCharacteristicsToRaportBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).BeginInit();
            this.radioBtnGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // barChart
            // 
            this.barChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.barChart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.barChart.Legends.Add(legend1);
            this.barChart.Location = new System.Drawing.Point(12, 12);
            this.barChart.Name = "barChart";
            this.barChart.Size = new System.Drawing.Size(662, 426);
            this.barChart.TabIndex = 0;
            this.barChart.Text = "chart1";
            // 
            // characteristic1
            // 
            this.characteristic1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.characteristic1.AutoSize = true;
            this.characteristic1.Location = new System.Drawing.Point(16, 14);
            this.characteristic1.Name = "characteristic1";
            this.characteristic1.Size = new System.Drawing.Size(110, 17);
            this.characteristic1.TabIndex = 1;
            this.characteristic1.TabStop = true;
            this.characteristic1.Text = "Charakterystyka 1";
            this.characteristic1.UseVisualStyleBackColor = true;
            this.characteristic1.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // characteristic2
            // 
            this.characteristic2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.characteristic2.AutoSize = true;
            this.characteristic2.Location = new System.Drawing.Point(16, 37);
            this.characteristic2.Name = "characteristic2";
            this.characteristic2.Size = new System.Drawing.Size(110, 17);
            this.characteristic2.TabIndex = 2;
            this.characteristic2.TabStop = true;
            this.characteristic2.Text = "Charakterystyka 2";
            this.characteristic2.UseVisualStyleBackColor = true;
            this.characteristic2.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // characteristic3
            // 
            this.characteristic3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.characteristic3.AutoSize = true;
            this.characteristic3.Location = new System.Drawing.Point(16, 60);
            this.characteristic3.Name = "characteristic3";
            this.characteristic3.Size = new System.Drawing.Size(110, 17);
            this.characteristic3.TabIndex = 3;
            this.characteristic3.TabStop = true;
            this.characteristic3.Text = "Charakterystyka 3";
            this.characteristic3.UseVisualStyleBackColor = true;
            this.characteristic3.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // characteristic4
            // 
            this.characteristic4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.characteristic4.AutoSize = true;
            this.characteristic4.Location = new System.Drawing.Point(17, 83);
            this.characteristic4.Name = "characteristic4";
            this.characteristic4.Size = new System.Drawing.Size(110, 17);
            this.characteristic4.TabIndex = 4;
            this.characteristic4.TabStop = true;
            this.characteristic4.Text = "Charakterystyka 4";
            this.characteristic4.UseVisualStyleBackColor = true;
            this.characteristic4.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // radioBtnGB
            // 
            this.radioBtnGB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioBtnGB.Controls.Add(this.characteristic1);
            this.radioBtnGB.Controls.Add(this.characteristic4);
            this.radioBtnGB.Controls.Add(this.characteristic2);
            this.radioBtnGB.Controls.Add(this.characteristic3);
            this.radioBtnGB.Location = new System.Drawing.Point(689, 12);
            this.radioBtnGB.Name = "radioBtnGB";
            this.radioBtnGB.Size = new System.Drawing.Size(138, 133);
            this.radioBtnGB.TabIndex = 5;
            // 
            // addToRaportBtn
            // 
            this.addToRaportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addToRaportBtn.Location = new System.Drawing.Point(689, 185);
            this.addToRaportBtn.Name = "addToRaportBtn";
            this.addToRaportBtn.Size = new System.Drawing.Size(138, 23);
            this.addToRaportBtn.TabIndex = 6;
            this.addToRaportBtn.Text = "Dodaj do raportu";
            this.addToRaportBtn.UseVisualStyleBackColor = true;
            this.addToRaportBtn.Click += new System.EventHandler(this.addToRaportBtn_Click);
            // 
            // errorLbl
            // 
            this.errorLbl.AutoSize = true;
            this.errorLbl.Location = new System.Drawing.Point(686, 271);
            this.errorLbl.Name = "errorLbl";
            this.errorLbl.Size = new System.Drawing.Size(29, 13);
            this.errorLbl.TabIndex = 7;
            this.errorLbl.Text = "label";
            // 
            // addAllCharacteristicsToRaportBtn
            // 
            this.addAllCharacteristicsToRaportBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addAllCharacteristicsToRaportBtn.Location = new System.Drawing.Point(689, 214);
            this.addAllCharacteristicsToRaportBtn.Name = "addAllCharacteristicsToRaportBtn";
            this.addAllCharacteristicsToRaportBtn.Size = new System.Drawing.Size(138, 45);
            this.addAllCharacteristicsToRaportBtn.TabIndex = 8;
            this.addAllCharacteristicsToRaportBtn.Text = "Dodaj wszystkie charakterystyki do raportu";
            this.addAllCharacteristicsToRaportBtn.UseVisualStyleBackColor = true;
            this.addAllCharacteristicsToRaportBtn.Click += new System.EventHandler(this.addAllCharacteristicsToRaportBtn_Click);
            // 
            // BarChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 450);
            this.Controls.Add(this.addAllCharacteristicsToRaportBtn);
            this.Controls.Add(this.errorLbl);
            this.Controls.Add(this.addToRaportBtn);
            this.Controls.Add(this.radioBtnGB);
            this.Controls.Add(this.barChart);
            this.Name = "BarChartForm";
            this.Text = "Wykres slupkowy";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BarChartForm_FormClosed);
            this.Load += new System.EventHandler(this.BarChartForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barChart)).EndInit();
            this.radioBtnGB.ResumeLayout(false);
            this.radioBtnGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart barChart;

        private System.Windows.Forms.RadioButton characteristic1;
        private System.Windows.Forms.RadioButton characteristic2;
        private System.Windows.Forms.RadioButton characteristic3;
        private System.Windows.Forms.RadioButton characteristic4;
        private System.Windows.Forms.Panel radioBtnGB;
        private System.Windows.Forms.Button addToRaportBtn;
        private System.Windows.Forms.Label errorLbl;
        private System.Windows.Forms.Button addAllCharacteristicsToRaportBtn;
    }
}