using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Objects.Models;
using Common_Objects.ViewModels;

namespace PCM_Module.Controllers
{
    public class PresentenceMainController : Controller
    {
        // GET: PresentenceMain
        public ActionResult Index(int id)
        {

            //get current username
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            //instanciate model repositry
            PCMCaseModel caseModel = new PCMCaseModel();

            //instanciate viewmodel
            PCMPersonViewModel personVM = new PCMPersonViewModel();

            //get person id by assessment id
            personVM.IntakeAssPar = id;
            return View(personVM);
        }
        
    }
}