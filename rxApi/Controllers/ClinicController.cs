using rxApi.Models.ClinicModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rxApi.Controllers
{
    public class ClinicController : ApiController
    {
        ClinicDataRxEntities dbClinic = new ClinicDataRxEntities();
        [HttpPost]
        public HttpResponseMessage setDoctor(Doctor p)
        {
            try
            {
                dbClinic.Doctor.Add(p);
                dbClinic.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, p.DName);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
