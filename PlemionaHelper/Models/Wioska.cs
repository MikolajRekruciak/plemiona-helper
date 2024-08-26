using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlemionaHelper
{
    public class Wioska
    {
        public int Id { get; set; }

        public int Kontynent { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public string NazwaWioski { get; set; }

        public string NazwaGracza { get; set; }

        public int PoziomSpichlerza { get; set; }
        public int PoziomSchowka { get; set; }

        public int PoziomTartak { get; set; }
        public int PoziomGlina { get; set; }
        public int PoziomZelazo { get; set; }

        public int PoziomMur { get; set; }

        public int HorsesAssigned { get; set; }

        public decimal Odleglosc { get; set; }


        public Atak OstatniAtak { get; set; }

        //public Atak NadchodzacyAtak { get; set; }
        public DateTime? DataWyslaniaOstatniegoAtaku { get; set; }
    }
}
