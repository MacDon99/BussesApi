using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Models
{
    public class Bus
    {
        public int Number { get; set; }
        public List<string> StationsFirstDirection { get; set; }
        public List<string> StationsSecondDirection { get; set; }

        public Bus()
        {
            StationsFirstDirection = new List<string>();
            StationsSecondDirection = new List<string>();
        }

    }
}
