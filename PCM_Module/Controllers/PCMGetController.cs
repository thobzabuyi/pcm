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
    public class PCMGetController : Controller
    {
        // GET: PCMGet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRecommendation()
        {
            List<PCMPreliminaryViewModel> stList = new List<PCMPreliminaryViewModel>();
            var m = new PCMPreliminaryModel();
            stList = m.GetRecommendation();
            return View(stList);
        }


        public ActionResult GetOffence()
        {
            List<PCMPreliminaryViewModel> oList = new List<PCMPreliminaryViewModel>();
            var m = new PCMPreliminaryModel();
            oList = m.GetOffence();
            return PartialView(oList);
        }
    }
}