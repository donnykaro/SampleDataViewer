using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataTableDataViewer
{
    public static class BoundaryCheck
    {
        public static float ReturnBoundary(string model, string characteristic_name, string cam_pos)
        {
            //string correct_model_name = model.Substring(0, 14);

            int camera_pos = GetCorrectCameraNumber(cam_pos);
            int camera_pos_for_boundary_check = 0;
            // pelny kod bylby dluzszy, dla potrzeb dema zostal skrocony
            switch (camera_pos)
            {
                case 3:
                    camera_pos_for_boundary_check = 1;
                    break;
                case 4:
                    camera_pos_for_boundary_check = 2;
                    break;
                case 5:
                    camera_pos_for_boundary_check = 1;
                    break;
                case 6:
                    camera_pos_for_boundary_check = 2;
                    break;
                case 7:
                    camera_pos_for_boundary_check = 1;
                    break;
                case 8:
                    camera_pos_for_boundary_check = 2;
                    break;
                case 9:
                    camera_pos_for_boundary_check = 1;
                    break;
                case 10:
                    camera_pos_for_boundary_check = 2;
                    break;
                case 11:
                    camera_pos_for_boundary_check = 1;
                    break;
                case 12:
                    camera_pos_for_boundary_check = 2;
                    break;
                case 13:
                    camera_pos_for_boundary_check = 1;
                    break;
                case 14:
                    camera_pos_for_boundary_check = 2;
                    break;
            }

            string[] modelsWithAvaiableBoundary =  { "1model", "2model", "3model" };

            switch (camera_pos_for_boundary_check)
            {
                case 1:
                    if (modelsWithAvaiableBoundary.Contains(model))
                        if (characteristic_name == "characteristic1" || characteristic_name == "characteristic2")
                            return 5.2f;
                        else
                            return 4f;
                    break;

                case 2:
                    if (modelsWithAvaiableBoundary.Contains(model))
                        if (characteristic_name == "characteristic1" || characteristic_name == "characteristic2")
                            return 4.6f;
                        else
                            return 3.2f;
                    break;
            }
            return 0;
        }

        //metoda tworzaca stringa do tooltipa
        public static string ViewBoundary(int column_index, string model, string cam_pos)
        {
            string boundary = "";
            
            switch (column_index)
            {
                case 7:
                    boundary = "      Zakres:  0  do " + ReturnBoundary(model, "characteristic1", cam_pos);
                    break;
                case 8:
                    boundary = "      Zakres:  0  do " + ReturnBoundary(model, "characteristic2", cam_pos);
                    break;
                case 9:
                    boundary = "      Zakres:  0  do " + ReturnBoundary(model, "characteristic3", cam_pos);
                    break;
                case 10:
                    boundary = "      Zakres:  0  do " + ReturnBoundary(model, "characteristic4", cam_pos);
                    break;
            }

            return boundary;
        }

        public static bool CheckBoundary(string model, string characteristic_name, string cam_pos, float? value)
        {
            float x = ReturnBoundary(model, characteristic_name, cam_pos);
            if (value > ReturnBoundary(model, characteristic_name, cam_pos))
                return false;
            return true;
        }

        private static int GetCorrectCameraNumber(string cam_pos)
        {
            int camera_pos = 0;
            if (cam_pos != "Wszystkie")
            {
                string s = "";
                if (cam_pos.Length > 2)
                    camera_pos = Int32.Parse(cam_pos.Substring(1, 2));
                else
                    camera_pos = Int32.Parse(cam_pos.Substring(1, 1));
            }

            return camera_pos;
        }
    }
}
