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
            WebClient client = new WebClient();
            ReceivedLists model = new ReceivedLists();
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
            List<string> Times = new List<string>();
            List<string> Stations = new List<string>();
            List<string> ListOfStationsAndTimesToGet = new List<string>();

            foreach (var td in htmlDocument.DocumentNode.SelectNodes("//td"))
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
            List<string> TotalListOfTimes = new List<string>();
            foreach (var td in htmlDocument.DocumentNode.SelectNodes("//td"))
            {
                HtmlAttribute classAttribute = td.Attributes["class"];

                if (classAttribute != null)
                {
                    if (classAttribute.Value == "godz")
                    {
                        innerText = td.InnerText.ToString();
                        var search = @"(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9](\s[A-Z])?";
                        Regex rgx = new Regex(search);

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
            List<string> Titles = new List<string>();
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

        private List<List> GetListsWithTitles(HtmlDocument htmlDocument)
        {
            List<List> ListOfBussesAndEverything = new List<List>();
            List<string> TotaLList = GetTotalListOfTimes(htmlDocument);
            List<string> Titles = GetListsOfTitles(htmlDocument);
            List Lista = new List();
            int position = 0;
            var count = TotaLList.Count();

            try
            {
                Lista.Period = Titles[2];
                ListOfBussesAndEverything.Add(Lista);
                Lista = new List();
                Lista.Period = Titles[3];
                ListOfBussesAndEverything.Add(Lista);
                Lista = new List();
                Lista.Period = Titles[4];
                ListOfBussesAndEverything.Add(Lista);
                Lista = new List();
                Lista.Period = Titles[5];
                ListOfBussesAndEverything.Add(Lista);
            }
            catch { }
            //list one
            try
            {
                if (!(ListOfBussesAndEverything[0].Period is null))
                {
                    for (int i = 0; i < count; i++)
                    {
                        //jeżeli liczba po konwersji z godziny np 14:10 -> 1410 jest mniejsza od poprzedniej, to działa dalej
                        if (Convert.ToInt32("" + TotaLList[i][0] + TotaLList[i][1] + TotaLList[i][3] + TotaLList[i][4]) < Convert.ToInt32("" + TotaLList[i + 1][0] + TotaLList[i + 1][1] + TotaLList[i + 1][3] + TotaLList[i + 1][4]))
                        {
                            ListOfBussesAndEverything[0].ListOfBussesInChosenPeriod.Add(TotaLList[i]);
                            // List[0].Add(TotaLList[i]);

                        }
                        else
                        {
                            ListOfBussesAndEverything[0].ListOfBussesInChosenPeriod.Add(TotaLList[i]);
                            position = i + 1;
                            break;

                        }

                    }
                }
            }
            catch { }
            //list two
            try
            {
                if (!(ListOfBussesAndEverything[1].Period is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        if (Convert.ToInt32("" + TotaLList[i][0] + TotaLList[i][1] + TotaLList[i][3] + TotaLList[i][4]) < Convert.ToInt32("" + TotaLList[i + 1][0] + TotaLList[i + 1][1] + TotaLList[i + 1][3] + TotaLList[i + 1][4]))
                        {
                            ListOfBussesAndEverything[1].ListOfBussesInChosenPeriod.Add(TotaLList[i]);

                        }
                        else
                        {
                            ListOfBussesAndEverything[1].ListOfBussesInChosenPeriod.Add(TotaLList[i]);
                            position = i + 1;
                            break;

                        }

                    }
                }
            }
            catch { }
            //list three
            try
            {
                if (!(ListOfBussesAndEverything[0].Period is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        if (Convert.ToInt32("" + TotaLList[i][0] + TotaLList[i][1] + TotaLList[i][3] + TotaLList[i][4]) < Convert.ToInt32("" + TotaLList[i + 1][0] + TotaLList[i + 1][1] + TotaLList[i + 1][3] + TotaLList[i + 1][4]))
                        {
                            ListOfBussesAndEverything[2].ListOfBussesInChosenPeriod.Add(TotaLList[i]);

                        }
                        else
                        {
                            ListOfBussesAndEverything[2].ListOfBussesInChosenPeriod.Add(TotaLList[i]);
                            position = i + 1;
                            break;

                        }

                    }
                }
            }
            catch { }
            //list four
            try
            {
                if (!(ListOfBussesAndEverything[3].Period is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        if (Convert.ToInt32("" + TotaLList[i][0] + TotaLList[i][1] + TotaLList[i][3] + TotaLList[i][4]) < Convert.ToInt32("" + TotaLList[i + 1][0] + TotaLList[i + 1][1] + TotaLList[i + 1][3] + TotaLList[i + 1][4]))
                        {
                            ListOfBussesAndEverything[3].ListOfBussesInChosenPeriod.Add(TotaLList[i]);

                        }
                        else
                        {
                            ListOfBussesAndEverything[3].ListOfBussesInChosenPeriod.Add(TotaLList[i]);
                            position = i + 1;
                            break;

                        }

                    }
                }
            }
            catch { }
            return ListOfBussesAndEverything;
        }

        private List<string> GetLegend(HtmlDocument htmlDocument)
        {
            List<string> Legend = new List<string>();
            string innerText;
            var dl = htmlDocument.DocumentNode.SelectNodes("//dl");
            try
            {
                var inner = dl[0].InnerHtml;
                var doc = new HtmlDocument();
                doc.LoadHtml(inner);

                foreach (var dt in doc.DocumentNode.SelectNodes("//dt"))
                {
                    innerText = dt.InnerText.ToString().Trim();
                    Legend.Add(innerText);
                }

                for (int i = 0; i < Legend.Count(); i++)
                {
                    try
                    {
                        var node = doc.DocumentNode.SelectNodes("//dd")[i];
                        Legend[i] += " " + node.InnerText.ToString().Trim();
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            catch { }
            return Legend;
        }
    }
}
