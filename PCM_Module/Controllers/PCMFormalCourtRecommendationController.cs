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
    public class PCMFormalCourtRecommendationController : Controller
    {
        PCMFormalCourtRecommendationModel fm = new PCMFormalCourtRecommendationModel();
        // GET: PCMFormalCourtRecommendation
        public ActionResult Index()
        {
            return PartialView();
        }

        public JsonResult GetPCMFormalCourtRecommendationList()
        {
            return Json(fm.GetPCMFormalCourtRecommendationList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPCMFormalCourtById(int PCM_Formal_Court_Recomm_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                PCM_Formal_Court_Recommendation model = db.PCM_Formal_Court_Recommendation.Where(x => x.PCM_Formal_Court_Recomm_Id == PCM_Formal_Court_Recomm_Id).SingleOrDefault();
                string value = string.Empty;
                value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                return Json(value, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult CreatePCMFormalCourt(PCMChildrensCourtViewModel vm)
        {
            PCMFormalCourtRecommendationModel fm = new PCMFormalCourtRecommendationModel();
            var result = false;
            int pcmreg = 3;
            int userId = 5;
            try
            {
                if(vm.PCM_Formal_Court_Recomm_Id > 0)
                {
                    fm.GetPCMFormalCourtEditDetails(vm.PCM_Formal_Court_Recomm_Id);
                    result = true;
                }
                else
                {
                    fm.CreatePCMFormalCourt(vm, pcmreg, userId);
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