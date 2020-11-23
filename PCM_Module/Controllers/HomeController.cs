using Common_Objects;
using Common_Objects.Models;
using Common_Objects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PCM_Module.Controllers
{
    public class HomeController : Controller
    {
        [CustomAuthorize("PCM", "Home", "Index")]
        public ActionResult Index()
        {
            var _db = new SDIIS_DatabaseEntities();
            PCMMainPageWorklistModelVM vm = new PCMMainPageWorklistModelVM();
            PCMWorkListModel model = new PCMWorkListModel();

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
            #region LOAD NEW CASES FROM INTAKE

            // Check for new case from intake and Insert

            model.CheckPCMAssementExistance();

            #endregion


            //Get all the cases assigned to the loggedinuser.
            List<Intake_Assessment> intakeAssesments = _db.Intake_Assessments.Where(x => x.Case_Manager_Id == userId).ToList();

            //Get User worklist for the logged user
            List<PCMSocialWorkerWorkListVM> Worklist = model.PCMGetUserWorkList(userId);

            List<PCMCaseGridMain> PCMOldworkList = model.GetPCMWorkList();

            List<PCMSocialWorkerWorkListVM> newWorklist = model.GetPCMUserNewWorkList(userId);

            List<PCMSocialWorkerEndpointCasesVM> newAllocatedCaseslist = model.GetNewEndpointAlloctedCasesList(userId);

            // All new work list assigned to user
            vm.PCMNewCasez = newWorklist;

            vm.PCMEndPointAllocatedCasez = newAllocatedCaseslist;

            //All the cases currently assigned and working on

            vm.PCMCurrentCases = Worklist;

            //All the adoption cases completed

            vm.PCMClients_Case_List = PCMOldworkList;
      

            return View(vm);
        }


        public ActionResult PCMSearch()
        {
            var currentUser = (User)Session["CurrentUser"];
            var userProvince = -1;
            var userId = -1;

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
            // get client list
            var caseViewModel = new PCMCaseListViewModel { Client_List = new List<Client>(), PCMClients_Case_List = new List<PCMCaseGridMain>() };
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            if (userProvince == -1)
            {
                // Logged In User is not assigned to a province, what should happen here?
            }
            else
            {

                var intakeAssessmentModel = new IntakeAssessmentModel();
                var assessments = intakeAssessmentModel.GetListOfPCMIntakeAssessments(userProvince, userId);
                PCMWorkListModel model = new PCMWorkListModel();

                var clientAssessments = model.GetListOfPersonsPCM();

                var clientItems = clientAssessments.Select(x => new PCMCaseGridMain()
                { 
                  PersonId = x.Person_Id,
                    FirstName = x.First_Name,
                    LastName = x.Last_Name,
                    IDNumber = x.Identification_Number,
                    AssessmentCount = x.Clients.Any() ? x.Clients.First().Intake_Assessments.Count : 0,
                    PCMNestedItems = !x.Clients.Any() ? new List<PCMCaseGridNested>() : x.Clients.First().Intake_Assessments.Select(y => new PCMCaseGridNested()
                    {

                        PersonId = x.Person_Id,
                        AssessmentId = y.Intake_Assessment_Id,
                        AssessmentDate = y.Assessment_Date,
                        POName = db.Users.Find(y.Assessed_By_Id).Last_Name
            }).ToList()

                }).ToList();

            caseViewModel.PCMClients_Case_List.AddRange(clientItems);

            }


            return PartialView(caseViewModel);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PCMSearch(PCMCaseListViewModel caseViewModel)
        {
            var currentUser = (User)Session["CurrentUser"];
            var userProvince = -1;
            var userId = -1;

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

            caseViewModel.PCMClients_Case_List = new List<PCMCaseGridMain>();
            if (userProvince == -1)
            {
                // Logged In User is not assigned to a province, what should happen here?
            }
            else
            {
                var intakeAssessmentModel = new IntakeAssessmentModel();
                var assessments = intakeAssessmentModel.GetListOfPCMIntakeAssessments(userProvince, userId);
                PCMWorkListModel model = new PCMWorkListModel();
                SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();


                //var clientAssessments = model.GetListOfPersonsPCM();

                var persons = model.GetListOfPersonsPCM();

                //var query = from c in clientAssessments select c;

                var query1 = from p in persons select p;

                if (!string.IsNullOrEmpty(caseViewModel.Search_First_Name))
                    query1 = query1.Where(p => p.First_Name.ToLower().Contains(caseViewModel.Search_First_Name.ToLower()));

                if (!string.IsNullOrEmpty(caseViewModel.Search_Last_Name))
                    query1 = query1.Where(p => p.Last_Name.ToLower().Contains(caseViewModel.Search_Last_Name.ToLower()));

                if (!string.IsNullOrEmpty(caseViewModel.Search_ID_Number))
                    query1 = query1.Where(p => p.Identification_Number.Contains(caseViewModel.Search_ID_Number));
                


                var filteredResults = query1.ToList();
                var clientItems = filteredResults.Select(x => new PCMCaseGridMain()
                {
                    PersonId = x.Person_Id,
                    FirstName = x.First_Name,
                    LastName = x.Last_Name,
                    IDNumber=x.Identification_Number,
                    AssessmentCount = x.Clients.Any() ? x.Clients.First().Intake_Assessments.Count : 0,
                    PCMNestedItems = !x.Clients.Any() ? new List<PCMCaseGridNested>() : x.Clients.First().Intake_Assessments.Select(y => new PCMCaseGridNested()
                    {
                        PersonId = x.Person_Id,
                        AssessmentId = y.Intake_Assessment_Id,
                        AssessmentDate = y.Assessment_Date,
                        POName = db.Users.Find(y.Assessed_By_Id).Last_Name
                    }).ToList()

                }).ToList().Distinct();

                caseViewModel.PCMClients_Case_List.AddRange(clientItems);
                     

            }
            return PartialView(caseViewModel);
        }

        public ActionResult Endpoint()
        {
            ViewBag.Message = "REPORT CONTENT COMING SOON...........................";

            return PartialView();
        }

        public ActionResult AcceptPCMCase(PCMSocialWorkerWorkListVM update, string id)
        {

            var currentUser = (User)Session["CurrentUser"];
            var userProvince = -1;
            var userId = -1;

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
            // Accetp Case Allocated 
            int assessmentId = Convert.ToInt32(id);
            var _db = new SDIIS_DatabaseEntities();
            var currentCasesCount = _db.PCM_Case_Details.Count();
            var nextSeqNumber = currentCasesCount + 1;
            var assessment = _db.Intake_Assessments.Where(x => x.Intake_Assessment_Id == assessmentId).FirstOrDefault();

            var list = new PCMSocialWorkerWorkListVM();
            PCMWorkListModel model = new PCMWorkListModel();

            var IntAss = model.GetAssessment(assessmentId);

            //Call update function..................................................................................................................

            model.UpdatePCMWorkListCase(update, assessmentId);

            return RedirectToAction("Index", "Home", new { assessmentId = assessmentId });
        }



        public ActionResult AcceptPCMEndPointCase(PCMSocialWorkerEndpointCasesVM update, string id)
        {

            var currentUser = (User)Session["CurrentUser"];
            var userProvince = -1;
            var userId = -1;

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
            // Accetp Case Allocated 
            int End_Point_POD_Id = Convert.ToInt32(id);
            var _db = new SDIIS_DatabaseEntities();
      

            var list = new PCMSocialWorkerEndpointCasesVM();
            PCMWorkListModel model = new PCMWorkListModel();
            

            //Call update function..................................................................................................................

            model.UpdatePCMEndpointWorkListCase(update, End_Point_POD_Id, userId);

            return RedirectToAction("Index", "Home", new { End_Point_POD_Id = End_Point_POD_Id });
        }



        #region Print Assessment

        public ActionResult PrintAssessment(string id)
        {
        
            return Redirect("~/Reports/AssessmentReportPcm.aspx?idd=" + id + "&id=" + 0);



        }

        #endregion






        public ActionResult About()
        {
            ViewBag.Message = "REPORT CONTENT COMING SOON...........................";

            return PartialView();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult MenuLayout()
        {
            var currentUser = new User();

            if ((Session["CurrentUser"] == null) && (Request.Cookies[FormsAuthentication.FormsCookieName] != null))
            {
                var authUser = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

                var userModel = new UserModel();
                currentUser = userModel.GetSpecificUser(authUser);

                Session.Remove("CurrentUser");
                Session.Remove("MenuLayout");
                Session.Add("CurrentUser", currentUser);
            }
            else
            {
                if (Session["CurrentUser"] != null)
                {
                    var loggedInUser = (User)Session["CurrentUser"];

                    var userModel = new UserModel();
                    currentUser = userModel.GetSpecificUser(loggedInUser.User_Id);
                }
            }

            var renderMenu = new Menu();

            if (Session["MenuLayout"] == null)
            {
                var menuModel = new MenuModel();
                var menu = menuModel.GetSpecificMenu((int)MenuContainerEnum.PCMMenu);

                var authorizedRoles = new List<Role>();

                authorizedRoles = currentUser.Roles.ToList();

                if (currentUser.Groups.Any())
                {
                    foreach (var group in currentUser.Groups)
                    {
                        var groupRoles = group.Roles.Select(r => r).ToList();
                        authorizedRoles.AddRange(groupRoles);
                    }
                }

                // TODO Add delegation here as well

                var effectiveRoles = authorizedRoles.Distinct().ToList();

                var menuItems = menu.Menu_Items.ToList();

                Helpers.SetAuthorizedRolesVisibility(ref menuItems, effectiveRoles);

                renderMenu = menu.Menu_Items.Any() ? new Menu { Menu_Items = new List<Menu_Item>(menuItems) } : null;

                Session["MenuLayout"] = renderMenu;
            }
            else
            {
                renderMenu = (Menu)Session["MenuLayout"];
            }

            return PartialView("_MenuLayout", renderMenu);
        }
    }
}