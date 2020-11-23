using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM_Module.Controllers
{
    public class PCMTabHBSController : Controller
    {
        // GET: PCMTabHBS
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}