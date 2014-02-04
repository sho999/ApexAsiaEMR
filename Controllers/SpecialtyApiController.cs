using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApexAsiaDAL;

namespace ApexAsiaEMR.Controllers
{
    public class SpecialtyApiController : ApiController
    {
        private DAL _db = new DAL();

        // GET api/specialty
        public List<Specialty> Get()
        {
            return _db.GetAllSpecialities();
        }

        // GET api/specialty/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/specialty
        public void Post([FromBody]string value)
        {
        }

        // PUT api/specialty/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/specialty/5
        public void Delete(int id)
        {
        }
    }
}
