using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiBus.Models;
using ApiBus.Models.ListsDownloader;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApiBus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IListDownloaderService _listService;
        public ValuesController(IListDownloaderService service)
        {
            _listService = service;
        }

        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {

            
           // Console.ReadKey();
            var result = new JsonResult(_listService.GetListsOfBusses(1,1));

          //  var xd = JsonConvert.SerializeObject(model);
            //var p = Convert.
            return result;
            // return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
