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
    public class DiversionController : Controller
    {

        PCMDiversionModel m = new PCMDiversionModel();
        PCMDiversionViewModel vm = new PCMDiversionViewModel();
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        // GET: Diversion



        #region Yanga Code fro Ass

        public ActionResult HomeIndex(int id)
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


            return View(personVM);
        }

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


            int ClientRefid = caseModel.GetClientRefIdssId(id);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            Session["IntakeassId"] = id;
            ViewBag.ModuleRef = ClientRef;
                  

            return PartialView(personVM);
        }

        public ActionResult GetServiceProviderForDivesionDiv()
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
        public ActionResult CreateDivesionDiv(PCMCaseDetailsViewModel vm)
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


        #endregion

        [HttpGet]
        public ActionResult DiversionAdd()
        {
            var StatusType = m.GetDescriptionType();
            vm.Description_Type = m.GetDescriptionType();

            return PartialView(vm);
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


        // Json Call to get Programme
        public JsonResult GetProgrammeP(string id)
        {
            List<SelectListItem> PCM_D_Programme = new List<SelectListItem>();
            var programmeList = this.GetProgramme(Convert.ToInt32(1));
            var programmeData = programmeList.Select(m => new SelectListItem()
            {
                Text = m.Programme_name,
                Value = m.Programme_Id.ToString(),
            });
            return Json(programmeData, JsonRequestBehavior.AllowGet);
        }

        // Get ServicesProvider from DB by ServicesProviderId
        public IList<PCM_D_Programme> GetProgramme(int ServicesProviderId)
        {
            return db.PCM_D_Programme.Where(m => m.Services_Provider_Id == ServicesProviderId).ToList();
        }


        //public JsonResult GetStatus(PCMDiversionViewModel vm)
        //{
        //    PCMDiversionModel m = new PCMDiversionModel();
        //    var result = false;
        //    //int pcmreg = 3;
        //    //int userId = 5;
        //    try
        //    {
        //        if (vm.Services_Provider_Id > 0)
        //        {
        //            m.GetStatus(2);
        //            result = true;
        //        }
        //        else
        //        {
        //            //acm.CreateAsset(vm, userId);
        //            result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return Json(result, JsonRequestBehavior.AllowGet);

        //}


        public JsonResult GetStatusByID(int ServicesProviderId)
        {
            PCM_D_ServicesProvider model = db.PCM_D_ServicesProvider.Where(x => x.Services_Provider_Id == ServicesProviderId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DiversionAdd(PCMDiversionViewModel vm)
        {
            var m = new PCMDiversionModel();
            m.DiversionAdd(vm, 6, 28377);
            ViewBag.Message = "Saved successfully";
            return PartialView();
        }
    }
}