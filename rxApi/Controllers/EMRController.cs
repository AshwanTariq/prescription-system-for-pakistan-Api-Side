using rxApi.Models.EMRModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Geolocation;
using rxApi.Models.ClinicModel;



namespace rxApi.Controllers
{
    public class EMRController : ApiController
    {
        
        EMRDataRxEntities db = new EMRDataRxEntities();
        ClinicDataRxEntities dbClinic = new ClinicDataRxEntities();

        [HttpGet]
        public HttpResponseMessage getHistory(String uname)
        {
            try
            {

                var history = db.Prescription.Where(rx=>rx.PatientUName==uname).Select(s=>s);
               
                return Request.CreateResponse(HttpStatusCode.OK, history);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage getDiseases(String PUname)
        {
            try
            {
               
                var dis = db.Patient.Where(p=>p.username==PUname).Select(s => s.disease).FirstOrDefault();
               

                return Request.CreateResponse(HttpStatusCode.OK, dis);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage getRx()
        {
            try
            {
                var data = db.Prescription.Select(s => s);
                //double distance = GeoCalculator.GetDistance(34.0675918, -118.3977091, 34.076234, -118.395314, 1); //Distance in miles

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
                var data = db.Prescription.Where(d=>d.DocUName==docName).OrderBy(arrange=>arrange.PatientUName).Select(s => s.PatientUName).ToList();
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
        [HttpGet]
        public HttpResponseMessage getPrescriptionForPharmacy(String value)
        {
            try
            {
               var data=db.Prescription.Select(rx=>rx).Where(chk=>chk.PharmacyUName==value).OrderBy(arrange=>arrange.rxDate);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage setRxRecived(int value)
        {
            try
            {
                //var data = db.Prescription.Select(rx => rx).Where(chk => chk.PharmacyUName == value);
                (from p in db.Prescription
                 where p.rxid == value
                 select p).ToList().ForEach(x => x.rxStatus = 1);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "1");
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage getPrescriptionByPatientName(String uname)
        {
            try
            {
                var data=db.Prescription.Where(u => u.PatientUName == uname).Select(s => s);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception exp)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, exp.Message);
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
