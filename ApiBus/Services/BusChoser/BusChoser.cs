using ApiBus.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Services.BusChoser
{
    public class BusChoser : IBusChoser
    {
        private readonly BussesRepository _repository = new BussesRepository();

        public string GetSiteString(int busNumber,int direction, int stationNumber)
        {
            var bus = _repository.Busses.FirstOrDefault(s => s.Number == busNumber);
            string siteString;
            try
            {
                
                switch (direction)
                {
                    case 0: siteString = bus.StationsFirstDirection[stationNumber - 1];
                        return siteString;
                    case 1: siteString = bus.StationsSecondDirection[stationNumber - 1];
                        return siteString;
                    default: siteString = "";
                        break;
                }
                
            }
            catch { }
            return "";
        }
    }
}
