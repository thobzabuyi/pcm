using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Objects.Models;
using Common_Objects.ViewModels;
using System.IO;
using Newtonsoft.Json;

namespace PCM_Module.Controllers
{
    public class PCMChildrensCourtDocController : Controller
    {
        // GET: PCMChildrensCourtDoc
        PCMChildrensCourtModel cm = new PCMChildrensCourtModel();
        PCMChildrensCourtViewModel vm = new PCMChildrensCourtViewModel();
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        //private SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        public ActionResult Index(int id)
        {
            vm = cm.GetCCOutComeByID(id);

            return PartialView(vm);
        }

        public JsonResult GetPCMCourtOutcomeById(int Outcome_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;

                PCM_Childrens_Court_Outcome model = db.PCM_Childrens_Court_Outcome.Where(x => x.Intake_Assessment_Id == Outcome_Id).SingleOrDefault();
                string value = string.Empty;
                value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
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



    }
}