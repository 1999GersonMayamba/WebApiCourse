using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Section_2UseSwagger.Controllers
{
   // [AuthorizationHeaderHandler]

    public class ValuesController : ApiController
    {
        // Restrict by user:
        //[AuthorizationHeaderHandler]
        // GET api/values
        [Route("api/Values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Route("api/Values/{id:int:min(1)}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {

        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        public void Delete(int id)
        {

        }
    }
}
