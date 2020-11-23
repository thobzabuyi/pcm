using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM_Module.Controllers
{
    public class PCMFCPController : Controller
    {
        // GET: PCMFCP
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}