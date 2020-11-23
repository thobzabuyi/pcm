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
    public class PCMPreliminaryController : Controller
    {
        PCMPreliminaryModel preModel = new PCMPreliminaryModel();
        PCMPreliminaryViewModel pVM = new PCMPreliminaryViewModel();

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
            Session["IntakeassId"] = id;
            ViewBag.ModuleRef = ClientRef;


         

                //int assID = Convert.ToInt32(Session["IntakeassId"]);


            int? Placementid = preModel.GetPlacementId(id);
            ViewBag.MessagePlacement = Placementid;
            Session["Placement"] = Placementid;


            return View(personVM);
        }
       

            #region preliminary function
       
        public ActionResult Index(int Id)
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


            var StatusType = preModel.GetStatusType();
            var RecommendationType = preModel.GetRecommendationType();

                int PcmPreliID = preModel.GetPCMPreliminaryIdDetailsByssId(Id);

                if (PcmPreliID != 0)
                {
                pVM = preModel.Display(PcmPreliID);


                    pVM.Status_Type = preModel.GetStatusType();

                    pVM.Recommendation_Type = preModel.GetRecommendationType();

                    pVM.PlacementRecomendation_List = preModel.GetPlacementRecomendation();

                    pVM.PreliminaryStatus_List = preModel.GetPreliminaryStatus();
                   

                    return PartialView(pVM);
                }

                else
                {

                    pVM.Status_Type = preModel.GetStatusType();
                    pVM.Recommendation_Type = preModel.GetRecommendationType();

                    pVM.PlacementRecomendation_List = preModel.GetPlacementRecomendation();

                    pVM.PreliminaryStatus_List = preModel.GetPreliminaryStatus();


                    preModel.Add(pVM, userId, Id);

                Session["Idc1"] = Id;
                Session["IntakeassId"] = Id;


                return PartialView(pVM);
                

            }
         


        }


        public ActionResult AssessmentRecomendation()
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

            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            
            int recAssid = assModel.GetPCMRecommendationByassId(assID);

            if (recAssid != 0)
            {


                assVM = assModel.GetRecomendationDetailsList(recAssid);
                assVM.RecommendationTyp_List = assModel.GetRecommendationType();
                assVM.PlacementType_List = assModel.GetPlacementType();
                Session["recupdate"] = recAssid;
            }

            return PartialView(assVM);


        }
        
        
        [HttpGet]
        public ActionResult addPreliminary()
        {

            pVM.Status_Type = preModel.GetStatusType();
            pVM.Recommendation_Type = preModel.GetRecommendationType();

            pVM.PlacementRecomendation_List = preModel.GetPlacementRecomendation();

            pVM.PreliminaryStatus_List = preModel.GetPreliminaryStatus();
            return PartialView("Index", pVM);
        }

        [HttpPost]
        public ActionResult addPreliminary(PCMPreliminaryViewModel vm, int Id)
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

            if (assID > 0)
            {
                int PcmPreliID = preModel.GetPCMPreliminaryIdDetailsByssId(assID);

                if (PcmPreliID> 0)
                    {

                    preModel.Update(vm, userId, PcmPreliID, assID);
                    
                }

                
            }

            return RedirectToAction("Home", "PCMPreliminary", new { Id = assID });


        }

        #endregion


        #region Offence 

        public ActionResult OffencedeatailsListPre()
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

            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            VM.Offence_List = Model.GetOffenceCategory();
            VM.OffenseSchedules_List = Model.GetOffenceSchedules();
            VM.OffenceType_List = Model.GetOffenceType();
            ViewBag.OffenceCategory = new SelectList(db.Offence_Categories.ToList(), "Offence_Category_Id", "Description");
            ViewBag.OffenceType = new SelectList(db.apl_Offence_Type.ToList(), "Offence_Type_Id", "Description");
            ViewBag.OffenceSchedule = new SelectList(db.apl_Offense_Schedules.ToList(), "Offence_Schedule_Id", "Description");

            return PartialView(VM);
        }
        public JsonResult ListOffence()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetOffenceList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                PCM_Offence_Id = x.PCM_Offence_Id,
                selectOffenceCategory = x.selectOffenceCategory,
                selectOffeceType = x.selectOffeceType,
                Offence_Circumstance = x.Offence_Circumstance,
                Value_Of_Goods = x.Value_Of_Goods,
                Value_Recovered = x.Value_Recovered,
                IsChild_Responsible = x.IsChild_Responsible,
                Responsibility_Details = x.Responsibility_Details
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public ActionResult IndexTabs()
        {

            int assID = Convert.ToInt32(Session["IntakeassId"]); 


            int? Recid = preModel.GetId(assID);
            ViewBag.Message = Recid;



            return PartialView();
        }

        #endregion
    }
}