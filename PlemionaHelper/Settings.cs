using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace PlemionaHelper
{
    public static class Settings
    {
        public static int Wioska_X => Convert.ToInt32(
            ConfigurationManager.AppSettings["Wioska_X"]);

        public static int Wioska_Y => Convert.ToInt32(
            ConfigurationManager.AppSettings["Wioska_Y"]);

        public static int Wioska_Id => Convert.ToInt32(
            ConfigurationManager.AppSettings["Wioska_Id"]);

        // 698 | 475 = 3.6
        // 
        public static int Flaga_Ladownosc => Convert.ToInt32(
            ConfigurationManager.AppSettings["Flaga_Ladownosc"]);
    }
}