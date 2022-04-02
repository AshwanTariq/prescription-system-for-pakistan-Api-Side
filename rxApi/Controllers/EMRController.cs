using rxApi.Models.EMRModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace rxApi.Controllers
{
    public class EMRController : ApiController
    {
        EMRDataRxEntities db = new EMRDataRxEntities();

        [HttpGet]
        public HttpResponseMessage getRx()
        {
            try
            {
                var data = db.Prescription.Select(s => s);
                return Request.CreateResponse(HttpStatusCode.OK,data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage setRx(Prescription p)
        {
            try
            {
                db.Prescription.Add(p);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, p.DocUName);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }

}
