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
    public class PCMDiversionSPController : Controller
    {

        PCMDiversionModel m = new PCMDiversionModel(); // model for servivice proveder & programme
        PCM_D_ModulesModels mm = new PCM_D_ModulesModels(); // model for nodules

        PCMDiversionViewModel vm = new PCMDiversionViewModel();
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        // GET: PCMDiversionSP

        #region Service Provider
        public ActionResult Index()
        {
            var StatusType = m.GetDescriptionType();
            vm.Description_Type = m.GetDescriptionType();

            vm.Province_List = m.GetAllProvinces();
            vm.District_List = m.GetAllDistrict();
            vm.OrganisationType_List = m.GetAllOrganisationType();
            vm.PCMOrganisation_List = m.GetAllPCMOrganisation();
            vm.LocalMunicipality_List = m.GetAllLocalMunicipality();
            vm.ProgrammeLevel_List = m.GetAllPCMProgrammeLevels();
            vm.RecomendationOrder_List = m.GetAllPCMOrders();
            vm.DiversionProgrammes_List = m.GetAllDiversion_Programmes();
            vm.SourceReferral_List = m.GetReferralSource();
            vm.Programme_List = m.GetAllPrograms();

            vm.ProgrammeLevel_List = m.GetProgrammeLevel();
            vm.ProgrammeAgeGroup_List = m.GetProgrammeAgeGroup();
            ViewBag.ProgrammeLevel_List = new SelectList(db.apl_Programme_Level.ToList(), "Programme_Level_Id", "Description");
            ViewBag.ProgrammeAgeGroup_List = new SelectList(db.apl_Programme_AgeGroup.ToList(), "Programme_AgeGroup_Id", "Description");

            ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description");
            ViewBag.Local_Municipality_List = new SelectList(db.Local_Municipalities.ToList(), "Local_Municipality_Id", "Description");


            ViewBag.OrganisationType_List = new SelectList(db.apl_Organisation_Type.ToList(), "IdType", "Description");
            ViewBag.PCMOrganisation_List = new SelectList(db.Organizations.ToList(), "Organization_Id", "Description");
            ViewBag.ProgrammeLevel_List = new SelectList(db.apl_Programme_Level.ToList(), "Programme_Level_Id", "Description");
            ViewBag.RecomendationOrder_List = new SelectList(db.apl_PCM_Orders.ToList(), "Recomendation_Order_Id", "Description");
            ViewBag.DiversionProgrammes_List = new SelectList(db.apl_Programmes.ToList(), "Programme_Id", "Programme_Name");
            ViewBag.SourceReferral_List = new SelectList(db.apl_PCM_Referral_Source.ToList(), "Source_Referral_Id", "Description");

            return PartialView(vm);
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
            
            if (intassid != 0)
            {
                List<PCMCaseDetailsViewModel> OurList = assModel.GetSelectedDivesionFromDB(intassid);
                return PartialView(OurList);
            }
            return PartialView();
        }

        public JsonResult GetDiversionProgrameById(int Diversion_Id, int AddEditValue)
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

            Session["AddEditValue"] = AddEditValue;
            int assID = Convert.ToInt32(Session["IntakeassId"]);



            PCMDiversionModel Model = new PCMDiversionModel();
            PCMDiversionViewModel vm = new PCMDiversionViewModel();
            vm.Province_List = Model.GetAllProvinces();
            vm.District_List = Model.GetAllDistrict();
            vm.OrganisationType_List = Model.GetAllOrganisationType();
            vm.PCMOrganisation_List = Model.GetAllPCMOrganisation();
            vm.LocalMunicipality_List = Model.GetAllLocalMunicipality();
            vm.ProgrammeLevel_List = Model.GetAllPCMProgrammeLevels();
            vm.RecomendationOrder_List = Model.GetAllPCMOrders();
            vm.DiversionProgrammes_List = Model.GetAllDiversion_Programmes();
            vm.SourceReferral_List = Model.GetReferralSource();
            vm.Programme_List = Model.GetAllPrograms();

            vm.ProgrammeLevel_List = Model.GetProgrammeLevel();
            vm.ProgrammeAgeGroup_List = Model.GetProgrammeAgeGroup();
            ViewBag.ProgrammeLevel_List = new SelectList(db.apl_Programme_Level.ToList(), "Programme_Level_Id", "Description");
            ViewBag.ProgrammeAgeGroup_List = new SelectList(db.apl_Programme_AgeGroup.ToList(), "Programme_AgeGroup_Id", "Description");


            ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description");
            ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description");
            ViewBag.Local_Municipality_List = new SelectList(db.Local_Municipalities.ToList(), "Local_Municipality_Id", "Description");


            ViewBag.OrganisationType_List = new SelectList(db.apl_Organisation_Type.ToList(), "IdType", "Description");
            ViewBag.PCMOrganisation_List = new SelectList(db.Organizations.ToList(), "Organization_Id", "Description");
            ViewBag.ProgrammeLevel_List = new SelectList(db.apl_Programme_Level.ToList(), "Programme_Level_Id", "Description");
            ViewBag.RecomendationOrder_List = new SelectList(db.apl_PCM_Orders.ToList(), "Recomendation_Order_Id", "Description");
            ViewBag.DiversionProgrammes_List = new SelectList(db.apl_Programmes.ToList(), "Programme_Id", "Programme_Name");
            ViewBag.SourceReferral_List = new SelectList(db.apl_PCM_Referral_Source.ToList(), "Source_Referral_Id", "Description");

          



            if (Convert.ToInt32(Model.GetDivesionFromDBDivbyId(Diversion_Id).Programme_Id) != 0)
            {
                vm = Model.GetDivesionFromDBDivbyId(Diversion_Id);

                int Programid = Convert.ToInt32(Model.GetDivesionFromDBDivbyId(Diversion_Id).Programme_Id);
                ViewBag.Programme_Name = db.apl_Programmes.Find(Programid).Programme_Name;

                //Get Town using the SAPS..........................................
                int organisationid = (from k in db.Organizations
                              join k1 in db.apl_Programmes on k.Organization_Id equals k1.Organization_Id
                                      where k1.Programme_Id == Programid
                                      select k.Organization_Id).FirstOrDefault();

                ViewBag.organisationD = new SelectList(db.Organizations.ToList(), "Organization_Id", "Description", organisationid);


                int organisationtypeid = (from k in db.apl_Organisation_Type
                                      join k1 in db.Organizations on k.IdType equals k1.Organisation_Type_Id
                                      where k1.Organization_Id == organisationid
                                          select k.IdType).FirstOrDefault();

                ViewBag.OrganisationType_List = new SelectList(db.apl_Organisation_Type.ToList(), "IdType", "Description", organisationtypeid);




                int? LocalMuniId = (from a in db.Local_Municipalities
                                   join b in db.Organizations on a.Local_Municipality_Id equals b.Local_Municipality_Id
                                   where b.Organization_Id.Equals(organisationid)
                                   select b.Local_Municipality_Id).FirstOrDefault();

                ViewBag.Local_Municipality_List = new SelectList(db.Local_Municipalities.ToList(), "Local_Municipality_Id", "Description", LocalMuniId);

                int DistrictIdOrg = (from k in db.Districts
                                        join k1 in db.Local_Municipalities on k.District_Id equals k1.District_Municipality_Id
                                        where k1.Local_Municipality_Id == LocalMuniId
                                        select k.District_Id).FirstOrDefault();

                ViewBag.District_List = new SelectList(db.Districts.ToList(), "District_Id", "Description", DistrictIdOrg);


                int ProvinceIdorg = (from a in db.Districts
                                        join b in db.Provinces on a.Province_Id equals b.Province_Id
                                        where a.District_Id.Equals(DistrictIdOrg)
                                        select b.Province_Id).FirstOrDefault();
                ViewBag.Province_List = new SelectList(db.Provinces.ToList(), "Province_Id", "Description", ProvinceIdorg);

                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);

            }

            else
            {
                vm = Model.GetDivesionFromDBDivbyId(Diversion_Id);
                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);

            }

            
        }

        public JsonResult SaveDiversionProgram(PCMDiversionViewModel prVM)
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
            int AddEditValue = Convert.ToInt32(Session["AddEditValue"]);
            int assID = Convert.ToInt32(Session["IntakeassId"]);

            try
            {
                if (AddEditValue ==0)
                {
                    
                    m.UpdatePCMDivesionsDeatils(prVM, prVM.Diversion_Id, userId);
                    result = true;
                }
                else
                {
                    m.PCM_Diversion_SPAddnew(prVM, assID, userId);
                    result = true;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPCMDivesionDetails()
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

            PCMDiversionViewModel VM = new PCMDiversionViewModel();
            PCMDiversionModel Model = new PCMDiversionModel();

            VM.ProgrammeAgeGroup_List = Model.GetProgrammeAgeGroup();
            VM.ProgrammeLevel_List = Model.GetProgrammeLevel();

            return Json(m.GetSelectedDivesionFromDBDiv(assID), JsonRequestBehavior.AllowGet);
        }

        // Json Call to get ServicesProvider
        public JsonResult GetServicesP(string id)
        {
            List<SelectListItem> PCM_D_ServicesProviders = new List<SelectListItem>();
            var servicesList = this.GetServicesProvider(Convert.ToInt32(id));
            var servicesData = servicesList.Select(m => new SelectListItem()
            {
                Text = m.Services_Provider_Name,
                Value = m.Services_Provider_Id.ToString(),
            });
            return Json(servicesData, JsonRequestBehavior.AllowGet);
        }

        // Get ServicesProvider from DB by ProvinceId
        public IList<PCM_D_ServicesProvider> GetServicesProvider(int ProvinceId)
        {
            return db.PCM_D_ServicesProvider.Where(m => m.Province_Id == ProvinceId).ToList();
        }


      


        #endregion

        #region PROGRAMME FUNCTION
        public ActionResult Programme()
        {
            //List<SelectListItem> S_PList = S_P();
            ViewBag.Country = new SelectList(db.PCM_Diversion_SP, "S_P_Id", "Services_Provider_Id");
            return PartialView();
        }

        // Json Call to get state
        public JsonResult GetProgrammes(string id)
        {
            List<SelectListItem> programmes = new List<SelectListItem>();
            var pList = this.GetProgramme(Convert.ToInt32(id));
            var pData = pList.Select(m => new SelectListItem()
            {
                Text = m.Programme_name,
                Value = m.Programme_Id.ToString(),
            });
            return Json(pData, JsonRequestBehavior.AllowGet);
        }

        // Get State from DB by country ID
        public IList<PCM_D_Programme> GetProgramme(int ServicesProviderId)
        {
            return db.PCM_D_Programme.Where(m => m.Services_Provider_Id == ServicesProviderId).ToList();
        }

        #endregion

        #region MODULES FUNCTIONS

        public ActionResult Module()
        {
            List<PCMDiversionViewModel> vm = new List<PCMDiversionViewModel>();
            var p = (from p1 in db.PCM_Diversion_P
                     join p2 in db.PCM_D_Programme
                     on p1.Programme_Id equals p2.Programme_Id
                     join p3 in db.PCM_Diversion_SP
                     on p1.S_P_Id equals p3.S_P_Id
                     select new
                     {
                         p1.P_Id,
                         p1.S_P_Id,
                         p1.Programme_Id,
                         p2.Programme_name,
                         p2.Programme_Status,
                         p2.Programme_Expiry_Date,
                         p2.Services_Provider_Id
                     }).ToList();
            ViewBag.ListOfDescription = new SelectList(p, "Programme_Id", "Programme_name");

            return PartialView();
        }

        // Json Call to get state
        public JsonResult GetModules(string id)
        {
            List<SelectListItem> modules = new List<SelectListItem>();
            var mList = this.Getmodule(Convert.ToInt32(id));
            var mData = mList.Select(m => new SelectListItem()
            {
                Text = m.Module_Name,
                Value = m.Modules_Id.ToString(),
            });
            return Json(mData, JsonRequestBehavior.AllowGet);
        }

        // Get State from DB by country ID
        public IList<PCM_D_Modules> Getmodule(int ModulesId)
        {
           return db.PCM_D_Modules.Where(m => m.Modules_Id == ModulesId).ToList();
        }


        public JsonResult GetDiversion_M()
        {
            return Json(mm.GetDiversion_M(), JsonRequestBehavior.AllowGet);
        }

      public JsonResult getProgramme()
        {
            return Json(mm.getProgramme(), JsonRequestBehavior.AllowGet);
        }

       

        public JsonResult AddUpdate(PCMDiversionViewModel dvm)
        {
            var result = false;
            try
            {
                if(dvm.M_Id > 0)
                {

                }
                else
                {

                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion



        public IList<Common_Objects.Models.District> GetDistrict(int Provinceid)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Districts.Where(m => m.Province_Id == Provinceid).ToList();
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

        public JsonResult GetDistrictsD(string id)
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
        public JsonResult GetOrganisationsD(string id, string LocMunId)
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

        public JsonResult GetDivesionsD(string id)
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

        public JsonResult GetLocalMunicipalitiesD(string id)
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

    }
}