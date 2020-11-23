using Common_Objects.Models;
using Common_Objects.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM_Module.Controllers
{
    public class PCMChildrensCourtController : Controller
    {

        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        PCMChildrensCourtModel cm = new PCMChildrensCourtModel();
        PCMChildrensCourtViewModel vm = new PCMChildrensCourtViewModel();



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


            bool IdExist = db.PCM_Childrens_Court.Any(x => x.Intake_Assessment_Id == id);
            if (IdExist)
            {
           



                PCMChildrensCourtModel cm = new PCMChildrensCourtModel();
                PCMChildrensCourtViewModel vm = new PCMChildrensCourtViewModel();
                vm.PlacementRecomendation_List = cm.GetPlacementRecomendation();
                vm.Recommendation_Type_List = cm.GetRecommendationType();
                vm.CourtOutcomeCaseStatusation_Type = cm.GetCourtOutcomeCaseStatusType();

                ViewBag.Recommendation_Type_List = new SelectList(db.apl_Recommendation_Type.ToList(), "Recommendation_Type_Id", "Description");
                ViewBag.PlacementRecomendation_List = new SelectList(db.apl_Placement_Type.ToList(), "Placement_Type_Id", "Description");
                ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");

                List<PCMChildrensCourtViewModel> vm1 = new List<PCMChildrensCourtViewModel>();
                vm1 = cm.GetPCMChildrensOutcometList();

            }
            else
            {
                PCMChildrensCourtModel cm = new PCMChildrensCourtModel();
                PCMChildrensCourtViewModel vm = new PCMChildrensCourtViewModel();
                vm.PlacementRecomendation_List = cm.GetPlacementRecomendation();
                vm.Recommendation_Type_List = cm.GetRecommendationType();
                vm.CourtOutcomeCaseStatusation_Type = cm.GetCourtOutcomeCaseStatusType();

                ViewBag.Recommendation_Type_List = new SelectList(db.apl_Recommendation_Type.ToList(), "Recommendation_Type_Id", "Description");
                ViewBag.PlacementRecomendation_List = new SelectList(db.apl_Placement_Type.ToList(), "Placement_Type_Id", "Description");
                ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");
                Response.Write("Does not Exist");
            }

            int? Placementid = cm.GetPlacementId(id);

            int? Recomendationid = cm.GetRecomendationId(id);

            ViewBag.MessagePlacement = Placementid;

            Session["Placementid"] = Placementid;

            Session["Rec1"] = Recomendationid;

            Session["Idc1"] = id;
            Session["IntakeassId"] = id;

            return PartialView(vm);
        }



        #region CHILDREN  COURT 
        [HttpGet]
        ActionResult CreatePCMCC()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreatePCMCC(PCMChildrensCourtViewModel vm)
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


            int RecomendationID = Convert.ToInt32(Session["Rec1"]); ;



            int assID = Convert.ToInt32(Session["IntakeassId"]);
            if (vm.Children_Court_Id > 0)
            {
                cm.UpdatePCMChildrensCourt(vm, vm.Children_Court_Id, userId);


            }
            else
            {
                cm.CreatePCMChildrensCourt(vm, RecomendationID, assID);

            }
            return RedirectToAction("Home", "PCMPreliminary", new { Id = assID });
        }

        public JsonResult GetCourtList()


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


            return Json(cm.GetCourtList(assID), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPCMCourtById(int Children_Court_Id)
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

                PCM_Childrens_Court model = db.PCM_Childrens_Court.Where(x => x.Children_Court_Id == Children_Court_Id).SingleOrDefault();
                string value = string.Empty;
                value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion


        #region OUT COME OF CHILDREN COURT

        public JsonResult GetOutcometList()
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

            PCMChildrensCourtModel cm = new PCMChildrensCourtModel();
            PCMChildrensCourtViewModel vm = new PCMChildrensCourtViewModel();
            vm.PlacementRecomendation_List = cm.GetPlacementRecomendation();
            vm.Recommendation_Type_List = cm.GetRecommendationType();
            vm.CourtOutcomeCaseStatusation_Type = cm.GetCourtOutcomeCaseStatusType();

            ViewBag.Recommendation_Type_List = new SelectList(db.apl_Recommendation_Type.ToList(), "Recommendation_Type_Id", "Description");
            ViewBag.PlacementRecomendation_List = new SelectList(db.apl_Placement_Type.ToList(), "Placement_Type_Id", "Description");
            ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");

            return Json(cm.GetOutcometList(assID), JsonRequestBehavior.AllowGet);

        }


        public JsonResult GetPCMCourtOutcomeById(int Outcome_Id)
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


            PCMChildrensCourtModel cm = new PCMChildrensCourtModel();
            PCMChildrensCourtViewModel vm = new PCMChildrensCourtViewModel();
            vm.PlacementRecomendation_List = cm.GetPlacementRecomendation();
            vm.Recommendation_Type_List = cm.GetRecommendationType();
            vm.CourtOutcomeCaseStatusation_Type = cm.GetCourtOutcomeCaseStatusType();

            ViewBag.Recommendation_Type_List = new SelectList(db.apl_Recommendation_Type.ToList(), "Recommendation_Type_Id", "Description");
            ViewBag.PlacementRecomendation_List = new SelectList(db.apl_Placement_Type.ToList(), "Placement_Type_Id", "Description");
            ViewBag.CourtOutcomeCaseStatusation_Type = new SelectList(db.apl_PCM_Court_Case_Status.ToList(), "Court_Case_Status_Id", "Description");

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                vm = cm.GetChildrensCourtOutcometById(Outcome_Id);

                string value = string.Empty;
                value = JsonConvert.SerializeObject(vm, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult OutComeList()
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
            var m = new PCMChildrensCourtModel();
            return Json(m.GetOutcometList(assID), JsonRequestBehavior.AllowGet);
        }



        public JsonResult CreatePCMCourtOutcome(PCMChildrensCourtViewModel vm)
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

            PCMChildrensCourtModel cm = new PCMChildrensCourtModel();
            int RecomendationID = Convert.ToInt32(Session["Rec1"]);

            int PlacementID = Convert.ToInt32(Session["Placementid"]);


            int assID = Convert.ToInt32(Session["IntakeassId"]);
            var result = false;

            try
            {
                if (vm.Outcome_Id > 0)
                {
                    cm.CreatePCMChildrensCourtOutcome(vm, RecomendationID, vm.Outcome_Id,  assID);
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



        #region DOCUMENTS 


        [HttpGet]
        public ActionResult UplaodDoc()
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            return PartialView();
        }

        [HttpPost]
        public ActionResult UplaodDoc(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string fileName = System.IO.Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(file.FileName));

                    file.SaveAs(path);

                    SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
                    db.PCM_Childrens_Court_Doc.Add(new PCM_Childrens_Court_Doc
                    {
                        Outcome_Id = 1,
                        Doc_Name = fileName,
                        Doc_Data = path,
                        Intake_Assessment_Id = 28377
                    });
                    db.SaveChanges();

                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return PartialView("Index");
        }

        [HttpGet]
        public ActionResult DownloadFile()
        {
            return PartialView();
        }
        #endregion






    }
}