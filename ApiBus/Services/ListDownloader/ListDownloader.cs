using ApiBus.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiBus.Services.ListsDownloader
{
    public class ListDownloader : IListDownloader
    {
        public ReceivedLists GetListsOfBusses(string site)
        {
            var client = new WebClient();
            var model = new ReceivedLists();
            var htmlDocument = new HtmlDocument();
            try
            {
                var pageContent = client.DownloadString(@site);
                htmlDocument.LoadHtml(pageContent);
            }
            catch
            {
                return model;
            }
            
            //gets all available lists of periods and times that busses are travelling
            model.ListsOfBusses = GetListsWithTitles(htmlDocument);
            //gets lists of names and times to travel to stations that are farer than a chosen one
            model.StationsAndTimesToTravel = GetListOfStationsAndTimesToTravel(htmlDocument);
            //gets a list of Legend
            model.Legend = GetLegend(htmlDocument);

            return model;

        }

        private List<string> GetListOfStationsAndTimesToTravel(HtmlDocument htmlDocument)
        {
            string innerText;
            var Times = new List<string>();
            var Stations = new List<string>();
            var ListOfStationsAndTimesToGet = new List<string>();

            foreach (HtmlNode td in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                HtmlAttribute classAttribute = td.Attributes["class"];

                if (classAttribute != null)
                {
                    if (classAttribute.Value == "czas")
                    {
                        innerText = td.InnerText.ToString().Trim();
                        Times.Add(innerText);
                    }
                    if (classAttribute.Value == "przyst")
                    {
                        innerText = td.InnerText.ToString().Trim();
                        Stations.Add(innerText);
                    }
                }
            }
            Times.Add("");

            ListOfStationsAndTimesToGet = GetValidList(Times, Stations);
            return ListOfStationsAndTimesToGet;
        }
        private List<string> GetValidList(List<string> Times, List<string> Stations )
        {
            var ListOfStationsAndTimesToGet = new List<string>();
            for (int i = 0; i < Times.Count(); i++)
            {

                if (Times[i] != "")
                {
                    ListOfStationsAndTimesToGet.Add(Times[i] + " " + Stations[i]);
                }
                else
                {
                    ListOfStationsAndTimesToGet.Add("      " + Stations[i]);
                }
                if (Times[i] != "" && (Times[i].Count() > Times[i + 1].Count() || Convert.ToInt32("" + Times[i+1][0] + Times[i + 1][1] + Times[i + 1][3] + Times[i + 1][4]) < Convert.ToInt32("" + Times[i][0] + Times[i][1] + Times[i][3] + Times[i][4])) )
                {
                    break;
                }
            }
        return ListOfStationsAndTimesToGet;
        }
        private List<string> GetTotalListOfTimes(HtmlDocument htmlDocument)
        {
            string innerText;
            var TotalListOfTimes = new List<string>();
            foreach (HtmlNode td in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                HtmlAttribute classAttribute = td.Attributes["class"];

                if (classAttribute != null)
                {
                    if (classAttribute.Value == "godz")
                    {
                        innerText = td.InnerText.ToString();
                        var search = @"(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9](\s[A-Z])?";
                        var rgx = new Regex(search);

                        foreach (Match match in rgx.Matches(innerText))
                        {
                            TotalListOfTimes.Add(match.ToString());
                        }
                    }

                }
            }
            return TotalListOfTimes;
        }

        private List<string> GetListsOfTitles(HtmlDocument htmlDocument)
        {
            var allNodes = htmlDocument.DocumentNode.SelectNodes("//h4");
            var Titles = new List<string>();
            string innerText;
            try
            {
                for (int j = 0; j < allNodes.Count(); j++)
                {

                    var currentNode = allNodes[j].InnerText.ToString().Trim();
                    if (currentNode != "Czas" || j == 0)
                    {
                        Titles.Add(innerText = allNodes[j].InnerText.ToString().Trim());
                    }
                    if (Titles[0] == currentNode && j != 0)
                    {
                        break;
                    }

                }
            }
            catch { }
            return Titles;
        }

        private void AddListsToTitles(ref List<List> ListOfBussesAndEverything, List<string> TotaLList, ref int position, int CurrentList)
        {
            //var ListOfBussesAndEverything = new List();
            var count = TotaLList.Count();
            //int position = 0;
            if (!(ListOfBussesAndEverything[CurrentList].Period is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        //jeżeli liczba po konwersji z godziny np 14:10 -> 1410 jest mniejsza od poprzedniej, to działa dalej
                        if (Convert.ToInt32("" + TotaLList[i][0] + TotaLList[i][1] + TotaLList[i][3] + TotaLList[i][4]) < Convert.ToInt32("" + TotaLList[i + 1][0] + TotaLList[i + 1][1] + TotaLList[i + 1][3] + TotaLList[i + 1][4]))
                        {
                            ListOfBussesAndEverything[CurrentList].ListOfBussesInChosenPeriod.Add(TotaLList[i]);
                            // List[0].Add(TotaLList[i]);

                        }
                        else
                        {
                            ListOfBussesAndEverything[CurrentList].ListOfBussesInChosenPeriod.Add(TotaLList[i]);
                            position = i + 1;
                            break;

                        }

                    }
                }
        }
        private List<List> GetListsWithTitles(HtmlDocument htmlDocument)
        {
            var ListOfBussesAndEverything = new List<List>();
            var TotaLList = GetTotalListOfTimes(htmlDocument);
            var Titles = GetListsOfTitles(htmlDocument);
            var Lista = new List();
            int position = 0;
            var count = TotaLList.Count();

            try
            {
                //list one
                Lista.Period = Titles[2];
                ListOfBussesAndEverything.Add(Lista);
                AddListsToTitles(ref ListOfBussesAndEverything, TotaLList, ref position, 0);
                //list two
                Lista = new List();
                Lista.Period = Titles[3];
                ListOfBussesAndEverything.Add(Lista);
                AddListsToTitles(ref ListOfBussesAndEverything, TotaLList, ref position, 1);
                //list three
                Lista = new List();
                Lista.Period = Titles[4];
                ListOfBussesAndEverything.Add(Lista);
                AddListsToTitles(ref ListOfBussesAndEverything, TotaLList, ref position, 2);
                //list four
                Lista = new List();
                Lista.Period = Titles[5];
                ListOfBussesAndEverything.Add(Lista);
                AddListsToTitles(ref ListOfBussesAndEverything, TotaLList, ref position, 3);
            }
            catch { }
            return ListOfBussesAndEverything;
        }

        private List<string> GetLegend(HtmlDocument htmlDocument)
        {
            var Legend = new List<string>();
            string innerText;
            var NodeDL = htmlDocument.DocumentNode.SelectNodes("//dl");
            try
            {
                var inner = NodeDL[0].InnerHtml;
                var doc = new HtmlDocument();
                doc.LoadHtml(inner);

                foreach (HtmlNode dt in doc.DocumentNode.SelectNodes("//dt"))
                {
                    innerText = dt.InnerText.ToString().Trim();
                    Legend.Add(innerText);
                }

                for (int i = 0; i < Legend.Count(); i++)
                {
                        var node = doc.DocumentNode.SelectNodes("//dd")[i];
                        Legend[i] += " " + node.InnerText.ToString().Trim();
                }
            }
            catch { }
            return Legend;
        }
    }
}
