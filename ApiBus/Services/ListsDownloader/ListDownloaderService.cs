using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiBus.Models.ListsDownloader
{
    public class ListDownloaderService : IListDownloaderService
    {
        public ApiModel GetListsOfBusses(int busNumber, int FromstationNumber)
        {
            WebClient client = new WebClient();
            ApiModel model = new ApiModel();
            var pageContent = client.DownloadString(@"https://mzkwejherowo.pl/rozklad-jazdy/2-421-02.html");
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageContent);
            List<string> Times = new List<string>();
            List<string> Stations = new List<string>();
            string innerText;
            List<string> Titles = new List<string>();
            List<string> listOfTimes_Total = new List<string>();
            int position = 0;
            //getting data from html
            //zapisuje nazwy przystanków i czasy w ile się na nie dojedzie z obecnego przystanku do dwóch osobnych list, dalej w kodzie jest łączenie tego
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
            //pobranie godzin przy użyciu regexa (ze znakiem legendy jeśli jest)
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
                            listOfTimes_Total.Add(match.ToString());
                        }
                    }

                }
            }
            string currentNode = "";
            //pobranie i zapisanie tytułów poszczególnych list (Odjazdy w niedziele i święta itp)
            var allNodes = htmlDocument.DocumentNode.SelectNodes("//h4");
            try
            {
                for (int j = 0; j < allNodes.Count(); j++)
                {

                    currentNode = allNodes[j].InnerText.ToString().Trim();
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
            try
            {
                model.TitleOne = Titles[2];
                model.TitleTwo = Titles[3];
                model.TitleThree = Titles[4];
                model.TitleFour = Titles[5];
            }
            catch { }
            //o tym pisałem wcześniej
            for (int i = 0; i < Times.Count(); i++)
            {

                if (Times[i] != "")
                {
                    model.ListStationsAndTimes.Add(Times[i] + " " + Stations[i]);
                }
                else
                {
                    model.ListStationsAndTimes.Add("      " + Stations[i]);
                }
                if (Times[i].Count() > Times[i + 1].Count())
                {
                    break;
                }
            }

            List<string> listOfLegend_Total = new List<string>();
            
            //pobranie i zapisanie legendy
            foreach (var dl in htmlDocument.DocumentNode.SelectNodes("//dl"))
            {
                HtmlAttribute classAttribute = dl.Attributes["class"];

                foreach (var dt in htmlDocument.DocumentNode.SelectNodes("//dt"))
                {
                    innerText = dt.InnerText.ToString().Trim();
                    listOfLegend_Total.Add(innerText);
                }
                
                for (int i = 0; i < listOfLegend_Total.Count(); i++)
                {
                    try
                    {
                        var node = htmlDocument.DocumentNode.SelectNodes("//dd")[i];
                        listOfLegend_Total[i] += " " + node.InnerText.ToString().Trim();
                    }
                    catch
                    {
                        break;
                    }
                }

            }
            int countt = listOfLegend_Total.Count();
            countt = countt / 2;
            for (int i = 0; i < countt; i++)
            {
                model.ListLegend.Add(listOfLegend_Total[i]);
            }


            // ApiModel model = new ApiModel();


            var count = listOfTimes_Total.Count();
            //list one
            try
            {
                if (!(model.TitleOne is null))
                {
                    for (int i = 0; i < count; i++)
                    {
                        //jeżeli liczba po konwersji z godziny np 14:10 -> 1410 jest mniejsza od poprzedniej, to działa dalej
                        if (Convert.ToInt32("" + listOfTimes_Total[i][0] + listOfTimes_Total[i][1] + listOfTimes_Total[i][3] + listOfTimes_Total[i][4]) < Convert.ToInt32("" + listOfTimes_Total[i + 1][0] + listOfTimes_Total[i + 1][1] + listOfTimes_Total[i + 1][3] + listOfTimes_Total[i + 1][4]))
                        {
                            model.ListOne.Add(listOfTimes_Total[i]);

                        }
                        else
                        {
                            model.ListOne.Add(listOfTimes_Total[i]);
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
                if (!(model.TitleTwo is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        if (Convert.ToInt32("" + listOfTimes_Total[i][0] + listOfTimes_Total[i][1] + listOfTimes_Total[i][3] + listOfTimes_Total[i][4]) < Convert.ToInt32("" + listOfTimes_Total[i + 1][0] + listOfTimes_Total[i + 1][1] + listOfTimes_Total[i + 1][3] + listOfTimes_Total[i + 1][4]))
                        {
                            model.ListTwo.Add(listOfTimes_Total[i]);

                        }
                        else
                        {
                            model.ListTwo.Add(listOfTimes_Total[i]);
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
                if (!(model.TitleThree is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        if (Convert.ToInt32("" + listOfTimes_Total[i][0] + listOfTimes_Total[i][1] + listOfTimes_Total[i][3] + listOfTimes_Total[i][4]) < Convert.ToInt32("" + listOfTimes_Total[i + 1][0] + listOfTimes_Total[i + 1][1] + listOfTimes_Total[i + 1][3] + listOfTimes_Total[i + 1][4]))
                        {
                            model.ListThree.Add(listOfTimes_Total[i]);

                        }
                        else
                        {
                            model.ListThree.Add(listOfTimes_Total[i]);
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
                if (!(model.TitleFour is null))
                {
                    for (int i = position; i < count; i++)
                    {
                        if (Convert.ToInt32("" + listOfTimes_Total[i][0] + listOfTimes_Total[i][1] + listOfTimes_Total[i][3] + listOfTimes_Total[i][4]) < Convert.ToInt32("" + listOfTimes_Total[i + 1][0] + listOfTimes_Total[i + 1][1] + listOfTimes_Total[i + 1][3] + listOfTimes_Total[i + 1][4]))
                        {
                            model.ListFour.Add(listOfTimes_Total[i]);

                        }
                        else
                        {
                            model.ListFour.Add(listOfTimes_Total[i]);
                            position = i + 1;
                            break;

                        }

                    }
                }
            }
            catch { }
            return model;

        }
    }
}
