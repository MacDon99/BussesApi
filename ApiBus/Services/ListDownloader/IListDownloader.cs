using ApiBus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiBus.Services.ListsDownloader
{
    public interface IListDownloader
    {
        ReceivedLists GetListsOfBusses(string site);
    }
}
