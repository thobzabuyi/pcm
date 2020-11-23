using Common_Objects.Models;
using Common_Objects.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCM_Module.Controllers
{
    public class PCMDSessionOutcomeFileController : Controller
    {
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        PCMDSessionOutcomeViewModel vm = new PCMDSessionOutcomeViewModel();
        // GET: PCMDSessionOutcomeFile
        public ActionResult Index()
        {
            return PartialView();
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
                    db.PCM_Diversion_File.Add(new PCM_Diversion_File
                    {
                        Intake_Assessment_Id = 28377,
                        File_Name = fileName,
                        File_Doc = path,
                       
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