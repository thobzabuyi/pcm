using Common_Objects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Common_Objects.Models
{
    public class PCMnPrintLetter
    {
        private SDIIS_DatabaseEntities _dbpcm = new SDIIS_DatabaseEntities();
        #region Child
      

        public Person GetPersonalInformationChild(int pcmCaseid)
        {
            return (from c in _dbpcm.Persons
                    join d in _dbpcm.Clients on c.Person_Id equals d.Person_Id
                    join e in _dbpcm.Intake_Assessments on d.Client_Id equals e.Client_Id
                    join a in _dbpcm.PCM_Case_Details on e.Intake_Assessment_Id equals a.Intake_Assessment_Id
                    where a.Intake_Assessment_Id == pcmCaseid
                    select c).FirstOrDefault();
        }
        public int GetchildClientIdChild(int pcmCaseid)
        {
            return (from c in _dbpcm.Persons
                    join d in _dbpcm.Clients on c.Person_Id equals d.Person_Id
                    join e in _dbpcm.Intake_Assessments on d.Client_Id equals e.Client_Id
                    where e.Intake_Assessment_Id == pcmCaseid
                    select d.Client_Id).FirstOrDefault();
        }

        public PCM_Assessment_Register GetAssessmentRegister(int? pcmCaseid)
        {
            return (from q in _dbpcm.PCM_Assessment_Register
                    join o in _dbpcm.Intake_Assessments on q.Intake_Assessment_Id equals o.Intake_Assessment_Id
                    where (q.Intake_Assessment_Id == pcmCaseid) 
                    select q).FirstOrDefault();
        }

        public PCM_Health_Details GetMedicalInformation(int? pcmCaseid)
        {
            return (from q in _dbpcm.PCM_Health_Details
                    join o in _dbpcm.Intake_Assessments on q.Intake_Assessment_Id equals o.Intake_Assessment_Id
                    where (q.Intake_Assessment_Id == pcmCaseid)
                    select q).FirstOrDefault();
        }

     

        public Person GetADOPTIVEparentClientIdMALEChild(int? pcmCaseid)
        {
            return (from q in _dbpcm.Client_Adoptive_Parents
                    join o in _dbpcm.Persons on q.Person_Id equals o.Person_Id
                    join p in _dbpcm.Clients on q.Client_Id equals p.Client_Id
                    join r in _dbpcm.Intake_Assessments on p.Client_Id equals r.Client_Id
                    join a in _dbpcm.ADOPT_Case_Details on r.Intake_Assessment_Id equals a.Intake_Assessment_Id
                    join g in _dbpcm.Genders on o.Gender_Id equals g.Gender_Id
                    where (a.Adopt_Case_Id == pcmCaseid) && (g.Gender_Id == 1)
                    select o).FirstOrDefault();
        }


        public string GetreferenceNumber(int pcmCaseid)
        {
            return (from t in _dbpcm.ADOPT_Case_Details
                    join u in _dbpcm.Intake_Assessments on t.Intake_Assessment_Id equals u.Intake_Assessment_Id
                    join v in _dbpcm.Clients on u.Client_Id equals v.Client_Id
                    join re in _dbpcm.int_Client_Module_Registration on v.Client_Id equals re.Client_Id
                    join a in _dbpcm.ADOPT_Case_Details on u.Intake_Assessment_Id equals a.Intake_Assessment_Id
                    where t.Adopt_Case_Id == pcmCaseid
                    select re.Client_Module_Ref_No).FirstOrDefault();
        }

        public void AddAdoptionCorrespondence(int id, string commentCap, string corId, string filenameDB, int loggedInUser, int iD)
        {
            var currentHoursAndMinutes = DateTime.Now.Hour.ToString("0#") + DateTime.Now.Minute.ToString("0#") + DateTime.Now.Millisecond.ToString("0#");
            AdoptionPrintLetter Model = new AdoptionPrintLetter();

            var Table = new ADOPT_Letters();
            Table.Adopt_Correspondence_Comments = commentCap;
            Table.Adopt_Case_Id = id;
            Table.Intake_Assessment_Id = iD;
            Table.Correspondence_Type_Id = Convert.ToInt32(corId);
            Table.Adopt_Correspondence_FileName = filenameDB;
            Table.Adopt_Correspondence_Date_Created = DateTime.Now;
            var userModel = new UserModel();
            Table.Adopt_Correspondence_Created_By = loggedInUser;
            Table.Adopt_Correspondence_FilePath = "PathTemp";
            _dbpcm.ADOPT_Letters.Add(Table);
            _dbpcm.SaveChanges();
        }

        #endregion
    }
}
