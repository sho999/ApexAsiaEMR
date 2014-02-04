using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApexAsiaDAL;

namespace ApexAsiaEMR.Controllers
{
    public class PhysicianApiController : ApiController
    {
        private ApexAsiaDAL.DAL _db = new ApexAsiaDAL.DAL();
        // GET api/physicianapi
        public List<PhysicianTitle> Get()
        {
            return this._db.GetAllPhysicians();
        }

        // GET api/physicianapi/5
        public List<PhysicianTitle> Get(int id)
        {
            return this._db.GetPhysiciansBySpecialtyId(id);
        }

        // POST api/physicianapi
        public PhysicianTitle Post(int id)
        {
            return this._db.GetPhysicianById(id);
        }

       
        

        // PUT api/physicianapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/physicianapi/5
        public void Delete(int id)
        {
        }
    }
}
