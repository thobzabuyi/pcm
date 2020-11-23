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
    public class PCMDDiversionOutcomeController : Controller
    {
        PCMDDiversionOutcomeModel m = new PCMDDiversionOutcomeModel();
        PCMDSessionOutcomeViewModel vm = new PCMDSessionOutcomeViewModel();
        SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        // GET: PCMDDiversionOutcome
        public ActionResult Index()
        {
            return PartialView();
        }

        public JsonResult GetOutcomeList()
        {
            return Json(m.GetOutcomeList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOutcomeById(int Diversion_Outotcome_Id)
        {
            PCM_D_Diversion_Outcome model = db.PCM_D_Diversion_Outcome.Where(x => x.Diversion_Outotcome_Id == Diversion_Outotcome_Id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateOutcome(PCMDSessionOutcomeViewModel vm)
        {
            var result = false;
            int Intake_Assessment_Id = 28377;

            try
            {
                if(vm.Diversion_Outotcome_Id > 0)
                {

                    result = true;
                }
                else
                {
                    m.CreateOutcome(vm, Intake_Assessment_Id);
                    result = true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


    }
}