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
    public class PCMPretrailController : Controller
    {
        PCMPretrailModel ppm = new PCMPretrailModel();
        PCMPretrailViewModel pVM = new PCMPretrailViewModel();

        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        // GET: PCMPretrail/home
        public ActionResult Home(int id)
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


            int ClientRefid = caseModel.GetClientRefIdssId(id);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            ViewBag.ModuleRef = ClientRef;

            return View(personVM);
        }
        // GET: PCMPretrail
        public ActionResult Index()
        {

            //get current username
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            var currentUser = (User)Session["CurrentUser"];
            var userProvince = -1;
            var userId = 0;

            if (currentUser != null)
            {
                userId = currentUser.User_Id;
                if (currentUser.Employees.Any())
                {
                    userProvince = currentUser.Employees.First().apl_Service_Office.apl_Local_Municipality.District.Province_Id;
                }
                if (currentUser.apl_Social_Worker.Any())
                {
                    userProvince = currentUser.apl_Social_Worker.First().apl_Service_Office.apl_Local_Municipality.District.Province_Id;
                }
            }

            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;


            var outcomeType = ppm.GetOutcomeType();
            pVM.Outcome_Type = ppm.GetOutcomeType();

            return PartialView(pVM);
        }
        public JsonResult ListAll()
        {
            return Json(ppm.ListAll(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPretrialById(int PCM_Pretrial_Id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            PCM_Pretrial_Details model = db.PCM_Pretrial_Details.Where(x => x.PCM_Pretrial_Id == PCM_Pretrial_Id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add(PCMPretrailViewModel pVM)
        {

            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            var currentUser = (User)Session["CurrentUser"];
            var userProvince = -1;
            var userId = 0;

            if (currentUser != null)
            {
                userId = currentUser.User_Id;
                if (currentUser.Employees.Any())
                {
                    userProvince = currentUser.Employees.First().apl_Service_Office.apl_Local_Municipality.District.Province_Id;
                }
                if (currentUser.apl_Social_Worker.Any())
                {
                    userProvince = currentUser.apl_Social_Worker.First().apl_Service_Office.apl_Local_Municipality.District.Province_Id;
                }
            }

            int assID = Convert.ToInt32(Session["IntakeassId"]);

            PCMPretrailModel pM = new PCMPretrailModel();
           
            int Intake_Assessment_Id = assID;
           
            var result = true;

            try
            {
                if(pVM.PCM_Pretrial_Id > 0)
                {
                    //pM.View(pVM, userId, p);
                    pM.update(pVM, pVM.PCM_Pretrial_Id, Intake_Assessment_Id, userId); 
                }
                else
                {
                    pM.Add(pVM, Intake_Assessment_Id, userId);
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetbyID(int ID)
        {
            var gID = ppm.ListAll().Find(x => x.PCM_Pretrial_Id.Equals(ID));
            return Json(gID, JsonRequestBehavior.AllowGet);
        }
       
    }
}