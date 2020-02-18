using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiBus.Models;
using ApiBus.Services.BusChoser;
using ApiBus.Services.ListsDownloader;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IListDownloader _listService;
        private readonly IBusChoser _busChoser;

        public ValuesController(IListDownloader service, IBusChoser busChoser)
        {
            _listService = service;
            _busChoser = busChoser;
        }

        // GET api/values
        [HttpGet("{busNumber}/{direction}/{stationNumber}")]
        public JsonResult Get(int busNumber, int direction, int stationNumber)
        {
            var result = new JsonResult(_listService.GetListsOfBusses(_busChoser.GetSiteString(busNumber, direction ,stationNumber)));
            return result;
        }
    }
}
