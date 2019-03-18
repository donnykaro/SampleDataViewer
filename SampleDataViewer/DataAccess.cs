using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataTableDataViewer
{
    class DataAccess
    {
        List<DataTable> dataTableEntityList = new List<DataTable>();

        public List<string> GetColumnNames()
        {
            //List<string> columnNames = new List<string>();
            //try
            //{
            //    using (SqlConnection dbConn = new SqlConnection(Helper.ConnVal("DataTable")))
            //    {
            //        dbConn.Open();
            //        SqlCommand query = dbConn.CreateCommand();
            //        query.CommandText = @"USE TestDatabase; SELECT COLUMN_NAME FROM TestDatabase.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'DataTable'";
            //        SqlDataReader reader = query.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            if (Equals(reader[0].ToString(), "timestamp"))
            //            {
            //                columnNames.Add("data");
            //                columnNames.Add("czas");
            //            }
            //            columnNames.Add(reader[0].ToString());
            //        }

            //        reader.Close();
            //        columnNames.RemoveAt(0);
            //        columnNames.RemoveAt(2);
            //    }

            //    return columnNames;
            //}
            //catch (SqlException error)
            //{
            //    MessageBox.Show("Blad polaczenia");
            //    return null;
            //}
            return new List<string> { "data", "czas", "product_name", "number", "camera_position", "camera_result", "part_result", };
        }

        public List<String> GetModelsList()
        {
            //List<string> modelsList = new List<string>();

            //try
            //{
            //    using (SqlConnection dbConn = new SqlConnection(Helper.ConnVal("TestDatabase")))
            //    {
            //        dbConn.Open();
            //        SqlCommand query = dbConn.CreateCommand();
            //        query.CommandText = @"USE TestDatabase; select distinct product_name from DataTable";
            //        SqlDataReader reader = query.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            modelsList.Add(reader[0].ToString());
            //        }

            //        reader.Close();
            //    }
            //}
            //catch (SqlException error)
            //{
            //    MessageBox.Show("Blad polaczenia");
            //    return null;
            //}

            //return modelsList;
            return new List<string> { "1model", "2model", "3model" };
        }

        public List<DataTable> GetDataTableData(string model, List<string> camera_positions, string starting_date, string starting_time, string ending_date, string ending_time, string part_number)
        {
            List<DataTable> dt = GetDummyData();

            if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();

            string dateTimeStart = string.Join(" ", starting_date, starting_time);
            DateTime dtStart = DateTime.ParseExact(dateTimeStart, "yyyy-MM-dd HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);
            string dateTimeEnd = string.Join(" ", ending_date, ending_time);
            DateTime dtEnd = DateTime.ParseExact(dateTimeEnd, "yyyy-MM-dd HH:mm:ss",
                                   System.Globalization.CultureInfo.InvariantCulture);

            // inicjalizacja zmiennej z wartoscia nie wystepujaca w tabeli, by zmienna nie byla pusta
            var list = (from product in dt
                        where (product.characteristic1 == 200)
                        select product).ToList();

            if (camera_positions.Count >= 1 && !Equals(camera_positions[0], "Wszystkie"))
            {
                if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
                for (int i = 0; i < camera_positions.Count; i++)
                {
                    list = (from product in dt
                            where ((product.product_name == model) && (product.camera_position == camera_positions[i]) && (product.timestamp >= dtStart) &&
                            (product.timestamp <= dtEnd) && (product.part_number == part_number))
                            select product
                                    ).ToList();
                    list.ForEach(p => dataTableEntityList.Add(p));
                }
            }
            if (camera_positions.Count == 1 && Equals(camera_positions[0], "Wszystkie"))
            {
                if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
                list = (from product in dt
                        where ((product.product_name == model) && (product.timestamp >= dtStart) &&
                        (product.timestamp <= dtEnd) && (product.part_number == part_number))
                        select product
                            ).ToList();
                list.ForEach(p => dataTableEntityList.Add(p));
            }

            if (camera_positions.Count == 1 && Equals(camera_positions[0], "Wszystkie") && Equals(part_number, ""))
            {
                if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
                list = (from product in dt
                        where ((product.product_name == model) && (product.timestamp >= dtStart) &&
                        (product.timestamp <= dtEnd))
                        select product
                            ).ToList();
                list.ForEach(p => dataTableEntityList.Add(p));
            }

            if (Equals(part_number, "") && camera_positions.Count >= 1 && !Equals(camera_positions[0], "Wszystkie"))
            {
                if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
                for (int i = 0; i < camera_positions.Count; i++)
                {
                    list = (from product in dt
                            where ((product.product_name == model) && (product.camera_position == camera_positions[i]) &&
                            (product.timestamp >= dtStart) && (product.timestamp <= dtEnd))
                            select product
                                ).ToList();
                    list.ForEach(p => dataTableEntityList.Add(p));
                }
            }
            return dataTableEntityList.OrderBy(p => p.timestamp).Where(p => p.characteristic1 != null).Where(p => p.characteristic2 != null).Where(p => p.characteristic3 != null)
                .Where(p => p.characteristic4 != null).ToList();

            //wykomenowany kod byl oryginalnie uzywany do komunikacji i obrobki danych
            #region code for sql db access
            //try
            //{
            //    using (DataTableDataClassesDataContext db = new DataTableDataClassesDataContext(Helper.ConnVal("DataTableDataViewer.Properties.Settings.SampleDatabaseConnectionString")))
            //    {
            //        if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();

            //        string dateTimeStart = string.Join(" ", starting_date, starting_time);
            //        DateTime dtStart = DateTime.ParseExact(dateTimeStart, "yyyy-MM-dd HH:mm:ss",
            //                               System.Globalization.CultureInfo.InvariantCulture);
            //        string dateTimeEnd = string.Join(" ", ending_date, ending_time);
            //        DateTime dtEnd = DateTime.ParseExact(dateTimeEnd, "yyyy-MM-dd HH:mm:ss",
            //                               System.Globalization.CultureInfo.InvariantCulture);

            //        var list = (from product in db.DataTables
            //                    where (product.characteristic1 == 200)
            //                    select product).ToList();

            //        if (camera_positions.Count >= 1 && !Equals(camera_positions[0], "Wszystkie"))
            //        {
            //            if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
            //            for (int i = 0; i < camera_positions.Count; i++)
            //            {
            //                list = (from product in db.DataTables
            //                        where ((product.product_name == model) && (product.camera_position == camera_positions[i]) && (product.timestamp >= dtStart) &&
            //                        (product.timestamp <= dtEnd) && (product.part_number == part_number))
            //                        select product
            //                                ).ToList();
            //                list.ForEach(p => dataTableEntityList.Add(p));
            //                //dataTableEntityList.Concat(list);
            //            }
            //        }
            //        if (camera_positions.Count == 1 && Equals(camera_positions[0], "Wszystkie"))
            //        {
            //            if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
            //            list = (from product in db.DataTables
            //                    where ((product.product_name == model) && (product.timestamp >= dtStart) &&
            //                    (product.timestamp <= dtEnd) && (product.part_number == part_number))
            //                    select product
            //                        ).ToList();
            //            list.ForEach(p => dataTableEntityList.Add(p));
            //            //dataTableEntityList.Concat(list);
            //        }

            //        if (camera_positions.Count == 1 && Equals(camera_positions[0], "Wszystkie") && Equals(part_number, ""))
            //        {
            //            if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
            //            list = (from product in db.DataTables
            //                    where ((product.product_name == model) && (product.timestamp >= dtStart) &&
            //                    (product.timestamp <= dtEnd))
            //                    select product
            //                        ).ToList();
            //            list.ForEach(p => dataTableEntityList.Add(p));
            //            //dataTableEntityList.Concat(list);
            //        }

            //        if (Equals(part_number, "") && camera_positions.Count >= 1 && !Equals(camera_positions[0], "Wszystkie"))
            //        {
            //            if (dataTableEntityList.Count >= 1) dataTableEntityList.Clear();
            //            for (int i = 0; i < camera_positions.Count; i++)
            //            {
            //                list = (from product in db.DataTables
            //                        where ((product.product_name == model) && (product.camera_position == camera_positions[i]) &&
            //                        (product.timestamp >= dtStart) && (product.timestamp <= dtEnd))
            //                        select product
            //                            ).ToList();
            //                list.ForEach(p => dataTableEntityList.Add(p));
            //                //dataTableEntityList.Concat(list);
            //            }
            //        }
            //        return dataTableEntityList.OrderBy(p => p.timestamp).Where(p => p.characteristic1 != null).Where(p => p.characteristic2 != null).Where(p => p.characteristic3 != null)
            //            .Where(p => p.characteristic4 != null).ToList();
            //    };
            //}
            //catch (SqlException e)
            //{
            //    MessageBox.Show("Blad polaczenia");
            //    return null;
            //}
            #endregion
        }

        private List<DataTable> GetDummyData(){
            return new List<DataTable> { new DataTable { ID = 1, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c1", camera_result = "OK", product_result = "OK", characteristic1 = 2.23f, characteristic2 = 0.45f, characteristic3 = 2.20f, characteristic4 = 0.45f },
                new DataTable { ID = 1, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c2", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.22f, characteristic3 = 2.20f, characteristic4 = 0.35f },
                new DataTable { ID = 2, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c3", camera_result = "OK", product_result = "OK", characteristic1 = 3.23f, characteristic2 = 0.13f, characteristic3 = 2.0f, characteristic4 = 0.25f },
                new DataTable { ID = 3, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c4", camera_result = "OK", product_result = "OK", characteristic1 = 2.43f, characteristic2 = 1.45f, characteristic3 = 2.25f, characteristic4 = 0.41f },
                new DataTable { ID = 4, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c5", camera_result = "OK", product_result = "OK", characteristic1 = 2.53f, characteristic2 = 2.45f, characteristic3 = 1.20f, characteristic4 = 0.21f },
                new DataTable { ID = 5, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c6", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 1.45f, characteristic3 = 0.20f, characteristic4 = 0.34f },
                new DataTable { ID = 6, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c7", camera_result = "OK", product_result = "OK", characteristic1 = 2f, characteristic2 = 0.25f, characteristic3 = 1.20f, characteristic4 = 0.36f },
                new DataTable { ID = 7, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c8", camera_result = "OK", product_result = "OK", characteristic1 = 2.3f, characteristic2 = 0.67f, characteristic3 = 2.430f, characteristic4 = 0.423f },
                new DataTable { ID = 8, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c9", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.82f, characteristic3 = 1.20f, characteristic4 = 1.45f },
                new DataTable { ID = 9, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c10", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 1.85f, characteristic3 = 2.83f, characteristic4 = 2.45f },
                new DataTable { ID = 10, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c11", camera_result = "OK", product_result = "OK", characteristic1 = 1.03f, characteristic2 = 2.45f, characteristic3 = 2.49f, characteristic4 = 0.345f },
                new DataTable { ID = 11, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c12", camera_result = "OK", product_result = "OK", characteristic1 = 2.03f, characteristic2 = 1.45f, characteristic3 = 2.56f, characteristic4 = 2.245f },
                new DataTable { ID = 12, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c13", camera_result = "OK", product_result = "OK", characteristic1 = 1.42f, characteristic2 = 1.45f, characteristic3 = 2.34f, characteristic4 = 1.45f },
                new DataTable { ID = 13, timestamp = DateTime.Now, product_name = "1model", part_number = "1", camera_position = "c14", camera_result = "OK", product_result = "OK", characteristic1 = 2.33f, characteristic2 = 1.36f, characteristic3 = 2.12f, characteristic4 = 2.45f },
                new DataTable { ID = 14, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c1", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.12f, characteristic3 = 2.67f, characteristic4 = 1.45f },
                new DataTable { ID = 15, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c2", camera_result = "OK", product_result = "OK", characteristic1 = 1.44f, characteristic2 = 0.045f, characteristic3 = 2.58f, characteristic4 = 1.45f },
                new DataTable { ID = 16, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c3", camera_result = "OK", product_result = "OK", characteristic1 = 2.56f, characteristic2 = 0.22f, characteristic3 = 2.69f, characteristic4 = 0.68f },
                new DataTable { ID = 17, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c4", camera_result = "OK", product_result = "OK", characteristic1 = 1.50f, characteristic2 = 0.34f, characteristic3 = 2.31f, characteristic4 = 0.59f },
                new DataTable { ID = 18, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c5", camera_result = "OK", product_result = "OK", characteristic1 = 1.40f, characteristic2 = 1.45f, characteristic3 = 2.28f, characteristic4 = 0.97f },
                new DataTable { ID = 19, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c6", camera_result = "OK", product_result = "OK", characteristic1 = 1.25f, characteristic2 = 2.45f, characteristic3 = 2.19f, characteristic4 = 0.59f },
                new DataTable { ID = 20, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c7", camera_result = "OK", product_result = "OK", characteristic1 = 2.11f, characteristic2 = 3.45f, characteristic3 = 2.34f, characteristic4 = 0.43f },
                new DataTable { ID = 21, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c8", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.65f, characteristic3 = 2.29f, characteristic4 = 0.28f },
                new DataTable { ID = 22, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c9", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.22f, characteristic3 = 2.42f, characteristic4 = 0.16f },
                new DataTable { ID = 23, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c10", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.33f, characteristic3 = 1.20f, characteristic4 = 0.43f },
                new DataTable { ID = 24, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c11", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 0.66f, characteristic3 = 0.20f, characteristic4 = 0.15f },
                new DataTable { ID = 25, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c12", camera_result = "OK", product_result = "OK", characteristic1 = 1.44f, characteristic2 = 2.58f, characteristic3 = 2.85f, characteristic4 = 0.34f },
                new DataTable { ID = 26, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c13", camera_result = "OK", product_result = "OK", characteristic1 = 0.32f, characteristic2 = 1.43f, characteristic3 = 2.12f, characteristic4 = 0.36f },
                new DataTable { ID = 27, timestamp = DateTime.Now, product_name = "1model", part_number = "2", camera_position = "c14", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 2.46f, characteristic3 = 1.80f, characteristic4 = 0.17f },

                new DataTable { ID = 1, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c1", camera_result = "OK", product_result = "OK", characteristic1 = 2.23f, characteristic2 = 0.45f, characteristic3 = 2.20f, characteristic4 = 0.45f },
                new DataTable { ID = 1, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c2", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.22f, characteristic3 = 2.20f, characteristic4 = 0.35f },
                new DataTable { ID = 2, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c3", camera_result = "OK", product_result = "OK", characteristic1 = 3.23f, characteristic2 = 0.13f, characteristic3 = 2.0f, characteristic4 = 0.25f },
                new DataTable { ID = 3, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c4", camera_result = "OK", product_result = "OK", characteristic1 = 2.43f, characteristic2 = 1.45f, characteristic3 = 2.25f, characteristic4 = 0.41f },
                new DataTable { ID = 4, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c5", camera_result = "OK", product_result = "OK", characteristic1 = 2.53f, characteristic2 = 2.45f, characteristic3 = 1.20f, characteristic4 = 0.21f },
                new DataTable { ID = 5, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c6", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 1.45f, characteristic3 = 0.20f, characteristic4 = 0.34f },
                new DataTable { ID = 6, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c7", camera_result = "OK", product_result = "OK", characteristic1 = 2f, characteristic2 = 0.25f, characteristic3 = 1.20f, characteristic4 = 0.36f },
                new DataTable { ID = 7, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c8", camera_result = "OK", product_result = "OK", characteristic1 = 2.3f, characteristic2 = 0.67f, characteristic3 = 2.430f, characteristic4 = 0.423f },
                new DataTable { ID = 8, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c9", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.82f, characteristic3 = 1.20f, characteristic4 = 1.45f },
                new DataTable { ID = 9, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c10", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 1.85f, characteristic3 = 2.83f, characteristic4 = 2.45f },
                new DataTable { ID = 10, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c11", camera_result = "OK", product_result = "OK", characteristic1 = 1.03f, characteristic2 = 2.45f, characteristic3 = 2.49f, characteristic4 = 0.345f },
                new DataTable { ID = 11, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c12", camera_result = "OK", product_result = "OK", characteristic1 = 2.03f, characteristic2 = 1.45f, characteristic3 = 2.56f, characteristic4 = 2.245f },
                new DataTable { ID = 12, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c13", camera_result = "OK", product_result = "OK", characteristic1 = 1.42f, characteristic2 = 1.45f, characteristic3 = 2.34f, characteristic4 = 1.45f },
                new DataTable { ID = 13, timestamp = DateTime.Now, product_name = "2model", part_number = "1", camera_position = "c14", camera_result = "OK", product_result = "OK", characteristic1 = 2.33f, characteristic2 = 1.36f, characteristic3 = 2.12f, characteristic4 = 2.45f },
                new DataTable { ID = 14, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c1", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.12f, characteristic3 = 2.67f, characteristic4 = 1.45f },
                new DataTable { ID = 15, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c2", camera_result = "OK", product_result = "OK", characteristic1 = 1.44f, characteristic2 = 0.045f, characteristic3 = 2.58f, characteristic4 = 1.45f },
                new DataTable { ID = 16, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c3", camera_result = "OK", product_result = "OK", characteristic1 = 2.56f, characteristic2 = 0.22f, characteristic3 = 2.69f, characteristic4 = 0.68f },
                new DataTable { ID = 17, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c4", camera_result = "OK", product_result = "OK", characteristic1 = 1.50f, characteristic2 = 0.34f, characteristic3 = 2.31f, characteristic4 = 0.59f },
                new DataTable { ID = 18, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c5", camera_result = "OK", product_result = "OK", characteristic1 = 1.40f, characteristic2 = 1.45f, characteristic3 = 2.28f, characteristic4 = 0.97f },
                new DataTable { ID = 19, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c6", camera_result = "OK", product_result = "OK", characteristic1 = 1.25f, characteristic2 = 2.45f, characteristic3 = 2.19f, characteristic4 = 0.59f },
                new DataTable { ID = 20, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c7", camera_result = "OK", product_result = "OK", characteristic1 = 2.11f, characteristic2 = 3.45f, characteristic3 = 2.34f, characteristic4 = 0.43f },
                new DataTable { ID = 21, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c8", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.65f, characteristic3 = 2.29f, characteristic4 = 0.28f },
                new DataTable { ID = 22, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c9", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.22f, characteristic3 = 2.42f, characteristic4 = 0.16f },
                new DataTable { ID = 23, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c10", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.33f, characteristic3 = 1.20f, characteristic4 = 0.43f },
                new DataTable { ID = 24, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c11", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 0.66f, characteristic3 = 0.20f, characteristic4 = 0.15f },
                new DataTable { ID = 25, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c12", camera_result = "OK", product_result = "OK", characteristic1 = 1.44f, characteristic2 = 2.58f, characteristic3 = 2.85f, characteristic4 = 0.34f },
                new DataTable { ID = 26, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c13", camera_result = "OK", product_result = "OK", characteristic1 = 0.32f, characteristic2 = 1.43f, characteristic3 = 2.12f, characteristic4 = 0.36f },
                new DataTable { ID = 27, timestamp = DateTime.Now, product_name = "2model", part_number = "2", camera_position = "c14", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 2.46f, characteristic3 = 1.80f, characteristic4 = 0.17f },

                new DataTable { ID = 1, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c1", camera_result = "OK", product_result = "OK", characteristic1 = 2.23f, characteristic2 = 0.45f, characteristic3 = 2.20f, characteristic4 = 0.45f },
                new DataTable { ID = 1, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c2", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.22f, characteristic3 = 2.20f, characteristic4 = 0.35f },
                new DataTable { ID = 2, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c3", camera_result = "OK", product_result = "OK", characteristic1 = 3.23f, characteristic2 = 0.13f, characteristic3 = 2.0f, characteristic4 = 0.25f },
                new DataTable { ID = 3, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c4", camera_result = "OK", product_result = "OK", characteristic1 = 2.43f, characteristic2 = 1.45f, characteristic3 = 2.25f, characteristic4 = 0.41f },
                new DataTable { ID = 4, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c5", camera_result = "OK", product_result = "OK", characteristic1 = 2.53f, characteristic2 = 2.45f, characteristic3 = 1.20f, characteristic4 = 0.21f },
                new DataTable { ID = 5, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c6", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 1.45f, characteristic3 = 0.20f, characteristic4 = 0.34f },
                new DataTable { ID = 6, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c7", camera_result = "OK", product_result = "OK", characteristic1 = 2f, characteristic2 = 0.25f, characteristic3 = 1.20f, characteristic4 = 0.36f },
                new DataTable { ID = 7, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c8", camera_result = "OK", product_result = "OK", characteristic1 = 2.3f, characteristic2 = 0.67f, characteristic3 = 2.430f, characteristic4 = 0.423f },
                new DataTable { ID = 8, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c9", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.82f, characteristic3 = 1.20f, characteristic4 = 1.45f },
                new DataTable { ID = 9, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c10", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 1.85f, characteristic3 = 2.83f, characteristic4 = 2.45f },
                new DataTable { ID = 10, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c11", camera_result = "OK", product_result = "OK", characteristic1 = 1.03f, characteristic2 = 2.45f, characteristic3 = 2.49f, characteristic4 = 0.345f },
                new DataTable { ID = 11, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c12", camera_result = "OK", product_result = "OK", characteristic1 = 2.03f, characteristic2 = 1.45f, characteristic3 = 2.56f, characteristic4 = 2.245f },
                new DataTable { ID = 12, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c13", camera_result = "OK", product_result = "OK", characteristic1 = 1.42f, characteristic2 = 1.45f, characteristic3 = 2.34f, characteristic4 = 1.45f },
                new DataTable { ID = 13, timestamp = DateTime.Now, product_name = "3model", part_number = "1", camera_position = "c14", camera_result = "OK", product_result = "OK", characteristic1 = 2.33f, characteristic2 = 1.36f, characteristic3 = 2.12f, characteristic4 = 2.45f },
                new DataTable { ID = 14, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c1", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.12f, characteristic3 = 2.67f, characteristic4 = 1.45f },
                new DataTable { ID = 15, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c2", camera_result = "OK", product_result = "OK", characteristic1 = 1.44f, characteristic2 = 0.045f, characteristic3 = 2.58f, characteristic4 = 1.45f },
                new DataTable { ID = 16, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c3", camera_result = "OK", product_result = "OK", characteristic1 = 2.56f, characteristic2 = 0.22f, characteristic3 = 2.69f, characteristic4 = 0.68f },
                new DataTable { ID = 17, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c4", camera_result = "OK", product_result = "OK", characteristic1 = 1.50f, characteristic2 = 0.34f, characteristic3 = 2.31f, characteristic4 = 0.59f },
                new DataTable { ID = 18, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c5", camera_result = "OK", product_result = "OK", characteristic1 = 1.40f, characteristic2 = 1.45f, characteristic3 = 2.28f, characteristic4 = 0.97f },
                new DataTable { ID = 19, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c6", camera_result = "OK", product_result = "OK", characteristic1 = 1.25f, characteristic2 = 2.45f, characteristic3 = 2.19f, characteristic4 = 0.59f },
                new DataTable { ID = 20, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c7", camera_result = "OK", product_result = "OK", characteristic1 = 2.11f, characteristic2 = 3.45f, characteristic3 = 2.34f, characteristic4 = 0.43f },
                new DataTable { ID = 21, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c8", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.65f, characteristic3 = 2.29f, characteristic4 = 0.28f },
                new DataTable { ID = 22, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c9", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.22f, characteristic3 = 2.42f, characteristic4 = 0.16f },
                new DataTable { ID = 23, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c10", camera_result = "OK", product_result = "OK", characteristic1 = 1.23f, characteristic2 = 0.33f, characteristic3 = 1.20f, characteristic4 = 0.43f },
                new DataTable { ID = 24, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c11", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 0.66f, characteristic3 = 0.20f, characteristic4 = 0.15f },
                new DataTable { ID = 25, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c12", camera_result = "OK", product_result = "OK", characteristic1 = 1.44f, characteristic2 = 2.58f, characteristic3 = 2.85f, characteristic4 = 0.34f },
                new DataTable { ID = 26, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c13", camera_result = "OK", product_result = "OK", characteristic1 = 0.32f, characteristic2 = 1.43f, characteristic3 = 2.12f, characteristic4 = 0.36f },
                new DataTable { ID = 27, timestamp = DateTime.Now, product_name = "3model", part_number = "2", camera_position = "c14", camera_result = "OK", product_result = "OK", characteristic1 = 0.23f, characteristic2 = 2.46f, characteristic3 = 1.80f, characteristic4 = 0.17f },
           };
    }
    }
}
