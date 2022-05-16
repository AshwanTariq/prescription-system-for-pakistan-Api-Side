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
        [HttpGet]
        public HttpResponseMessage getRecentPatients(String docName)
        {
            try
            {
                var data = db.Prescription.Where(d=>d.DocUName==docName).Select(s => s.PatientUName).ToList();
                List<dynamic> p=new List<dynamic>();
                foreach (var item in data)
                {
                    dynamic temp = db.Patient.Where(con => con.username == item).Select(pat => new { pat.Name,pat.Gender,pat.disease,pat.lat,pat.@long}).FirstOrDefault();
                    p.Add(temp);
                }
                return Request.CreateResponse(HttpStatusCode.OK, p);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage getAllDrugs()
        {
            try
            {
                var data = db.Drugs.Select(drgs => drgs);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage setPatinet(Patient p)
        {
            try
            {
                db.Patient.Add(p);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, p.Name);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage getPrescriptionCount()
        {
            try
            {
                int value=db.Prescription.Count();
                return Request.CreateResponse(HttpStatusCode.OK, value);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage getPrescriptionForPharmacy(String value)
        {
            try
            {
               var data=db.Prescription.Select(rx=>rx).Where(chk=>chk.PharmacyUName==value);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
    class PatientsModel
    {
        String Name;
        String gender;
        String Disease;
    }

}
