using Common_Objects.Models;
using Common_Objects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM_Module.Controllers
{
    public class PCMTabController : Controller
    {
        PCMPreliminaryModel m = new PCMPreliminaryModel();
        PCMPretrailViewModel pVM = new PCMPretrailViewModel();

        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        // GET: PCMTab
        public ActionResult Index(int id)
        {
            int? iiiid = m.GetId(id);
            ViewBag.Message = iiiid;

            return View();
        }
    }
}