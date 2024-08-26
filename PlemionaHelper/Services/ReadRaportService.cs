using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace PlemionaHelper.Services
{
    public static class ReadRaportService
    {
        static (string NazwaWioski, int X, int Y, int Kontynent) ExtractData(string input)
        {
            string pattern = @"(szpieguje|atakuje) (?<NazwaWioski>.+?) \((?<X>\d+)\|(?<Y>\d+)\) K(?<Kontynent>\d+)";
            var match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string nazwaWioski = match.Groups["NazwaWioski"].Value;
                int x = int.Parse(match.Groups["X"].Value);
                int y = int.Parse(match.Groups["Y"].Value);
                int kontynent = int.Parse(match.Groups["Kontynent"].Value);

                return (nazwaWioski, x, y, kontynent);
            }

            throw new ArgumentException("Input string does not match the expected format.");
        }


        private static Wioska Read_NoSpacingSection(
            Wioska wioska,
            HtmlDocument document)
        {
            var mainTable = document.DocumentNode.SelectSingleNode("//table[@class='no_spacing']");

            var h3_nodes = mainTable.SelectNodes("//h3");
            var h4_nodes = mainTable.SelectNodes("//h4");

            // h3_1
            var result = ExtractData(h3_nodes[0].InnerText);
            wioska.NazwaWioski = result.NazwaWioski;
            wioska.X = result.X;
            wioska.Y = result.Y;
            wioska.Kontynent = result.Kontynent;
            wioska.Odleglosc = Decimal.Round(
                (decimal)Math.Sqrt(
                Math.Pow(wioska.X - Settings.Wioska_X, 2) + Math.Pow(wioska.Y - Settings.Wioska_Y, 2))
                , 1, MidpointRounding.AwayFromZero);

            // h4_1
            wioska.OstatniAtak = new Atak
            {
                CzasAtaku = DateTime.ParseExact(h4_nodes[0].InnerText, "dd.MM.yy HH:mm:ss", CultureInfo.InvariantCulture)
            };


            // h3_2
            wioska.NazwaGracza = String.Join(" ", h3_nodes[1].InnerText.Split(' ').Skip(2));

            return wioska;
        }


        private static int GetSurowceAmount(HtmlNode node)
        {
            if (node == null)
                return 0;

            return Convert.ToInt32(node.InnerText.Replace(".", ""));
        }

        private static Wioska Read_WyszpiegowaneSurowce(
            Wioska wioska,
            HtmlDocument document)
        {
            var mainTable = document.DocumentNode.SelectSingleNode("//table[@id='attack_spy_resources']");

            var spans = mainTable.SelectNodes(".//span[@class='nowrap']");

            wioska.OstatniAtak.WyszpiegowaneDrewno = GetSurowceAmount(
                spans?.FirstOrDefault(q => q.InnerHtml.Contains("Drewno")));
            wioska.OstatniAtak.WyszpiegowanaGlina = GetSurowceAmount(
                spans?.FirstOrDefault(q => q.InnerHtml.Contains("Glina")));
            wioska.OstatniAtak.WyszpiegowaneZelazo = GetSurowceAmount(
                spans?.FirstOrDefault(q => q.InnerHtml.Contains("Żelazo")));

            return wioska;
        }

        private static int GetBuildingLvl(HtmlNode node)
        {
            if (node == null)
                return 0;

            return Convert.ToInt32(
                node.ChildNodes.Where(q => q.Name == "td").ToList()[1].InnerText.Trim());
        }

        private static Wioska Read_Buildings(
            Wioska wioska,
            HtmlDocument document)
        {
            var mainTableLeft = document.DocumentNode.SelectSingleNode("//table[@id='attack_spy_buildings_left']");
            var mainTableRight = document.DocumentNode.SelectSingleNode("//table[@id='attack_spy_buildings_right']");

            var tableNodes = mainTableLeft.ChildNodes.Where(q => q.Name == "tr").Concat(
                mainTableRight.ChildNodes.Where(q => q.Name == "tr")).ToList();

            wioska.PoziomTartak = GetBuildingLvl(
                tableNodes.FirstOrDefault(q => q.InnerHtml.Contains("Tartak")));

            wioska.PoziomGlina = GetBuildingLvl(
                tableNodes.FirstOrDefault(q => q.InnerHtml.Contains("Cegielnia")));

            wioska.PoziomZelazo = GetBuildingLvl(
                tableNodes.FirstOrDefault(q => q.InnerHtml.Contains("Huta żelaza")));

            wioska.PoziomSpichlerza = GetBuildingLvl(
                tableNodes.FirstOrDefault(q => q.InnerHtml.Contains("Spichlerz")));

            wioska.PoziomSchowka = GetBuildingLvl(
                tableNodes.FirstOrDefault(q => q.InnerHtml.Contains("Schowek")));

            wioska.PoziomMur = GetBuildingLvl(
                tableNodes.FirstOrDefault(q => q.InnerHtml.Contains("Mur")));

            return wioska;
        }


        public static Wioska ReadRaport(string url)
        {
            Wioska ret = new Wioska();

            var web = new HtmlWeb();
            var doc = web.Load(url);

            Read_NoSpacingSection(ret, doc);
            Read_WyszpiegowaneSurowce(ret, doc);
            Read_Buildings(ret, doc);

            return ret;
        }
    }
}
