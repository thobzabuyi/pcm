using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Common_Objects.Models;

namespace PCM_Module.Controllers
{
    public class PCMCCController : Controller
    {
        // GET: PCMCC
        public ActionResult Index()
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            return PartialView(db.PCM_Childrens_Court_Doc.ToList());
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase postedFile)
        {
            //Extract Image File Name.
            string fileName = System.IO.Path.GetFileName(postedFile.FileName);

            //Set the Image File Path.
            string filePath = "~/PCM_Module/Uploads/" + fileName;

            //Save the Image File in Folder.
            postedFile.SaveAs(Server.MapPath(filePath));

            //Insert the Image File details in Table.
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            db.PCM_Childrens_Court_Doc.Add(new PCM_Childrens_Court_Doc
            {
                Doc_Name = fileName,
                //Doc_Data = filePath
            });
            db.SaveChanges();

            //Redirect to Index Action.
            return PartialView(db.PCM_Childrens_Court_Doc.ToList());
        }

    }
}