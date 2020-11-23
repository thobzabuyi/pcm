using Common_Objects.Models;
using Common_Objects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM_Module.Controllers
{
    public class AssessmentController : Controller
    {
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
            personVM.IntakeAssPar=id;
            return View(personVM);
        }

        public ActionResult IndexPre(int id)
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