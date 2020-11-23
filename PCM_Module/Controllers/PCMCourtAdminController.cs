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
    public class PCMCourtAdminController : Controller
    {
        PCMCourtAdminModel m = new PCMCourtAdminModel();

        // GET: PCMCourtAdmin
        public ActionResult Index()
        {
            PCMCourtAdminViewModel vm = new PCMCourtAdminViewModel();

            //m.GetRecId(28377);

            //if(vm.Recommendation_Type_Id > 0)
            //{
            //    Response.Write("Not equal to zero");
            //}
            //else if(vm.Recommendation_Type_Id == 3)
            //{
            //    Response.Write("Recommendation type is equal to three");
            //}
            //else if(vm.Recommendation_Type_Id == 4)
            //{
            //    Response.Write("Recommendation type is equal to three");
            //}
            //else
            //{
            //    Response.Write("awe");
            //}

            //int r = 4;

            //if(r == 3)
            //{
            //    Response.Write("three");
            //}
            //else if(r == 4)
            //{
            //    Response.Write("four..");
            //}

            return View();
        }
    }
}