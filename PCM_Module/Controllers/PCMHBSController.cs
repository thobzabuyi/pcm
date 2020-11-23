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
    public class PCMHBSController : Controller
    {
        PCMHBSModel m = new PCMHBSModel();
        PCMHBSViewModel vm = new PCMHBSViewModel();
        PCMPreliminaryModel preModel = new PCMPreliminaryModel();
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        // GET: HBS
        public ActionResult Home(int id)
        {
            //get current username
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            //instanciate model repositry
            //instanciate model repositry
            PCMCaseModel caseModel = new PCMCaseModel();

            //instanciate viewmodel
            PCMPersonViewModel personVM = new PCMPersonViewModel();

            //get person id by assessment id
            personVM.IntakeAssPar = id;

            bool IntakeAssessmentIdExisit = db.PCM_HB_Supervision.Any(x => x.Intake_Assessment_Id == id);


            int ClientRefid = caseModel.GetClientRefIdssId(id);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            Session["IntakeassId"] = id;
            ViewBag.ModuleRef = ClientRef;

            int? Placementid = preModel.GetPlacementId(id);
            ViewBag.MessagePlacement = Placementid;
            Session["Placement"] = Placementid;

            if (IntakeAssessmentIdExisit)
            {
                Session["Idc1"] = id;
                Session["IntakeassId"] = id;


                return View(personVM);
            }
            else
            {
                Session["Idc1"] = id;
                Session["IntakeassId"] = id;


                //Response.Write("does not Exist");
                return View(personVM);
            }
        }
        #region Home Based Supervion controller functions


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
            var HBSConditionType = m.GetCondition();
            vm.Condition_List = m.GetCondition();

            var HBSsupersorType = m.GetHBSsupervisorType();
            vm.HBSsupervisor_Type = m.GetHBSsupervisorType();

            bool IntakeAssessmentIdExisit = db.PCM_HB_Supervision.Any(x => x.Intake_Assessment_Id == id);



            int assID = Convert.ToInt32(Session["IntakeassId"]);
            //instanciate model repositry
            PCMCaseModel caseModel = new PCMCaseModel();

            //instanciate viewmodel
            PCMPersonViewModel personVM = new PCMPersonViewModel();

            //get person id by assessment id
            personVM.IntakeAssPar = assID;


            int ClientRefid = caseModel.GetClientRefIdssId(assID);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            Session["IntakeassId"] = assID;
            ViewBag.ModuleRef = ClientRef;




            if (IntakeAssessmentIdExisit)
            {
                Session["Idc1"] = id;
                Session["IntakeassId"] = id;

   
                return PartialView(vm);
            }
            else
            {
                Session["Idc1"] = id;
                Session["IntakeassId"] = id;

        
                //Response.Write("does not Exist"); 
                return PartialView(vm);
            }


        }


        public JsonResult GetHBSList()
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
            return Json(m.GetHBSList(assID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetHBSById(int HomeBasedSupervision_Id)
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
            var CaseStatus_Type = m.GetCaseStatusType();
            vm.CaseStatus_Type = m.GetCaseStatusType();
            ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");

            PCM_HB_Supervision model = db.PCM_HB_Supervision.Where(x => x.HomeBasedSupervision_Id == HomeBasedSupervision_Id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateHBS(PCMHBSViewModel vm)
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

            PCMHBSModel cm = new PCMHBSModel();


            //int PlacementID = Convert.ToInt32(Session["Placementid"]); 


            int assID = Convert.ToInt32(Session["IntakeassId"]);

            
            var result = false;

            try
            {
                if (vm.HomeBasedSupervision_Id > 0)
                {
                    cm.UpdateHBS(vm, userId,vm.HomeBasedSupervision_Id, assID);
                    result = true;

                    result = true;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        #endregion

        #region visitation outcome
        public ActionResult IndexVO()
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
            //instanciate model repositry
            PCMCaseModel caseModel = new PCMCaseModel();

            //instanciate viewmodel
            PCMPersonViewModel personVM = new PCMPersonViewModel();

            //get person id by assessment id
            personVM.IntakeAssPar = assID;


            int ClientRefid = caseModel.GetClientRefIdssId(assID);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            Session["IntakeassId"] = assID;
            ViewBag.ModuleRef = ClientRef;
            var Compliance_Type = m.GetComplianceType();
            vm.Compliance_Type = m.GetComplianceType();
            ViewBag.Compliance_List = new SelectList(db.apl_PCM_Compliance.ToList(), "Compliance_Id", "Description");

            return PartialView(vm);
        }

        public JsonResult GetVOList()
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
            var Compliance_Type = m.GetComplianceType();
            vm.Compliance_Type = m.GetComplianceType();
            ViewBag.Compliance_List = new SelectList(db.apl_PCM_Compliance.ToList(), "Compliance_Id", "Description");

            return Json(m.GetVOList(assID), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetVOById(int HB_Visitaion_Outcome_Id)
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
            
            
            PCM_HB_VistationOutcome model = db.PCM_HB_VistationOutcome.Where(x => x.HB_Visitaion_Outcome_Id == HB_Visitaion_Outcome_Id).SingleOrDefault();

            PCMHBSModel m = new PCMHBSModel();
            PCMHBSViewModel vm = new PCMHBSViewModel();
            var Compliance_Type = m.GetComplianceType();
            vm.Compliance_Type = m.GetComplianceType();
            ViewBag.Compliance_List = new SelectList(db.apl_PCM_Compliance.ToList(), "Compliance_Id", "Description");
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                vm = m.GetVisitationOutcometById(HB_Visitaion_Outcome_Id);

                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CreateVO(PCMHBSViewModel vm)
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
            int Intake_Assessment_Id = Convert.ToInt32(Session["IntakeassId"]);
            var result = false;

            try
            {
                if (vm.HB_Visitaion_Outcome_Id > 0)
                {
                    m.EditVO(vm.HB_Visitaion_Outcome_Id);
                    m.UpdateVO(vm, vm.HB_Visitaion_Outcome_Id, Intake_Assessment_Id);
                    result = true;
                }
                else
                {
                    int HomebaseId = m.GetPCMHBByassId(Intake_Assessment_Id);
                    m.CreateVO(vm, Intake_Assessment_Id, HomebaseId);
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

        #region court outcome
        public ActionResult IndexCourtOutCome()
        {     //get current username
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
            int Intake_Assessment_Id = Convert.ToInt32(Session["IntakeassId"]);

            var CaseStatus_Type = m.GetCaseStatusType();
            vm.CaseStatus_Type = m.GetCaseStatusType();
            ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");


            PCMCaseModel caseModel = new PCMCaseModel();

            //instanciate viewmodel
            PCMPersonViewModel personVM = new PCMPersonViewModel();

            //get person id by assessment id
            personVM.IntakeAssPar = Intake_Assessment_Id;


            int ClientRefid = caseModel.GetClientRefIdssId(Intake_Assessment_Id);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            Session["IntakeassId"] = Intake_Assessment_Id;
            ViewBag.ModuleRef = ClientRef;

            return PartialView(vm);
        }

        public JsonResult GetCourtOutComeList()
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
            int Intake_Assessment_Id = Convert.ToInt32(Session["IntakeassId"]);
            var CaseStatus_Type = m.GetCaseStatusType();
            vm.CaseStatus_Type = m.GetCaseStatusType();
            ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");
            return Json(m.GetCourtOutComeList(Intake_Assessment_Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourtOutComeById(int HB_CourtOutcome_Id)
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
            int Intake_Assessment_Id = Convert.ToInt32(Session["IntakeassId"]);

            var CaseStatus_Type = m.GetCaseStatusType();
            vm.CaseStatus_Type = m.GetCaseStatusType();
            ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                vm = m.GetHBCourtOutcometById(HB_CourtOutcome_Id);

                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CreateCourtOutcome(PCMHBSViewModel vm)
        {     //get current username
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
            int Intake_Assessment_Id = Convert.ToInt32(Session["IntakeassId"]);
            var result = false;

            try
            {
                if (vm.HB_CourtOutcome_Id > 0)
                {
                    m.EditCourtOutCome(vm.HB_CourtOutcome_Id);
                    m.UpdateCourtOutCome(vm, vm.HB_CourtOutcome_Id, Intake_Assessment_Id);
                    result = true;
                }
                else
                {
                    m.CreateCourtOutCome(vm, Intake_Assessment_Id);
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

        public ActionResult IndexRelationType()
        {
            return PartialView();
        }

        #region Conditions

        public ActionResult GetConditiondetails()
        {
            int intassid = Convert.ToInt32(Session["IntakeassId"]);
            // get current username
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
            PCMHBSModel assModel = new PCMHBSModel();

            PCMHBSViewModel assVM = new PCMHBSViewModel();
            assVM.Condition_List = assModel.GetCondition();
            ViewBag.Condition_List = new SelectList(db.apl_PCM_Conditions.ToList(), "Condition_Id", "Description");

            return PartialView(assVM);
        }

        [HttpPost]
        public ActionResult CreateConditions(PCMHBSViewModel vm)
        {
            int intassid = Convert.ToInt32(Session["IntakeassId"]);
            // get current username
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
            PCMHBSModel assModel = new PCMHBSModel();

            PCMHBSViewModel assVM = new PCMHBSViewModel();

            // get recomendation Id for insert..................
            int HomebaseId = assModel.GetPCMHBByassId(intassid);
            ViewBag.Error = null;
            if (vm.Condition_Id != null && HomebaseId!=0)
            {
                var selectedItems = db.apl_PCM_Conditions.Where(p => vm.Condition_Id.Contains((p.Condition_Id))).ToList();



                if (selectedItems != null)
                {
                    foreach (var selectedItem in selectedItems)
                    {

                        int Condition_Id = selectedItem.Condition_Id;
                        assModel.CreatePCMConditionsDeatils(vm, HomebaseId, Condition_Id, userId);

                    }

                }


            }

            else

            {

                ViewBag.Error = string.Format("Error you must add Home base under recommendation", DateTime.Now.ToString());
            }

            return RedirectToAction("Home", "PCMHBS", new { Id = intassid });
        }

        public ActionResult GetSelectedConditionsFromDB()
        {
            int intassid = Convert.ToInt32(Session["IntakeassId"]);
            // get current username
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
            PCMHBSModel assModel = new PCMHBSModel();

            PCMHBSViewModel assVM = new PCMHBSViewModel();

            int HomebaseId = assModel.GetPCMHBByassId(intassid);
            if (HomebaseId != 0)
            {
                List<PCMHBSViewModel> OurList = assModel.GetSelectedConditionFromDB(HomebaseId);
                return PartialView(OurList);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteConditionFromTable(int id)
        {
            int intassid = Convert.ToInt32(Session["IntakeassId"]);
            // get current username
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
            PCMHBSModel assModel = new PCMHBSModel();

            PCMHBSViewModel assVM = new PCMHBSViewModel();
            if (id != 0)
            {
                try
                {
                    assModel.DeleteConditionRecord(id);
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";
            return RedirectToAction("Index", "PCMHBS", new { Id = intassid });
        }

        #endregion
    }
}