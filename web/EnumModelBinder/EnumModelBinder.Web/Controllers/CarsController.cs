using EnumModelBinder.Web.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnumModelBinder.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        // GET api/values
        [HttpGet("parts")]
        public IEnumerable<EnumItem> GetParts()
        {
            return EnumExts.GetItems(typeof(CarPartName));
        }

        [HttpGet("conditions")]
        public IEnumerable<EnumItem> GetConditions()
        {
            return EnumExts.GetItems(typeof(CarPartCondition));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] IEnumerable<CarPartDamage> values)
        {
        }

        // PUT api/values/5
        [HttpPut]
        public void Put([FromBody] EnumItem value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
