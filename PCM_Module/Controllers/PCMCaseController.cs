using Common_Objects;
using Common_Objects.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common_Objects.ViewModels;
using Novacode;
using Spire.Doc;
using System.Diagnostics;

namespace PCM_Module.Controllers
{
    public class PCMCaseController : Controller
    {
        #region Child Details
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

            //instanciate model repositry
            PCMCaseModel caseModel = new PCMCaseModel();

            //instanciate viewmodel
            PCMPersonViewModel personVM = new PCMPersonViewModel();

            //get person id by assessment id
            int personId = caseModel.GetPCMPersonIdByintAssId(id);

            //get person record
            personVM = caseModel.GetPCMPerson(personId);

            int ClientRefid = caseModel.GetClientRefIdssId(id);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            var personIdUpdate = personId;
            if (ClientRef == null || ClientRef == "")
            {
                #region CreateReferenceNumber

                var dbContext = new SDIIS_DatabaseEntities();
                #region ObtainProvinceOfLoggedInUser
                //var currentUser = (User)Session["CurrentUser"];
                var userName = string.Empty;
                var currentUserProvinceId = -1;
                //if (currentUser != null)
                //{
                //    userName = currentUser.User_Name;
                //}
                //if (currentUser.Employees.Any())
                //    userProvince = currentUser.apl_Social_Worker.First().apl_Service_Office.apl_Local_Municipality.District.Province_Id;
                #endregion
                ClientModel clientModel = new ClientModel();
                int client_ID = dbContext.Intake_Assessments.Find(id).Client_Id;
                int module_ID = Convert.ToInt32(dbContext.Problem_Sub_Categories.Find(dbContext.Intake_Assessments.Find(id).Problem_Sub_Category_Id).Module_Id);
                var CheckExist = (from a in dbContext.int_Client_Module_Registration
                                  where a.Client_Id == client_ID && a.Module_Id == module_ID
                                  select a).ToList();
                if (CheckExist.Count() == 0)
                {
                    int ProblemCatId = dbContext.Problem_Sub_Categories.Find(dbContext.Intake_Assessments.Find(id).Problem_Sub_Category_Id).Problem_Category_Id;
                    int_Client_Module_Registration Adopt_int_Client = new int_Client_Module_Registration();
                    //string ProvinceAbbr = dbContext.Provinces.Find(currentUserProvinceId).Abbreviation;
                    Adopt_int_Client.Client_Module_Ref_No = clientModel.CreatePCMReferenceNumber( DateTime.Now.Year.ToString(), ProblemCatId, client_ID);
                    Adopt_int_Client.Client_Id = client_ID;
                    Adopt_int_Client.Module_Id = module_ID;
                    dbContext.int_Client_Module_Registration.Add(Adopt_int_Client);
                    dbContext.SaveChanges();

                    ClientRefid = caseModel.GetClientRefIdssId(id);
                    //Get Module Reference number ....................
                    ClientRef = caseModel.GetClientRef(ClientRefid);
                }
                #endregion

            }

            Session["ClientRef"] = ClientRef;
            ViewBag.ModuleRef = ClientRef;



            //populate dropdown lists
            var idType = caseModel.GetAllIdentificationType();
            var languageType = caseModel.GetAllLanguageType();
            var genderType = caseModel.GetAllGenderType();
            var maritalType = caseModel.GetAllMaritalType();
            var contactType = caseModel.GetAllContactType();
            var religionType = caseModel.GetAllReligionType();
            var nationalityType = caseModel.GetAllNationalityType();
            var disabilityType = caseModel.GetAllDisabilityType();
            var populationGroup = caseModel.GetAllPopulationType();
            //var townType = caseModel.GetAllTownType();

            personVM.Identification_Type = idType;
            personVM.Language_Type = languageType;
            personVM.Gender_Type = genderType;
            personVM.Marital_Type = maritalType;
            personVM.Contact_Type = contactType;
            personVM.Religion_Type = religionType;
            personVM.Nationality_Group = nationalityType;
            personVM.Disability_Group = disabilityType;
            personVM.Population_Group = populationGroup;
            Session["PersonId"] = personIdUpdate;
            Session["Idassessment"] = id;

            return PartialView(personVM);
        }

        [HttpGet]
        public ActionResult UpdatePerson()
        {
            //get current username
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            //initialise model
            PCMCaseModel caseModel = new PCMCaseModel();
            //initialise view model
            PCMPersonViewModel personVM = new PCMPersonViewModel();
            // Load Dropdown List calling the method in the model
            var personIdUpdate = Convert.ToInt32(Session["PersonId"]);
            var Idassessment = Convert.ToInt32(Session["Idassessment"]);

            //populate dropdown lists
            var idType = caseModel.GetAllIdentificationType();
            var languageType = caseModel.GetAllLanguageType();
            var genderType = caseModel.GetAllGenderType();
            var maritalType = caseModel.GetAllMaritalType();
            var contactType = caseModel.GetAllContactType();
            var religionType = caseModel.GetAllReligionType();
            var nationalityType = caseModel.GetAllNationalityType();
            var disabilityType = caseModel.GetAllDisabilityType();
            var populationGroup = caseModel.GetAllPopulationType();
            //var townType = caseModel.GetAllTownType();

            personVM.Identification_Type = idType;
            personVM.Language_Type = languageType;
            personVM.Gender_Type = genderType;
            personVM.Marital_Type = maritalType;
            personVM.Contact_Type = contactType;
            personVM.Religion_Type = religionType;
            personVM.Nationality_Group = nationalityType;
            personVM.Disability_Group = disabilityType;
            personVM.Population_Group = populationGroup;


            return PartialView("Assessment", personVM);
        }

        [HttpPost]
        public ActionResult UpdatePerson(PCMPersonViewModel UpdateCase)
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

            var personIdUpdate = Convert.ToInt32(Session["PersonId"]);
            var Idassessment = Convert.ToInt32(Session["Idassessment"]);


            PCMCaseModel Model = new PCMCaseModel();

            //Call upadte function.........................................
            string myStringuserId = userId.ToString();
            Model.UpdatePersonalDetails(UpdateCase, myStringuserId, personIdUpdate);

            return RedirectToAction("Index", "Assessment", new { Id = Idassessment });
        }

        #endregion

        #region ADD UPDATE PCM CASE DETAILS

        #region GET CASE
        public ActionResult GetPCMCaseDetails(int id)
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


            int PcmCaseID = Model.GetPCMChildCaseIdDetailsByssId(id);
            if (PcmCaseID != 0)
            {
                int ClientRefid = Model.GetClientRefIdssId(id);
                //Get Module Reference number ....................
                string ClientRef = Model.GetClientRef(ClientRefid);
                Session["ClientRef"] = ClientRef;
                ViewBag.ModuleRef = ClientRef;
                PCMCaseDetailsViewModel VM1 = new PCMCaseDetailsViewModel();
                VM1 = Model.GetPCMCaseDetailsByPCMCaseId(PcmCaseID);//.............................?

                //populate dropdown lists

                VM1.Province_List = Model.GetAllProvinces();
                VM1.District_List = Model.GetAllDistrict();
                VM1.SAPS_List = Model.GetAllSAPStation();//..................?
                VM1.Place_Of_Detention_List = Model.GetAllPlaceOfDetention();
                VM1.Court_List = Model.GetAllCourt();
                VM1.LocalMunicipality_List = Model.GetAllLocalMunicipality();
                VM1.Town_List = Model.GetAllTowns();
                VM1.HasLegal_List = Model.GetHasLegal();

                if (Convert.ToInt32(Model.GetPCMCaseDetailsByPCMCaseId(PcmCaseID).Court_Id) != 0)
                {
                    int Courtid = Convert.ToInt32(Model.GetPCMCaseDetailsByPCMCaseId(PcmCaseID).Court_Id);
                    ViewBag.Court = db.Courts.Find(Courtid).Description;

                    //Get District using the court..........................................
                    int DistrictId = (from k in db.Districts
                                      join k1 in db.Courts on k.District_Id equals k1.District_Id
                                      where k1.Court_Id == Courtid
                                      select k.District_Id).FirstOrDefault();

                    ViewBag.district1 = new SelectList(db.Districts.ToList(), "District_Id", "Description", DistrictId);

                    //VM1.District_List = Model.GetAllDistrictByCourtID(DistrictId);

                    //Get Province using the District..........................................

                    int ProvinceId = (from a in db.Districts
                                      join b in db.Provinces on a.Province_Id equals b.Province_Id
                                      where a.District_Id.Equals(DistrictId)
                                      select b.Province_Id).FirstOrDefault();
                    ViewBag.province1 = new SelectList(db.Provinces.ToList(), "Province_Id", "Description", ProvinceId);


                    var list = new SelectList(new[]
                                   {
                                          new{ID="0",Name="- Please Select -"},
                                          new{ID="Yes",Name="Yes"},
                                          new{ID="No",Name="No"},
                                      }, "ID", "Name", 0);
                    ViewBag.List = list;
                }


                // Police Not NULL
                if (Convert.ToInt32(Model.GetPCMCaseDetailsByPCMCaseId(PcmCaseID).SDIISSAPS_Station_Id) != 0)
                {
                    int Sapsid = Convert.ToInt32(Model.GetPCMCaseDetailsByPCMCaseId(PcmCaseID).SDIISSAPS_Station_Id);
                    ViewBag.SAPS = db.SAPS_Stations.Find(Sapsid).Description;

                    //Get Town using the SAPS..........................................
                    int TownId = (from k in db.Towns
                                      join k1 in db.SAPS_Stations on k.Town_Id equals k1.Town_Id
                                  where k1.SAPS_Station_Id == Sapsid
                                      select k.Town_Id).FirstOrDefault();

                    ViewBag.Towns = new SelectList(db.Towns.ToList(), "Town_Id", "Description", TownId);

                    //VM1.District_List = Model.GetAllDistrictByCourtID(DistrictId);

                    //Get local municilaity using the town..........................................

                    int LocalMuniId = (from a in db.Local_Municipalities
                                      join b in db.Towns on a.Local_Municipality_Id equals b.Local_Municipality_Id
                                       where b.Town_Id.Equals(TownId)
                                      select b.Local_Municipality_Id).FirstOrDefault();
                    ViewBag.LocalMuni = new SelectList(db.Local_Municipalities.ToList(), "Local_Municipality_Id", "Description", LocalMuniId);


                    int DistrictIdPolice = (from k in db.Districts
                                      join k1 in db.Local_Municipalities on k.District_Id equals k1.District_Municipality_Id
                                      where k1.Local_Municipality_Id == LocalMuniId
                                            select k.District_Id).FirstOrDefault();

                    ViewBag.districtPolice = new SelectList(db.Districts.ToList(), "District_Id", "Description", DistrictIdPolice);

                   

                    //Get Province using the District..........................................

                    int ProvinceIdPolice = (from a in db.Districts
                                      join b in db.Provinces on a.Province_Id equals b.Province_Id
                                      where a.District_Id.Equals(DistrictIdPolice)
                                      select b.Province_Id).FirstOrDefault();
                    ViewBag.provincePolice = new SelectList(db.Provinces.ToList(), "Province_Id", "Description", ProvinceIdPolice);


                    var list = new SelectList(new[]
                                   {
                                          new{ID="0",Name="- Please Select -"},
                                          new{ID="Yes",Name="Yes"},
                                          new{ID="No",Name="No"},
                                      }, "ID", "Name", 0);
                    ViewBag.List = list;
                }


                Session["Idc1"] = id;
                Session["IntakeassId"] = id;
                return PartialView(VM1);
            }
            else
            {

                int ClientRefid = Model.GetClientRefIdssId(id);
                Session["ClientRefid1"] = ClientRefid;
                // Store client Ref Id in Session      
                int clientrefid = Convert.ToInt32(Session["ClientRefid1"]);
                PCMCaseModel Model1 = new PCMCaseModel();
                PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
                //initialise view model
                PCMCaseDetailsViewModel VMC = new PCMCaseDetailsViewModel();

                VM.Province_List = Model.GetAllProvinces();
                VM.District_List = Model.GetAllDistrict();
                VM.SAPS_List = Model.GetAllSAPStation();//..................?
                VM.Place_Of_Detention_List = Model.GetAllPlaceOfDetention();
                VM.Court_List = Model.GetAllCourt();
                VM.LocalMunicipality_List = Model.GetAllLocalMunicipality();
                VM.Town_List = Model.GetAllTowns();
                VM.HasLegal_List = Model.GetHasLegal();

                var list = new SelectList(new[]
                             {
                                          new{ID="0",Name="- Please Select -"},
                                          new{ID="Yes",Name="Yes"},
                                          new{ID="No",Name="No"},
                                      }, "ID", "Name", 0);
                ViewBag.List = list;

                //funtion that calls insert PCMCase
                Model.InsertPCMCase(VM, id);

                int PcmCaseID1 = Model1.GetPCMChildCaseIdDetailsByssId(id);

                //Call get case PCM Details after create................................................

                VMC = Model1.GetPCMCaseDetailsByPCMCaseId(PcmCaseID1);
                //Load drop downs after create
                VMC.Province_List = Model.GetAllProvinces();
                VMC.District_List = Model.GetAllDistrict();
                VMC.SAPS_List = Model.GetAllSAPStation();//..................?
                VMC.Place_Of_Detention_List = Model.GetAllPlaceOfDetention();
                VMC.Court_List = Model.GetAllCourt();
                VMC.LocalMunicipality_List = Model.GetAllLocalMunicipality();
                VMC.Town_List = Model.GetAllTowns();
                VMC.HasLegal_List = Model.GetHasLegal();


                Session["ClientRefid1"] = null;
                string ClientRef = Model.GetClientRef(clientrefid);
                Session["ClientRef"] = ClientRef;
                ViewBag.ModuleRef = ClientRef;
                Session["NewId"] = id;
                Session["IntakeassId"] = id;
                return PartialView(VMC);
            }

        }


        #endregion

        #region UPDATE CASE DETAILS


        [HttpGet]
        public ActionResult UpdatePCMCase()
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
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();



            //populate dropdown lists
            VM.Province_List = Model.GetAllProvinces();
            VM.District_List = Model.GetAllDistrict();
            VM.SAPS_List = Model.GetAllSAPStation();//..................?
            VM.Place_Of_Detention_List = Model.GetAllPlaceOfDetention();
            VM.Court_List = Model.GetAllCourt();
            VM.LocalMunicipality_List = Model.GetAllLocalMunicipality();
            VM.Town_List = Model.GetAllTowns();
            VM.HasLegal_List = Model.GetHasLegal();

            return PartialView("GetPCMCaseDetails", VM);

        }

        [HttpPost]
        public ActionResult UpdatePCMCase(PCMCaseDetailsViewModel caseVM)
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

            int idcreate = Convert.ToInt32(Session["NewId"]);

            int Idupdate = Convert.ToInt32(Session["Idc1"]);
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
            PCMCaseModel Model = new PCMCaseModel();

            if (ModelState.IsValid)
            {
                // Call Update Function
                Model.UpdatePCMCase(caseVM, userId, Id);
                ViewBag.Message = "Updated successfully";
            }
            else
            {
                ViewBag.Message = "not updated ";
            }
            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("Index", "Assessment", new { Id = Id });
        }

        public ActionResult checkAssessmentId(int assID)
        {
            string loginName = User.Identity.Name;
            Session["LoginName"] = loginName;

            ////instanciate model
            PCMCaseModel pcmcasemodel = new PCMCaseModel();

            ////instanciate viewmodel
            PCMCaseDetailsViewModel pcmcasevm = new PCMCaseDetailsViewModel();

            int IntAss = pcmcasemodel.GetPCMChildCaseIdDetailsByssId(assID);
            if (IntAss != 0)
            {
                return PartialView(pcmcasevm);
            }
            else
            {
                return PartialView(pcmcasevm);
            }

            //return PartialView(pcmcasevm);
        }


        #endregion

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

        public JsonResult SAPSStationList(int id)
        {
            var city = from c in db.SAPS_Stations
                       where c.Town_Id == id
                       select c;
            return Json(new SelectList(city.ToArray(), "PCMSAPS_Station_Id", "Description"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TownList(int id)
        {
            var city = from c in db.Towns
                       join b in db.Local_Municipalities on c.Local_Municipality_Id equals b.Local_Municipality_Id
  
                       join e in db.Districts on b.District_Municipality_Id equals e.District_Id

                       where e.District_Id == id
                       select c;
            return Json(new SelectList(city.ToArray(), "Town_Id", "Description"), JsonRequestBehavior.AllowGet);
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

        public IList<Common_Objects.Models.SAPS_Station> GetSAPSStation(int TownId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.SAPS_Stations.Where(m => m.Town_Id == TownId).ToList();
        }

        public IList<Common_Objects.Models.Town> GetTown(int LocMunId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Towns.Where(m => m.Local_Municipality_Id == LocMunId).ToList();
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

        public JsonResult GetSAPSStations(string id)
        {
            List<SelectListItem> SAPS = new List<SelectListItem>();
            var SAPSList = this.GetSAPSStation(Convert.ToInt32(id));
            var SAPSData = SAPSList.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.SAPS_Station_Id.ToString(),
            });
            return Json(SAPSData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTowns(string id)
        {
            List<SelectListItem> SAPS = new List<SelectListItem>();
            var TownList = this.GetTown(Convert.ToInt32(id));
            var TownData = TownList.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.Town_Id.ToString(),
            });
            return Json(TownData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion

        #region ADD UPDATE ASESSMENT REGISTER

        public ActionResult AssessmentList()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            //populate dropdown lists
            VM.Form_Of_Notification_List = Model.GetAllNotificationType();
            ViewBag.FormOfNotification = new SelectList(db.apl_Form_Of_Notification, "Form_Of_Notification_Id", "Description", "Put your selected value here");

            return PartialView(VM);
        }
        public JsonResult List()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetAssmentRegisterList(caseid).Select(x => new PCMCaseDetailsViewModel
            
            {
                Assesment_Register_Id = x.Assesment_Register_Id,
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                Assessment_Date = x.Assessment_Date,
                Assessment_Time = x.Assessment_Time,
                FormOfNotificationDescription = x.FormOfNotificationDescription
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAssessmentById(int AssesmentRegisterId)
        {



            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCMCaseModel Model = new PCMCaseModel();
                PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();
                vm.Form_Of_Notification_List = Model.GetAllNotificationType();
                vm = Model.GetPCMAssessmentRegDetails(AssesmentRegisterId);
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveDataInDatabase(PCMCaseDetailsViewModel vm)
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
            var result = false;
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            int Assid = vm.Assesment_Register_Id;
            PCMCaseModel Model = new PCMCaseModel();
            try
            {
                if (vm.Assesment_Register_Id > 0)
                {

                    Model.UpdatePCMAssesmentRegister(vm, Assid, userId);
                    result = true;
                }
                else
                {
                    Model.CreatePCMAssesmentRegister(vm, caseid, userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTableAsessmentreg(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecFromTableAsessmentreg(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        #endregion

        #region ADD UPDATE MEDICAL INFOMATION
        public ActionResult MedicaldeatailsList()
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
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            VM.Health_Status_List = Model.GetHealth();
            ViewBag.Health = new SelectList(db.apl_Health_Status.ToList(), "Health_Status_Id", "Description");

            return PartialView(VM);
        }
        public JsonResult ListMedication()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetHealthstatusList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                Health_Details_Id = x.Health_Details_Id,
                HealthStatusDescription = x.HealthStatusDescription,
                AllergyDescription = x.AllergyDescription,
                Injuries = x.Injuries,
                Medical_Appointments = x.Medical_Appointments,
                Medication = x.Medication,
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetMedicalById(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCMCaseModel Model = new PCMCaseModel();
                PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();
                ViewBag.Health = new SelectList(db.apl_Health_Status.ToList(), "Health_Status_Id", "Description");
                vm = Model.GetPCMHealthOnEditDetails(Id);
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult SaveMedicalInDatabase(PCMCaseDetailsViewModel vm)
        {
            int healthid = Convert.ToInt32(vm.Health_Details_Id);
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
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                if (vm.Health_Details_Id > 0)
                {

                    Model.UpdatePCMHealthDetails(vm, healthid, userId);
                    result = true;
                }
                else
                {
                    Model.CreatePCMHealthDeatils(vm, caseid, userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTableME(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecFromTableME(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }
        #endregion

        #region ADD UPDATE EDUCATION/////////DIFFERENCE WITH INTAKE ONE?

        public ActionResult EducationdeatailsList()
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
            }
                string ClientRef = Convert.ToString(Session["ClientRef"]);
                ViewBag.ModuleRef = ClientRef;
                int caseid = Convert.ToInt32(Session["IntakeassId"]);
                PCMCaseModel Model = new PCMCaseModel();

                //initialise view model
                PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

                List<PCMCaseDetailsViewModel> List = Model.GetEducationList(caseid).Select(x => new PCMCaseDetailsViewModel
                {
                    Intake_Assessment_Id = x.Intake_Assessment_Id,
                    School_Id = x.School_Id,
                    School_Name = x.School_Name,
                    Contact_Person = x.Contact_Person,
                    Telephone_Number = x.Telephone_Number,
                    Year_Completed = x.Year_Completed,
                    Date_Last_Attended = x.Date_Last_Attended,
                    Grade_Completed = x.Grade_Completed,
                    Person_Education_Id = x.Person_Education_Id
                }).ToList();

                return PartialView(VM);
            
        }

        #endregion

        #region ADD UPDATE RELATIONSHIPS ////////DIFFERENCE WITH INTAKE ONE?

        public ActionResult GetRelationItemsByAjax()
        {
            string relationTypeId = "-1";
            var personModel = new PersonModel();
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;
            PCMCaseModel caseModel = new PCMCaseModel();
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            int personId = caseModel.GetPCMPersonIdByintAssId(caseid);
            var person = personModel.GetSpecificPerson(personId);

            var relationItems = new List<IntakeRelationItem>();

            var client = person.Clients.FirstOrDefault();
            if (string.IsNullOrEmpty(relationTypeId))
                relationTypeId = "-1";

            if (client != null)
            {
                if ((relationTypeId == "-1") || (int.Parse(relationTypeId) == (int)RelationTypeEnum.AdoptiveParents))
                {
                    relationItems.AddRange(from p in client.Client_Adoptive_Parents.ToList()
                                           select new IntakeRelationItem()
                                           {
                                               Item_Id = p.Client_Adoptive_Parent_Id,
                                               Person_Id = p.Person_Id,
                                               Relation_Person = p.Person,
                                               //Relation_Address = p.Address,
                                               Relation_Type_Id = (int)RelationTypeEnum.AdoptiveParents,
                                               Relation_Type_Description = "Adoptive Parent"
                                           });
                }
                if ((relationTypeId == "-1") || (int.Parse(relationTypeId) == (int)RelationTypeEnum.BiologicalParents))
                {
                    relationItems.AddRange(from p in client.Client_Biological_Parents.ToList()
                                           select new IntakeRelationItem()
                                           {
                                               Item_Id = p.Client_Biological_Parent_Id,
                                               Person_Id = p.Person_Id,
                                               Relation_Person = p.Person,
                                               //Relation_Address = p.Address,
                                               Relation_Type_Id = (int)RelationTypeEnum.BiologicalParents,
                                               Relation_Type_Description = "Biological Parent"
                                           });
                }
                if ((relationTypeId == "-1") || (int.Parse(relationTypeId) == (int)RelationTypeEnum.FamilyMembers))
                {
                    relationItems.AddRange(from p in client.Client_Family_Members.ToList()
                                           select new IntakeRelationItem()
                                           {
                                               Item_Id = p.Client_Family_Member_Id,
                                               Person_Id = p.Person_Id,
                                               Relation_Person = p.Person,
                                               Relation_Type_Id = (int)RelationTypeEnum.FamilyMembers,
                                               Relation_Type_Description = "Family Member"
                                           });
                }
                if ((relationTypeId == "-1") || (int.Parse(relationTypeId) == (int)RelationTypeEnum.FosterParents))
                {
                    relationItems.AddRange(from p in client.Client_Foster_Parents.ToList()
                                           select new IntakeRelationItem()
                                           {
                                               Item_Id = p.Client_Foster_Parent_Id,
                                               Person_Id = p.Person_Id,
                                               Relation_Person = p.Person,
                                               //Relation_Address = p.Address,
                                               Relation_Type_Id = (int)RelationTypeEnum.FosterParents,
                                               Relation_Type_Description = "Foster Parent"
                                           });
                }
                if ((relationTypeId == "-1") || (int.Parse(relationTypeId) == (int)RelationTypeEnum.Caregivers))
                {
                    relationItems.AddRange(from p in client.Client_CareGivers.ToList()
                                           select new IntakeRelationItem()
                                           {
                                               Item_Id = p.Client_Caregiver_Id,
                                               Person_Id = p.Person_Id,
                                               Relation_Person = p.Person,
                                               //Relation_Address = p.Address,
                                               Relation_Type_Id = (int)RelationTypeEnum.Caregivers,
                                               Relation_Type_Description = "Caregiver"
                                           });
                }
            }

            return PartialView(relationItems);
        }


        #endregion

        #region ADD UPDATE SOCIO ECONOMICS

        public ActionResult GetSocioEconomics()
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
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;
            PCMCaseModel Model = new PCMCaseModel();
            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                int SocioEconomyid = Model.GetPCMSocioEconomicDetailsByassId(intassid);
                if (SocioEconomyid != 0)
                {

                    VM = Model.GetSocioEconomicsList(SocioEconomyid);
                    Session["socupdate"] = SocioEconomyid;

                    return PartialView(VM);
                }
                else
                {
                    PCMCaseModel Model1 = new PCMCaseModel();
                    PCMCaseDetailsViewModel VM1 = new PCMCaseDetailsViewModel();
                    //initialise view model
                    PCMCaseDetailsViewModel VMC = new PCMCaseDetailsViewModel();

                    //funtion that calls insert Socio Economy
                    Model1.InsertSocioEconomy(VM1, intassid, userId);
                    int SocioEconomyidadd = Model.GetPCMSocioEconomicDetailsByassId(intassid);
                    VM1 = Model1.GetSocioEconomicsList(SocioEconomyidadd);
                    Session["socupadd"] = SocioEconomyidadd;
                    return PartialView("GetSocioEconomics", VM1);
                }

            }
        }
        [HttpGet]
        public ActionResult UpdateSocioEconomy()
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
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            return PartialView("GetSocioEconomics", VM);

        }

        [HttpPost]
        public ActionResult UpdateSocioEconomy(PCMCaseDetailsViewModel VM)
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
            int idcreate = Convert.ToInt32(Session["socupadd"]);

            int Idupdate = Convert.ToInt32(Session["socupdate"]);
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
            PCMCaseModel Model = new PCMCaseModel();


            // Call Update Function
            Model.UpdateSocioEconomy(VM, userId, Id);
            ViewBag.Message = "Updated successfully";

            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }


        #endregion

        #region ADD UPDATE DEVELOPMENTAL ASSESSMENT

        public ActionResult GetDevAssessment()
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
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;
            PCMCaseModel Model = new PCMCaseModel();
            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                int DevAssid = Model.GetPCMDevelopmentAssessmentByassId(intassid);
                if (DevAssid != 0)
                {

                    VM = Model.GetDevelopmentAssessmentList(DevAssid);
                    Session["Devupdate"] = DevAssid;

                    return PartialView(VM);
                }
                else
                {
                    PCMCaseModel Model1 = new PCMCaseModel();
                    PCMCaseDetailsViewModel VM1 = new PCMCaseDetailsViewModel();
                    //initialise view model
                    PCMCaseDetailsViewModel VMC = new PCMCaseDetailsViewModel();

                    //funtion that calls insert Socio Economy
                    Model1.InsertDevelopmentAssessment(VM1, intassid, userId);
                    int DevAssidadd = Model.GetPCMDevelopmentAssessmentByassId(intassid);
                    VM1 = Model1.GetDevelopmentAssessmentList(DevAssidadd);
                    Session["Deveadd"] = DevAssidadd;
                    return PartialView("GetDevAssessment", VM1);
                }

            }
        }

        public ActionResult UpdateDevAssessment()
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
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            return PartialView("GetDevAssessment", VM);

        }

        [HttpPost]
        public ActionResult UpdateDevAssessment(PCMCaseDetailsViewModel VM)
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
            int idcreate = Convert.ToInt32(Session["Deveadd"]);

            int Idupdate = Convert.ToInt32(Session["Devupdate"]);
            int Ids;
            if (idcreate != 0)
            {

                Ids = idcreate;
            }
            else
            {
                Ids = Idupdate;
            }


            //instanciate model repositry
            PCMCaseModel Model = new PCMCaseModel();


            // Call Update Function
            Model.UpdateDevelopmentAssessment(VM, userId, Ids);
            ViewBag.Message = "Updated successfully";

            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }


        #endregion

        #region ADD UPDATE RECOMENDATIONS

        public ActionResult Recomendation()
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
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.RecommendationTyp_List = assModel.GetRecommendationType();
            assVM.PlacementType_List = assModel.GetPlacementType();
            assVM.RecomendationOrder_List = assModel.GetOrder();

            return PartialView("Recomendation", assVM);
        }
        public ActionResult GetRecomendationDetails()
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

            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                int recAssid = assModel.GetPCMRecommendationByassId(intassid);
                if (recAssid != 0)
                {


                    assVM = assModel.GetRecomendationDetailsList(recAssid);
                    assVM.RecommendationTyp_List = assModel.GetRecommendationType();
                    assVM.PlacementType_List = assModel.GetPlacementType();
                    assVM.RecomendationOrder_List = assModel.GetOrder();
                    Session["recupdate"] = recAssid;

                    return PartialView(assVM);
                }
                else
                {
                    PCMCaseModel Model1 = new PCMCaseModel();
                    PCMCaseDetailsViewModel VM1 = new PCMCaseDetailsViewModel();
                    //initialise view model
                    PCMCaseDetailsViewModel VMC = new PCMCaseDetailsViewModel();

                    //funtion that calls insert Socio Economy
                    VM1.RecommendationTyp_List = Model1.GetRecommendationType();
                    VM1.PlacementType_List = Model1.GetPlacementType();
                    VM1.RecomendationOrder_List = Model1.GetOrder();

                    Model1.CreatePCMRecomendationDeatils(VM1, intassid, userId);
                    int recAssidadd = assModel.GetPCMRecommendationByassId(intassid);
                    VM1 = Model1.GetRecomendationDetailsList(recAssidadd);
                    Session["recadd"] = recAssidadd;
                    return PartialView("Recomendation", VM1);
                }

            }
        }

        public ActionResult UpdatePCMRecomendationDetails()
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
            PCMCaseModel assModel = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.RecommendationTyp_List = assModel.GetRecommendationType();
            assVM.PlacementType_List = assModel.GetPlacementType();
            assVM.RecomendationOrder_List = assModel.GetOrder();

            return PartialView("Recomendation", assVM);

        }

        [HttpPost]
        public ActionResult UpdatePCMRecomendationDetails(PCMCaseDetailsViewModel VM)
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
            int idcreate = Convert.ToInt32(Session["recadd"]);

            int Idupdate = Convert.ToInt32(Session["recupdate"]);
            int Ids;
            if (idcreate != 0)
            {

                Ids = idcreate;
            }
            else
            {
                Ids = Idupdate;
            }


            //instanciate model repositry
            PCMCaseModel Model = new PCMCaseModel();


            // Call Update Function
            Model.UpdatePCMRecomendationDetails(VM, Ids, userId);
            ViewBag.Message = "Updated successfully";

            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        public ActionResult GetPlacementDetails()
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


            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.RecommendationTyp_List = assModel.GetRecommendationType();
            assVM.PlacementType_List = assModel.GetPlacementType();
            assVM.RecomendationOrder_List = assModel.GetOrder();

            return PartialView("Recomendation", assVM);
        }

        public ActionResult GetServiceProviderdetails()
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

            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.RecommendationTyp_List = assModel.GetRecommendationType();
            assVM.PlacementType_List = assModel.GetPlacementType();
            assVM.RecomendationOrder_List = assModel.GetOrder();

            return PartialView("Recomendation", assVM);
        }

        //...............................Order Details..............................................................................
        public ActionResult GetOrderdetails()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.RecomendationOrder_List = assModel.GetOrder();
            ViewBag.OrderList = new SelectList(db.apl_PCM_Orders.ToList(), "Recomendation_Order_Id", "Description");

            return PartialView(assVM);
        }

        [HttpPost]
        public ActionResult Createorder(PCMCaseDetailsViewModel vm)
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
    PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();

            // get recomendation Id for insert..................
            int RecId = assModel.GetPCMRecomendationByassId(intassid);

            if (vm.Recomendation_Order_Id != null)
            {
                var selectedItems = db.apl_PCM_Orders.Where(p => vm.Recomendation_Order_Id.Contains((p.Recomendation_Order_Id))).ToList();



                if (selectedItems != null)
                {
                    foreach (var selectedItem in selectedItems)
                    {

                        int Recomendation_Order_Id = selectedItem.Recomendation_Order_Id;
                        assModel.CreatePCMOrderDeatils(vm, RecId, Recomendation_Order_Id, userId);

                    }

                }


            }

            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        public ActionResult GetSelectedOrdersFromDB()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            int RecId = assModel.GetPCMRecomendationByassId(intassid);
            if (RecId != 0)
            {
                List<PCMCaseDetailsViewModel> OurList = assModel.GetSelectedOrdersFromDB(RecId);
                return PartialView(OurList);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteRecFromTable(int id)
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
    PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            if (id != 0)
            {
                try
                {
                    assModel.DeleteRecord(id);
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";
            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }


        #endregion

        #region ADD UPDATE RECOMMENDATION DIVESIONS

        public ActionResult GetServiceProviderForDivesion()
        {
            int intassid = Convert.ToInt32(Session["IntakeassId"]);

            int orgidSession = Convert.ToInt32(Session["orgid"]);
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.OrganisationType_List = assModel.GetAllOrganisationType();
            assVM.PCMOrganisation_List = assModel.GetAllPCMOrganisation();
            assVM.LocalMunicipality_List = assModel.GetAllLocalMunicipality();

            assVM.DiversionProgrammes_List = assModel.GetAllDiversion_Programmes();
            ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description");
            ViewBag.Local_Municipality_List = new SelectList(db.Local_Municipalities.ToList(), "Local_Municipality_Id", "Description");


            return PartialView(assVM);


        }

        [HttpPost]
        public ActionResult CreateDivesion(PCMCaseDetailsViewModel vm)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();

            // get recomendation Id for insert..................
            int RecId = assModel.GetPCMRecomendationByassId(intassid);

            if (vm.Div_Program_Id != null)
            {
                var selectedItems = db.apl_Diversion_Programmes.Where(p => vm.Div_Program_Id.Contains((p.Div_Program_Id))).ToList();



                if (selectedItems != null)
                {
                    foreach (var selectedItem in selectedItems)
                    {

                        int Div_Program_Id = selectedItem.Div_Program_Id;
                        assModel.CreatePCMDivesionsDeatils(vm, RecId, Div_Program_Id, userId);

                    }

                }


            }

            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        public ActionResult GetSelectedProgrammesFromDB()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            int RecId = assModel.GetPCMRecomendationByassId(intassid);
            if (RecId != 0)
            {
                List<PCMCaseDetailsViewModel> OurList = assModel.GetSelectedDivesionFromDB(RecId);
                return PartialView(OurList);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteRecProgrammeFromTable(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            if (id != 0)
            {
                try
                {
                    assModel.DeleteProgrammeRecord(id);
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";
            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }




        public IList<Common_Objects.Models.Local_Municipality> GetLocalMunicipality(int DistrID)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Local_Municipalities.Where(m => m.District_Municipality_Id == DistrID).ToList();
        }
        public IList<Common_Objects.Models.Organization> GetOrganisation(int IdType, int LocMunId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Organizations.Where(m => m.Organisation_Type_Id == IdType && m.Local_Municipality_Id == LocMunId).ToList();
        }
        public IList<Common_Objects.Models.apl_Diversion_Programmes> GetDeveLists(int OrgId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.apl_Diversion_Programmes.Where(m => m.Organization_Id == OrgId).ToList();
        }
        public JsonResult GetOrganisations(string id, string LocMunId)
        {
            List<SelectListItem> organisations = new List<SelectListItem>();
            var OrganisationList = this.GetOrganisation(Convert.ToInt32(id), Convert.ToInt32(LocMunId));
            var OrganisationData = OrganisationList.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.Organization_Id.ToString(),
            });
            return Json(OrganisationData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDivesions(string id)
        {
            List<SelectListItem> Devs = new List<SelectListItem>();
            var devList = this.GetDeveLists(Convert.ToInt32(id));
            var DevData = devList.Select(m => new SelectListItem()
            {
                Text = m.Programme_Name,
                Value = m.Div_Program_Id.ToString(),
            });
            return Json(DevData, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLocalMunicipalities(string id)
        {
            List<SelectListItem> localMunicipalities = new List<SelectListItem>();
            var LocalMunicipalitList = this.GetLocalMunicipality(Convert.ToInt32(id));
            var LocalMunicipalitData = LocalMunicipalitList.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.Local_Municipality_Id.ToString(),
            });
            return Json(LocalMunicipalitData, JsonRequestBehavior.AllowGet);
        }

        //#endregion


        #endregion

        #region ADD UPDATE OFFENCE

        #region  Cascading using Json

        // Json Call to get District

       


        public JsonResult OffenceTypeList(int OffenceCatId, int scheduleId)
        {
            var offencetype = from s in db.apl_Offence_Type
                        where s.Offence_Category_Id == OffenceCatId && s.Offence_Schedule_Id== scheduleId
                              select s;
            return Json(new SelectList(offencetype.ToArray(), "Offence_Type_Id", "Description"), JsonRequestBehavior.AllowGet);
        }
        
        public IList<Common_Objects.Models.apl_Offence_Type> GetOffenceType(int OffenceCatId,  int scheduleId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.apl_Offence_Type.Where(m => m.Offence_Category_Id == OffenceCatId && m.Offence_Schedule_Id == scheduleId).ToList();
        }

        public JsonResult GetOffenceTypes(int Catid, int scid)
        {
            List<SelectListItem> OffenceType = new List<SelectListItem>();
            var OffenceTypeList = this.GetOffenceType(Convert.ToInt32(Catid), Convert.ToInt32(scid));
            var OffenceTypeData = OffenceTypeList.Select(m => new SelectListItem()
            {
                Text = m.Description,
                Value = m.Offence_Type_Id.ToString(),
            });
            return Json(OffenceTypeData, JsonRequestBehavior.AllowGet);
        }
        

        #endregion

        public ActionResult OffencedeatailsList()
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

        public JsonResult GetOffenceById(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCMCaseModel Model = new PCMCaseModel();
                PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();
                vm.Offence_List = Model.GetOffenceCategory();
                vm.OffenseSchedules_List = Model.GetOffenceSchedules();
                vm.OffenceType_List = Model.GetOffenceType();
                ViewBag.OffenceCategory = new SelectList(db.Offence_Categories.ToList(), "Offence_Category_Id", "Description");
                ViewBag.OffenceType = new SelectList(db.apl_Offence_Type.ToList(), "Offence_Type_Id", "Description");
                ViewBag.OffenceSchedule = new SelectList(db.apl_Offense_Schedules.ToList(), "Offence_Schedule_Id", "Description");
                vm = Model.GetPCMOffenceOnEditDetails(Id);
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }
        
        public JsonResult SaveOffenceInDatabase(PCMCaseDetailsViewModel vm)
        {
            int Id = Convert.ToInt32(vm.PCM_Offence_Id);
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
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                if (vm.PCM_Offence_Id > 0)
                {

                    Model.UpdatePCMOffenceDetails(vm, Id, userId);
                    result = true;
                }
                else
                {
                    Model.CreatePCMOffenceDeatils(vm, caseid, userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTableOff(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecFromTableOff(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }
        #endregion

        #region ADD UPDATE CHARGES

        public ActionResult GetChargedetails()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.Charge_List = assModel.GetAllCharges();
            ViewBag.ChargeList = new SelectList(db.apl_PCM_Charges.ToList(), "Charge_Id", "Charge_Description");

            return PartialView(assVM);
        }

        [HttpPost]
        public ActionResult CreateCharge(PCMCaseDetailsViewModel vm)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();

            // get recomendation Id for insert..................
            int RecId = assModel.GetPCMChargeByassId(intassid);

            if (vm.Charge_Id != null)
            {
                var selectedItems = db.apl_PCM_Charges.Where(p => vm.Charge_Id.Contains((p.Charge_Id))).ToList();



                if (selectedItems != null)
                {
                    foreach (var selectedItem in selectedItems)
                    {

                        int Charge_Id = selectedItem.Charge_Id;
                        assModel.CreatePCMChargeDeatils(vm, RecId, Charge_Id, userId);

                    }

                }


            }

            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        public ActionResult GetSelectedChargeFromDB()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            int RecId = assModel.GetPCMChargeByassId(intassid);
            if (RecId != 0)
            {
                List<PCMCaseDetailsViewModel> OurList = assModel.GetSelectedChargeFromDB(RecId);
                return PartialView(OurList);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteRecChargeFromTable(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            if (id != 0)
            {
                try
                {
                    assModel.DeleteChargeRecord(id);
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }





        #endregion

        #region ADD UPDATE FACILITY BED SPACE
        
        public ActionResult GetFacilitySpacedetails()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            assVM.Province_List = assModel.GetAllProvinces();
            ViewBag.provinceList = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            assVM.AdmissionType_List = assModel.GetAdmissionType();
            assVM.RequestStatus_List = assModel.GetBedSpaceRequeststatus();

            ViewBag.AdmissionReasonList = new SelectList(db.apl_Admission_Type.ToList(), "Admission_Type_Id", "Description");

            ViewBag.RequeststatusList = new SelectList(db.apl_BedSpace_Request.ToList(), "Request_Status_Id", "Description");


            return PartialView(assVM);
        }

        public JsonResult ListFacility()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetFacilityFormaloadList().Select(x => new PCMCaseDetailsViewModel
            {
                Facility_Id = x.Facility_Id,
                ProvinceDescription = x.ProvinceDescription,
                SelectFacility = x.SelectFacility,
                FacilityTell = x.FacilityTell,
                Facilityemail = x.Facilityemail,
                FacilityOfficialCapacity = x.FacilityOfficialCapacity
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ListFacilitybyProvince(int id)
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetFacilityList(id).Select(x => new PCMCaseDetailsViewModel
            {
                Facility_Id = x.Facility_Id,
                ProvinceDescription = x.ProvinceDescription,
                SelectFacility = x.SelectFacility,
                FacilityTell = x.FacilityTell,
                Facilityemail = x.Facilityemail,
                FacilityOfficialCapacity = x.FacilityOfficialCapacity
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListMaleSpace(int fid)
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetMaleSpaceList(fid).Select(x => new PCMCaseDetailsViewModel
            {
                Male_Total_Space = x.Male_Total_Space,
                Male_Available_Space = x.Male_Available_Space,
                Male_Used_Space = x.Male_Used_Space
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }


        public JsonResult ListFemaleSpace(int fid)
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetFemaleSpaceList(fid).Select(x => new PCMCaseDetailsViewModel
            {

                Female_Total_Space = x.Female_Total_Space,
                Female_Available_Space = x.Female_Available_Space,
                Female_Used_Space = x.Female_Used_Space

            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ListProgrammes(int fid)
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetFacilityProgramList(fid).Select(x => new PCMCaseDetailsViewModel
            {
                
                ProgramNames = x.ProgramNames,
                ProgramDescription = x.ProgramDescription,
                ProgramStartDate = x.ProgramStartDate,
                ProgramEndDate = x.ProgramEndDate,
                ProgramCapacity = x.ProgramCapacity
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetFacilityById(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCMCaseModel Model = new PCMCaseModel();
                PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();
                vm.Province_List = Model.GetAllProvinces();
                ViewBag.provinceList = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
                vm.AdmissionType_List = Model.GetAdmissionType();
                vm.RequestStatus_List = Model.GetBedSpaceRequeststatus();

                ViewBag.AdmissionReasonList = new SelectList(db.apl_Admission_Type.ToList(), "Admission_Type_Id", "Description");

                ViewBag.RequeststatusList = new SelectList(db.apl_BedSpace_Request.ToList(), "Request_Status_Id", "Description");
                vm = Model.GetFacilityMailList(Id);
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }


        [HttpPost]
        public JsonResult SavebedspacerequestInDatabase(PCMCaseDetailsViewModel vm)
        {
            int Id = Convert.ToInt32(vm.PCM_Offence_Id);
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
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                if (vm.Facility_Id > 0)
                {

                    Model.CreateFacilitybedSpaceDeatils(vm, caseid, userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult GetSelectedFacilitySpaceFromDB()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            int RecId = assModel.GetPCMChargeByassId(intassid);
            if (RecId != 0)
            {
                List<PCMCaseDetailsViewModel> OurList = assModel.GetSelectedChargeFromDB(RecId);
                return PartialView(OurList);
            }
            return PartialView();
        }

        [HttpPost]
        public ActionResult DeleteRecFacilitySpaceFromTable(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            if (id != 0)
            {
                try
                {
                    assModel.DeleteChargeRecord(id);
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }
        #endregion

        #region REPLY TO BED SPACE
        
        public ActionResult GetFacilityBedSpaceRequestdetails()
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();

            return PartialView(assVM);
        }

        public ActionResult ListBedSpaceRequest()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetFacilitybedSpaceList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Request_Id = x.Request_Id,
                selectBedRequestStatus = x.selectBedRequestStatus,
                selectAdmissionType = x.selectAdmissionType,
                SelectFacility = x.SelectFacility,
                Request_Comments = x.Request_Comments,
                Date_Created = x.Date_Created
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult AcceptDeclineBedspace(PCMCaseDetailsViewModel assVM ,int id, string acceptdecline)
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
            PCMCaseModel assModel = new PCMCaseModel();

           
            var result = false;
            if (id != 0)
            {

                try
                {

                   


                        assModel.UpdateFacilitybedSpaceAcceptDetails(assVM, id, userId, acceptdecline);
                        result = true;
                

                    
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }
        #endregion

        #region ADD UPDATE PREVIOUS ENVOLVEMENT
        public ActionResult GetPreviousEnvolvementList()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            VM.Offence_List = Model.GetOffenceCategory();
            ViewBag.OffenceCategory = new SelectList(db.Offence_Categories.ToList(), "Offence_Category_Id", "Description");

            VM.WhenEscaped_List = Model.GetEscapePeriod();
            ViewBag.WhenEscaped = new SelectList(db.apl_Escape_Period.ToList(), "When_Escaped_Id", "Description");

            VM.HasLegal_List = Model.GetHasLegal();
            ViewBag.HasLegal = new SelectList(db.apl_PCM_Has_Legal_Representive.ToList(), "HasLegal_Id", "Description");

            return PartialView(VM);
        }
        public JsonResult ListPreviousEnvolvement()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetPreviousEnvolvementList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                Involvement_Id = x.Involvement_Id,
                Previous_Involved = x.Previous_Involved,
                IsArrest = x.IsArrest,
                selectOffenceCategory = x.selectOffenceCategory,
                PreviousArrest_Date = x.PreviousArrest_Date,
                IsConvicted = x.IsConvicted,
                previousConviction_Date = x.previousConviction_Date,
                IsEscape = x.IsEscape,
                Escapes_Date = x.Escapes_Date
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPreviousEnvolvementById(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCMCaseModel Model = new PCMCaseModel();
                PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();
                ViewBag.Offence_Category = new SelectList(db.Offence_Categories.ToList(), "Offence_Category_Id", "Description");
                ViewBag.WhenEscaped = new SelectList(db.apl_Escape_Period.ToList(), "When_Escaped_Id", "Description");
                vm = Model.GetPreviousEnvolvementOnEditDetails(Id);
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SavePreviousEnvolvementInDatabase(PCMCaseDetailsViewModel vm)
        {
            int Id = Convert.ToInt32(vm.Involvement_Id);
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
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                if (vm.Involvement_Id > 0)
                {

                    Model.UpdatePreviousEnvolvementDetails(vm, Id, userId);
                    result = true;
                }
                else
                {
                    Model.CreatePreviousEnvolvementDeatils(vm, caseid, userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTablePR(int id)
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

            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecFromTablePR(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        #endregion

        #region VICTIM DETAILS

        public ActionResult VictimdeatailsList()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            VM.Province_List = Model.GetAllProvinces();
            VM.District_List = Model.GetAllDistrict();
         

            return PartialView(VM);
        }
        public JsonResult ListVictim()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetVictimList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                Victim_Id = x.Victim_Id,
                Victim_First_Name = x.Victim_First_Name,
                Victim_Last_Name = x.Victim_Last_Name,
                Victim_Age = x.Victim_Age,
                Victim_Phone_Number = x.Victim_Phone_Number
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ListPerson()
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
            PCMCaseModel Model = new PCMCaseModel();
            PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();


            return PartialView(vm);



        }
        public JsonResult getPersonByLastName(string Id)
        {

            var result = from r in db.Persons
                         where r.Last_Name == Id
                         select new { r.Person_Id, r.First_Name, r.Last_Name, r.Phone_Number, r.Age };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveVictimInDatabase(PCMCaseDetailsViewModel vm, int Id)
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
            var result = false;


            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                Model.CreateVictimDeatils(vm, caseid, Id, userId);
                result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTableVictim(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecordVictim(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }


        public ActionResult Persondeatails()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            VM.Gender_List = Model.GetAllGender();
            //VM.Province_List = Model.GetAllProvinces();
            //VM.District_List = Model.GetAllDistrict();
            ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description");

            return PartialView(VM);
        }

        [HttpPost]
        public ActionResult SavePersonDetails(PCMCaseDetailsViewModel personDetail)
        {
            
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
            var result = false;

            try
            {
                var personModel = new PCMCaseModel();
             
                    var dateCreated = DateTime.Now;
                    //string createdBy = Convert.ToInt32 (userId);
                    const bool isActive = true;
                    const bool isDeleted = false;

                    PCMCaseModel Model = new PCMCaseModel();

                    Model.PersonCreate_Victim(personDetail, caseid,Convert.ToString(userId), isActive, isDeleted);


                
                result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }



        #region VICTIM ORGANISATION

        public ActionResult ListOrganisationVictim()
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
            PCMCaseModel Model = new PCMCaseModel();
            PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();

            ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description");

        
            return PartialView(vm);



        }

        public JsonResult OrganisationVictimList()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetVictimOrganisationList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                OrganisationVictim_Id = x.OrganisationVictim_Id,
                OrganisationName = x.OrganisationName,
                ContactPersonfirstname = x.ContactPersonfirstname,
                ContactPersonlastname = x.ContactPersonlastname,
                Victim_Occupation = x.Victim_Occupation,
                OrganisationTell = x.OrganisationTell
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetOrganisationVictimById(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCMCaseModel Model = new PCMCaseModel();
                PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();
               
                vm = Model.GeVictimOrganisationOnEditDetails(Id);
                
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult SaveOrganisationVictimDatabase(PCMCaseDetailsViewModel vm)
        {
            int Id = Convert.ToInt32(vm.OrganisationVictim_Id);
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
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                if (vm.OrganisationVictim_Id > 0)
                {

                    Model.UpdateVictimOrganisationDetails(vm, Id, userId);
                    result = true;
                }
                else
                {
                    Model.CreateVictimOrganisationDeatils(vm, caseid, userId);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTableVictimOrg(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecordVictimOrg(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }


        #endregion

        #endregion

        #region ADD UPDATE CO ACCUSED
        public ActionResult GetCoAccusedList()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();


            return PartialView(VM);
        }
        public ActionResult ListPersonCoAccused()
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
            PCMCaseModel Model = new PCMCaseModel();
            PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();


            return PartialView(vm);



        }
        public JsonResult ListCoAccused()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetCoAccusedList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                Co_Accused_Id = x.Co_Accused_Id,
                Co_Accused_First_Name = x.Co_Accused_First_Name,
                Co_Accused_Last_Name = x.Co_Accused_Last_Name,
                Cubacc = x.Cubacc,

            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }
        
        public JsonResult SaveCoAccusedInDatabase(PCMCaseDetailsViewModel vm, int Id)
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
            var result = false;


            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                Model.CreateCoAccusedDeatils(vm, caseid, Id, userId);
                result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTableCoAccused(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecordCoAccused(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        public ActionResult PersondeatailsCoAccused()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            VM.Gender_List = Model.GetAllGender();
            //VM.Province_List = Model.GetAllProvinces();
            //VM.District_List = Model.GetAllDistrict();
            ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description");

            return PartialView(VM);
        }

        [HttpPost]
        public ActionResult SavePersonCoAccusedDetails(PCMCaseDetailsViewModel personDetail)
        {

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
            var result = false;

            try
            {
                var personModel = new PCMCaseModel();

                var dateCreated = DateTime.Now;
                //string createdBy = Convert.ToInt32 (userId);
                const bool isActive = true;
                const bool isDeleted = false;

                PCMCaseModel Model = new PCMCaseModel();

                Model.PersonCreate_CoAccused(personDetail, caseid, Convert.ToString(userId), isActive, isDeleted);



                result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ADD UPDATE FAMILYINFOMATION
        public ActionResult GetFamillyBGList()
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
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;
            PCMCaseModel Model = new PCMCaseModel();
            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                int FamilyinformationId = Model.GetPCMFamillyBGDetailsByassId(intassid);
                if (FamilyinformationId != 0)
                {

                    VM = Model.GetFamillyBGList(FamilyinformationId);
                    Session["Fupdate"] = FamilyinformationId;

                    return PartialView(VM);
                }
                else
                {
                    PCMCaseModel Model1 = new PCMCaseModel();
                    PCMCaseDetailsViewModel VM1 = new PCMCaseDetailsViewModel();
                    //initialise view model
                    PCMCaseDetailsViewModel VMC = new PCMCaseDetailsViewModel();

                    //funtion that calls insert Socio Economy
                    Model1.InsertFamillyBG(VM1, intassid, userId);
                    int FamilyinformationIdadd = Model.GetPCMFamillyBGDetailsByassId(intassid);
                    VM1 = Model1.GetFamillyBGList(FamilyinformationIdadd);
                    Session["Fpadd"] = FamilyinformationIdadd;
                    return PartialView("GetSocioEconomics", VM1);
                }

            }
        }

        public ActionResult UpdateFamillyBG()
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
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            return PartialView("GetFamillyBGList", VM);

        }

        [HttpPost]
        public ActionResult UpdateFamillyBG(PCMCaseDetailsViewModel VM)
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
            int idcreate = Convert.ToInt32(Session["Fpadd"]);

            int Idupdate = Convert.ToInt32(Session["Fupdate"]);
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
            PCMCaseModel Model = new PCMCaseModel();


            // Call Update Function
            Model.UpdateFamillyBG(VM, userId, Id);
            ViewBag.Message = "Updated successfully";

            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        // FAMILY MEMBERS..............................................................................

        public ActionResult FamilyMemberdeatailsList()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            ViewBag.Relationtype = new SelectList(db.Relationship_Types.ToList(), "Relationship_Type_Id", "Description");

            VM.RelationshipType_List = Model.GetAllRelationType();

            return PartialView(VM);
        }

        public ActionResult ListPersonFamilyMember()
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
            PCMCaseModel Model = new PCMCaseModel();
            PCMCaseDetailsViewModel vm = new PCMCaseDetailsViewModel();


            ViewBag.Relationtype = new SelectList(db.Relationship_Types.ToList(), "Relationship_Type_Id", "Description");

            vm.RelationshipType_List = Model.GetAllRelationType();
            return PartialView(vm);



        }

        public JsonResult ListFamilyMember()
        {
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            List<PCMCaseDetailsViewModel> List = Model.GetFamilyMemberList(caseid).Select(x => new PCMCaseDetailsViewModel
            {
                Intake_Assessment_Id = x.Intake_Assessment_Id,
                Family_Member_Id = x.Family_Member_Id,
                SelectRelationshipType = x.SelectRelationshipType,
                Family_Member_Name = x.Family_Member_Name,
                Family_Member_Last_Name = x.Family_Member_Last_Name,
                Family_Member_Age = x.Family_Member_Age,
            }).ToList();

            return Json(List, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SaveFamilyMemberInDatabase(PCMCaseDetailsViewModel vm, int Id)
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
            var result = false;
            int caseid = Convert.ToInt32(Session["IntakeassId"]);
            PCMCaseModel Model = new PCMCaseModel();

            try
            {
                Model.CreateFamilyMemberDeatils(vm, caseid, Id, userId);
                result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteRecFromTableFamilyMember(int id)
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
            PCMCaseModel assModel = new PCMCaseModel();

            PCMCaseDetailsViewModel assVM = new PCMCaseDetailsViewModel();
            var result = false;
            if (id != 0)
            {

                try
                {
                    assModel.DeleteRecordFamilyMember(id);
                    result = true;
                }
                catch (Exception ex)
                {
                    //Log errror
                }
            }

            //return "failed";

            return Json(result, JsonRequestBehavior.AllowGet);
            //return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }

        public ActionResult PersondeatailsFamilyMember()
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

            PCMCaseModel Model = new PCMCaseModel();

            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();
            VM.Gender_List = Model.GetAllGender();
            //VM.Province_List = Model.GetAllProvinces();
            //VM.District_List = Model.GetAllDistrict();
            ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description");

            VM.RelationshipType_List = Model.GetAllRelationType();

            return PartialView(VM);
        }

        [HttpPost]
        public ActionResult SavePersonFamilyMemberDetails(PCMCaseDetailsViewModel personDetail)
        {

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
            var result = false;

            try
            {
                int relation = Convert.ToInt32( personDetail.Relationship_Type_Id);
                var personModel = new PCMCaseModel();

                var dateCreated = DateTime.Now;
                //string createdBy = Convert.ToInt32 (userId);
                const bool isActive = true;
                const bool isDeleted = false;

                PCMCaseModel Model = new PCMCaseModel();

                Model.PersonCreate_FamilyMember(personDetail, caseid, Convert.ToString(userId), relation, isActive, isDeleted);



                result = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return Json(result, JsonRequestBehavior.AllowGet);
        }


        #endregion


        #region ADD UPDATE GENERAL DETAILS

        public ActionResult GetGeneralDetails()
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
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;
            PCMCaseModel Model = new PCMCaseModel();
            //initialise view model
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                int genAssid = Model.GetPCMGeneralDetailsByassId(intassid);
                if (genAssid != 0)
                {

                    VM = Model.GetGeneralDetailsList(genAssid);
                    Session["genupdate"] = genAssid;

                    return PartialView(VM);
                }
                else
                {
                    PCMCaseModel Model1 = new PCMCaseModel();
                    PCMCaseDetailsViewModel VM1 = new PCMCaseDetailsViewModel();
                    //initialise view model
                    PCMCaseDetailsViewModel VMC = new PCMCaseDetailsViewModel();

                    //funtion that calls insert Socio Economy
                    Model1.InsertGeneralDetails(VM1, intassid, userId);
                    int genAssidadd = Model.GetPCMGeneralDetailsByassId(intassid);
                    VM1 = Model1.GetGeneralDetailsList(genAssidadd);
                    Session["genadd"] = genAssidadd;
                    return PartialView("GetGeneralDetails", VM1);
                }

            }
        }


        public ActionResult UpdateGeneralDetails()
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
            PCMCaseDetailsViewModel VM = new PCMCaseDetailsViewModel();

            return PartialView("GetGeneralDetails", VM);

        }

        [HttpPost]
        public ActionResult UpdateGeneralDetails(PCMCaseDetailsViewModel VM)
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
            int idcreate = Convert.ToInt32(Session["genadd"]);

            int Idupdate = Convert.ToInt32(Session["genupdate"]);
            int Ids;
            if (idcreate != 0)
            {

                Ids = idcreate;
            }
            else
            {
                Ids = Idupdate;
            }


            //instanciate model repositry
            PCMCaseModel Model = new PCMCaseModel();


            // Call Update Function
            Model.UpdatePCM_General_Details(VM, userId, Ids);
            ViewBag.Message = "Updated successfully";

            //return View("UpdatePCMCase", caseVM);
            return RedirectToAction("Index", "Assessment", new { Id = intassid });
        }


        #endregion

        #region Print Assessment report

        public ActionResult Reports()
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

           
            ViewBag.UserId_check = userId;
            ViewBag.AssId = intassid;

            return PartialView();
        }

        #endregion








    }
}