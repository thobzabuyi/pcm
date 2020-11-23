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
    public class PCMReferralsController : Controller
    {
        PCMReferralsModel rm = new PCMReferralsModel();
        PCMReferralsViewModel vm = new PCMReferralsViewModel();
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        public ActionResult Home(int id)
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
            personVM.IntakeAssPar = id;


            int ClientRefid = caseModel.GetClientRefIdssId(id);
            //Get Module Reference number ....................
            string ClientRef = caseModel.GetClientRef(ClientRefid);
            Session["ClientRef"] = ClientRef;
            ViewBag.ModuleRef = ClientRef;
            Session["IntakeassId"] = id;



            return View(personVM);
        }

        // GET: PCMReferrals



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
            int assID = Convert.ToInt32(Session["IntakeassId"]);
            string ClientRef = Convert.ToString(Session["ClientRef"]);
            ViewBag.ModuleRef = ClientRef;

        

            List<apl_Referral_Type> RefList = db.apl_Referral_Type.ToList();
            ViewBag.ListofReferrals = new SelectList(RefList, "Type_Referral_Id", "Type_Referral");

            Session["Idc1"] = assID;
            Session["IntakeassId"] = assID;

            return PartialView(vm);

        }




        public ActionResult DisplayReferrals()
        {
           
            return View(db.PCM_Referrals.ToList());
        }
        public ActionResult PCM_Referrals(int id)
        {

            if (id > 0)
            {
                vm = rm.ViewReferralData(id);
                //pVM.Recommendation_Type = preModel.GetRecommendationType();
            }
            else
            {
                //clear all the fields
            }

            var typerefferal = (from c in db.PCM_Referrals
                                where c.Referrals_Id == 1011
                                select c.Type_Referral_Id).FirstOrDefault();
            ViewBag.typerefferal = typerefferal;

            List<apl_Referral_Type> RefList = db.apl_Referral_Type.ToList();
            ViewBag.ListofReferrals = new SelectList(RefList, "Type_Referral_Id", "Type_Referral");
            return View();
        }

        public JsonResult GetPCMReferraltypeById(int Referrals_Id, int Type_Referral_Id)
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

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                string value = string.Empty;
                if (Referrals_Id != 0 && Type_Referral_Id == 1)
                {
                    PCM_Referral_To_Court model1 = db.PCM_Referral_To_Court.Where(x => x.Referrals_Id == Referrals_Id).SingleOrDefault();


                    value = JsonConvert.SerializeObject(model1, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    Session["Referrals_Id"] = Referrals_Id;
                }

                else if (Referrals_Id != 0 && Type_Referral_Id == 2)
                {
                    PCM_Referral_To_Programme model1 = db.PCM_Referral_To_Programme.Where(x => x.Referrals_Id == Referrals_Id).SingleOrDefault();

                    value = JsonConvert.SerializeObject(model1, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    Session["Referrals_Id"] = Referrals_Id;
                }
                else if (Referrals_Id != 0 && Type_Referral_Id == 3)
                {
                    PCM_Referral_Counselling model1 = db.PCM_Referral_Counselling.Where(x => x.Referrals_Id == Referrals_Id).SingleOrDefault();

                    value = JsonConvert.SerializeObject(model1, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    Session["Referrals_Id"] = Referrals_Id;
                }
                else if (Referrals_Id != 0 && Type_Referral_Id == 4)
                {
                    PCM_Referral_SocilaWorker model1 = db.PCM_Referral_SocilaWorker.Where(x => x.Referrals_Id == Referrals_Id).SingleOrDefault();
                    value = JsonConvert.SerializeObject(model1, Formatting.Indented, new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    });
                    Session["Referrals_Id"] = Referrals_Id;
                }

                string valueFinal = string.Empty;
                valueFinal = value;

                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

        #region referrals info
        public JsonResult GetPCMReferralsList()
        {

            var result = false;
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

           
            return Json(rm.GetPCMReferralsList(Intake_Assessment_Id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPCMReferralById(int Referrals_Id)
        {
            //GetPCMReferralById
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                PCM_Referrals model = db.PCM_Referrals.Where(x => x.Referrals_Id == Referrals_Id).SingleOrDefault();
                string value = string.Empty;
                value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

        

        public JsonResult SaveReferralsData(PCMReferralsViewModel prVM)
        {
      
            var result = false;
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

            try
            {
                if(prVM.Referrals_Id > 0)
                {
                    rm.UpdateReferralsData(prVM, prVM.Referrals_Id, userId, Intake_Assessment_Id);
                    rm.ViewReferralData(prVM.Referrals_Id);
                    result = true;
                }
                else
                {
                    rm.SaveReferralsData(prVM,userId, Intake_Assessment_Id);
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

        public ActionResult PCM_CourtReferrals()
        {
            return PartialView();
        }

        #region PCM_Referral_To_Court

        public JsonResult GetPCMCourtReferralsList()
        {
            return Json(rm.GetPCMCourtReferralsList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPCMCourtReferralsById(int Court_Referrals_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                PCM_Referral_To_Court model = db.PCM_Referral_To_Court.Where(x => x.Court_Referrals_Id == Court_Referrals_Id).SingleOrDefault();
                string value = string.Empty;
                value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult SaveCourtData(PCMReferralsViewModel prVM)
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

            int Referrals_Id = Convert.ToInt32(Session["Referrals_Id"]);

            try
            {
                if(Referrals_Id > 0)
                {
                    int Court_Referrals_Id = rm.GetCourtIdByReferalId(Referrals_Id);
                    rm.SaveCourtData(prVM, Court_Referrals_Id);
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region referrals to counseliing

        public JsonResult SaveReferalToCouncelling(PCMReferralsViewModel vm)
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

            int Referrals_Id = Convert.ToInt32(Session["Referrals_Id"]);

            try
            {
                if(Referrals_Id > 0)
                {
                    int Referrals_Councelling_Id = rm.GetCouncellingIdByReferalId(Referrals_Id);
                    rm.UpdateReferalToCouncelling(vm, userId, Referrals_Councelling_Id);
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        #endregion

        #region Referrals to Accredited Programme


        public JsonResult SaveReferalToProgramme(PCMReferralsViewModel vm)
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
            int Referrals_Id = Convert.ToInt32(Session["Referrals_Id"]);
            try
            {
                if (Referrals_Id > 0)
                {
                    int Referrals_Programme_Id = rm.GetProgrammeIdByReferalId(Referrals_Id);
              

                    rm.UpdateReferalToProgramme(vm, userId, Referrals_Programme_Id);
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

        #region Referrals to Social Worker

        public JsonResult SaveReferalToSocialWorker(PCMReferralsViewModel vm)
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
            int Referrals_Id = Convert.ToInt32(Session["Referrals_Id"]);
            try
            {
                if (Referrals_Id > 0)
                {
                    int Referrals_SocilaWorker_Id = rm.GetSocilaworkerIdByReferalId(Referrals_Id);
                    rm.UpdateReferalToSocialWorker(vm, userId, Referrals_SocilaWorker_Id);
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