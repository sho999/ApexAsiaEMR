using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApexAsiaDAL;
using System.Web;

namespace ApexAsiaEMR.Controllers
{
    public class PatientApiController : ApiController
    {
        private ApexAsiaDAL.DAL db = new DAL();
        private HttpRequest _request = HttpContext.Current.Request;


        // GET api/patientapi
        public List<Patient> Get()
        {
            return db.GetTop30Patients();
        }

        // GET api/patientapi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/patientapi
        public int Post(ApexAsiaDAL.Patient patient)
        {
            int result = 0;
            if (patient != null)
            {
                patient.EnteredDateTime = DateTime.Now;
                result = db.InsertPatient(patient);
            }
            return result;
        }

        // POST api/patientapi
        public bool Post(int id)
        {
            try
            {
                if (id > 0)
                {
                    Patient patientToBeUpdate = db.GetPatientById(id);
                    if (patientToBeUpdate != null)
                    {
                        patientToBeUpdate.Name = string.IsNullOrEmpty(_request["Name"]) ? string.Empty : _request["Name"];
                        patientToBeUpdate.Gender = string.IsNullOrEmpty(_request["Gender"]) ? string.Empty : _request["Gender"];

                        patientToBeUpdate.FatherName = string.IsNullOrEmpty(_request["FatherName"]) ? string.Empty : _request["FatherName"];
                        patientToBeUpdate.DateOfBirth = string.IsNullOrEmpty(_request["DateOfBirth"]) ? patientToBeUpdate.DateOfBirth : Convert.ToDateTime(_request["DateOfBirth"]);
                        patientToBeUpdate.NrcNo = string.IsNullOrEmpty(_request["NrcNo"]) ? string.Empty : _request["NrcNo"];

                        patientToBeUpdate.MarritalStatus = string.IsNullOrEmpty(_request["MarritalStatus"]) ? string.Empty : _request["MarritalStatus"];
                        patientToBeUpdate.HomePhone = string.IsNullOrEmpty(_request["HomePhone"]) ? string.Empty : _request["HomePhone"];
                        patientToBeUpdate.MobilePhone = string.IsNullOrEmpty(_request["MobilePhone"]) ? string.Empty : _request["MobilePhone"];

                        patientToBeUpdate.Address = string.IsNullOrEmpty(_request["Address"]) ? string.Empty : _request["Address"];
                        patientToBeUpdate.Township = string.IsNullOrEmpty(_request["Township"]) ? string.Empty : _request["Township"];
                        patientToBeUpdate.City = string.IsNullOrEmpty(_request["City"]) ? string.Empty : _request["City"];

                        patientToBeUpdate.KinName = string.IsNullOrEmpty(_request["KinName"]) ? patientToBeUpdate.KinName : _request["KinName"];
                        patientToBeUpdate.KinRelationship = string.IsNullOrEmpty(_request["KinRelationship"]) ? patientToBeUpdate.KinRelationship : _request["KinRelationship"];

                        patientToBeUpdate.Company = string.IsNullOrEmpty(_request["Company"]) ? string.Empty : _request["Company"];
                        patientToBeUpdate.PositionTitle = string.IsNullOrEmpty(_request["PositionTitle"]) ? string.Empty : _request["PositionTitle"];
                        patientToBeUpdate.EmployerAddress = string.IsNullOrEmpty(_request["EmployerAddress"]) ? string.Empty : _request["EmployerAddress"];
                        patientToBeUpdate.EmployerPhoneNumber = string.IsNullOrEmpty(_request["EmployerPhoneNumber"]) ? string.Empty : _request["EmployerPhoneNumber"];



                        patientToBeUpdate.KinAddress = string.IsNullOrEmpty(_request["KinAddress"]) ? string.Empty : _request["KinAddress"];
                        patientToBeUpdate.KinHomePhone = string.IsNullOrEmpty(_request["KinHomePhone"]) ? string.Empty : _request["KinHomePhone"];
                        patientToBeUpdate.KinMobilePhone = string.IsNullOrEmpty(_request["KinMobilePhone"]) ? string.Empty : _request["KinMobilePhone"];
                        patientToBeUpdate.EnteredBy = User.Identity.Name;
                        patientToBeUpdate.EnteredDateTime = DateTime.Now;
                       
                        db.UpdatePatient(patientToBeUpdate);
                    }
                }
                //responseObj.StatusCode = HttpStatusCode.OK;
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
                //responseObj.StatusCode = HttpStatusCode.InternalServerError;
                //responseObj.Content = new StringContent(string.Format("Error while calling UpdateUser() for UserId : {0} . Details = " + ex.Message , id.ToString()));
            }
        }

        // PUT api/patientapi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/patientapi/5
        public void Delete(int id)
        {
            if (id > 0)
            {
                Patient patientToBeDelete = db.GetPatientById(id);
                if (patientToBeDelete != null)
                {
                    db.DeletePatient(patientToBeDelete);
                }
            }
        }
    }
}
