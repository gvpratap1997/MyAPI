using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyAPI.BL;
using MyAPI.Models;

namespace MyAPI.Controllers
{
    [Route("api/Values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        SQL_Helper instance;
        public ValuesController()
        {
            instance = new SQL_Helper();
        }
        // GET api/values
        [HttpGet]
        public ActionResult<string>Get()
        {
            List<Resources> result = instance.LoadDb();
            return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            List<Resources> result = instance.LoadDbGetId(id);
            return Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented);
        }

        // POST api/values
        [HttpPost]
        public void Post(Resources Resource)
        {
            instance.insert(Resource);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, Resources Resource)
        {
            instance.update(id, Resource);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            instance.delete(id);
        }
    }
}
