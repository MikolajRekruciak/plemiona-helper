using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlemionaHelper.Services
{
    public static class IO_Service
    {
        static string _fileName = "wioski.json";
        static string _directory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            "Plemiona helper");
        static string _filePath = Path.Combine(_directory, _fileName);

        public static List<Wioska> GetSavedState()
        {
            if (!File.Exists(_filePath))
                return new List<Wioska>();

            return JsonConvert.DeserializeObject<List<Wioska>>(
                File.ReadAllText(_filePath));
        }

        public static void SaveCurrentState(List<Wioska> wioski)
        {
            if (!Directory.Exists(_directory))
                Directory.CreateDirectory(_directory);

            File.WriteAllText(_filePath, JsonConvert.SerializeObject(wioski));
        }
    }
}
