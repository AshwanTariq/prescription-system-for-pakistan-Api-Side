using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using rxApi.Models.EMRModel;
using rxApi.Models.ClinicModel;
using rxApi.Models.PharmacyModel;

namespace rxApi.Controllers
{
    public class AdminController : ApiController
    {
        EMRDataRxEntities dbEMr = new EMRDataRxEntities();
        ClinicDataRxEntities dbClinic = new ClinicDataRxEntities();
        PharmacyDataRxEntities dbPharmacy = new PharmacyDataRxEntities();
        [HttpGet]
        public dynamic loginPatient(String username, String password)
        {
            try
            {
                //dbEMr.Database.ExecuteSqlCommand(""); Execute everything except select
                dynamic userRole = dbEMr.Patient.Where(p => p.username == username && p.password == password).Select(pat => pat.role).FirstOrDefault();
                return userRole;
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }
        [HttpGet]
        public dynamic loginDoctor(String username, String password)
        {
            try
            {
                dynamic userRole= dbClinic.Doctor.Where(d => d.DUsername == username && d.DPassword == password).Select(doc => doc.role).FirstOrDefault();
                return userRole;
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }
        [HttpGet]
        public dynamic loginPharmacy(String username, String password)
        {
            try
            {
                dynamic userRole =  dbPharmacy.Pharmacy.Where(ph => ph.username == username && ph.password == password).Select(ph => ph.role).FirstOrDefault();
                return userRole;
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }
        /*public HttpResponseMessage getDrugsCount()
    {

        try
        {
            List<int> cont = new List<int>();
            cont.Add(dbEMr.Drugs.Count());
            cont.Add(dbEMr.Drugs.Where(did => did.Dstatus == 1).Count());
            cont.Add(dbEMr.Drugs.Where(did => did.Dstatus == 0).Count());
            return Request.CreateResponse(HttpStatusCode.InternalServerError, cont);
        }
        catch (Exception exp)
        {

            return Request.CreateResponse(HttpStatusCode.InternalServerError, exp.Message);
        }
    }*/

        [HttpGet]
        public HttpResponseMessage getDrugsOnStatus(int status)
       {

           try
           {
              var data= dbEMr.Drugs.Where(dDetails => dDetails.Dstatus == status).OrderBy(order=>order.DName).Select(s=>new {s.DName,s.ExpDate,s.Did }).ToList();
             
               return Request.CreateResponse(HttpStatusCode.OK, data);
           }
           catch (Exception exp)
           {

               return Request.CreateResponse(HttpStatusCode.InternalServerError, exp.Message);
           }
       }
        [HttpPost]
        public HttpResponseMessage changeDrugStatus(int status,int id)
        {

            try
            {
                //var data = dbEMr.Drugs.Where(dDetails => dDetails.Dstatus == status).OrderBy(order => order.ExpDate).Select(s => new { s.DName, s.ExpDate, s.Did }).ToList();
                (from d in dbEMr.Drugs
                 where d.Did == id
                 select d).ToList().ForEach(x => x.Dstatus = status);
                dbEMr.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "1");
            }
            catch (Exception exp)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, exp.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage getallpharmacyForAdmin()
        {
            try
            {
                var data = dbPharmacy.Pharmacy.Select(s => s).OrderByDescending(a => a.rating);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage getallDoctors()
        {
            try
            {
                var data = dbClinic.Doctor.Select(s => s).OrderBy(a => a.DName);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
