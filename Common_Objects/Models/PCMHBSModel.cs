using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common_Objects.ViewModels;

namespace Common_Objects.Models
{
    public class PCMHBSModel
    {
        #region Home Based Supervision function
        public List<PCMHBSViewModel> GetHBSList(int assID)
        {
            List<PCMHBSViewModel> vm = new List<PCMHBSViewModel>();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            var hbsList = (from h in db.PCM_HB_Supervision
                           join courtt in db.apl_PCM_Court_Type on h.Court_Type_Id equals courtt.Court_Type_Id


                           where h.Intake_Assessment_Id == assID

                           select new
                           {
                               h.HomeBasedSupervision_Id,
                               h.Intake_Assessment_Id,
                               h.Visitation_Period,
                               h.Number_of_Visit,
                               h.Placement_Date,
                               h.Conditions_Id,
                               courtt.Court_Type_Id

                           }).ToList();
            foreach(var item in hbsList)
            {
                PCMHBSViewModel obj = new PCMHBSViewModel();
                obj.HomeBasedSupervision_Id = item.HomeBasedSupervision_Id;
                obj.Intake_Assessment_Id = item.Intake_Assessment_Id;
                obj.Visitation_Period = item.Visitation_Period;
                obj.Number_of_Visit = item.Number_of_Visit;
                obj.Placement_Date = item.Placement_Date;
                obj.Compliance_Id = item.Conditions_Id;
                obj.Court_Type = db.apl_PCM_Court_Type.Find(item.Court_Type_Id).Description;

                vm.Add(obj);
            }

            return vm;
        }

        public void CreateHBS(PCMHBSViewModel vm, int Intake_Assessment_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    PCM_HB_Supervision newHB = new PCM_HB_Supervision();

                    newHB.Intake_Assessment_Id = Intake_Assessment_Id;
                    newHB.Conditions_Id = vm.Conditions_Id;
                    newHB.Visitation_Period = vm.Visitation_Period;
                    newHB.Number_of_Visit = vm.Number_of_Visit;
                    newHB.Placement_Date = vm.Placement_Date;
                    newHB.HBS_Supervisor_Id = vm.HBS_Supervisor_Id;
                    newHB.Placement_Confirmed = vm.Placement_Confirmed;

                    db.PCM_HB_Supervision.Add(newHB);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        public void UpdateHBS(PCMHBSViewModel vm, int userId, int HomeBasedSupervision_Id, int assID)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    PCM_HB_Supervision newCC = db.PCM_HB_Supervision.Find(HomeBasedSupervision_Id);

                    newCC.HomeBasedSupervision_Id = HomeBasedSupervision_Id;
                    newCC.Conditions_Id = vm.Conditions_Id;
                    newCC.Visitation_Period = vm.Visitation_Period;
                    newCC.Number_of_Visit = vm.Number_of_Visit;
                    newCC.Placement_Date = vm.Placement_Date;
                    newCC.HBS_Supervisor_Id = vm.HBS_Supervisor_Id;
                    newCC.Placement_Confirmed = vm.Placement_Confirmed;

                    db.SaveChanges();

                

                    PCM_HB_CourtOutcome exist2 = db.PCM_HB_CourtOutcome.Where(x => x.HomeBasedSupervision_Id == HomeBasedSupervision_Id).SingleOrDefault();
                    if (exist2  == null)
                    {

                        PCM_HB_CourtOutcome newHB = new PCM_HB_CourtOutcome();

                        newHB.Intake_Assessment_Id = assID;
                        newHB.HomeBasedSupervision_Id = vm.HomeBasedSupervision_Id;


                        db.PCM_HB_CourtOutcome.Add(newHB);
                        db.SaveChanges();
                    }

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        /// <summary>
        /// to edit PCM_HB_Supervision
        /// to get HomeBasedSupervision_Id
        /// </summary>
        /// <param name="HomeBasedSupervision_Id"></param>
        /// <returns></returns>
        public PCMHBSViewModel EditHBS(int HomeBasedSupervision_Id)
        {
            PCMHBSViewModel vm = new PCMHBSViewModel();
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? id = (from c in db.PCM_HB_Supervision
                               where (c.HomeBasedSupervision_Id == HomeBasedSupervision_Id)
                               select c.HomeBasedSupervision_Id).FirstOrDefault();

                    PCM_HB_Supervision s = db.PCM_HB_Supervision.Find(id);
                    if(s != null)
                    {
                        vm.HomeBasedSupervision_Id = db.PCM_HB_Supervision.Find(s.HomeBasedSupervision_Id).HomeBasedSupervision_Id;
                        vm.Intake_Assessment_Id = s.Intake_Assessment_Id;
                        vm.Compliance_Id = s.Conditions_Id;
                        vm.Visitation_Period = s.Visitation_Period;
                        vm.Number_of_Visit = s.Number_of_Visit;
                        vm.Placement_Date = s.Placement_Date;
                        vm.HBS_Supervisor_Id = s.HBS_Supervisor_Id;
                        vm.Placement_Confirmed = s.Placement_Confirmed;

                        db.SaveChanges();
                    }

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            return vm;
        }

        public PCMHBSViewModel GetHBS(int Id)
        {
            PCMHBSViewModel pvm = new PCMHBSViewModel();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            //var gd = db.PCM_General_Details.Find(Id);
            var gd = db.PCM_HB_Supervision.Find(Id);
            pvm.HomeBasedSupervision_Id = gd.HomeBasedSupervision_Id;
            pvm.Intake_Assessment_Id = gd.Intake_Assessment_Id;
            pvm.Compliance_Id = gd.Conditions_Id;
            pvm.Visitation_Period = gd.Visitation_Period;
            pvm.Number_of_Visit = gd.Number_of_Visit;
            pvm.Placement_Date = gd.Placement_Date;


            return pvm;
        }


        public int GetPCMHBByassId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join Case in db.PCM_HB_Supervision on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        where i.Intake_Assessment_Id.Equals(intAssessmentId)
                        select Case.HomeBasedSupervision_Id).FirstOrDefault();
            }
        }


        public void CreatePCMConditionsDeatils(PCMHBSViewModel cases, int HomebaseId, int Condition_Id, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    PCM_HBS_Condition newCase = new PCM_HBS_Condition();

                    newCase.HomeBasedSupervision_Id = HomebaseId;
                    newCase.Condition_Id = Condition_Id;
                    newCase.Created_By = userId;
                    newCase.Date_Created = DateTime.Now;
                    db.PCM_HBS_Condition.Add(newCase);

                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }


            }
        }

        public List<PCMHBSViewModel> GetSelectedConditionFromDB(int HomebaseId)
        {

            List<PCMHBSViewModel> avm = new List<PCMHBSViewModel>();
            // initialise connection
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            // get work list for user logged in
            var ConditionList =
                (from r in db.PCM_HB_Supervision
                 join o in db.PCM_HBS_Condition on r.HomeBasedSupervision_Id equals
                 o.HomeBasedSupervision_Id
                 join ot in db.apl_PCM_Conditions on o.Condition_Id equals ot.Condition_Id
                 //join rt in db.apl_Recommendation_Type on r.Recommendation_Type_Id equals rt.Recommendation_Type_Id

                 where (r.HomeBasedSupervision_Id == HomebaseId)
                 select new
                 {
                     r.Intake_Assessment_Id,
                     r.HomeBasedSupervision_Id,
                     o.HB_Condition_Id,
                     ot.Condition_Id,
                     //rt.Recommendation_Type_Id

                 }).ToList();
            ;
            foreach (var item in ConditionList)
            {

                // initialising view model
                PCMHBSViewModel obj = new PCMHBSViewModel();

                obj.HB_Condition_Id = item.HB_Condition_Id;
                obj.Description_Condition = db.apl_PCM_Conditions.Find(item.Condition_Id).Description;


                avm.Add(obj);
            }

            return avm;
        }


        public void DeleteConditionRecord(int id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                PCM_HBS_Condition Obj = (from j in db.PCM_HBS_Condition
                                         where j.HB_Condition_Id == id
                                  select j).FirstOrDefault();
                db.PCM_HBS_Condition.Remove(Obj);
                db.SaveChanges();
            }
        }


        /// <summary>
        /// populate Codition type
        /// table : PCM_HBS_Conditions
        /// </summary>
        /// <returns></returns>
        public List<ConditionLookupPcm> GetCondition()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Type = db.apl_PCM_Conditions.Select(o => new ConditionLookupPcm
                {
                    Condition_Id = o.Condition_Id,
                    Description = o.Description
                }).ToList();

                return Type;
            }
        }

        public List<HBSsupervisorTypeLookup> GetHBSsupervisorType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var HBSsupervisor_Type = db.PCM_HB.Select(o => new HBSsupervisorTypeLookup
                {
                    HBS_Supervisor_Id = o.HBS_Supervisor_Id,
                    HBS_Supervisor = o.HBS_Supervisor
                }).ToList();

                return HBSsupervisor_Type;
            }
        }


        public int GetPCMHBSByssId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Intake_Assessments
                        join Case in db.PCM_HB_Supervision on p.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        where p.Intake_Assessment_Id.Equals(intAssessmentId)
                        select Case.HomeBasedSupervision_Id).FirstOrDefault();
            }
        }

        #endregion


        #region visitation out come

        public List<PCMHBSViewModel> GetVOList(int assID)
        {
            List<PCMHBSViewModel> vm = new List<PCMHBSViewModel>();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            var hbsList = (from v in db.PCM_HB_VistationOutcome
                           join c in db.PCM_HB_Supervision
                           on v.HomeBasedSupervision_Id equals c.HomeBasedSupervision_Id
                           //join s in db.PCM_HB
                           //on h.HBS_Supervisor_Id equals s.HBS_Supervisor_Id

                           select new
                           {
                               v.HB_Visitaion_Outcome_Id,
                               v.Intake_Assessment_Id,
                               v.Conatact_Number,
                               v.Process_Notes,
                               v.Visitaion_Register,
                               v.HomeBasedSupervision_Id
                           }).ToList();
            foreach (var item in hbsList)
            {
                PCMHBSViewModel obj = new PCMHBSViewModel();
                obj.HB_Visitaion_Outcome_Id = item.HB_Visitaion_Outcome_Id;
                obj.Intake_Assessment_Id = item.Intake_Assessment_Id;
                obj.Conatact_Number = item.Conatact_Number;
                obj.Process_Notes = item.Process_Notes;
                obj.Visitaion_Register = item.Visitaion_Register;
                obj.Placement_Date = db.PCM_HB_Supervision.Find( item.HomeBasedSupervision_Id).Placement_Date;
                

                vm.Add(obj);
            }

            return vm;
        }

        public void CreateVO(PCMHBSViewModel vm, int Intake_Assessment_Id, int HomebaseId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    PCM_HB_VistationOutcome newVO = new PCM_HB_VistationOutcome();

                    newVO.Intake_Assessment_Id = Intake_Assessment_Id;
                    newVO.Conatact_Number = vm.Conatact_Number;
                    newVO.Process_Notes = vm.Process_Notes;
                    newVO.Visitaion_Register = vm.Visitaion_Register;
                    newVO.Compliance_Id = vm.Compliance_Id;
                    newVO.HomeBasedSupervision_Id = HomebaseId;

                    db.PCM_HB_VistationOutcome.Add(newVO);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                           
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        public PCMHBSViewModel EditVO(int HB_Visitaion_Outcome_Id)
        {
            PCMHBSViewModel vm = new PCMHBSViewModel();
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? id = (from c in db.PCM_HB_VistationOutcome
                               where (c.HB_Visitaion_Outcome_Id == HB_Visitaion_Outcome_Id)
                               select c.HB_Visitaion_Outcome_Id).FirstOrDefault();

                    PCM_HB_VistationOutcome s = db.PCM_HB_VistationOutcome.Find(id);
                    if (s != null)
                    {
                        vm.HB_Visitaion_Outcome_Id = db.PCM_HB_VistationOutcome.Find(s.HB_Visitaion_Outcome_Id).HB_Visitaion_Outcome_Id;
                        vm.Intake_Assessment_Id = s.Intake_Assessment_Id;
                        vm.Conatact_Number = s.Conatact_Number;
                        vm.Process_Notes = s.Process_Notes;
                        vm.Visitaion_Register = s.Visitaion_Register;
                        vm.Compliance_Id = s.Compliance_Id;

                        db.SaveChanges();
                    }

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            return vm;
        }

        public void UpdateVO(PCMHBSViewModel vm, int id, int Intake_Assessment_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    PCM_HB_VistationOutcome cc = db.PCM_HB_VistationOutcome.Find(id);
                    cc.Intake_Assessment_Id = Intake_Assessment_Id;
                    cc.Conatact_Number = vm.Conatact_Number;
                    cc.Process_Notes = vm.Process_Notes;
                    cc.Visitaion_Register = vm.Visitaion_Register;
                    cc.Compliance_Id = vm.Compliance_Id;

                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        
        }


        public List<ComplianceTypeLookup> GetComplianceType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Compliance_Type = db.apl_PCM_Compliance.Select(o => new ComplianceTypeLookup
                {
                    Compliance_Id = o.Compliance_Id,
                    Compliance = o.Description
                }).ToList();

                return Compliance_Type;
            }
        }



        public PCMHBSViewModel GetVisitationOutcometById(int VisitoutId)
        {
            PCMHBSViewModel vm = new PCMHBSViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    PCM_HB_VistationOutcome act = db.PCM_HB_VistationOutcome.Find(VisitoutId);
                    if (act != null)
                    {
                        vm.HB_Visitaion_Outcome_Id = act.HB_Visitaion_Outcome_Id;
                        vm.Intake_Assessment_Id = act.Intake_Assessment_Id;
                        vm.Conatact_Number = act.Conatact_Number;
                        vm.Process_Notes = act.Process_Notes;
                        vm.Visitaion_Register = act.Visitaion_Register;
                        vm.Compliance_Id = act.Compliance_Id;

                        if (act.Compliance_Id > 0)
                        {
                            vm.Compliance = db.apl_PCM_Compliance.Find(act.Compliance_Id).Description;
                        }
                       
                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                return vm;
            }
        }

        #endregion


        #region court outcome

        public List<PCMHBSViewModel> GetCourtOutComeList(int AssID)
        {
            List<PCMHBSViewModel> vm = new List<PCMHBSViewModel>();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            var OutComeList = (from a in db.PCM_HB_CourtOutcome

                           join b in db.PCM_HB_Supervision

                           on a.HomeBasedSupervision_Id equals b.HomeBasedSupervision_Id

                               select new
                           {
                               a.HB_CourtOutcome_Id,
                               a.Intake_Assessment_Id,
                               a.Remand,
                               a.Reason_Remand,
                               a.Court_Outcome,
                               b.HomeBasedSupervision_Id

                               }).ToList();
            foreach (var item in OutComeList)
            {
                PCMHBSViewModel obj = new PCMHBSViewModel();
                obj.HB_CourtOutcome_Id = item.HB_CourtOutcome_Id;
                obj.Intake_Assessment_Id = item.Intake_Assessment_Id;
                obj.Remand = item.Remand;
                obj.Reason_Remand = item.Reason_Remand;
                obj.Court_Outcome = item.Court_Outcome;
                obj.Placement_Date = db.PCM_HB_Supervision.Find(item.HomeBasedSupervision_Id).Placement_Date;
  
                vm.Add(obj);
            }
            return vm;
        }

        public void CreateCourtOutCome(PCMHBSViewModel vm, int Intake_Assessment_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    PCM_HB_CourtOutcome newOutCome = new PCM_HB_CourtOutcome();

                    newOutCome.Intake_Assessment_Id = Intake_Assessment_Id;
                    newOutCome.Remand = vm.Remand;
                    newOutCome.Reason_Remand = vm.Reason_Remand;
                    newOutCome.Next_Court_Date = vm.Next_Court_Date;
                    newOutCome.Court_Outcome = vm.Court_Outcome;
                    newOutCome.Court_Case_Status_Id = vm.HB_Case_Status_Id;

                    db.PCM_HB_CourtOutcome.Add(newOutCome);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);

                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
        }

        public PCMHBSViewModel EditCourtOutCome(int HB_CourtOutcome_Id)
        {
            PCMHBSViewModel vm = new PCMHBSViewModel();
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    //int? id = (from c in db.PCM_HB_CourtOutcome
                    //           where (c.HB_CourtOutcome_Id == HB_CourtOutcome_Id)
                    //           select c.HB_CourtOutcome_Id).FirstOrDefault();

                    PCM_HB_CourtOutcome s = db.PCM_HB_CourtOutcome.Find(HB_CourtOutcome_Id);
                    if (s != null)
                    {
                        vm.HB_CourtOutcome_Id = db.PCM_HB_CourtOutcome.Find(s.HB_CourtOutcome_Id).HB_CourtOutcome_Id;
                        vm.Intake_Assessment_Id = s.Intake_Assessment_Id;
                        vm.Remand = s.Remand;
                        vm.Reason_Remand = s.Reason_Remand;
                        vm.Court_Outcome = s.Court_Outcome;
                        vm.HB_Case_Status_Id = s.Court_Case_Status_Id;

                        db.SaveChanges();
                    }

                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
            }
            return vm;
        }

        public void UpdateCourtOutCome(PCMHBSViewModel vm, int id, int Intake_Assessment_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    PCM_HB_CourtOutcome cc = db.PCM_HB_CourtOutcome.Find(id);

                    cc.Intake_Assessment_Id = Intake_Assessment_Id;
                    cc.Remand = vm.Remand;
                    cc.Reason_Remand = vm.Reason_Remand;
                    cc.Court_Outcome = vm.Court_Outcome;
                    cc.Court_Case_Status_Id = vm.HB_Case_Status_Id;


                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<CaseStatusTypeLookup> GetCaseStatusType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var CaseStatus_Type = db.apl_PCM_Court_Case_Status.Select(o => new CaseStatusTypeLookup
                {
                    HB_Case_Status_Id = o.Court_Case_Status_Id,
                    HB_Case_Status = o.Description
                }).ToList();

                return CaseStatus_Type;
            }
        }


        public PCMHBSViewModel GetHBCourtOutcometById(int HB_CourtOutcome_Id)
        {
            PCMHBSViewModel vm = new PCMHBSViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    PCM_HB_CourtOutcome act = db.PCM_HB_CourtOutcome.Find(HB_CourtOutcome_Id);
                    if (act != null)
                    {
                        vm.HB_CourtOutcome_Id = act.HB_CourtOutcome_Id;
                        vm.Intake_Assessment_Id = act.Intake_Assessment_Id;
                        vm.Remand = act.Remand;
                        vm.Reason_Remand = act.Reason_Remand;
                        vm.Next_Court_Date = act.Next_Court_Date;
                        vm.Court_Outcome = act.Court_Outcome;
                        vm.HB_Case_Status_Id = act.Court_Case_Status_Id;

                        if (act.Court_Case_Status_Id > 0)
                        {
                            vm.HB_Case_Status = db.apl_PCM_Court_Case_Status.Find(act.Court_Case_Status_Id).Description;
                        }

                    }
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {

                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                return vm;
            }
        }

        #endregion

        
    }
}
