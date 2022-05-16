using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using rxApi.Models.PharmacyModel;

namespace rxApi.Controllers
{
    public class PharmacyController : ApiController
    {
        PharmacyDataRxEntities db = new PharmacyDataRxEntities();
        public HttpResponseMessage getallpharmacy()
        {
            try
            {
                var data = db.Pharmacy.Select(s => s);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
