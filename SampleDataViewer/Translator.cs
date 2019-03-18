using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTableDataViewer
{
    public static class Translator
    {
        public static string TranslateColumnName(string columnName)
        {
            switch (columnName)
            {
                case "data":
                    return "Data";
                case "czas":
                    return "Czas";
                case "product_name":
                    return "Model";
                case "number":
                    return "Numer";
                case "camera_position":
                    return "Kamera";
                case "camera_result":
                    return "Wynik kamery";
                case "part_result":
                    return "Wynik czesci";
                case "characteristic1":
                    return "Pierwsza charakterystyka";
                case "characteristic2":
                    return "Druga charakterystyka";
                case "characteristic3":
                    return "Trzecia charakterystyka";
                case "characteristic4":
                    return "Czwarta charakterystyka";
            }

            return null;
        }
    }
}
