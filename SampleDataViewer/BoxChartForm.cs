using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Accord.Statistics;

namespace DataTableDataViewer
{
    public partial class BoxChartForm : Form
    {
        DataAccess da = new DataAccess();
        List<DataTable> DataTableEntityList = new List<DataTable>();
        List<DataTable> DataTableEntityListTemp = new List<DataTable>();
        List<string> camera_positions = new List<string>();
        Point? prevPosition = null;
        ToolTip tooltip = new ToolTip();
        Bitmap boxPlotImg;
        string model;
        string starting_date;
        string starting_time;
        string ending_date;
        string ending_time;
        string btnName;

        public BoxChartForm()
        {
            InitializeComponent();
        }

        public BoxChartForm(string model, List<string> camera_positions, string starting_date, string starting_time, string ending_date, string ending_time)
        {
            InitializeComponent();

            this.model = model;
            this.camera_positions = camera_positions;
            //this.camera_position = camera_position;
            this.starting_date = starting_date;
            this.starting_time = starting_time;
            this.ending_date = ending_date;
            this.ending_time = ending_time;

            LoadBoxPlotLegend();

            if (boxChart.ChartAreas.Any()) boxChart.ChartAreas.Clear();

            errorLbl.Text = string.Empty;
            errorLbl.ForeColor = Color.Red;
        }

        private void BoxChartForm_Load(object sender, EventArgs e)
        {
            this.Owner.Enabled = false;
        }

        private void BoxChartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Owner.Enabled = true;
        }

        // use one event handler to hanle all radio buttons
        private void CheckedChanged(object sender, EventArgs e)
        {
            if(boxChart.Series.Any())
                boxChart.Series.Clear();

            var button = radioBtnGB.Controls.OfType<RadioButton>()
                           .FirstOrDefault(n => n.Checked);
            btnName = button.Name;
            SetData(button.Name);
        }

        private void SetData(string btnName)
        {
            if (boxChart.ChartAreas.Any()) boxChart.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("ChartArea1");
            boxChart.ChartAreas.Add(chartArea1);

            boxChart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            if ((DataTableEntityList = da.GetDataTableData(model, camera_positions, starting_date, starting_time, ending_date, ending_time, "")) != null)
            {
                //DataTableEntityList = DataTableEntityListTemp.Where(x => camera_positions.Contains(x.camera_position)).ToList();
                List<string> cameras = new List<string>{ "c1", "c2", "c3", "c4", "c5", "c6", "c7", "c8", "c9", "c10", "c11", "c12", "c13", "c14" };
                float?[] dataArr;
                int count;

                boxChart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
                boxChart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

                boxChart.Series.Add("BoxPlotSeries");
                boxChart.Series["BoxPlotSeries"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.BoxPlot;
                boxChart.Series["BoxPlotSeries"]["BoxPlotShowMedian"] = "true";
                boxChart.Series["BoxPlotSeries"]["BoxPlotShowUnusualValues"] = "true";
                boxChart.Series["BoxPlotSeries"].IsVisibleInLegend = false;

                if (camera_positions[0] != "Wszystkie") {
                    cameras.Clear();
                    for(int i = 0; i < camera_positions.Count; i++)
                    cameras.Add(camera_positions[i]);
                }
                count = DataTableEntityList.Select(x => x.camera_position).Distinct().Count();
                switch (btnName)
                {
                    case "characteristic1":
                        for(int i = 0; i < count; i++)
                        {
                            dataArr = DataTableEntityList.Where(y => y.camera_position == cameras[i]).Where(y => y.characteristic1 != null).Select(y => y.characteristic1).ToArray();
                            SetEndData(cameras, dataArr, i);
                        }
                        break;
                    case "characteristic2":
                        for (int i = 0; i < count; i++)
                        {
                            dataArr = DataTableEntityList.Where(x => x.camera_position == cameras[i]).Where(y => y.characteristic2!= null).Select(y => y.characteristic2).ToArray();
                            SetEndData(cameras, dataArr, i);
                        }
                        break;
                    case "characteristic3":
                        for (int i = 0; i < count; i++)
                        {
                            dataArr = DataTableEntityList.Where(x => x.camera_position == cameras[i]).Where(y => y.characteristic3 != null).Select(y => y.characteristic3).ToArray();
                            SetEndData(cameras, dataArr, i);
                        }
                        break;
                    case "characteristic4":
                        for (int i = 0; i < count; i++)
                        {
                            dataArr = DataTableEntityList.Where(x => x.camera_position == cameras[i]).Where(y => y.characteristic4 != null).Select(y => y.characteristic4).ToArray();
                            SetEndData(cameras, dataArr, i);
                        }
                        break;
                }
            }
        }

        private void SetEndData(List<string> cameras, float?[] dataArr, int i)
        {
            double[] convertedDataArr;
            float?[] emptyDataArr;
            double firstQuartile;
            double median;
            double thirdQuartile;
            double IQR;
            double lowerWhisker;
            double upperWhisker;
            //double[] outliers;

            convertedDataArr = Array.ConvertAll(dataArr, y => Double.Parse(y.ToString()));
            if (!dataArr.Any())
            {
                List<float?> lst = new List<float?>(dataArr);
                lst.Add(0);
                emptyDataArr = lst.ToArray();
                convertedDataArr = Array.ConvertAll(emptyDataArr, y => Double.Parse(y.ToString()));
            }

            firstQuartile = Measures.LowerQuartile(convertedDataArr);
            median = Measures.Median(convertedDataArr);
            thirdQuartile = Measures.UpperQuartile(convertedDataArr);
            IQR = thirdQuartile - firstQuartile;
            lowerWhisker = firstQuartile - 1.5 * IQR;
            upperWhisker = thirdQuartile + 1.5 * IQR;
            //outliers = FindOutliers(convertedDataArr, lowerWhisker, upperWhisker);

            boxChart.Series["BoxPlotSeries"].Points.AddXY(cameras[i], lowerWhisker, upperWhisker, firstQuartile, thirdQuartile, median);
            boxChart.Series["BoxPlotSeries"].Points[i].Color = Color.LightBlue;

            //boxChart.ChartAreas[0].AxisY.Interval = 0.4;
            //boxChart.ChartAreas[0].AxisY.IsMarginVisible = false;
        }

        private double[] FindOutliers(double[] data, double lowerWhisker, double upperWhisker)
        {
            List<double> returnData = new List<double>();

            foreach(double d in data)
            {
                if (d < lowerWhisker || d > upperWhisker)
                    returnData.Add(d);
            }

            return returnData.ToArray();
        }

        void boxChart_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            if (prevPosition.HasValue && pos == prevPosition.Value)
                return;
            tooltip.RemoveAll();
            prevPosition = pos;
            var results = boxChart.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        // check if the cursor is really close to the point (2 pixels around the point)
                        //if (Math.Abs(pos.X - pointXPixel) < 2 &&
                        //    Math.Abs(pos.Y - pointYPixel) < 2)
                        //{
                        tooltip.Show("Gorna granica zakresu = " + prop.YValues[1] + Environment.NewLine + "Trzeci kwartyl = " + prop.YValues[3] + Environment.NewLine +
                            "Mediana = " + prop.YValues[4] + Environment.NewLine + "Pierwszy kwartyl = " + prop.YValues[2] + Environment.NewLine +
                            "Dolna granica zakresu = " + prop.YValues[0],
                            this.boxChart,
                        pos.X, pos.Y - 15);
                        //tooltip.Show(prop.YValues[0]);
                        //}
                    }
                }
            }
        }

        private void LoadBoxPlotLegend()
        {
            string filePath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"files\wykres-pudelkowy.png");
            if (File.Exists(filePath))
                boxPlotImg = new Bitmap(filePath);

            boxPlotPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            boxPlotPictureBox.Image = (Image)boxPlotImg;
        }

        private void addToRaportBtn_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(boxChart.Width, boxChart.Height);
            boxChart.DrawToBitmap(bmp, new Rectangle(0, 0, boxChart.Width, boxChart.Height));
            if (DataTableEntityList.Count == 0)
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
                Bitmap bmp = new Bitmap(boxChart.Width, boxChart.Height);
                boxChart.DrawToBitmap(bmp, new Rectangle(0, 0, boxChart.Width, boxChart.Height));
                if (DataTableEntityList.Count == 0)
                {
                    errorLbl.Text = "Brak danych";
                    this.Enabled = true;
                    return;
                }
                else
                {
                    //errorLbl.Text = "Zaimportowano";
                    Form1.SetPicturesForRaport(bmp, btnName);
                }
            }

            this.Enabled = true;
            errorLbl.Text = "Zaimportowano";
        }
    }
}
