using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataTableDataViewer
{
    public partial class YieldForm : Form
    {
        List<string> cameras = new List<string> { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14" };
        string model;
        List<string> camera_positions = new List<string>();
        DataAccess da = new DataAccess();
        List<DataTable> DataTableEntityList = new List<DataTable>();
        List<List<decimal>> boundaries = new List<List<decimal>>();
        int boundaryDataTableEntityListCount;
        int correctBoundaryDataTableEntityListCount;
        string starting_date;
        string starting_time;
        string ending_date;
        string ending_time;
        float minBoundary;
        float maxBoundary;
        List<float> maxBoundaries = new List<float>();
        string btnName;
        string boundaryType;

        public YieldForm()
        {
            InitializeComponent();
        }

        public YieldForm(string model, string starting_date, string starting_time, string ending_date, string ending_time)
        {
            InitializeComponent();

            //InitializeViewListColumns();

            this.model = model;
            this.camera_positions = new List<string> { "Wszystkie" };
            this.starting_date = starting_date;
            this.starting_time = starting_time;
            this.ending_date = ending_date;
            this.ending_time = ending_time;

            calculateMaxBtn.Enabled = false;

            infoLbl.Text = "Wybierz charakterystyke";
        }

        private void YieldForm_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
        }

        private void YieldForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }

        // use one event handler to hanle all radio buttons
        private void CheckedChanged(object sender, EventArgs e)
        {
            var button = radioBtnGB.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);

            infoLbl.Text = "";
            //barChart.Series.Clear();
            btnName = button.Name;

            numericUpDown17.Value = 0;
            numericUpDown15.Value = 0;
            numericUpDown13.Value = 0;
            numericUpDown11.Value = 0;
            numericUpDown16.Value = 0;
            numericUpDown14.Value = 0;
            numericUpDown12.Value = 0;
            numericUpDown10.Value = 0;
            errorLbl.Text = "";

            calculateMaxBtn.Enabled = true;

            SetBoundary(button.Name);
        }

        private void SetBoundary(string characteristic)
        {
            minBoundary = 0;//BoundaryCheck.ViewBoundary(characteristic + "_min");

            //numericUpDown1.Value = (decimal)minBoundary;
            //numericUpDown18.Value = 0;
        }

        private void InitializeListViewData(List<List<decimal>> boundaries, ListView listView)
        {
            listView.View = View.Details;
            listView.GridLines = true;
            listView.HideSelection = true;

            if (listView.Columns.Count != 0) listView.Columns.Clear();
            if (listView.Items.Count != 0) listView.Items.Clear();
            if (maxBoundaries.Any()) maxBoundaries.Clear();
            listView.Columns.Add("Kamery", -2, HorizontalAlignment.Left);
            if ((DataTableEntityList = da.GetDataTableData(model, camera_positions, starting_date, starting_time, ending_date, ending_time, "").ToList()) != null)
            {
                listView.Columns.Add("0 do max zakresu", -2, HorizontalAlignment.Left);
                
                if (andUpCheckBox.Checked && boundaryType == "maximal" && numericUpDown11.Value != 0)
                {
                    List<decimal> tempList = new List<decimal> { numericUpDown10.Value, 100 };
                    boundaries.Add(tempList);
                }

                foreach (List<decimal> valBoundaries in boundaries)
                {
                    listView.Columns.Add(valBoundaries[0].ToString() + " do " + valBoundaries[1].ToString(), -2, HorizontalAlignment.Left);

                    if (listView.Items.Count != 0)
                    {
                        int camItemIndex = 0;
                        foreach (string camPos in cameras)
                        {
                            boundaryDataTableEntityListCount = GetBoundaryEntityList(DataTableEntityList, btnName, valBoundaries, camPos);

                            float percentage = (float)boundaryDataTableEntityListCount / (float)DataTableEntityList.Where(x => x.camera_position == camPos).Count() * 100;
                            listView.Items[camItemIndex].SubItems.Add(Math.Round(percentage, 3).ToString() + "%");
                            camItemIndex++;
                        }
                        continue;
                    }
                    foreach (string camPos in cameras)
                    {
                        maxBoundary = BoundaryCheck.ReturnBoundary(model, btnName, camPos);
                        maxBoundaries.Add(maxBoundary);
                        ListViewItem item = new ListViewItem(camPos + " z zakresem: " + maxBoundary);
                        List<decimal> correctBoundary = new List<decimal> { (decimal)minBoundary, (decimal)maxBoundary };

                        correctBoundaryDataTableEntityListCount = GetBoundaryEntityList(DataTableEntityList, btnName, correctBoundary, camPos);

                        //List<List<float>> valBoundariesWithBoundaryForCurrentCamera = 
                        valBoundaries[0] = (decimal)maxBoundary;
                        listView.Columns[2].Text = "od max zakresu do " + numericUpDown17.Value;

                        boundaryDataTableEntityListCount = GetBoundaryEntityList(DataTableEntityList, btnName, valBoundaries, camPos);

                        float percentageCorrect = (float)correctBoundaryDataTableEntityListCount / (float)DataTableEntityList.Where(x => x.camera_position == camPos).Count() * 100;
                        float percentage = (float)boundaryDataTableEntityListCount / (float)DataTableEntityList.Where(x => x.camera_position == camPos).Count() * 100;
                        float percentageCorrectRound = 0;
                        float percentageRound = 0;
                        //if (float.IsNaN(percentage))
                        //    percentage = 0;
                        if (!float.IsNaN(percentageCorrect) && !float.IsNaN(percentage))
                        {
                            percentageCorrectRound = (float)Math.Round(percentageCorrect, 3);
                            percentageRound = (float)Math.Round(percentage, 3);
                        }

                        item.SubItems.Add(percentageCorrectRound.ToString() + "%");
                        item.SubItems.Add(percentageRound.ToString() + "%");

                        listView.Items.AddRange(new ListViewItem[] { item });
                    }
                }
            }
            for(int i = 0; i < 3; i++)
                listView.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void numericUpDown17_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckMaxBorderValue(numericUpDown17, numericUpDown1))
                return;
            numericUpDown16.Value = numericUpDown17.Value;
        }

        private void numericUpDown15_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckMaxBorderValue(numericUpDown15, numericUpDown16))
                return;
            numericUpDown14.Value = numericUpDown15.Value;
        }

        private void numericUpDown13_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckMaxBorderValue(numericUpDown13, numericUpDown14))
                return;
            numericUpDown12.Value = numericUpDown13.Value;
        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckMaxBorderValue(numericUpDown11, numericUpDown12))
                return;
            numericUpDown10.Value = numericUpDown11.Value;
        }

        private bool CheckMaxBorderValue(NumericUpDown fvalue, NumericUpDown svalue)
        {
            if (fvalue.Value < svalue.Value)
            {
                fvalue.Value = 0;
                errorLbl.Text = "Wartosc nie moze byc mniejsza od poprzedniej";
                svalue.Value = 0;
                errorLbl.ForeColor = Color.Red;
                return false;
            }
            else
            {
                errorLbl.Text = "";
                return true;
            }
            //return true;
        }

        private void calculateMaxBtn_Click(object sender, EventArgs e)
        {
            if (boundaries.Any()) boundaries.Clear();
            //firstBoundary = new List<decimal>{ numericUpDown1.Value, numericUpDown2.Value };
            boundaries.Add(new List<decimal> { 0, numericUpDown17.Value });

            if (numericUpDown15.Value != 0)
                boundaries.Add(new List<decimal> { numericUpDown16.Value, numericUpDown15.Value });
            if (numericUpDown13.Value != 0)
                boundaries.Add(new List<decimal> { numericUpDown14.Value, numericUpDown13.Value });
            if (numericUpDown11.Value != 0)
                boundaries.Add(new List<decimal> { numericUpDown12.Value, numericUpDown11.Value });
            //if (numericUpDown10.Value != 0 && andUpCheckBox.Checked)
            //    boundaries.Add(new List<decimal> { numericUpDown10.Value, 100 });

            boundaryType = "maximal";

            InitializeListViewData(boundaries, maxBoundaryListView);
        }

        private int GetBoundaryEntityList(List<DataTable> vwEntityList, string characteristic, List<decimal> boundaries, string camPos)
        {
            int boundaryEntityListCount = 0;
            switch (characteristic)
            {
                case "characteristic1":
                    boundaryEntityListCount = vwEntityList.Where(x => (float?)x.characteristic1 >= (float)boundaries[0] && (float?)x.characteristic1 <= (float?)boundaries[1] && x.camera_position == camPos).ToList().Count();
                    break;
                case "characteristic2":
                    boundaryEntityListCount = vwEntityList.Where(x => (float?)x.characteristic2 >= (float?)boundaries[0] && (float?)x.characteristic2 <= (float?)boundaries[1] && x.camera_position == camPos).ToList().Count();
                    break;
                case "characteristic3":
                    boundaryEntityListCount = vwEntityList.Where(x => (float?)x.characteristic3 >= (float)boundaries[0] && (float?)x.characteristic3 <= (float?)boundaries[1] && x.camera_position == camPos).ToList().Count();
                    break;
                case "characteristic4":
                    boundaryEntityListCount = vwEntityList.Where(x => (float?)x.characteristic4 >= (float?)boundaries[0] && (float?)x.characteristic4 <= (float?)boundaries[1] && x.camera_position == camPos).ToList().Count();
                    break;
            }
            return boundaryEntityListCount;
        }
    }
}
