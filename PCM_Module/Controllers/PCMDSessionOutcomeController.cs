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
    public class PCMDSessionOutcomeController : Controller
    {
        PCMDSessionOutcomeModel m = new PCMDSessionOutcomeModel();
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        // GET: PCMDSessionOutcome
        public ActionResult Index()
        {
            return PartialView();
        }

        //public ActionResult FileUplaodDoc()
        //{
        //    SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        //    return PartialView();
        //}

        //[HttpPost]
        //public ActionResult FileUplaodDoc(HttpPostedFileBase file)
        //{
        //    if (file != null && file.ContentLength > 0)
        //        try
        //        {
        //            string fileName = System.IO.Path.GetFileName(file.FileName);
        //            string path = Path.Combine(Server.MapPath("~/Uploads"), Path.GetFileName(file.FileName));

        //            file.SaveAs(path);

        //            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        //            db.PCM_Diversion_File.Add(new PCM_Diversion_File
        //            {
        //                Intake_Assessment_Id = 28377,
        //                //Doc_Name = fileName,
        //                File_Doc = path,
        //                File_Name = fileName
        //                //Intake_Assessment_Id = 28377
        //            });
        //            db.SaveChanges();

        //            ViewBag.Message = "File uploaded successfully";
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = "ERROR:" + ex.Message.ToString();
        //        }
        //    else
        //    {
        //        ViewBag.Message = "You have not specified a file.";
        //    }
        //    return PartialView("Index");
        //}




        public JsonResult GetPCMDOSList()
        {
            return Json(m.GetDSOList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPCMDSOById(int DSession_Id)
        {
            PCM_D_Session_Outcome model = db.PCM_D_Session_Outcome.Where(x => x.DSession_Id == DSession_Id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreatePCMDSO(PCMDSessionOutcomeViewModel vm)
        {
           
            //int pcmreg = 3;
            var result = false;

            try
            {
                if (vm.DSession_Id > 0)
                {
                    //cm.GetPCMChildrensCourtEditDetails(vm.Children_Court_Id);
                    //cm.CreatePCMChildrensCourt(vm, pcmreg, Intake_Assessment_Id);
                    result = true;
                }
                else
                {
                    m.CreateDSO(vm, 28377);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}