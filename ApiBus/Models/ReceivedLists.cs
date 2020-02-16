using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Models
{
    public class ReceivedLists
    {
        public List<List> ListsOfBusses { get; set; }
        public List<string> Legend { get; set; }
        public List<string> StationsAndTimesToTravel { get; set; }

        public ReceivedLists()
        {
            Legend = new List<string>();
            StationsAndTimesToTravel = new List<string>();
        }
    }
        
}
