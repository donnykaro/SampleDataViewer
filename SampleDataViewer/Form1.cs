using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
using Patagames.Pdf.Net;
using Patagames.Pdf;
using Patagames.Pdf.Enums;
using System.Globalization;

namespace DataTableDataViewer
{
    public partial class Form1 : Form
    {
        DataAccess da = new DataAccess();
        private Bitmap image;
        private static List<Bitmap> picsForRaport = new List<Bitmap>();
        private static List<string> btnNamesForRaport = new List<string>();
        List<DataTable> DataTableEntityList = new List<DataTable>();
        List<string> cameraPositions = new List<string>();
        List<string> camerasChecked = new List<string>();
        ToolTip mTooltip;
        Point mLastPos = new Point(-1, -1);
        public Form1()
        {
            //CreatePdf();
            Application.EnableVisualStyles();
            InitializeComponent();

            datePickerStart.MaxDate = DateTime.Now;
            datePickerStart.CustomFormat = " ";
            datePickerStart.Format = DateTimePickerFormat.Custom;
            datePickerEnd.MaxDate = DateTime.Now;
            datePickerEnd.CustomFormat = " ";
            datePickerEnd.Format = DateTimePickerFormat.Custom;
            timePickerStart.Format = DateTimePickerFormat.Time;
            timePickerStart.Value = new DateTime(2017, 11, 01, 0, 0, 0);
            timePickerStart.ShowUpDown = true;
            timePickerEnd.Format = DateTimePickerFormat.Time;
            timePickerEnd.ShowUpDown = true;

            InitializeModelList();
            //InitializeCameraPositionList();
            InitializeViewListColumns();
            modelComboBox.Sorted = true;
            //modelComboBox.SelectedIndex = 0;
            //cameraPositionComboBox.SelectedIndex = 0;

            errorLbl.ForeColor = Color.Red;
            errorLbl2.ForeColor = Color.Red;
        }

        public static void SetPicturesForRaport(Bitmap picForRaport, string charName)
        {
            picsForRaport.Add(picForRaport);
            btnNamesForRaport.Add(charName);
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            if (!CheckDate()) return;
            errorLbl.Text = "";

            if (!Equals(cameraPositions.Count, 0)) cameraPositions.Clear();
            foreach (object ob in cameraPositionCheckedListBox.CheckedItems)
            {
                cameraPositions.Add(ob.ToString());
            }
            if (!DataCheck(cameraPositions)) return;

            listViewFromDB.Items.Clear();

            string model = modelComboBox.GetItemText(modelComboBox.SelectedItem);
            // fixing bug when running on machine with win10
            string starting_date = datePickerStart.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerStart.Value.ToShortDateString();
            string starting_time = timePickerStart.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();
            string ending_date = datePickerEnd.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerEnd.Value.ToShortDateString();
            string ending_time = timePickerEnd.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();
            string part_number = partNumberTextBox.Text;

            if ((DataTableEntityList = da.GetDataTableData(model, cameraPositions, starting_date, starting_time, ending_date, ending_time, part_number)) != null)
            {
                foreach (DataTable de in DataTableEntityList)
                {
                    string[] dateTimeArr = de.timestamp.ToString().Split(' ');
                    ListViewItem item = new ListViewItem(dateTimeArr[0]);

                    item.Checked = true;
                    item.SubItems.Add(dateTimeArr[1]);
                    item.SubItems.Add(de.product_name.ToString());
                    item.SubItems.Add(de.part_number.ToString());
                    item.SubItems.Add(de.camera_position.ToString());
                    item.SubItems.Add(de.camera_result.ToString());
                    item.SubItems.Add(de.product_result.ToString());

                    item.SubItems.Add(de.characteristic1.ToString());
                    item.SubItems[7].BackColor = (BoundaryCheck.CheckBoundary(model, "characteristic1", de.camera_position, de.characteristic1)) ? Color.White : Color.Red;
                    item.SubItems[7].ForeColor = (BoundaryCheck.CheckBoundary(model, "characteristic1", de.camera_position, de.characteristic1)) ? Color.Black : Color.White;
                    item.SubItems.Add(de.characteristic2.ToString());
                    item.SubItems[8].BackColor = (BoundaryCheck.CheckBoundary(model, "characteristic2", de.camera_position, de.characteristic2)) ? Color.White : Color.Red;
                    item.SubItems[8].ForeColor = (BoundaryCheck.CheckBoundary(model, "characteristic2", de.camera_position, de.characteristic2)) ? Color.Black : Color.White;
                    item.SubItems.Add(de.characteristic3.ToString());
                    item.SubItems[9].BackColor = (BoundaryCheck.CheckBoundary(model, "characteristic3", de.camera_position, de.characteristic3)) ? Color.White : Color.Red;
                    item.SubItems[9].ForeColor = (BoundaryCheck.CheckBoundary(model, "characteristic3", de.camera_position, de.characteristic3)) ? Color.Black : Color.White;
                    item.SubItems.Add(de.characteristic4.ToString());
                    item.SubItems[10].BackColor = (BoundaryCheck.CheckBoundary(model, "characteristic4", de.camera_position, de.characteristic4)) ? Color.White : Color.Red;
                    item.SubItems[10].ForeColor = (BoundaryCheck.CheckBoundary(model, "characteristic4", de.camera_position, de.characteristic4)) ? Color.Black : Color.White;
                    item.UseItemStyleForSubItems = false;

                    listViewFromDB.Items.AddRange(new ListViewItem[] { item });
                }
                for (int i = 0; i < 4; i++)
                    listViewFromDB.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listViewFromDB.Columns[6].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            if (DataTableEntityList.Select(x => x.part_number).Distinct().Count() == 1)
                boxChartBtn.Enabled = false;
            else
                boxChartBtn.Enabled = true;

        }

        private void InitializeViewListColumns()
        {
            listViewFromDB.View = View.Details;
            // Display grid lines.
            listViewFromDB.GridLines = true;
            listViewFromDB.HideSelection = false;
            listViewFromDB.FullRowSelect = true;
            // Create columns for the items and subitems.
            // Width of -2 indicates auto-size.
            List<string> columnNames = new List<string>();
            if ((columnNames = da.GetColumnNames()) != null)
            {
                foreach (string s in columnNames)
                {
                    listViewFromDB.Columns.Add(Translator.TranslateColumnName(s), -2, HorizontalAlignment.Left);
                }
            }
        }

        private void listViewFromDB_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            string model = listViewFromDB.Items[e.ItemIndex].SubItems[2].Text;
            string date = listViewFromDB.Items[e.ItemIndex].SubItems[0].Text.Replace('-', '.');
            string part_number = listViewFromDB.Items[e.ItemIndex].SubItems[3].Text;
            string camera_position = listViewFromDB.Items[e.ItemIndex].SubItems[4].Text;
            string camera_result = listViewFromDB.Items[e.ItemIndex].SubItems[5].Text;
            camera_result = (Equals(camera_result, "PartDefect")) ? "Defect" : camera_result;
            ShowImage(model, date, part_number, camera_position, camera_result);
        }

        private void ShowImage(string model, string date, string part_number, string camera_position, string camera_result)
        {
            string fileName = model + ' ' + date + ' ' + part_number + ' ' + camera_position + ' ' + camera_result;
            string imgPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "files", Path.ChangeExtension(fileName, ".png"));

            if (image != null)
            {
                image.Dispose();
            }

            if (File.Exists(imgPath))
            {
                image = new Bitmap(imgPath);
            }
            else
            {
                //image = new Bitmap(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"files\default.png"));
                
            }
            pictureBoxPhoto.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxPhoto.Image = (Image)image;
        }

        private void InitializeModelList()
        {
            List<string> modelsList = da.GetModelsList();
            foreach (string s in modelsList)
            {
                switch (s) {
                    case "A205":
                        modelComboBox.Items.Add("Z " + s);
                        break;
                    case "A213":
                        modelComboBox.Items.Add("Z " + s);
                        break;
                    case "A217":
                        modelComboBox.Items.Add("Z " + s);
                        break;
                    case "A222":
                        modelComboBox.Items.Add("Z " + s);
                        break;
                    default:
                        modelComboBox.Items.Add(s);
                        break;
                }
            }
        }

        private void barChartBtn_Click(object sender, EventArgs e)
        {
            if (!CheckDate()) return;

            errorLbl.Text = "";

            if (!Equals(cameraPositions.Count, 0)) cameraPositions.Clear();
            foreach (object ob in cameraPositionCheckedListBox.CheckedItems)
            {
                cameraPositions.Add(ob.ToString());
                //string o = ob.ToString();
            }
            if (!DataCheck(cameraPositions)) return;
            if (!DateBoundaryForBarChart()) return;

            string model = modelComboBox.GetItemText(modelComboBox.SelectedItem);
            //string camera_position = cameraPositionComboBox.GetItemText(cameraPositionComboBox.SelectedItem);
            string starting_date = datePickerStart.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerStart.Value.ToShortDateString();
            string starting_time = timePickerStart.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();
            string ending_date = datePickerEnd.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerEnd.Value.ToShortDateString();
            string ending_time = timePickerEnd.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();
            string part_number = partNumberTextBox.Text;

            BarChartForm barChartForm;
            barChartForm = new BarChartForm(model, cameraPositions, starting_date, starting_time, ending_date, ending_time, part_number);

            barChartForm.Owner = this;
            barChartForm.Show();
        }

        private void boxChartBtn_Click(object sender, EventArgs e)
        {
            if (!CheckDate()) return;
            errorLbl.Text = "";

            if (!Equals(cameraPositions.Count, 0)) cameraPositions.Clear();
            foreach (object ob in cameraPositionCheckedListBox.CheckedItems)
            {
                cameraPositions.Add(ob.ToString());
                //string o = ob.ToString();
            }
            if (!DataCheck(cameraPositions)) return;

            string model = modelComboBox.GetItemText(modelComboBox.SelectedItem);
            //string camera_position = cameraPositionComboBox.GetItemText(cameraPositionComboBox.SelectedItem); ;
            string starting_date = datePickerStart.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerStart.Value.ToShortDateString();
            string starting_time = timePickerStart.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();
            string ending_date = datePickerEnd.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerEnd.Value.ToShortDateString();
            string ending_time = timePickerEnd.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();

            BoxChartForm boxChartForm;
            boxChartForm = new BoxChartForm(model, cameraPositions, starting_date, starting_time, ending_date, ending_time);

            boxChartForm.Owner = this;
            boxChartForm.Show();
        }

        private void datePickerStart_ValueChanged(object sender, EventArgs e)
        {
            datePickerStart.Format = DateTimePickerFormat.Long;
        }

        private void datePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            datePickerEnd.Format = DateTimePickerFormat.Long;
        }

        private void cameraPositionCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (object ob in cameraPositionCheckedListBox.CheckedItems)
            {
                camerasChecked.Add(ob.ToString());
            }

            if (camerasChecked.Count >= 1
                && cameraPositionCheckedListBox.GetItemCheckState(0) == CheckState.Checked
                && !Equals(cameraPositionCheckedListBox.SelectedItem, "Wszystkie")
                && !Equals(cameraPositionCheckedListBox.SelectedIndex, 0))
                cameraPositionCheckedListBox.SetItemChecked(0, false);

            if (camerasChecked.Count >= 1
            && cameraPositionCheckedListBox.GetItemCheckState(0) == CheckState.Checked
            && Equals(cameraPositionCheckedListBox.SelectedItem, "Wszystkie")
            && Equals(cameraPositionCheckedListBox.SelectedIndex, 0))
                for (int i = 1; i < cameraPositionCheckedListBox.Items.Count; i++)
                    if (cameraPositionCheckedListBox.GetItemCheckState(i) == CheckState.Checked)
                        cameraPositionCheckedListBox.SetItemChecked(i, false);
        }

        private void partNumberTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(partNumberTextBox.Text))
                boxChartBtn.Enabled = true;
            else
                boxChartBtn.Enabled = false;
        }

        private bool CheckDate()
        {
            if (datePickerStart.Format == DateTimePickerFormat.Custom || datePickerEnd.Format == DateTimePickerFormat.Custom)
            {
                errorLbl2.Text = "Wprowadz date";
                return false;
            }
            else
                errorLbl2.Text = "";
            if (datePickerStart.Value > datePickerEnd.Value)
            {
                errorLbl.Text = "Niepoprawny format daty";
                return false;
            }
            return true;
        }

        private bool DataCheckForYield()
        {
            if (Equals(modelComboBox.Text, ""))
            {
                errorLbl.Text = "Podaj model";
                return false;
            }
            else
                errorLbl.Text = "";
            return true;
        }

            private bool DataCheck(List<string> cameraPositions)
        {
            if (Equals(modelComboBox.Text, "") || Equals(cameraPositions.Count, 0))
            {
                errorLbl.Text = "Podaj model i pozycje kamery";
                return false;
            }
            else
                errorLbl.Text = "";
            return true;
        }

        private bool DateBoundaryForBarChart()
        {
            int days = (datePickerEnd.Value - datePickerStart.Value).Days;
            if (days > 40)
            {
                errorLbl2.Text = "Zakres daty dla wykresu slupkowego nie moze";
                errorLbl.Text = "wynosic wiecej niz 40 dni";
                return false;
            }
            return true;
        }

        private void createRaportBtn_Click(object sender, EventArgs e)
        {
            string model = modelComboBox.GetItemText(modelComboBox.SelectedItem);
            //string camera_position = cameraPositionComboBox.GetItemText(cameraPositionComboBox.SelectedItem); ;
            string starting_date = datePickerStart.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerStart.Value.ToShortDateString();
            string ending_date = datePickerEnd.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerEnd.Value.ToShortDateString();

            if (!picsForRaport.Any())
            {
                errorLbl.Text = "Brak zapisanych wykresow" + Environment.NewLine + "do stworzenia raportu";
                return;
            }
            else
                errorLbl.Text = "";
            CreatePdf(model, starting_date, ending_date);
        }
        
        public void CreatePdf(string model, string starting_date, string ending_date)
        {
            saveFileDialog.Filter = "PDF|*.pdf";
            saveFileDialog.Title = "Zapisz raport";
            saveFileDialog.ShowDialog();

            int pageIndex = 0;
            int charIndex = 0;
            int imgYPos = 450;
            int textYPos = 770;
            int imgCount = 0;
            PdfCommon.Initialize();
            using (var doc = PdfDocument.CreateNew())
            {
                PdfFont calibryBold = PdfFont.CreateFont(doc, "CalibriBold", true, 0, 0, 0, FontCharSet.ANSI_CHARSET, false);
                PdfTextObject textObject;
                if (DataTableEntityList.Select(x => x.part_number).Distinct().Count() == 1)
                {
                    List<string> part_number = DataTableEntityList.Select(x => x.part_number).Distinct().ToList();
                    textObject = PdfTextObject.Create("Model: " + model + "        Numer Czesci: " + part_number[0] + "        Data: " + starting_date + " - " + ending_date, 15, 810, calibryBold, 12);
                }
                else
                    textObject = PdfTextObject.Create("Model: " + model + "           Data: " + starting_date + " - " + ending_date, 15, 810, calibryBold, 12);
                textObject.FillColor = Color.Black;
                doc.Pages.InsertPageAt(0, 8.27f * 72, 11.69f * 72);
                doc.Pages[0].PageObjects.Add(textObject);

                foreach (Bitmap pic in picsForRaport)
                {
                    using (var image = pic)
                    {
                        using (PdfBitmap pdfBitmap = new PdfBitmap(image.Width, image.Height, true))
                        {
                            using (var g = Graphics.FromImage(pdfBitmap.Image))
                            {
                                g.DrawImage(image, 0, 0, image.Width, image.Height);
                            }
                            PdfTextObject characteristicTextObject = PdfTextObject.Create(Translator.TranslateColumnName(btnNamesForRaport[charIndex]), 15, textYPos, calibryBold, 12);
                            textYPos -= 350;
                            characteristicTextObject.FillColor = Color.Black;
                            charIndex++;
                            doc.Pages[pageIndex].PageObjects.Add(characteristicTextObject);

                            var imageObject = PdfImageObject.Create(doc, pdfBitmap, 0, 0);

                            imageObject.Matrix = new FS_MATRIX(500, 0, 0, 300, 50, imgYPos);
                            doc.Pages[pageIndex].PageObjects.Add(imageObject);
                            imgYPos -= 350;

                            doc.Pages[pageIndex].GenerateContent();
                            imgCount++;
                            if (imgCount == 2)
                            {
                                pageIndex++;
                                doc.Pages.InsertPageAt(pageIndex, 8.27f * 72, 11.69f * 72);
                                imgYPos = 450;
                                textYPos = 770;
                                imgCount = 0;
                            }
                        }
                    }
                }
                if (imgCount == 0) doc.Pages.DeleteAt(doc.Pages.Count - 1);
                if (saveFileDialog.FileName != "")
                {
                    doc.Save(saveFileDialog.FileName, SaveFlags.NoIncremental);
                    saveFileDialog.FileName = "";
                }
                //doc.Save(@"C:\Users\" + Environment.UserName + @"\Documents\sample_document.pdf", SaveFlags.NoIncremental);

                if (picsForRaport.Any()) picsForRaport.Clear();
                if (btnNamesForRaport.Any()) btnNamesForRaport.Clear();
            }
        }

        private void listViewFromDB_MouseMove(object sender, MouseEventArgs e)
        {
            string model = modelComboBox.GetItemText(modelComboBox.SelectedItem);
            ListViewHitTestInfo info = listViewFromDB.HitTest(e.X, e.Y);

            if (mTooltip == null)
                mTooltip = new ToolTip();

            if (mLastPos != e.Location)
            {
                if (info.Item != null && info.SubItem != null)
                {
                    int columnindex = info.Item.SubItems.IndexOf(info.SubItem);
                    string camera_position = info.Item.SubItems[4].Text;

                    mTooltip.Show((BoundaryCheck.ViewBoundary(columnindex, model, camera_position)), info.Item.ListView, e.X, e.Y, 5000);
                }
                else
                {
                    mTooltip.SetToolTip(listViewFromDB, string.Empty);
                }
            }
            mLastPos = e.Location;
        }

        private void YieldBtn_Click(object sender, EventArgs e)
        {
            if (!CheckDate()) return;
            errorLbl.Text = "";

            if (!Equals(cameraPositions.Count, 0)) cameraPositions.Clear();
            foreach (object ob in cameraPositionCheckedListBox.CheckedItems)
            {
                cameraPositions.Add(ob.ToString());
            }
            if (!DataCheckForYield())
            {
                return;
            }

            string model = modelComboBox.GetItemText(modelComboBox.SelectedItem);
            string starting_date = datePickerStart.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerStart.Value.ToShortDateString();
            string starting_time = timePickerStart.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();
            string ending_date = datePickerEnd.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);//datePickerEnd.Value.ToShortDateString();
            string ending_time = timePickerEnd.Value.ToString("HH:mm:ss", CultureInfo.InvariantCulture);//ToLongTimeString();
            string part_number = partNumberTextBox.Text;

            YieldForm yieldChart;
            yieldChart = new YieldForm(model, starting_date, starting_time, ending_date, ending_time);

            yieldChart.Owner = this;
            yieldChart.Show();
        }

        private void pictureBoxPhoto_Paint(object sender, PaintEventArgs e)
        {
            using (Font myFont = new Font("Arial", 14))
            {
                e.Graphics.DrawString("No image available", myFont, Brushes.Green, new Point(2, 2));
            }
        }
    }
}
