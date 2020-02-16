using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Models
{
    public class ApiModel
    {

        public string TitleOne { get; set; }
        public List<string> ListOne { get; set; }
        public string TitleTwo { get; set; }
        public List<string> ListTwo { get; set; }
        public string TitleThree { get; set; }
        public List<string> ListThree { get; set; }
        public string TitleFour { get; set; }
        public List<string> ListFour { get; set; }
        public List<string> ListLegend { get; set; }
        public List<string> ListStationsAndTimes { get; set; }

        public ApiModel()
        {
            ListOne = new List<string>();
            ListTwo = new List<string>();
            ListThree = new List<string>();
            ListFour = new List<string>();
            ListLegend = new List<string>();
            ListStationsAndTimes = new List<string>();
        }
    }
        
}
