using Common_Objects.Models;
using Common_Objects.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM_Module.Controllers
{
    public class PCMDiversionPEController : Controller
    {
        // GET: PCMDiversionPE
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        public ActionResult Index(int id)
        {
            var sp = from a in db.PCM_Diversion_SP
                     join b in db.PCM_D_ServicesProvider
                     on a.Services_Provider_Id equals b.Services_Provider_Id
                     where a.Intake_Assessment_Id == id
                     select new
                     {
                         S_P_Id = a.S_P_Id,
                         Services_Provider_Name = b.Services_Provider_Name
                     };

            ViewBag.sp = new SelectList(sp.ToArray(), "S_P_Id", "Services_Provider_Name");
         

            return PartialView();
        }

        //public JsonResult getProgrammeList(int id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<PCM_D_Programme> pList = db.PCM_D_Programme.Where(x => x.Services_Provider_Id == id).ToList();
        //    return Json(pList, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult FillProgrammeList(int ServicesProviderId)
        //{
        //    var p = db.PCM_D_Programme.Where(c => c.Services_Provider_Id == ServicesProviderId);
        //    return Json(p, JsonRequestBehavior.AllowGet);
        //}


    }
}