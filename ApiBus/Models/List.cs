using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Models
{
    public class List
    {
        public string Period { get; set; }
        public List<string> ListOfBussesInChosenPeriod { get; set; }

        public List()
        {
            ListOfBussesInChosenPeriod = new List<string>();
        }
    }
}
