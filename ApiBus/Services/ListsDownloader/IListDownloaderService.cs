using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Models.ListsDownloader
{
    public interface IListDownloaderService
    {
        ApiModel GetListsOfBusses(int busNumber, int FromstationNumber);
    }
}
