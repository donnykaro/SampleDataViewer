namespace DataTableDataViewer
{
    partial class Form1
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
            this.modelComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.partNumberTextBox = new System.Windows.Forms.TextBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.listViewFromDB = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBoxPhoto = new System.Windows.Forms.PictureBox();
            this.timePickerStart = new System.Windows.Forms.DateTimePicker();
            this.timePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.barChartBtn = new System.Windows.Forms.Button();
            this.boxChartBtn = new System.Windows.Forms.Button();
            this.datePickerStart = new System.Windows.Forms.DateTimePicker();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.errorLbl = new System.Windows.Forms.Label();
            this.cameraPositionCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.errorLbl2 = new System.Windows.Forms.Label();
            this.createRaportBtn = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.YieldBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).BeginInit();
            this.SuspendLayout();
            // 
            // modelComboBox
            // 
            this.modelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.modelComboBox.FormattingEnabled = true;
            this.modelComboBox.Location = new System.Drawing.Point(28, 73);
            this.modelComboBox.Name = "modelComboBox";
            this.modelComboBox.Size = new System.Drawing.Size(121, 21);
            this.modelComboBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Model";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pozycja kamery";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Zakres daty";
            // 
            // partNumberTextBox
            // 
            this.partNumberTextBox.Location = new System.Drawing.Point(605, 74);
            this.partNumberTextBox.Name = "partNumberTextBox";
            this.partNumberTextBox.Size = new System.Drawing.Size(191, 20);
            this.partNumberTextBox.TabIndex = 7;
            this.partNumberTextBox.TextChanged += new System.EventHandler(this.partNumberTextBox_TextChanged);
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(810, 74);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(75, 23);
            this.searchBtn.TabIndex = 8;
            this.searchBtn.Text = "Szukaj";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // listViewFromDB
            // 
            this.listViewFromDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listViewFromDB.Location = new System.Drawing.Point(28, 154);
            this.listViewFromDB.Name = "listViewFromDB";
            this.listViewFromDB.Size = new System.Drawing.Size(462, 284);
            this.listViewFromDB.TabIndex = 9;
            this.listViewFromDB.UseCompatibleStateImageBehavior = false;
            this.listViewFromDB.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewFromDB_ItemSelectionChanged);
            this.listViewFromDB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listViewFromDB_MouseMove);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(605, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Numer czesci";
            // 
            // pictureBoxPhoto
            // 
            this.pictureBoxPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPhoto.Location = new System.Drawing.Point(513, 154);
            this.pictureBoxPhoto.Name = "pictureBoxPhoto";
            this.pictureBoxPhoto.Size = new System.Drawing.Size(407, 284);
            this.pictureBoxPhoto.TabIndex = 11;
            this.pictureBoxPhoto.TabStop = false;
            this.pictureBoxPhoto.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxPhoto_Paint);
            // 
            // timePickerStart
            // 
            this.timePickerStart.Location = new System.Drawing.Point(314, 100);
            this.timePickerStart.Name = "timePickerStart";
            this.timePickerStart.Size = new System.Drawing.Size(141, 20);
            this.timePickerStart.TabIndex = 12;
            // 
            // timePickerEnd
            // 
            this.timePickerEnd.Location = new System.Drawing.Point(461, 99);
            this.timePickerEnd.Name = "timePickerEnd";
            this.timePickerEnd.Size = new System.Drawing.Size(138, 20);
            this.timePickerEnd.TabIndex = 13;
            this.timePickerEnd.Value = new System.DateTime(2018, 11, 14, 23, 59, 59, 0);
            // 
            // barChartBtn
            // 
            this.barChartBtn.Location = new System.Drawing.Point(763, 103);
            this.barChartBtn.Name = "barChartBtn";
            this.barChartBtn.Size = new System.Drawing.Size(122, 23);
            this.barChartBtn.TabIndex = 14;
            this.barChartBtn.Text = "Wykres slupkowy";
            this.barChartBtn.UseVisualStyleBackColor = true;
            this.barChartBtn.Click += new System.EventHandler(this.barChartBtn_Click);
            // 
            // boxChartBtn
            // 
            this.boxChartBtn.Location = new System.Drawing.Point(763, 125);
            this.boxChartBtn.Name = "boxChartBtn";
            this.boxChartBtn.Size = new System.Drawing.Size(122, 23);
            this.boxChartBtn.TabIndex = 15;
            this.boxChartBtn.Text = "Wykres pudelkowy";
            this.boxChartBtn.UseVisualStyleBackColor = true;
            this.boxChartBtn.Click += new System.EventHandler(this.boxChartBtn_Click);
            // 
            // datePickerStart
            // 
            this.datePickerStart.Location = new System.Drawing.Point(314, 74);
            this.datePickerStart.MaxDate = new System.DateTime(2100, 11, 10, 0, 0, 0, 0);
            this.datePickerStart.MinDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
            this.datePickerStart.Name = "datePickerStart";
            this.datePickerStart.Size = new System.Drawing.Size(141, 20);
            this.datePickerStart.TabIndex = 16;
            this.datePickerStart.ValueChanged += new System.EventHandler(this.datePickerStart_ValueChanged);
            // 
            // datePickerEnd
            // 
            this.datePickerEnd.Location = new System.Drawing.Point(461, 74);
            this.datePickerEnd.MaxDate = new System.DateTime(2100, 11, 10, 0, 0, 0, 0);
            this.datePickerEnd.MinDate = new System.DateTime(2017, 11, 1, 0, 0, 0, 0);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.Size = new System.Drawing.Size(138, 20);
            this.datePickerEnd.TabIndex = 17;
            this.datePickerEnd.ValueChanged += new System.EventHandler(this.datePickerEnd_ValueChanged);
            // 
            // errorLbl
            // 
            this.errorLbl.AutoSize = true;
            this.errorLbl.Location = new System.Drawing.Point(380, 44);
            this.errorLbl.Name = "errorLbl";
            this.errorLbl.Size = new System.Drawing.Size(0, 13);
            this.errorLbl.TabIndex = 18;
            // 
            // cameraPositionCheckedListBox
            // 
            this.cameraPositionCheckedListBox.CheckOnClick = true;
            this.cameraPositionCheckedListBox.FormattingEnabled = true;
            this.cameraPositionCheckedListBox.Items.AddRange(new object[] {
            "Wszystkie",
            "c1",
            "c2",
            "c3",
            "c4",
            "c5",
            "c6",
            "c7",
            "c8",
            "c9",
            "c10",
            "c11",
            "c12",
            "c13",
            "c14"});
            this.cameraPositionCheckedListBox.Location = new System.Drawing.Point(172, 73);
            this.cameraPositionCheckedListBox.Name = "cameraPositionCheckedListBox";
            this.cameraPositionCheckedListBox.Size = new System.Drawing.Size(120, 49);
            this.cameraPositionCheckedListBox.TabIndex = 19;
            this.cameraPositionCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.cameraPositionCheckedListBox_SelectedIndexChanged);
            // 
            // errorLbl2
            // 
            this.errorLbl2.AutoSize = true;
            this.errorLbl2.Location = new System.Drawing.Point(380, 31);
            this.errorLbl2.Name = "errorLbl2";
            this.errorLbl2.Size = new System.Drawing.Size(0, 13);
            this.errorLbl2.TabIndex = 20;
            // 
            // createRaportBtn
            // 
            this.createRaportBtn.Location = new System.Drawing.Point(635, 103);
            this.createRaportBtn.Name = "createRaportBtn";
            this.createRaportBtn.Size = new System.Drawing.Size(122, 23);
            this.createRaportBtn.TabIndex = 21;
            this.createRaportBtn.Text = "Stworz raport";
            this.createRaportBtn.UseVisualStyleBackColor = true;
            this.createRaportBtn.Click += new System.EventHandler(this.createRaportBtn_Click);
            // 
            // YieldBtn
            // 
            this.YieldBtn.Location = new System.Drawing.Point(635, 125);
            this.YieldBtn.Name = "YieldBtn";
            this.YieldBtn.Size = new System.Drawing.Size(122, 23);
            this.YieldBtn.TabIndex = 22;
            this.YieldBtn.Text = "YIELD";
            this.YieldBtn.UseVisualStyleBackColor = true;
            this.YieldBtn.Click += new System.EventHandler(this.YieldBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 450);
            this.Controls.Add(this.YieldBtn);
            this.Controls.Add(this.createRaportBtn);
            this.Controls.Add(this.errorLbl2);
            this.Controls.Add(this.cameraPositionCheckedListBox);
            this.Controls.Add(this.errorLbl);
            this.Controls.Add(this.datePickerEnd);
            this.Controls.Add(this.datePickerStart);
            this.Controls.Add(this.boxChartBtn);
            this.Controls.Add(this.barChartBtn);
            this.Controls.Add(this.timePickerEnd);
            this.Controls.Add(this.timePickerStart);
            this.Controls.Add(this.pictureBoxPhoto);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listViewFromDB);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.partNumberTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.modelComboBox);
            this.Name = "Form1";
            this.Text = "DataViewer";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox modelComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox partNumberTextBox;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.ListView listViewFromDB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBoxPhoto;
        private System.Windows.Forms.DateTimePicker timePickerStart;
        private System.Windows.Forms.DateTimePicker timePickerEnd;
        private System.Windows.Forms.Button barChartBtn;
        private System.Windows.Forms.Button boxChartBtn;
        private System.Windows.Forms.DateTimePicker datePickerStart;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.Label errorLbl;
        private System.Windows.Forms.CheckedListBox cameraPositionCheckedListBox;
        private System.Windows.Forms.Label errorLbl2;
        private System.Windows.Forms.Button createRaportBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button YieldBtn;
    }
}

