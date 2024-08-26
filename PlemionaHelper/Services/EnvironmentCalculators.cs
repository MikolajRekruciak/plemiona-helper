using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlemionaHelper.Services
{
    public static class EnvironmentCalculators
    {
        public static int ObliczAktualnyStanZasobu(int poziomKopalni,
            DateTime dataOstatniegoAtaku,
            int stanZasobuPoAtaku)
        {
            if (dataOstatniegoAtaku > DateTime.Now)
                return 0;

            TimeSpan timeSpan = DateTime.Now - dataOstatniegoAtaku;
            return stanZasobuPoAtaku +
                (int)(timeSpan.TotalHours * KopalniaLvlToGodzinneWydobycie(poziomKopalni));
        }

        public static int KopalniaLvlToGodzinneWydobycie(int poziomKopalni)
        {
            int[] wydobycie = { 6, // 1
                30, 35, 41, 47, 55, 64, 74, 86, 100, 117, 136, 158, 184, 214, 249, 289, //16
                337, 391, 455, 530, 616, 717, 833, 969, 1127, 1311, 1525, 1774, 2063, 2400 //14
            };

            if (poziomKopalni >= 0 && poziomKopalni <= wydobycie.Length + 1)
            {
                return wydobycie[poziomKopalni];
            }
            else
            {
                throw new ArgumentOutOfRangeException("poziomKopalni", "Invalid mine level");
            }
        }

        public static int SchowekLvlToSchowekPojemnosc(int poziomSchowka)
        {
            int[] zakresChowania =
            {
                150
                , 200
                , 267
                , 356
                , 474
                , 632
                , 843
                , 1125
                , 1500
                , 2000
            };

            if (poziomSchowka >= 1 && poziomSchowka <= zakresChowania.Length)
            {
                return zakresChowania[poziomSchowka - 1];
            }
            else
            {
                throw new ArgumentOutOfRangeException("zakresChowania", "Invalid schowek level");
            }
        }

        public static int SpichlerzLvlToSpichlerzPojemnosc(int spichlerzLvl)
        {
            int[] pojemnosci =
            {
                1000
                , 1229
                , 1512
                , 1859
                , 2285
                , 2810
                , 3454
                , 4247
                , 5222
                , 6420
                , 7893
                , 9705
                , 11932
                , 14670
                , 18037
                , 22177
                , 27266
                , 33523
                , 41217
                , 50675
                , 62305
                , 76604
                , 94184
                , 115798
                , 142373
                , 175047
                , 215219
                , 264611
                , 325337
                , 400000
            };

            if (spichlerzLvl >= 1 && spichlerzLvl <= pojemnosci.Length)
            {
                return pojemnosci[spichlerzLvl - 1];
            }
            else
            {
                throw new ArgumentOutOfRangeException("pojemnosci", "Invalid pojemnosci level");
            }
        }
    }
}
