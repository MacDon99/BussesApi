using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Services.BusChoser
{
    public interface IBusChoser
    {
        string GetSiteString(int busNumber,int direction, int stationNumber);
    }
}
