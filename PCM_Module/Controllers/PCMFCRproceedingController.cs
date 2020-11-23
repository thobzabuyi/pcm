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
    public class PCMFCRproceedingController : Controller
    {
        PCMFCRViewModel vm = new PCMFCRViewModel();
        PCMFCRModel m = new PCMFCRModel();


        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();



        // GET: PCMFCRproceeding
        public ActionResult Index(int id)
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

            int? Recid = m.GetPlacementId(id);
            ViewBag.Message = Recid;


            PCMFCRModel Model = new PCMFCRModel();
            PCMFCRViewModel vm = new PCMFCRViewModel();
            vm.PlacementRecomendation_List = Model.GetPlacementRecomendation();
            vm.Recommendation_Type_List = Model.GetRecommendationType();
            vm.Case_Type = Model.GetCaseStatus();
            ViewBag.Recommendation_Type_List = new SelectList(db.apl_Recommendation_Type.ToList(), "Recommendation_Type_Id", "Description");
            ViewBag.PlacementRecomendation_List = new SelectList(db.apl_Placement_Type.ToList(), "Placement_Type_Id", "Description");
            ViewBag.FormalCaseStatus_List = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");

            var CourtsType = m.GetCourtsType();
            vm.Courts_Type = m.GetCourtsType();
            var CaseType = m.GetCaseType();
            vm.Case_Type = m.GetCaseType();

            Session["Idc1"] = id;
            Session["IntakeassId"] = id;

            return PartialView(vm);
        }


        #region FORMAL COURT
        public JsonResult GetPCMFormalCourtList()
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

            int assID = Convert.ToInt32(Session["IntakeassId"]);
            return Json(m.GetPCMFormalCourtList(assID), JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetProceedingById(int FormalCourt_Id)
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

            int assID = Convert.ToInt32(Session["IntakeassId"]);
            db.Configuration.LazyLoadingEnabled = false;

            PCM_FCR_Proceeding model = db.PCM_FCR_Proceeding.Where(x => x.FormalCourt_Id == FormalCourt_Id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddProceeding(PCMFCRViewModel vm)
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

            int assID = Convert.ToInt32(Session["IntakeassId"]);

            PCMFCRModel m = new PCMFCRModel();

            int Intake_Assessment_Id = assID;
            var result = false;

            try
            {
                if (vm.FormalCourt_Id > 0)
                {
                    m.UpdateProceeding(vm, vm.FormalCourt_Id, Intake_Assessment_Id, userId);
                    result = true;
                }
                else
                {
                    m.AddProceeding(vm, Intake_Assessment_Id, userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region FORMAL COURT OUTCOME

        public ActionResult IndexOutcome()
        {
            var CaseType = m.GetCaseType();
            vm.Case_Type = m.GetCaseType();
            return PartialView(vm);
        }

        public JsonResult GetPCMFCRoutcome()
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
            int assID = Convert.ToInt32(Session["IntakeassId"]);
            PCMFCRModel Model = new PCMFCRModel();
            PCMFCRViewModel vm = new PCMFCRViewModel();
            vm.PlacementRecomendation_List = Model.GetPlacementRecomendation();
            vm.Recommendation_Type_List = Model.GetRecommendationType();
            vm.Case_Type = Model.GetCaseStatus();
            ViewBag.Recommendation_Type_List = new SelectList(db.apl_Recommendation_Type.ToList(), "Recommendation_Type_Id", "Description");
            ViewBag.PlacementRecomendation_List = new SelectList(db.apl_Placement_Type.ToList(), "Placement_Type_Id", "Description");
            ViewBag.FormalCaseStatus_List = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");

            return Json(m.GetPCMFCRoutcome(assID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOutcomeById(int FormalCourtOutcome_Id)
        {
            db.Configuration.LazyLoadingEnabled = false;

            PCMFCRModel Model = new PCMFCRModel();
            PCMFCRViewModel vm = new PCMFCRViewModel();
            vm.PlacementRecomendation_List = Model.GetPlacementRecomendation();
            vm.Recommendation_Type_List = Model.GetRecommendationType();
            vm.Case_Type = Model.GetCaseStatus();
            ViewBag.Recommendation_Type_List = new SelectList(db.apl_Recommendation_Type.ToList(), "Recommendation_Type_Id", "Description");
            ViewBag.PlacementRecomendation_List = new SelectList(db.apl_Placement_Type.ToList(), "Placement_Type_Id", "Description");
            ViewBag.FormalCaseStatus_List = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");


            vm = Model.GetFormalCourtById(FormalCourtOutcome_Id);
            string value = string.Empty;
            value = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddOutcome(PCMFCRViewModel vm)
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
            int assID = Convert.ToInt32(Session["IntakeassId"]);
            PCMFCRModel m = new PCMFCRModel();


            
            var result = false;

            try
            {
                if (vm.FormalCourtOutcome_Id > 0)
                {
                    m.UpdateOutcome(vm, vm.FormalCourtOutcome_Id, assID);
                    result = true;
                }
             
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion




    }
}