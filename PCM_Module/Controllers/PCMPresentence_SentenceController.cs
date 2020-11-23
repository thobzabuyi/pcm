using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Objects.ViewModels;
using Common_Objects;
using Common_Objects.Models;
using Newtonsoft.Json;

namespace PCM_Module.Controllers
{
    public class PCMPresentence_SentenceController : Controller
    {
        // GET: PCMPresentence_Sentence
        #region Child Details
        public ActionResult IndexPresentence(int id)
        {
            //get current username
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            //instanciate model repositry
            PCMPresentenceModel caseModel = new PCMPresentenceModel();

            //instanciate viewmodel
            PCMPersonViewModel personVM = new PCMPersonViewModel();
            PCMCaseModel caseModelC = new PCMCaseModel();
            //get person id by assessment id
            int personId = caseModel.GetPCMPersonIdByintAssId(id);

            //get person record
            personVM = caseModel.GetPCMPerson(personId);

            int ClientRefid = caseModelC.GetClientRefIdssId(id);
            //Get Module Reference number ....................
            string ClientRef = caseModelC.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            ViewBag.ModuleRef = ClientRef;
            return PartialView(personVM);
        }

        #endregion

        #region PRESENTENCE SUMMARY

        #region GET SUMMARY
        public ActionResult GetPCMPresentenseSummaryDetails(int id)
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
        
            
            //initialise model repositry
            PCMPresentenceModel Model = new PCMPresentenceModel();

            //initialise view model
            PCMCaseModel caseModelC = new PCMCaseModel();

            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;

            int PreSummaryID = Model.GetPCMPresentenseSummaryByassId(id);
            if (PreSummaryID != 0)
            {
                PCMPresentenceDetailsViewModel VM1 = new PCMPresentenceDetailsViewModel();
                VM1 = Model.GetPresentenseSummaryList(PreSummaryID);//.............................?
                
                Session["Idc1"] = PreSummaryID;
                Session["IntakeassId"] = id;
                return PartialView(VM1);
            }
            else
            {


                PCMPresentenceModel Model1 = new PCMPresentenceModel();
                PCMPresentenceDetailsViewModel VM = new PCMPresentenceDetailsViewModel();
                //initialise view model
                PCMPresentenceDetailsViewModel VMC = new PCMPresentenceDetailsViewModel();
                
                Model.InsertPresentenceSummaries(VM, id, userId);

                int PreSummaryID1 = Model1.GetPCMPresentenseSummaryByassId(id);
                
                VMC = Model1.GetPresentenseSummaryList(PreSummaryID1);

                Session["NewId"] = id;
                Session["IntakeassId"] = id;
                return PartialView(VMC);
            }

        }
        #endregion

        #region UPDATE Summary DETAILS


        [HttpGet]
        public ActionResult UpdatePresentenceSummaries()
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
            //initialise model repositry
            PCMPresentenceModel Model = new PCMPresentenceModel();

            //initialise view model
            PCMPresentenceDetailsViewModel VM = new PCMPresentenceDetailsViewModel();



            return PartialView("GetPCMPresentenseSummaryDetails", VM);

        }

        [HttpPost]
        public ActionResult UpdatePresentenceSummaries(PCMPresentenceDetailsViewModel caseVM)
        {
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
    int id =Convert.ToInt32(Session["IntakeassId"]) ;
            int idcreate = Convert.ToInt32(Session["NewId"]);

            int Idupdate = Convert.ToInt32(Session["Idc1"]);
            int Id;
            if (Idupdate != 0)
            {

                Id = Idupdate;
            }
            else
            {
                Id = idcreate;
            }

            //instanciate model repositry
            PCMPresentenceModel Model = new PCMPresentenceModel();

            if (ModelState.IsValid)
            {
                // Call Update Function
                Model.UpdatePresentenceSummaries(caseVM, userId, Id);
                ViewBag.Message = "Updated successfully";
            }
            else
            {
                ViewBag.Message = "not updated ";
            }
            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("IndexPre", "Assessment", new { Id = id });
        }

        public ActionResult checkAssessmentId(int assID)
        {
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            ////instanciate model
            PCMPresentenceModel pcmcasemodel = new PCMPresentenceModel();

            ////instanciate viewmodel
            PCMPresentenceDetailsViewModel pcmcasevm = new PCMPresentenceDetailsViewModel();

            int SummId = pcmcasemodel.GetPCMPresentenseSummaryByassId(assID);
            if (SummId != 0)
            {
                return PartialView(pcmcasevm);
            }
            else
            {
                return PartialView(pcmcasevm);
            }
        }


        #endregion

        #endregion

        #region PRESENTENCE

        #region ADD UPDATE PRESENTENCE DETAILS

        public ActionResult GetPCMPresentense()
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
    PCMCaseModel caseModelC = new PCMCaseModel();

            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;

            PCMPresentenceModel Model = new PCMPresentenceModel();
            //initialise view model
            PCMPresentenceDetailsViewModel VM = new PCMPresentenceDetailsViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                int Presentenseid = Model.GetPCMPresentenseByassId(intassid);
                if (Presentenseid != 0)
                {

                    VM = Model.GetPresentenseList(Presentenseid);

                    //populate dropdown lists

                    VM.CommunityBasedOptions_List = Model.GetCommunityBasedOption();
                    VM.SupendedPostponedSentence_List = Model.GetSupendedPostponedSentence();
                    VM.CaseStatus_List = Model.GetCaseStatus();//..................?
                    VM.RestorectiveJustice_List = Model.GetRestorectiveJustice();
                    VM.ProgrammeType_List = Model.GetProgrammeType();
                    VM.Programme_List = Model.GetProgramme();
                    VM.Imprisoment_List = Model.GetImprisoment();//..................?
                    VM.Department_List = Model.GetDepartment();
                    VM.Court_List = Model.GetAllCourt();
                    VM.Province_List = Model.GetAllProvinces();
                    VM.District_List = Model.GetAllDistrict();


                    if (Convert.ToInt32(Model.GetPresentenseList(Presentenseid).Court_id) != 0)
                    {
                        int Courtid = Convert.ToInt32(Model.GetPresentenseList(Presentenseid).Court_id);
                        ViewBag.Court = db.Courts.Find(Courtid).Description;

                        //Get District using the court..........................................
                        int DistrictId = (from k in db.Districts
                                          join k1 in db.Courts on k.District_Id equals k1.District_Id
                                          where k1.Court_Id == Courtid
                                          select k.District_Id).FirstOrDefault();

                    ViewData["district"] = new SelectList(db.Districts, "District_Id", "Description", DistrictId).ToList();

                    //Get Province using the District..........................................

                    int ProvinceId = (from a in db.Districts
                                      join b in db.Provinces on a.Province_Id equals b.Province_Id
                                      where a.District_Id.Equals(DistrictId)
                                      select b.Province_Id).FirstOrDefault();
                    ViewData["province"] = new SelectList(db.Provinces, "Province_Id", "Description", ProvinceId).ToList();
                    }

                    Session["presentdate"] = Presentenseid;

                    return PartialView(VM);
                }
                else
                {
                    PCMPresentenceModel Model1 = new PCMPresentenceModel();
                    PCMPresentenceDetailsViewModel VM1 = new PCMPresentenceDetailsViewModel();
                    //initialise view model

                    VM1.CommunityBasedOptions_List = Model.GetCommunityBasedOption();
                    VM1.SupendedPostponedSentence_List = Model.GetSupendedPostponedSentence();
                    VM1.CaseStatus_List = Model.GetCaseStatus();//..................?
                    VM1.RestorectiveJustice_List = Model.GetRestorectiveJustice();
                    VM1.ProgrammeType_List = Model.GetProgrammeType();
                    VM1.Programme_List = Model.GetProgramme();
                    VM1.Imprisoment_List = Model.GetImprisoment();//..................?
                    VM1.Department_List = Model.GetDepartment();
                    VM1.Court_List = Model.GetAllCourt();
                    VM1.Province_List = Model.GetAllProvinces();
                    VM1.District_List = Model.GetAllDistrict();

                    //funtion that calls insert Socio Economy
                    Model1.InsertPresentence(VM1, intassid, userId);

                    PCMPresentenceDetailsViewModel VM2 = new PCMPresentenceDetailsViewModel();
                    int Presentenceidadd = Model.GetPCMPresentenseByassId(intassid);
                    VM2 = Model1.GetPresentenseList(Presentenceidadd);

                    VM2.CommunityBasedOptions_List = Model.GetCommunityBasedOption();
                    VM2.SupendedPostponedSentence_List = Model.GetSupendedPostponedSentence();
                    VM2.CaseStatus_List = Model.GetCaseStatus();//..................?
                    VM2.RestorectiveJustice_List = Model.GetRestorectiveJustice();
                    VM2.ProgrammeType_List = Model.GetProgrammeType();
                    VM2.Programme_List = Model.GetProgramme();
                    VM2.Imprisoment_List = Model.GetImprisoment();//..................?
                    VM2.Department_List = Model.GetDepartment();
                    VM2.Court_List = Model.GetAllCourt();
                    VM2.Province_List = Model.GetAllProvinces();
                    VM2.District_List = Model.GetAllDistrict();
                    Session["presentadd"] = Presentenceidadd;
                    return PartialView("GetPCMPresentense", VM2);
                }

            }
        }
        [HttpGet]
        public ActionResult UpdatePresentence()
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
    //initialise model repositry
    PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMPresentenceDetailsViewModel VM = new PCMPresentenceDetailsViewModel();

            return PartialView("GetPCMPresentense", VM);

        }

        [HttpPost]
        public ActionResult UpdatePresentence(PCMPresentenceDetailsViewModel VM)
        {
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
    int intassid = Convert.ToInt32(Session["IntakeassId"]);
            int idcreate = Convert.ToInt32(Session["presentadd"]);

            int Idupdate = Convert.ToInt32(Session["presentdate"]);
            int Id;
            if (idcreate != 0)
            {

                Id = idcreate;
            }
            else
            {
                Id = Idupdate;
            }
            
            //instanciate model repositry
            PCMPresentenceModel Model = new PCMPresentenceModel();
            // Call Update Function
            Model.UpdatePresentence(VM, userId, Id);
            ViewBag.Message = "Updated successfully";

            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("IndexPre", "Assessment", new { Id = intassid });
        }
        #endregion

        #region JASON Yanga
        #region  Cascading using Json

        // Json Call to get District

        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        public JsonResult DistrictList(int Id)
        {
            var state = from s in db.Districts
                        where s.Province_Id == Id
                        select s;
            return Json(new SelectList(state.ToArray(), "District_Id", "Description"), JsonRequestBehavior.AllowGet);
        }

        // Json Call to get Court
        public JsonResult CourtList(int id)
        {
            var city = from c in db.Courts
                       where c.District_Id == id
                       select c;
            return Json(new SelectList(city.ToArray(), "Court_Id", "Description"), JsonRequestBehavior.AllowGet);
        }

        // Get District from DB by Province ID
        public IList<Common_Objects.Models.District> GetDistrict(int Provinceid)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Districts.Where(m => m.Province_Id == Provinceid).ToList();
        }
        // Get Court from DB by District ID
        public IList<Common_Objects.Models.Court> GetCourt(int DistrictId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Courts.Where(m => m.District_Id == DistrictId).ToList();
        }
        public JsonResult GetDistricts(string id)
        {
            List<SelectListItem> districts = new List<SelectListItem>();
            var DistricList = this.GetDistrict(Convert.ToInt32(id));
            var DistrictData = DistricList.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.District_Id.ToString(),
            });
            return Json(DistrictData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCourts(string id)
        {
            List<SelectListItem> courts = new List<SelectListItem>();
            var CourtList = this.GetCourt(Convert.ToInt32(id));
            var CourtData = CourtList.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.Court_Id.ToString(),
            });
            return Json(CourtData, JsonRequestBehavior.AllowGet);
        }

        #endregion


        #endregion

        #endregion
        
        #region ADD UPDATE SENTENCE
        
        public ActionResult SentenseList()
        {
            //get current username
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;
            int caseid = Convert.ToInt32(Session["IntakeassId"]);

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

            PCMPresentenceModel Model = new PCMPresentenceModel();

            //initialise view model
            PCMPresentenceDetailsViewModel VM = new PCMPresentenceDetailsViewModel();
            VM.CommunityBasedOptions_List = Model.GetCommunityBasedOption();
            VM.SupendedPostponedSentence_List = Model.GetSupendedPostponedSentence();
            VM.CaseStatus_List = Model.GetCaseStatus();//..................?
            VM.RestorectiveJustice_List = Model.GetRestorectiveJustice();
            VM.ProgrammeType_List = Model.GetProgrammeType();
            VM.Programme_List = Model.GetProgramme();
            VM.Imprisoment_List = Model.GetImprisoment();//..................?
            VM.Department_List = Model.GetDepartment();
            VM.Court_List = Model.GetAllCourt();
            VM.Province_List = Model.GetAllProvinces();
            VM.District_List = Model.GetAllDistrict();

            ViewBag.CommunityBasedOptions = new SelectList(db.apl_PCM_Community_Based_Options.ToList(), "Community_Based_Options_Id", "Description");
            ViewBag.SupendedPostponedSentence = new SelectList(db.apl_PCM_Supended_Postponed_Sentence.ToList(), "Suspended_Postponed_Sentence_Id", "Description");
            ViewBag.CaseStatus = new SelectList(db.apl_PCM_Case_Status.ToList(), "PCM_Case_Status_Id", "Description");

            ViewBag.RestorectiveJustice = new SelectList(db.apl_Restorective_Justice_Types.ToList(), "Restorective_Justice_Option_Id", "Description");
            ViewBag.ProgrammeType = new SelectList(db.apl_Programme_Type.ToList(), "Programme_Type_Id", "Description");
            ViewBag.Programme = new SelectList(db.apl_Programmes.ToList(), "Programme_Type_Id", "Programme_Name");

            ViewBag.Imprisoment = new SelectList(db.apl_PCM_Imprisoment.ToList(), "Imprisoment_Id", "Description");
            ViewBag.Department = new SelectList(db.apl_Department.ToList(), "Department_Id", "Description");
            ViewBag.Province = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District = new SelectList(db.Districts.ToList(), "District_Id", "Description");
            ViewBag.Court = new SelectList(db.Courts.ToList(), "Court_Id", "Description");

            return PartialView(VM);
        }
        public JsonResult ListSentense()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMPresentenceModel Model = new PCMPresentenceModel();

            //initialise view model
            PCMPresentenceDetailsViewModel VM = new PCMPresentenceDetailsViewModel();

            List<PCMPresentenceDetailsViewModel> List = Model.GetSentenseList(caseid).Select(x => new PCMPresentenceDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                Sentence_Id = x.Sentence_Id,
                Court_Date = x.Court_Date,
                NextCourtDate = x.NextCourtDate,
                SelectCaseStatusDetails = x.SelectCaseStatusDetails
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSentenseById(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCMPresentenceModel Model = new PCMPresentenceModel();

                //initialise view model
                PCMPresentenceDetailsViewModel vm = new PCMPresentenceDetailsViewModel();

                vm.CommunityBasedOptions_List = Model.GetCommunityBasedOption();
                vm.SupendedPostponedSentence_List = Model.GetSupendedPostponedSentence();
                vm.CaseStatus_List = Model.GetCaseStatus();//..................?
                vm.RestorectiveJustice_List = Model.GetRestorectiveJustice();
                vm.ProgrammeType_List = Model.GetProgrammeType();
                vm.Programme_List = Model.GetProgramme();
                vm.Imprisoment_List = Model.GetImprisoment();//..................?
                vm.Department_List = Model.GetDepartment();
                vm.Court_List = Model.GetAllCourt();
                vm.Province_List = Model.GetAllProvinces();
                vm.District_List = Model.GetAllDistrict();

                ViewBag.CommunityBasedOptions = new SelectList(db.apl_PCM_Community_Based_Options.ToList(), "Community_Based_Options_Id", "Description");
                ViewBag.SupendedPostponedSentence = new SelectList(db.apl_PCM_Supended_Postponed_Sentence.ToList(), "Suspended_Postponed_Sentence_Id", "Description");
                ViewBag.CaseStatus = new SelectList(db.apl_PCM_Case_Status.ToList(), "PCM_Case_Status_Id", "Description");

                ViewBag.RestorectiveJustice = new SelectList(db.apl_Restorective_Justice_Types.ToList(), "Restorective_Justice_Option_Id", "Description");
                ViewBag.ProgrammeType = new SelectList(db.apl_Programme_Type.ToList(), "Programme_Type_Id", "Description");
                ViewBag.Programme = new SelectList(db.apl_Programmes.ToList(), "Programme_Type_Id", "Programme_Name");

                ViewBag.Imprisoment = new SelectList(db.apl_PCM_Imprisoment.ToList(), "Imprisoment_Id", "Description");
                ViewBag.Department = new SelectList(db.apl_Department.ToList(), "Department_Id", "Description");
                ViewBag.Province = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
                ViewBag.District = new SelectList(db.Districts.ToList(), "District_Id", "Description");
                ViewBag.Court = new SelectList(db.Courts.ToList(), "Court_Id", "Description");

                vm = Model.GetPCMSentenseOnEditDetails(Id);
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveSentenseInDatabase(PCMPresentenceDetailsViewModel vm)
        {
            int Id = Convert.ToInt32(vm.Sentence_Id);// pass the right
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
    var result = false;


            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMPresentenceModel Model = new PCMPresentenceModel();

            try
            {
                if (vm.Sentence_Id > 0)
                {

                    Model.UpdateSentenseDetails(vm, Id, userId);
                    result = true;
                }
                else
                {
                    Model.CreateSentenseDeatils(vm, caseid, userId);
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
