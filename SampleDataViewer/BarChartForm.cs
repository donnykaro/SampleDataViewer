using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DataTableDataViewer
{
    public partial class BarChartForm : Form
    {
        DataAccess da = new DataAccess();
        List<DataTable> dataTableEntityList = new List<DataTable>();
        List<string> camera_positions = new List<string>();
        List<string> cameraNames = new List<string>();
        StripLine upperBoundaryStripLine = new StripLine();
        StripLine lowerBoundaryStripLine = new StripLine();
        Bitmap imgForRaport;
        string model;
        string starting_date;
        string starting_time;
        string ending_date;
        string ending_time;
        string part_number;
        string btnName;

        double? maxValue = 0.0;
        double? minValue = 0.0;

        public BarChartForm()
        {
            InitializeComponent();
        }

        public BarChartForm(string model, List<string> camera_positions, string starting_date, string starting_time, string ending_date, string ending_time, string part_number)
        {
            InitializeComponent();

            this.model = model;
            this.camera_positions = camera_positions;
            //this.camera_position = camera_position;
            this.starting_date = starting_date;
            this.starting_time = starting_time;
            this.ending_date = ending_date;
            this.ending_time = ending_time;
            this.part_number = part_number;

            if (barChart.ChartAreas.Any()) barChart.ChartAreas.Clear();
            //barChart.ChartAreas[0].AxisY.Maximum = 2.5;

            errorLbl.Text = string.Empty;
            errorLbl.ForeColor = Color.Red;
        }

        private void BarChartForm_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
        }

        private void BarChartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }

        // use one event handler to handle all radio buttons
        private void CheckedChanged(object sender, EventArgs e)
        {
            //barChart.Series.Clear();

            var button = radioBtnGB.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);
            btnName = button.Name;
            SetData(button.Name);
        }

        private void SetData(string valueName)
        {
            List<string> cameras = new List<string> { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14" };
            //barChart.ChartAreas[0].AxisY.IsMarginVisible = false;
            //if (dataTableEntityList.Any()) dataTableEntityList.Clear();
            if (barChart.ChartAreas.Any()) barChart.ChartAreas.Clear();
            ChartArea ChartArea1 = new ChartArea("ChartArea1");
            barChart.ChartAreas.Add(ChartArea1);

            barChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            barChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            barChart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            if ((dataTableEntityList = da.GetDataTableData(model, camera_positions, starting_date, starting_time, ending_date, ending_time, part_number)).OrderBy(x => x.camera_position).ToList() != null)
            {
                //List<DataTable> sorteddataTableEntityList = dataTableEntityList.OrderBy(x => x.camera_position).ToList();
                //cameraNames = sorteddataTableEntityList.OrderBy(x => x.camera_position).Select(x => x.camera_position).Distinct().ToList();
                List<string> dataTableEntityListDistinct = dataTableEntityList.Select(x => x.part_number).Distinct().ToList();

                if (barChart.Series.Any())
                    barChart.Series.Clear();

                int i = 0;
                foreach (string part_number in dataTableEntityListDistinct)
                {
                    string part_num = part_number.ToString();
                    barChart.Series.Add(part_num);
                    barChart.Series[part_num].BorderWidth = 2;
                    barChart.Series[i].IsVisibleInLegend = false;
                    i++;
                }
                i = 0;

                IEnumerable<DataTable> fullSinglePartList = new List<DataTable>();
                List<DataTable> fullDataTableList = new List<DataTable>();
                List<string> iteratedPartNumber = new List<string>();
                foreach (DataTable de in dataTableEntityList)
                {
                    if (iteratedPartNumber.Count != 0 && iteratedPartNumber.Contains(de.part_number))
                        continue;
                    List<DataTable> singlePart = dataTableEntityList.Where(x => x.part_number == de.part_number).ToList();
                    List<string> singlePartCameras = dataTableEntityList.Where(x => x.part_number == de.part_number).Select(s => s.camera_position).ToList();
                    IEnumerable<string> differenceQuery = cameras.Except(singlePartCameras);
                    List<DataTable> emptyDataTableDataList = new List<DataTable>();
                    foreach (string camName in differenceQuery)
                        emptyDataTableDataList.Add(new DataTable
                        {
                            ID = 90000,
                            timestamp = de.timestamp,
                            product_name = de.product_name,
                            part_number = de.part_number,
                            camera_position = camName,
                            camera_result = de.camera_result,
                            product_result = de.product_result,
                            characteristic1 = 0,
                            characteristic2 = 0,
                            characteristic3 = 0,
                            characteristic4 = 0
                        });
                    fullSinglePartList = singlePart.Concat(emptyDataTableDataList)/*.OrderBy(x => x.camera_position)*/;

                    foreach (DataTable dr in fullSinglePartList)
                    {
                        fullDataTableList.Add(dr);
                    }

                    iteratedPartNumber.Add(de.part_number);
                }

                foreach (DataTable dt in fullDataTableList.Distinct())
                {
                    SetEndData(dt, valueName);
                }
            }
        }

        private void SetEndData(DataTable de, string position)
        {
            List<string> cameras = new List<string> { "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14" };

            upperBoundaryStripLine.Interval = 0;
            lowerBoundaryStripLine.Interval = 0;

            switch (position)
            {
                case "characteristic1":
                    barChart.Series[de.part_number].Points.AddXY(de.camera_position, de.characteristic1);
                    SetMaxMinYAxis(de.characteristic1);
                    break;
                case "characteristic2":
                    barChart.Series[de.part_number].Points.AddXY(de.camera_position, de.characteristic2);
                    SetMaxMinYAxis(de.characteristic2);
                    break;
                case "characteristic3":
                    barChart.Series[de.part_number].Points.AddXY(de.camera_position, de.characteristic3);
                    SetMaxMinYAxis(de.characteristic3);
                    break;
                case "characteristic4":
                    barChart.Series[de.part_number].Points.AddXY(de.camera_position, de.characteristic4);
                    SetMaxMinYAxis(de.characteristic4);
                    break;
            }

            upperBoundaryStripLine.Interval = 0;
            upperBoundaryStripLine.StripWidth = 0.01;
            upperBoundaryStripLine.BackColor = Color.Red;
            barChart.ChartAreas[0].AxisY.StripLines.Add(upperBoundaryStripLine);

            lowerBoundaryStripLine.Interval = 0;
            lowerBoundaryStripLine.StripWidth = 0.01;
            lowerBoundaryStripLine.BackColor = Color.Red;
            barChart.ChartAreas[0].AxisY.StripLines.Add(lowerBoundaryStripLine);
        }

        private void SetMaxMinYAxis(double? value)
        {
            if (value > maxValue)
            {
                barChart.ChartAreas[0].RecalculateAxesScale();
                maxValue = value;
                return;
            }
            if (value < minValue)
            {
                barChart.ChartAreas[0].RecalculateAxesScale();
                minValue = value;
                return;
            }

            barChart.ChartAreas[0].AxisY.Interval = 0.4;
        }

        private void addToRaportBtn_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(barChart.Width, barChart.Height);
            barChart.DrawToBitmap(bmp, new Rectangle(0, 0, barChart.Width, barChart.Height));
            if (dataTableEntityList.Count == 0)
                errorLbl.Text = "Najpierw wypelnij" + Environment.NewLine + "wykres danymi";
            else
            {
                errorLbl.Text = string.Empty;
                Form1.SetPicturesForRaport(bmp, btnName);
            }
        }

        private void addAllCharacteristicsToRaportBtn_Click(object sender, EventArgs e)
        {
            errorLbl.Text = "Prosze czekac," + Environment.NewLine + "trwa importowanie" + Environment.NewLine + "wszystkich wykresow" + Environment.NewLine + "do raportu....";
            this.Enabled = false;

            foreach (RadioButton rb in radioBtnGB.Controls.OfType<RadioButton>())
            {
                rb.Checked = true;
                Bitmap bmp = new Bitmap(barChart.Width, barChart.Height);
                barChart.DrawToBitmap(bmp, new Rectangle(0, 0, barChart.Width, barChart.Height));
                if (dataTableEntityList.Count == 0)
                {
                    errorLbl.Text = "Brak danych";
                    this.Enabled = true;
                    return;
                }
                else
                {
                    Form1.SetPicturesForRaport(bmp, btnName);
                }
            }

            this.Enabled = true;
            errorLbl.Text = "Zaimportowano";
        }
    }
}
