﻿using Common_Objects.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Json;
using System.Web.Mvc;
using static Common_Objects.ViewModels.RACAPCaseViewModel;
using System.Data.Entity;
using System.Net.Mail;
using System.Web;

namespace Common_Objects.Models
{
    public class RACAPCaseDetailsModel
    {
        private SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

        #region Notification


        #region Child_Notification
        public RACAPNotifVMChild GetChildDetails(int Id, string UserName)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var DetailsDBTable = (from a in db.Intake_Assessments
                                  join b in db.Clients on a.Client_Id equals b.Client_Id
                                  join c in db.Persons on b.Person_Id equals c.Person_Id
                                  where a.Intake_Assessment_Id == Id
                                  select c).FirstOrDefault();
            var NewObj = new RACAPNotifVMChild();
            NewObj.PerSonId = DetailsDBTable.Person_Id;
            NewObj.AddedUpdated = 1;
            NewObj.From = (from t in db.Users
                           where t.User_Name.Contains(UserName)
                           select t.First_Name).FirstOrDefault()
              + " "
              + (from t in db.Users
                 where t.User_Name.Contains(UserName)
                 select t.Last_Name).FirstOrDefault();
            NewObj.ChildId = DetailsDBTable.Identification_Number;
            NewObj.ChildName = DetailsDBTable.First_Name;
            NewObj.ChildSurname = DetailsDBTable.Last_Name;
            NewObj.ChildRefNo = (from a in db.RACAP_Case_WorkList
                                where a.Intake_Assessment_Id == Id
                                 select a.Reference_Number).FirstOrDefault();
            return NewObj;
        }

        public RACAPNotifVMChild GetChildDetailsForMailing(int Id, string UserName)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var DetailsDBTable = (from a in db.Intake_Assessments
                                  join b in db.Clients on a.Client_Id equals b.Client_Id
                                  join c in db.Persons on b.Person_Id equals c.Person_Id
                                  where c.Person_Id == Id
                                  select c).FirstOrDefault();
            var NewObj = new RACAPNotifVMChild();
            NewObj.PerSonId = Id;
            NewObj.AddedUpdated = 1;
            NewObj.From = (from t in db.Users
                           where t.User_Name.Contains(UserName)
                           select t.First_Name).FirstOrDefault()
              + " "
              + (from t in db.Users
                 where t.User_Name.Contains(UserName)
                 select t.Last_Name).FirstOrDefault();
            NewObj.ChildId = DetailsDBTable.Identification_Number;
            NewObj.ChildName = DetailsDBTable.First_Name;
            NewObj.ChildSurname = DetailsDBTable.Last_Name;
            NewObj.ChildRefNo = (from a in db.Clients
                                 where a.Person_Id == DetailsDBTable.Person_Id
                                 select a.Reference_Number).FirstOrDefault();
            return NewObj;
        }


        #region SendingNotification
        //public async void EmailNotification(string userEmail, int Id, string UserName, string AU)
        //{
        //    SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
        //    SmtpClient mailUser = new SmtpClient();
        //    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

        //    msg.From.Host.Equals("10.121.72.80");
        //    msg.From.Address.Equals("hermanb@dsd.gov.za");


        //    //string userEmail = "RuthM@dsd.gov.za";
        //    msg.To.Add(userEmail);
        //    if (AU == "1") { 
        //    msg.Subject = "Newly captured Prospective Adoptive Parent";
        //    }
        //    if (AU == "2")
        //    {
        //        msg.Subject = "Updated captured Prospective Adoptive Parent";

        //    }
        //    msg.IsBodyHtml = true;
        //    msg.Body = "password1";
        //    mailUser.Send(msg);




        //    var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
        //    var message = new System.Net.Mail.MailMessage();
        //    string RecipientAddress = "Hermanb@dsd.gov.za";
        //    message.To.Add(new MailAddress(Convert.ToString(RecipientAddress)));  // replace with valid value 
        //    if (AU == "1")
        //    {
        //        var MessageSubject = "Newly captured Prospective Adoptive Parent".ToUpper();
        //        message.Subject = Convert.ToString(MessageSubject);
        //    }
        //    if (AU == "2")
        //    {
        //        var MessageSubject = "Updated captured Prospective Adoptive Parent".ToUpper();
        //        message.Subject = Convert.ToString(MessageSubject);
        //    }
        //    if (AU == "1") { 
        //    var MessageContent = "You are herewith informed that the following prospective adoptive Parent has been captured"
        //        +"<br/> onto the Social Develepment Integrated Case Management System (SDICMS): "
        //        +"<br/> Reference Number: " + (from r in db.Clients
        //                                     where r.Person_Id==Id
        //                                     select r.Reference_Number).FirstOrDefault()
        //        + "<br/> Parent Name: "+db.Persons.Find(Id).First_Name
        //        + "<br/> Parent Surname: " + db.Persons.Find(Id).Last_Name
        //        +"<br/> On: " +(from d in db.RACAP_Case_Details
        //                      join e in db.Intake_Assessments on d.Intake_Assessment_Id equals e.Intake_Assessment_Id
        //                      join f in db.Clients on e.Client_Id equals f.Client_Id
        //                      where f.Person_Id==Id
        //                      select d.Date_Created).FirstOrDefault()
        //        + "<br/> Captured by: " + UserName
        //        + "<br/>"
        //        + "<br/>"
        //        + "<br/>"
        //        + "<br/> Message submitted via the SDICMS.";
        //    var SendFromWhom = "Department of Social Development: SDICMS";

        //    //message.Attachments.Add(new Attachment("D:/IISsites/HRSystem1/HRSystem1/App_Data/AppointmentLetters/" + File__ + ".docx", "application/docx"));
        //    //message.Attachments.Add(new Attachment("D:\\IISsites\\HRSystem1\\HRSystem1\\App_Data\\Templates\\OfferOfEmployment.pdf"));

        //    message.Body = string.Format(body, Convert.ToString(SendFromWhom), Convert.ToString("npomail@dsd.gov.za"), Convert.ToString(MessageContent));
        //    message.IsBodyHtml = true;
        //    }
        //    if (AU == "2")
        //    { 
        //        var MessageContent = "You are herewith informed that the following prospective adoptive parent information has been updated"
        //    + "<br/> on the Social Develepment Integrated Case Management System (SDICMS): "
        //    + "<br/> Reference Number: " + (from r in db.Clients
        //                                    where r.Person_Id == Id
        //                                    select r.Reference_Number).FirstOrDefault()
        //    + "<br/> Parent Name: " + db.Persons.Find(Id).First_Name
        //    + "<br/> Parent Surname: " + db.Persons.Find(Id).Last_Name
        //    + "<br/> On: " + (from d in db.RACAP_Case_Details
        //                      join e in db.Intake_Assessments on d.Intake_Assessment_Id equals e.Intake_Assessment_Id
        //                      join f in db.Clients on e.Client_Id equals f.Client_Id
        //                      where f.Person_Id == Id
        //                      select d.Date_Created).FirstOrDefault()
        //    + "<br/> Captured by: " + UserName
        //    + "<br/>"
        //    + "<br/>"
        //    + "<br/>"
        //    + "<br/> Message submitted via the SDICMS.";
        //    var SendFromWhom = "Department of Social Development: SDICMS";

        //    //message.Attachments.Add(new Attachment("D:/IISsites/HRSystem1/HRSystem1/App_Data/AppointmentLetters/" + File__ + ".docx", "application/docx"));
        //    //message.Attachments.Add(new Attachment("D:\\IISsites\\HRSystem1\\HRSystem1\\App_Data\\Templates\\OfferOfEmployment.pdf"));

        //    message.Body = string.Format(body, Convert.ToString(SendFromWhom), Convert.ToString("npomail@dsd.gov.za"), Convert.ToString(MessageContent));
        //    message.IsBodyHtml = true;

        //}
        //    using (var smtp = new SmtpClient())
        //    {
        //        try
        //        {
        //            await smtp.SendMailAsync(message);
        //        }
        //        catch (SmtpFailedRecipientsException ex)
        //        {
        //            //for (int i = 0; i < ex.InnerExceptions.Length; i++)
        //            //{
        //            //    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
        //            //    if (status == SmtpStatusCode.MailboxBusy ||
        //            //        status == SmtpStatusCode.MailboxUnavailable)
        //            //    {
        //            //        MARMail.UpdateEmailFailure(message, DBTableAppnt.ApplicantId, 2);
        //            //        System.Threading.Thread.Sleep(5000);
        //            //        await smtp.SendMailAsync(message);
        //            //    }
        //            //    else
        //            //    {
        //            //        MARMail.UpdateEmailFailure(message, DBTableAppnt.ApplicantId, 3);
        //            //        Console.WriteLine("Failed to deliver message to {0}",
        //            //            ex.InnerExceptions[i].FailedRecipient);
        //            //    }
        //            //}
        //        }
        //        catch (Exception ex)
        //        {
        //            //MARMail.UpdateEmailFailure(message, DBTableAppnt.ApplicantId, 1);
        //            //Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
        //            //        ex.ToString());
        //        }
        //    }
           
        //}
        #endregion

        #endregion

        #region Parent_Notification
        public RACAPNotifVMParent GetParentDetails(int Id, string UserName)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var DetailsDBTable = (from a in db.Intake_Assessments
                                  join b in db.Clients on a.Client_Id equals b.Client_Id
                                  join c in db.Persons on b.Person_Id equals c.Person_Id
                                  where a.Intake_Assessment_Id == Id
                                  select c).FirstOrDefault();
            var NewObj = new RACAPNotifVMParent();
            NewObj.PerSonId = DetailsDBTable.Person_Id;
            //NewObj.AddedUpdated = 1;
            NewObj.From = (from t in db.Users
                          where t.User_Name.Contains(UserName)
                          select t.First_Name).FirstOrDefault() 
                          +" "
                          + (from t in db.Users
                             where t.User_Name.Contains(UserName)
                             select t.Last_Name).FirstOrDefault();
            NewObj.ParentId = DetailsDBTable.Identification_Number;
            NewObj.ParentName = DetailsDBTable.First_Name;
            NewObj.ParentSurname = DetailsDBTable.Last_Name;
            NewObj.ParentRefNo = (from a in db.RACAP_Case_WorkList
                                  where a.Intake_Assessment_Id == Id
                                  select a.Reference_Number).FirstOrDefault();
            return NewObj;
        }

        public RACAPNotifVMParent GetParentDetailsForMailing(int Id, string UserName)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var DetailsDBTable = (from a in db.Intake_Assessments
                                  join b in db.Clients on a.Client_Id equals b.Client_Id
                                  join c in db.Persons on b.Person_Id equals c.Person_Id
                                  where c.Person_Id == Id
                                  select c).FirstOrDefault();
            var NewObj = new RACAPNotifVMParent();
            NewObj.PerSonId = DetailsDBTable.Person_Id;
            NewObj.AddedUpdated = 1;
            NewObj.From = (from t in db.Users
                           where t.User_Name.Contains(UserName)
                           select t.First_Name).FirstOrDefault()
                          + " "
                          + (from t in db.Users
                             where t.User_Name.Contains(UserName)
                             select t.Last_Name).FirstOrDefault();
            NewObj.ParentId = DetailsDBTable.Identification_Number;
            NewObj.ParentName = DetailsDBTable.First_Name;
            NewObj.ParentSurname = DetailsDBTable.Last_Name;
            NewObj.ParentRefNo = (from a in db.Clients
                                  where a.Person_Id == DetailsDBTable.Person_Id
                                  select a.Reference_Number).FirstOrDefault();
            return NewObj;
        }

        public RACAPNotifVMParent GetSecondParentDetails(int Id)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            int ClienIdofSecondParent = (from a in db.Clients
                          join b in db.Intake_Assessments on a.Client_Id equals b.Client_Id
                          where a.Person_Id == Id
                          select a.Client_Id).FirstOrDefault();
            var SecondParentDetail = (from t in db.int_Client_ProspectiveAdoptiveParents
                                      join p in db.Persons on t.Person_Id equals p.Person_Id
                                      where t.Client_Id== ClienIdofSecondParent
                                      select p).FirstOrDefault();
            if (SecondParentDetail != null)
            {
                var NewObj = new RACAPNotifVMParent();
                NewObj.PerSonId = SecondParentDetail.Person_Id;

                NewObj.ParentId = SecondParentDetail.Identification_Number;
                NewObj.ParentName = SecondParentDetail.First_Name;
                NewObj.ParentSurname = SecondParentDetail.Last_Name;
                NewObj.ParentRefNo = (from a in db.Clients
                                      where a.Person_Id == SecondParentDetail.Person_Id
                                      select a.Reference_Number).FirstOrDefault();
                return NewObj;

            }
            else {
                var NewObj = new RACAPNotifVMParent();
                return NewObj;

            }
        }

        

        #region SendingNotification
        public async void EmailNotificationChild(string userEmail, int Id, string UserName, string AU)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            SmtpClient mailUser = new SmtpClient();
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.From.Host.Equals("10.121.72.80");
            msg.From.Address.Equals("SDICMS@dsd.gov.za");


            //string userEmail = "RuthM@dsd.gov.za";
            msg.To.Add(userEmail);
            if (AU == "1")
            {
                msg.Subject = "Newly captured Prospective Adoptive Child";
            }
            if (AU == "2")
            {
                msg.Subject = "Updated Prospective Adoptive Child";

            }
            msg.IsBodyHtml = true;
            msg.Body = "password1";
            mailUser.Send(msg);

            var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
            var message = new System.Net.Mail.MailMessage();
            string RecipientAddress = "hermanb@dsd.gov.za";
            message.To.Add(new MailAddress(Convert.ToString(RecipientAddress)));  // replace with valid value 
            if (AU == "1")
            {
                var MessageSubject = "Newly captured Prospective Adoptive Parent".ToUpper();

                message.Subject = Convert.ToString(MessageSubject);
            }
            if (AU == "2")
            {
                var MessageSubject = "Updated Prospective Adoptive Child".ToUpper();

                message.Subject = Convert.ToString(MessageSubject);
            }
            if (AU == "1")
            {
                var MessageContent = "You are herewith informed that the following prospective adoptive child has been captured"
                + "<br/> onto the Social Develepment Integrated Case Management System (SDICMS): "
                + "<br/> Reference Number: " + (from r in db.Clients
                                                where r.Person_Id == Id
                                                select r.Reference_Number).FirstOrDefault()
                + "<br/> Parent Name: " + db.Persons.Find(Id).First_Name
                + "<br/> Parent Surname: " + db.Persons.Find(Id).Last_Name
                + "<br/> Second Parent Name: " + db.Persons.Find(Id).First_Name
                + "<br/> Second Parent Surname: " + db.Persons.Find(Id).Last_Name
                + "<br/> On: " + (from d in db.RACAP_Case_Details
                                  join e in db.Intake_Assessments on d.Intake_Assessment_Id equals e.Intake_Assessment_Id
                                  join f in db.Clients on e.Client_Id equals f.Client_Id
                                  where f.Person_Id == Id
                                  select d.Date_Created).FirstOrDefault()
                + "<br/>"
                + "<br/> Captured by: " + UserName
                + "<br/>"
                + "<br/>"
                + "<br/> Message submitted via the SDICMS.";

                var SendFromWhom = "Department of Social Development: SDICMS";

                //message.Attachments.Add(new Attachment("D:/IISsites/HRSystem1/HRSystem1/App_Data/AppointmentLetters/" + File__ + ".docx", "application/docx"));
                //message.Attachments.Add(new Attachment("D:\\IISsites\\HRSystem1\\HRSystem1\\App_Data\\Templates\\OfferOfEmployment.pdf"));

                message.Body = string.Format(body, Convert.ToString(SendFromWhom), Convert.ToString("npomail@dsd.gov.za"), Convert.ToString(MessageContent));
                message.IsBodyHtml = true;
            }
            if (AU == "2")
            {
                var MessageContent = "You are herewith informed that the following prospective adoptive child's information was updated"
                + "<br/> on the Social Develepment Integrated Case Management System (SDICMS): "
                + "<br/> Reference Number: " + (from r in db.Clients
                                                where r.Person_Id == Id
                                                select r.Reference_Number).FirstOrDefault()
                + "<br/> Parent Name: " + db.Persons.Find(Id).First_Name
                + "<br/> Parent Surname: " + db.Persons.Find(Id).Last_Name
                + "<br/> Second Parent Name: " + db.Persons.Find(Id).First_Name
                + "<br/> Second Parent Surname: " + db.Persons.Find(Id).Last_Name
                + "<br/> On: " + (from d in db.RACAP_Case_Details
                                  join e in db.Intake_Assessments on d.Intake_Assessment_Id equals e.Intake_Assessment_Id
                                  join f in db.Clients on e.Client_Id equals f.Client_Id
                                  where f.Person_Id == Id
                                  select d.Date_Created).FirstOrDefault()
                + "<br/>"
                + "<br/> Captured by: " + UserName
                + "<br/>"
                + "<br/>"
                + "<br/> Message submitted via the SDICMS.";

                var SendFromWhom = "Department of Social Development: SDICMS";

                //message.Attachments.Add(new Attachment("D:/IISsites/HRSystem1/HRSystem1/App_Data/AppointmentLetters/" + File__ + ".docx", "application/docx"));
                //message.Attachments.Add(new Attachment("D:\\IISsites\\HRSystem1\\HRSystem1\\App_Data\\Templates\\OfferOfEmployment.pdf"));

                message.Body = string.Format(body, Convert.ToString(SendFromWhom), Convert.ToString("npomail@dsd.gov.za"), Convert.ToString(MessageContent));
                message.IsBodyHtml = true;
            }
            using (var smtp = new SmtpClient())
            {
                try
                {
                    await smtp.SendMailAsync(message);
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    Console.WriteLine(ex.Message);
                    //for (int i = 0; i < ex.InnerExceptions.Length; i++)
                    //{
                    //    SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
                    //    if (status == SmtpStatusCode.MailboxBusy ||
                    //        status == SmtpStatusCode.MailboxUnavailable)
                    //    {
                    //        MARMail.UpdateEmailFailure(message, DBTableAppnt.ApplicantId, 2);
                    //        System.Threading.Thread.Sleep(5000);
                    //        await smtp.SendMailAsync(message);
                    //    }
                    //    else
                    //    {
                    //        MARMail.UpdateEmailFailure(message, DBTableAppnt.ApplicantId, 3);
                    //        Console.WriteLine("Failed to deliver message to {0}",
                    //            ex.InnerExceptions[i].FailedRecipient);
                    //    }
                    //}
                }
                catch (Exception ex)
                {
                    //MARMail.UpdateEmailFailure(message, DBTableAppnt.ApplicantId, 1);
                    //Console.WriteLine("Exception caught in RetryIfBusy(): {0}",
                    //        ex.ToString());
                    Console.WriteLine(ex.Message);
                }
            }

        }
        #endregion

        #endregion
        #endregion

        #region Child
        #region Documents

        public List<RACAPViewParentDetailsViewModel> GetDocumentsList(int IntakeassId)
        {
            // initialising view model 
            List<RACAPViewParentDetailsViewModel> avm = new List<RACAPViewParentDetailsViewModel>();
            // initialise connection
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            // get work list for user logged in


            var ListD = (
                 from p in db.Persons
                 join c in db.Clients on p.Person_Id equals c.Person_Id
                 join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                 join j in db.RACAP_Case_Details on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                 join Case in db.RACAP_Supporting_Document on j.RACAP_Case_Id equals Case.RACAP_Case_Id

                 where i.Intake_Assessment_Id.Equals(IntakeassId)
                 select new
                 {
                     i.Intake_Assessment_Id,
                     Case.RACAP_File_Id,
                     Case.FileName,
                     Case.Document_Description,
                 }).ToList();

            foreach (var item in ListD)
            {

                // initialising view model
                RACAPViewParentDetailsViewModel obj = new RACAPViewParentDetailsViewModel();

                obj.int_Assesment_Id = item.Intake_Assessment_Id;
                obj.SupDocId = item.RACAP_File_Id;
                obj.SupDocDescription = item.FileName;
                avm.Add(obj);
            }
            return avm;
        }



        public void CreateRACAPDocuments(RACAPSupportingDocumentsVM docAttached)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())

            {
                try
                {
                    RACAPSupportingDocument newdocAttached = new RACAPSupportingDocument();
                    newdocAttached.Intake_Assessment_Id = docAttached.Intake_Assessment_Id;
                    newdocAttached.DocumentsFileName = docAttached.DocumentsFileName;
                    newdocAttached.DocumentsDescription = docAttached.DocumentsDescription;
                    newdocAttached.DocumentsLabel = 1;
                    newdocAttached.DocumentsPath = "~/App_Data/RACAPSupportingDocuments/";
                    newdocAttached.DocumentsCheck = docAttached.DocumentsCheck;
                    db.RACAPSupportingDocuments.Add(newdocAttached);
                    db.SaveChanges();
                }
                catch
                {

                }

            }

        }

        public RACAPSupportingDocumentsVM GetRACAPSupDocuments(int Id)
        {
            RACAPSupportingDocumentsVM ASDvm = new RACAPSupportingDocumentsVM();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var adoptDocDB = db.AdoptionSupportingDocuments.Find(Id);
            ASDvm.DocumentsId = adoptDocDB.DocumentsId;
            ASDvm.DocumentsFileName = adoptDocDB.DocumentsFileName;
            ASDvm.DocumentsDescription = adoptDocDB.DocumentsDescription;
            ASDvm.DocumentsLabel = adoptDocDB.DocumentsLabel;
            ASDvm.DocumentsPath = adoptDocDB.DocumentsPath;
            ASDvm.Intake_Assessment_Id = Convert.ToInt32(adoptDocDB.Intake_Assessment_Id);
            return ASDvm;
        }

        public List<RACAPSupportingDocumentsVM> GetRACAPSupportDocumentsList(int intake_Assessment_Id)
        {
            List<RACAPSupportingDocumentsVM> avm = new List<RACAPSupportingDocumentsVM>();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var documentsList =
                (from doc in db.RACAPSupportingDocuments
                 where (doc.Intake_Assessment_Id == intake_Assessment_Id)
                 select new
                 {
                     doc.Intake_Assessment_Id,
                     doc.SelectKindOfDocId,
                     doc.DocumentsFileName,
                     doc.DocumentsDescription,
                     doc.DocumentsLabel,
                     doc.DocumentsPath,
                     doc.DocumentsCheck,
                     doc.Document_Id

                 }).ToList();
            foreach (var item in documentsList)
            {
                RACAPSupportingDocumentsVM objadptDcmntsvm = new RACAPSupportingDocumentsVM();
                objadptDcmntsvm.DocumentsId = item.Document_Id;
                objadptDcmntsvm.Intake_Assessment_Id = item.Intake_Assessment_Id;
                objadptDcmntsvm.DocumentsFileName = item.DocumentsFileName;
                objadptDcmntsvm.DocumentsDescription = item.DocumentsDescription;
                objadptDcmntsvm.DocLabel = db.apl_ADOPTKindOfDocument.Find(item.DocumentsLabel).Description;
                objadptDcmntsvm.DocumentsPath = item.DocumentsPath;
                objadptDcmntsvm.KindOfDocument = db.apl_ADOPTKindOfDocument.Find(item.DocumentsCheck).Description;

                avm.Add(objadptDcmntsvm);
            }
            return avm;
        }

        #endregion Documents

        #region PERSON DETAILS
        // Get Adoption Client by int assessment ID
        public int GetRACAPPersonIdByintAssId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        where i.Intake_Assessment_Id.Equals(intAssessmentId)
                        select p.Person_Id).SingleOrDefault();
            }
        }

        //Update Intake Person Details from RACAP

        public void UpdateIntakePersonDetails(int personId, MainPageModelRACAPVM UpdatedInf)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var CheckPerson = db.Persons.Find(personId);
                if (UpdatedInf.RACAPPersonViewModel.Language_Id != CheckPerson.Language_Id)
                {
                    CheckPerson.Language_Id = UpdatedInf.RACAPPersonViewModel.Language_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.First_Name != CheckPerson.First_Name)
                {
                    CheckPerson.First_Name = UpdatedInf.RACAPPersonViewModel.First_Name;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Last_Name != CheckPerson.Last_Name)
                {
                    CheckPerson.Last_Name = UpdatedInf.RACAPPersonViewModel.Last_Name;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Known_As != CheckPerson.Known_As)
                {
                    CheckPerson.Known_As = UpdatedInf.RACAPPersonViewModel.Known_As;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Identification_Type_Id != CheckPerson.Identification_Type_Id)
                {
                    CheckPerson.Identification_Type_Id = UpdatedInf.RACAPPersonViewModel.Identification_Type_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Identification_Number != CheckPerson.Identification_Number)
                {
                    CheckPerson.Identification_Number = UpdatedInf.RACAPPersonViewModel.Identification_Number;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Date_Of_Birth != CheckPerson.Date_Of_Birth)
                {
                    CheckPerson.Date_Of_Birth = UpdatedInf.RACAPPersonViewModel.Date_Of_Birth;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Age != CheckPerson.Age)
                {
                    CheckPerson.Age = UpdatedInf.RACAPPersonViewModel.Age;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Is_Estimated_Age != CheckPerson.Is_Estimated_Age)
                {
                    CheckPerson.Is_Estimated_Age = UpdatedInf.RACAPPersonViewModel.Is_Estimated_Age;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Gender_Id != CheckPerson.Gender_Id)
                {
                    CheckPerson.Gender_Id = UpdatedInf.RACAPPersonViewModel.Gender_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Population_Group_Id != CheckPerson.Population_Group_Id)
                {
                    CheckPerson.Population_Group_Id = UpdatedInf.RACAPPersonViewModel.Population_Group_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Religion_Id != CheckPerson.Religion_Id)
                {
                    CheckPerson.Religion_Id = UpdatedInf.RACAPPersonViewModel.Religion_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Marital_Status_Id != CheckPerson.Marital_Status_Id)
                {
                    CheckPerson.Marital_Status_Id = UpdatedInf.RACAPPersonViewModel.Marital_Status_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Nationality_Id != CheckPerson.Nationality_Id)
                {
                    CheckPerson.Nationality_Id = UpdatedInf.RACAPPersonViewModel.Nationality_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Email_Address != CheckPerson.Email_Address)
                {
                    CheckPerson.Email_Address = UpdatedInf.RACAPPersonViewModel.Email_Address;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (UpdatedInf.RACAPPersonViewModel.Mobile_Phone_Number != CheckPerson.Mobile_Phone_Number)
                {
                    CheckPerson.Mobile_Phone_Number = UpdatedInf.RACAPPersonViewModel.Mobile_Phone_Number;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                //var clientPhysicalAddress = CheckPerson == null ? null : CheckPerson.Addresses.FirstOrDefault(x => x.Address_Type_Id.Equals((int)AddressTypeEnum.PhysicalAddress));
                //var clientPostalAddress = CheckPerson == null ? null : CheckPerson.Addresses.FirstOrDefault(x => x.Address_Type_Id.Equals((int)AddressTypeEnum.PostalAddress));
                var aDdresses = db.Persons.Find(personId).Addresses;
                var phadd = (from a in aDdresses
                             select a).FirstOrDefault();
                var posadd = (from a in aDdresses
                              select a).LastOrDefault();

                #region PhysicalAddress

                //if (UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Address_Line_1 != phadd.Address_Line_1)
                if (phadd != null) {
                    if (UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Address_Line_1 != phadd.Address_Line_1)
                    {
                        phadd.Address_Line_1 = UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Address_Line_1;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                    if (UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Address_Line_2 != phadd.Address_Line_2)
                    {
                        phadd.Address_Line_2 = UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Address_Line_2;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                    if (UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Address_Type_Id != phadd.Address_Type_Id)
                    {
                        phadd.Address_Type_Id = UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Address_Type_Id;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                    if (UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Town_Id != phadd.Town_Id)
                    {
                        phadd.Town_Id = UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Town_Id;
                        db.SaveChanges();
                        db.SaveChanges();
                    }

                    if (UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Postal_Code != phadd.Postal_Code)
                    {
                        phadd.Postal_Code = UpdatedInf.RACAPPersonViewModel.PhysicalAddress.Postal_Code;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                }
                #endregion

                #region PostalAddress
                if (posadd != null) {
                    if (UpdatedInf.RACAPPersonViewModel.PostalAddress.Address_Line_1 != posadd.Address_Line_1)
                    {
                        posadd.Address_Line_1 = UpdatedInf.RACAPPersonViewModel.PostalAddress.Address_Line_1;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                    if (UpdatedInf.RACAPPersonViewModel.PostalAddress.Address_Line_2 != posadd.Address_Line_2)
                    {
                        posadd.Address_Line_2 = UpdatedInf.RACAPPersonViewModel.PostalAddress.Address_Line_2;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                    if (UpdatedInf.RACAPPersonViewModel.PostalAddress.Address_Type_Id != 2)
                    {
                        posadd.Address_Type_Id = 2;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                    if (UpdatedInf.RACAPPersonViewModel.PostalAddress.Town_Id != posadd.Town_Id)
                    {
                        posadd.Town_Id = UpdatedInf.RACAPPersonViewModel.PostalAddress.Town_Id;
                        db.SaveChanges();
                        db.SaveChanges();
                    }

                    if (UpdatedInf.RACAPPersonViewModel.PostalAddress.Postal_Code != posadd.Postal_Code)
                    {
                        posadd.Postal_Code = UpdatedInf.RACAPPersonViewModel.PostalAddress.Postal_Code;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                }
                #endregion


            }
        }

        public void UpdateIntakeSecondPersonDetails(int IntakeAssessmentId, RACAPSecondParent SecParentObj, FormCollection Form_s)
        {

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                #region Form_S
                string useThisVal1 = "";
                if (Form_s.GetValue("Email_Address2P") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Email_Address2P").AttemptedValue;
                    if (Form_s.GetValue("Email_Address2P") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal1 = (Form_s.GetValue("Email_Address2P").AttemptedValue);
                        SecParentObj.Email_Address2P = useThisVal1;
                    }
                }


                string useThisVal2 = "";
                if (Form_s.GetValue("Mobile_Phone_Number") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Mobile_Phone_Number").AttemptedValue;
                    if (Form_s.GetValue("Mobile_Phone_Number") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal2 = (Form_s.GetValue("Mobile_Phone_Number").AttemptedValue);
                        SecParentObj.Mobile_Phone_Number = useThisVal2;
                    }
                }

                int useThisVal3 = 0;
                if (Form_s.GetValue("Marital_Status_Id") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Marital_Status_Id").AttemptedValue;
                    if (Form_s.GetValue("Marital_Status_Id") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal3 = Convert.ToInt32(Form_s.GetValue("Marital_Status_Id").AttemptedValue);
                        SecParentObj.Marital_Status_Id = useThisVal3;
                    }
                }

                int useThisVal4 = 0;
                if (Form_s.GetValue("Nationality_Id") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Nationality_Id").AttemptedValue;
                    if (Form_s.GetValue("Nationality_Id") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal4 = Convert.ToInt32(Form_s.GetValue("Nationality_Id").AttemptedValue);
                        SecParentObj.Nationality_Id = useThisVal4;
                    }
                }

                int useThisVal5 = 0;
                if (Form_s.GetValue("Population_Group_Id") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Population_Group_Id").AttemptedValue;
                    if (Form_s.GetValue("Population_Group_Id") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal5 = Convert.ToInt32(Form_s.GetValue("Population_Group_Id").AttemptedValue);
                        SecParentObj.Population_Group_Id = useThisVal5;
                    }
                }

                int useThisVal6 = 0;
                if (Form_s.GetValue("Religion_Id") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Religion_Id").AttemptedValue;
                    if (Form_s.GetValue("Religion_Id") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal6 = Convert.ToInt32(Form_s.GetValue("Religion_Id").AttemptedValue);
                        SecParentObj.Religion_Id = useThisVal6;
                    }
                }

                int useThisVal7 = 0;
                if (Form_s.GetValue("Gender_Id") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Gender_Id").AttemptedValue;
                    if (Form_s.GetValue("Gender_Id") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal7 = Convert.ToInt32(Form_s.GetValue("Gender_Id").AttemptedValue);
                        SecParentObj.Gender_Id = useThisVal7;
                    }
                }

                int useThisVal8 = 0;
                if (Form_s.GetValue("Age") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Age").AttemptedValue;
                    if (Form_s.GetValue("Age") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal8 = Convert.ToInt32(Form_s.GetValue("Age").AttemptedValue);
                        SecParentObj.Age = useThisVal8;
                    }
                }

                string useThisVal9 = "";
                if (Form_s.GetValue("IDNumber") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("IDNumber").AttemptedValue;
                    if (Form_s.GetValue("IDNumber") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal9 = (Form_s.GetValue("IDNumber").AttemptedValue);
                        SecParentObj.IDNumber = useThisVal9;
                    }
                }

                string useThisVal10 = "";
                if (Form_s.GetValue("PhysicalAddress.Address_Line_1") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PhysicalAddress.Address_Line_1").AttemptedValue;
                    if (Form_s.GetValue("PhysicalAddress.Address_Line_1") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal10 = (Form_s.GetValue("PhysicalAddress.Address_Line_1").AttemptedValue);
                        SecParentObj.PhysicalAddress.Address_Line_1 = useThisVal10;
                    }
                }
                string useThisVal11 = "";
                if (Form_s.GetValue("PhysicalAddress.Address_Line_2") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PhysicalAddress.Address_Line_2").AttemptedValue;
                    if (Form_s.GetValue("PhysicalAddress.Address_Line_2") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal11 = (Form_s.GetValue("PhysicalAddress.Address_Line_2").AttemptedValue);
                        SecParentObj.PhysicalAddress.Address_Line_2 = useThisVal11;
                    }
                }

                int useThisVal12 = 0;
                if (Form_s.GetValue("PhysicalAddress.Town_Id") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PhysicalAddress.Town_Id").AttemptedValue;
                    if (Form_s.GetValue("PhysicalAddress.Town_Id") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal12 = Convert.ToInt32(Form_s.GetValue("PhysicalAddress.Town_Id").AttemptedValue);
                        SecParentObj.PhysicalAddress.Town_Id = useThisVal12;
                    }
                }

                string useThisVal13 = "";
                if (Form_s.GetValue("PhysicalAddress.Postal_Code") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PhysicalAddress.Postal_Code").AttemptedValue;
                    if (Form_s.GetValue("PhysicalAddress.Postal_Code") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal13 = (Form_s.GetValue("PhysicalAddress.Postal_Code").AttemptedValue);
                        SecParentObj.PhysicalAddress.Address_Line_2 = useThisVal13;
                    }
                }

                string useThisVal14 = "";
                if (Form_s.GetValue("PostalAddress.Address_Line_1") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PostalAddress.Address_Line_1").AttemptedValue;
                    if (Form_s.GetValue("PostalAddress.Address_Line_1") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal14 = (Form_s.GetValue("PostalAddress.Address_Line_1").AttemptedValue);
                        SecParentObj.PostalAddress.Address_Line_1 = useThisVal14;
                    }
                }

                string useThisVal15 = "";
                if (Form_s.GetValue("PostalAddress.Address_Line_2") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PostalAddress.Address_Line_2").AttemptedValue;
                    if (Form_s.GetValue("PostalAddress.Address_Line_2") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal15 = (Form_s.GetValue("PostalAddress.Address_Line_2").AttemptedValue);
                        SecParentObj.PostalAddress.Address_Line_2 = useThisVal15;
                    }
                }

                int useThisVal16 = 0;
                if (Form_s.GetValue("PostalAddress.Town_Id") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PostalAddress.Town_Id").AttemptedValue;
                    if (Form_s.GetValue("PostalAddress.Town_Id") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal16 = Convert.ToInt32(Form_s.GetValue("PostalAddress.Town_Id").AttemptedValue);
                        SecParentObj.PostalAddress.Town_Id = useThisVal16;
                    }
                }

                string useThisVal17 = "";
                if (Form_s.GetValue("PostalAddress.Postal_Code") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("PostalAddress.Postal_Code").AttemptedValue;
                    if (Form_s.GetValue("PostalAddress.Postal_Code") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal17 = (Form_s.GetValue("PostalAddress.Postal_Code").AttemptedValue);
                        SecParentObj.PostalAddress.Address_Line_2 = useThisVal17;
                    }
                }

                string useThisVal18 = "";
                if (Form_s.GetValue("Identification_Type_Id_ParentTwo") != null)
                {
                    string TestToSeeIfEMailIsNotNull = Form_s.GetValue("Identification_Type_Id_ParentTwo").AttemptedValue;
                    if (Form_s.GetValue("Identification_Type_Id_ParentTwo") != null && TestToSeeIfEMailIsNotNull != "")
                    {
                        useThisVal18 = (Form_s.GetValue("Identification_Type_Id_ParentTwo").AttemptedValue);
                        SecParentObj.Identification_Type_Id_ParentTwo = useThisVal18;
                    }
                }




                #endregion

                int SecPersonId = (from p in db.Persons
                                   join q in db.Clients on p.Person_Id equals q.Person_Id
                                   join s in db.Intake_Assessments on q.Client_Id equals s.Client_Id
                                   join o in db.int_Client_ProspectiveAdoptiveParents on q.Client_Id equals o.Client_Id
                                   where s.Intake_Assessment_Id == IntakeAssessmentId
                                   select o.Person_Id).FirstOrDefault();
                var CheckPerson = db.Persons.Find(SecPersonId);

                if (SecParentObj.FirstName != CheckPerson.First_Name)
                {
                    CheckPerson.First_Name = SecParentObj.FirstName;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.LastName != CheckPerson.Last_Name)
                {
                    CheckPerson.Last_Name = SecParentObj.LastName;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.KnownAs != CheckPerson.Known_As)
                {
                    CheckPerson.Known_As = SecParentObj.KnownAs;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Identification_Type_Id_ParentTwo != "" || SecParentObj.Identification_Type_Id_ParentTwo != null)
                {
                    int IdentificationTypeParentTwo = (from i in db.Identification_Types
                                                       where i.Description.Contains(SecParentObj.Identification_Type_Id_ParentTwo)
                                                       select i.Identification_Type_Id).FirstOrDefault();

                    if (IdentificationTypeParentTwo != CheckPerson.Identification_Type_Id)
                    {
                        CheckPerson.Identification_Type_Id = IdentificationTypeParentTwo;
                        db.SaveChanges();
                        db.SaveChanges();
                    }
                }
                if (SecParentObj.IDNumber != CheckPerson.Identification_Number)
                {
                    CheckPerson.Identification_Number = SecParentObj.IDNumber;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Date_Of_Birth_ParentTwo != CheckPerson.Date_Of_Birth)
                {
                    CheckPerson.Date_Of_Birth = SecParentObj.Date_Of_Birth_ParentTwo;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Age != CheckPerson.Age)
                {
                    CheckPerson.Age = SecParentObj.Age;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Is_Estimated_Age != CheckPerson.Is_Estimated_Age)
                {
                    CheckPerson.Is_Estimated_Age = SecParentObj.Is_Estimated_Age;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Gender_Id != CheckPerson.Gender_Id) {
                    CheckPerson.Gender_Id = SecParentObj.Gender_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Population_Group_Id != CheckPerson.Population_Group_Id)
                {
                    CheckPerson.Population_Group_Id = SecParentObj.Population_Group_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Religion_Id != CheckPerson.Religion_Id)
                {
                    CheckPerson.Religion_Id = SecParentObj.Religion_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Marital_Status_Id != CheckPerson.Marital_Status_Id)
                {
                    CheckPerson.Marital_Status_Id = SecParentObj.Marital_Status_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Nationality_Id != CheckPerson.Nationality_Id)
                {
                    CheckPerson.Nationality_Id = SecParentObj.Nationality_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.Email_Address2P != CheckPerson.Email_Address)
                {
                    CheckPerson.Email_Address = SecParentObj.Email_Address2P;
                    db.SaveChanges();
                    db.SaveChanges();
                }

                if (SecParentObj.Mobile_Phone_Number != CheckPerson.Mobile_Phone_Number)
                {
                    CheckPerson.Mobile_Phone_Number = SecParentObj.Mobile_Phone_Number;
                    db.SaveChanges();
                    db.SaveChanges();
                }


                var aDdresses = db.Persons.Find(SecPersonId).Addresses;
                var phadd = (from a in aDdresses
                             select a).FirstOrDefault();
                var posadd = (from a in aDdresses
                              select a).LastOrDefault();

                #region PhysicalAddress

                if (SecParentObj.PhysicalAddress.Address_Line_1 != phadd.Address_Line_1)
                {
                    phadd.Address_Line_1 = SecParentObj.PhysicalAddress.Address_Line_1;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.PhysicalAddress.Address_Line_2 != phadd.Address_Line_2)
                {
                    phadd.Address_Line_2 = SecParentObj.PhysicalAddress.Address_Line_2;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.PhysicalAddress.Address_Type_Id != 1)
                {
                    phadd.Address_Type_Id = 1;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.PhysicalAddress.Town_Id != phadd.Town_Id)
                {
                    phadd.Town_Id = SecParentObj.PhysicalAddress.Town_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }

                if (SecParentObj.PhysicalAddress.Postal_Code != phadd.Postal_Code)
                {
                    phadd.Postal_Code = SecParentObj.PhysicalAddress.Postal_Code;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                #endregion

                #region PostalAddress
                if (SecParentObj.PostalAddress.Address_Line_1 != posadd.Address_Line_1)
                {
                    posadd.Address_Line_1 = SecParentObj.PostalAddress.Address_Line_1;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.PostalAddress.Address_Line_2 != posadd.Address_Line_2)
                {
                    posadd.Address_Line_2 = SecParentObj.PostalAddress.Address_Line_2;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.PostalAddress.Address_Type_Id != 2)
                {
                    posadd.Address_Type_Id = 2;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                if (SecParentObj.PostalAddress.Town_Id != posadd.Town_Id)
                {
                    posadd.Town_Id = SecParentObj.PostalAddress.Town_Id;
                    db.SaveChanges();
                    db.SaveChanges();
                }

                if (SecParentObj.PostalAddress.Postal_Code != posadd.Postal_Code)
                {
                    posadd.Postal_Code = SecParentObj.PostalAddress.Postal_Code;
                    db.SaveChanges();
                    db.SaveChanges();
                }
                #endregion
            }
        }


        // GET A PERSON BY PERSON ID
        public RACAPPersonViewModel GetRACAPPerson(int personId, int Id)
        {
            RACAPPersonViewModel Racapvm = new RACAPPersonViewModel();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            var act = (from a in db.Persons
                       join b in db.Clients on a.Person_Id equals b.Person_Id
                       join c in db.Intake_Assessments on b.Client_Id equals c.Client_Id
                       where a.Person_Id == personId && c.Intake_Assessment_Id == Id
                       select a).FirstOrDefault();
            //var clientPhysicalAddress = act == null ? null : act.Addresses.FirstOrDefault(x => x.Address_Type_Id.Equals((int)AddressTypeEnum.PhysicalAddress));
            //var clientPostalAddress = act == null ? null : act.Addresses.FirstOrDefault(x => x.Address_Type_Id.Equals((int)AddressTypeEnum.PostalAddress));

            var aDdresses = db.Persons.Find(personId).Addresses;
            var clientPhysicalAddress = (from a in aDdresses
                                         select a).FirstOrDefault();
            var clientPostalAddress = (from a in aDdresses
                                       select a).LastOrDefault();
            //db.Persons.Find(personId);
            Racapvm.Person_Id = personId;
            Racapvm.First_Name = act.First_Name;
            Racapvm.Last_Name = act.Last_Name;
            Racapvm.Known_As = act.Known_As;
            Racapvm.Identification_Type_Id = act.Identification_Type_Id;
            Racapvm.Identification_Number = act.Identification_Number;
            Racapvm.Date_Of_Birth = act.Date_Of_Birth;
            Racapvm.Age = act.Age;
            Racapvm.Is_Estimated_Age = act.Is_Estimated_Age;
            Racapvm.Language_Id = act.Language_Id;
            Racapvm.Gender_Id = act.Gender_Id;
            Racapvm.Marital_Status_Id = act.Marital_Status_Id;
            Racapvm.Preferred_Contact_Type_Id = act.Preferred_Contact_Type_Id;
            Racapvm.Religion_Id = act.Religion_Id;
            Racapvm.Phone_Number = act.Phone_Number;
            Racapvm.Mobile_Phone_Number = act.Mobile_Phone_Number;
            Racapvm.Email_Address = act.Email_Address;
            Racapvm.Population_Group_Id = act.Population_Group_Id;
            Racapvm.Nationality_Id = act.Nationality_Id;
            Racapvm.Disability_Type_Id = act.Disability_Type_Id;
            Racapvm.PhysicalAddress = clientPhysicalAddress;
            if (clientPhysicalAddress != null) {
                Racapvm.PhysicalAddress.Selected_Province_Id = (from k in db.Provinces
                                                                join l in db.Districts on k.Province_Id equals l.Province_Id
                                                                join m in db.Local_Municipalities on l.District_Id equals m.District_Municipality_Id
                                                                join n in db.Towns on m.Local_Municipality_Id equals n.Local_Municipality_Id
                                                                where n.Town_Id == clientPhysicalAddress.Town_Id
                                                                select k.Province_Id).FirstOrDefault();
                Racapvm.PhysicalAddress.Selected_Municipality_Id = (from k in db.Provinces
                                                                    join l in db.Districts on k.Province_Id equals l.Province_Id
                                                                    join m in db.Local_Municipalities on l.District_Id equals m.District_Municipality_Id
                                                                    join n in db.Towns on m.Local_Municipality_Id equals n.Local_Municipality_Id
                                                                    where n.Town_Id == clientPhysicalAddress.Town_Id
                                                                    select l.District_Id).FirstOrDefault();
                Racapvm.PhysicalAddress.Selected_Local_Municipality_Id = (from k in db.Provinces
                                                                          join l in db.Districts on k.Province_Id equals l.Province_Id
                                                                          join m in db.Local_Municipalities on l.District_Id equals m.District_Municipality_Id
                                                                          join n in db.Towns on m.Local_Municipality_Id equals n.Local_Municipality_Id
                                                                          where n.Town_Id == clientPhysicalAddress.Town_Id
                                                                          select m.Local_Municipality_Id).FirstOrDefault();
            }
            Racapvm.PostalAddress = clientPostalAddress;
            if (clientPostalAddress != null) {
                Racapvm.PostalAddress.Selected_Province_Id = (from k in db.Provinces
                                                              join l in db.Districts on k.Province_Id equals l.Province_Id
                                                              join m in db.Local_Municipalities on l.District_Id equals m.District_Municipality_Id
                                                              join n in db.Towns on m.Local_Municipality_Id equals n.Local_Municipality_Id
                                                              where n.Town_Id == clientPostalAddress.Town_Id
                                                              select k.Province_Id).FirstOrDefault();
                Racapvm.PostalAddress.Selected_Municipality_Id = (from k in db.Provinces
                                                                  join l in db.Districts on k.Province_Id equals l.Province_Id
                                                                  join m in db.Local_Municipalities on l.District_Id equals m.District_Municipality_Id
                                                                  join n in db.Towns on m.Local_Municipality_Id equals n.Local_Municipality_Id
                                                                  where n.Town_Id == clientPostalAddress.Town_Id
                                                                  select l.District_Id).FirstOrDefault();
                Racapvm.PostalAddress.Selected_Local_Municipality_Id = (from k in db.Provinces
                                                                        join l in db.Districts on k.Province_Id equals l.Province_Id
                                                                        join m in db.Local_Municipalities on l.District_Id equals m.District_Municipality_Id
                                                                        join n in db.Towns on m.Local_Municipality_Id equals n.Local_Municipality_Id
                                                                        where n.Town_Id == clientPostalAddress.Town_Id
                                                                        select m.Local_Municipality_Id).FirstOrDefault();
            }
            return Racapvm;
        }

        public RACAPPersonViewModel GetRACAPPersonChild(int personId)
        {
            RACAPPersonViewModel Adoptvm = new RACAPPersonViewModel();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            var act = db.Persons.Find(personId);
            Adoptvm.Person_Id = personId;
            Adoptvm.First_Name = act.First_Name;
            Adoptvm.Last_Name = act.Last_Name;
            Adoptvm.Known_As = act.Known_As;
            Adoptvm.Identification_Type_Id = act.Identification_Type_Id;
            Adoptvm.Identification_Number = act.Identification_Number;
            Adoptvm.Date_Of_Birth = act.Date_Of_Birth;

            DateTime old = Convert.ToDateTime(Adoptvm.Date_Of_Birth);

            int age = Convert.ToInt32(act.Age);
            //int age = DateTime.Now.AddYears(-old.Year).Year;

            Adoptvm.Age = age;

            Adoptvm.Is_Estimated_Age = act.Is_Estimated_Age;
            Adoptvm.Language_Id = act.Language_Id;
            Adoptvm.Gender_Id = act.Gender_Id;
            Adoptvm.Marital_Status_Id = act.Marital_Status_Id;
            Adoptvm.Preferred_Contact_Type_Id = act.Preferred_Contact_Type_Id;
            Adoptvm.Religion_Id = act.Religion_Id;
            Adoptvm.Phone_Number = act.Phone_Number;
            Adoptvm.Mobile_Phone_Number = act.Mobile_Phone_Number;
            Adoptvm.Email_Address = act.Email_Address;
            Adoptvm.Population_Group_Id = act.Population_Group_Id;
            Adoptvm.Nationality_Id = act.Nationality_Id;
            Adoptvm.Disability_Type_Id = act.Disability_Type_Id;

            return Adoptvm;
        }

        //UPDATE PERSON DETAILS 
        public void UpdateAdoptionPerson(RACAPPersonViewModel person, string userName)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    Person newPerson = db.Persons.Find(person.Person_Id);

                    newPerson.First_Name = person.First_Name;
                    newPerson.Last_Name = person.Last_Name;
                    newPerson.Known_As = person.Known_As;
                    newPerson.Identification_Type_Id = person.Identification_Type_Id;
                    newPerson.Identification_Number = person.Identification_Number;
                    newPerson.Date_Of_Birth = person.Date_Of_Birth;
                    newPerson.Age = person.Age;
                    newPerson.Is_Estimated_Age = person.Is_Estimated_Age;
                    newPerson.Language_Id = person.Language_Id;
                    newPerson.Gender_Id = person.Gender_Id;
                    newPerson.Marital_Status_Id = person.Marital_Status_Id;
                    newPerson.Preferred_Contact_Type_Id = person.Preferred_Contact_Type_Id;
                    newPerson.Religion_Id = person.Religion_Id;
                    newPerson.Phone_Number = person.Phone_Number;
                    newPerson.Mobile_Phone_Number = person.Mobile_Phone_Number;
                    newPerson.Email_Address = person.Email_Address;
                    newPerson.Population_Group_Id = person.Population_Group_Id;
                    newPerson.Nationality_Id = person.Nationality_Id;
                    newPerson.Disability_Type_Id = person.Disability_Type_Id;
                    newPerson.Modified_By = userName;
                    newPerson.Date_Last_Modified = DateTime.Now;
                    db.SaveChanges();
                }
                catch
                {

                }

            }

        }



        #endregion FOR PESRON DETAILS

        // RACAP CHILD CODE..................................................................................................
        #region  RACAP CHILD CODE  

        #region SEARCH ADD AND UPDATE RACAP CASE CHILD

        #region Other Function For Child

        public int GetNumberOfUploadedFiles(int RacapcaseId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var NumOfFiles = (from l in db.RACAP_Supporting_Document
                              where l.RACAP_Case_Id == RacapcaseId
                              select l).ToList();
            return NumOfFiles.Count();
        }
        public List<RACAPCaseGridMain> GetRACAPMATCHAss(int AdoptChildId)
        {
            // initializing view model
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            List<RACAPCaseGridMain> caseViewModel = new List<RACAPCaseGridMain>();
            int? RacapAdoptChildIntAssId = db.RACAP_Adoptive_Child.Find(AdoptChildId).Intake_Assessment_Id;
            int? RacapAdoptChildCaseId = db.RACAP_Adoptive_Child.Find(AdoptChildId).RACAP_Case_Id;

            var CurrentAdoptChild = (from l in db.Intake_Assessments
                                     join m in db.Clients on l.Client_Id equals m.Client_Id
                                     join n in db.Persons on m.Person_Id equals n.Person_Id
                                     join o in db.RACAP_Case_Details on l.Intake_Assessment_Id equals o.Intake_Assessment_Id
                                     where o.RACAP_Case_Id == RacapAdoptChildCaseId
                                     select n).FirstOrDefault();
            // get Adoption cases that have been accepted on adoption module

            var clientAssessments = (from p in db.Intake_Assessments
                                     join client in db.Clients on p.Client_Id equals client.Client_Id
                                     join person in db.Persons on client.Person_Id equals person.Person_Id
                                     join racapcase in db.RACAP_Case_Details on p.Intake_Assessment_Id equals racapcase.Intake_Assessment_Id
                                     //join subcat in db.Problem_Sub_Categories on p.Problem_Sub_Category_Id equals subcat.Problem_Sub_Category_Id
                                     //Problemsubcategory= 20
                                     join Parent in db.RACAP_Prospective_Parent on racapcase.RACAP_Case_Id equals Parent.RACAP_Case_Id
                                     //DataGridUpdate
                                     join ROR in db.RACAP_OrgansationResponsible on racapcase.RACAP_Case_Id equals ROR.RACAP_Case_Id into ps
                                     from ROR in ps.DefaultIfEmpty()
                                     join RCW in db.RACAP_Case_WorkList on racapcase.Intake_Assessment_Id equals RCW.Intake_Assessment_Id into rcw
                                     from RCW in rcw.DefaultIfEmpty()
                                     where (
                                               //Amendment3_20180713
                                               (p.Problem_Sub_Category_Id == 20)
                                               &&
                                               ((Parent.Gender_Id == CurrentAdoptChild.Gender_Id) && (Parent.Race_Id==CurrentAdoptChild.Population_Group_Id|| Parent.Race_Id==7) && (CurrentAdoptChild.Age>= Parent.Age_From) && (CurrentAdoptChild.Age <= Parent.Age_To))

                                           &&
                                           (racapcase.RACAP_Record_Status_Id == 1 )
                                           )// To be modified to check record Status

                                     group p by new
                                     {
                                         client.Client_Id,
                                         person.First_Name,
                                         person.Last_Name,
                                         person.Identification_Number,
                                         p.Intake_Assessment_Id,
                                         person.Population_Group_Id,
                                         person.Gender_Id,
                                         person.Age,
                                         racapcase.RACAP_Record_Status_Id,
                                         Parent.RACAP_Prospective_Parent_Id,
                                         ROR.Social_Worker_Id,
                                         RCW.Reference_Number
                                     }

                                     into g
                                     select new
                                     {
                                         g.Key.Intake_Assessment_Id,
                                         g.Key.Client_Id,
                                         g.Key.First_Name,
                                         g.Key.Last_Name,
                                         g.Key.Identification_Number,
                                         g.Key.RACAP_Record_Status_Id,
                                         g.Key.RACAP_Prospective_Parent_Id,
                                         g.Key.Social_Worker_Id,
                                         g.Key.Reference_Number,
                                         Assessments = g.ToList()
                                     }).ToList();

            ;
            foreach (var item in clientAssessments)
            {
                RACAPCaseGridMain obj = new RACAPCaseGridMain();

                obj.IntakeAssessmentId = item.Intake_Assessment_Id;
                obj.ClientId = item.Client_Id;
                obj.FirstName = item.First_Name;
                obj.LastName = item.Last_Name;
                obj.IDNumber = item.Identification_Number;
                if (item.RACAP_Record_Status_Id != null) { 
                obj.RACAPRecordStatusDescription = db.apl_RACAP_Record_Status.Find(item.RACAP_Record_Status_Id).Description;
                }
                if (item.Social_Worker_Id != null)
                {
                    obj.SocialWorkerName = db.Users.Find(item.Social_Worker_Id).First_Name + " " + db.Users.Find(item.Social_Worker_Id).Last_Name;
                    obj.MobileNumber = (from t in db.Employees
                                       where t.User_Id== (item.Social_Worker_Id)
                                       select t.Mobile_Phone_Number).FirstOrDefault() ;
                    obj.EmailAddress = db.Users.Find(item.Social_Worker_Id).Email_Address;
                }
                obj.RefNumber = item.Reference_Number;
                obj.RACAP_Prospective_Parent_Id = item.RACAP_Prospective_Parent_Id;
                caseViewModel.Add(obj);
            }
            return caseViewModel;
        }


        // GET Match In Proccess
        public List<RACAPCaseGridMain> GetRACAPMATCHInProcess(int id)
        {
            // initializing view model
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            List<RACAPCaseGridMain> caseViewModel = new List<RACAPCaseGridMain>();

            // get Adoption cases that have been accepted on adoption module

            var CheckIfFinailised = (from r in db.RACAP_Matches
                                     where r.RACAP_Adoptive_Child_Id == id && r.Created_By != null
                                     select r).ToList();
            if (CheckIfFinailised.Count > 0)
            {
                var ListofMatchesInProgress = (from matchesParent in db.RACAP_Matches

                                               join parent in db.RACAP_Adoptive_Child on matchesParent.RACAP_Adoptive_Child_Id equals parent.RACAP_Adoptive_Child_Id
                                               join caser in db.RACAP_Case_Details on parent.RACAP_Case_Id equals caser.RACAP_Case_Id
                                               //join p in db.Intake_Assessments on caser.Intake_Assessment_Id equals p.Intake_Assessment_Id
                                               //join client in db.Clients on p.Client_Id equals client.Client_Id
                                               //join person in db.Persons on client.Person_Id equals person.Person_Id
                                               where (matchesParent.RACAP_Adoptive_Child_Id == id) 
                                               //&& ((caser.RACAP_Record_Status_Id == 2|| caser.RACAP_Record_Status_Id == 3))
                                               //&& matchesParent.Created_By != null)
                                               select matchesParent).ToList();


                foreach (var item in ListofMatchesInProgress)
                {
                    RACAPCaseGridMain obj = new RACAPCaseGridMain();
                    int? PaId = db.RACAP_Adoptive_Child.Find(item.RACAP_Adoptive_Child_Id).Intake_Assessment_Id;
                    int UseThisClientId = (from a in db.Intake_Assessments
                                           join b in db.Clients on a.Client_Id equals b.Client_Id
                                           where a.Intake_Assessment_Id == (PaId)
                                           select b.Client_Id).FirstOrDefault();

                    int? IntAssChild = db.RACAP_Adoptive_Child.Find(item.RACAP_Adoptive_Child_Id).Intake_Assessment_Id;
                    int RACAPCaseIdParent = (from z in db.RACAP_Case_Details
                                             where z.Intake_Assessment_Id == IntAssChild
                                             select z.RACAP_Case_Id).FirstOrDefault();
                    int? StatusIdChild = db.RACAP_Case_Details.Find(RACAPCaseIdParent).RACAP_Record_Status_Id;

                    obj.IntakeAssessmentId = (from k in db.RACAP_Prospective_Parent
                                              where k.RACAP_Prospective_Parent_Id == (item.RACAP_Prospective_Parent_Id)
                                              select k.Intake_Assessment_Id).FirstOrDefault();
                    obj.ClientId = UseThisClientId;
                    obj.FirstName = (from b in db.Intake_Assessments
                                     join c in db.Clients on b.Client_Id equals c.Client_Id
                                     join p in db.Persons on c.Person_Id equals p.Person_Id
                                     where b.Intake_Assessment_Id == obj.IntakeAssessmentId
                                     select p.First_Name).FirstOrDefault();
                    obj.LastName = (from b in db.Intake_Assessments
                                    join c in db.Clients on b.Client_Id equals c.Client_Id
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where b.Intake_Assessment_Id == obj.IntakeAssessmentId
                                    select p.Last_Name).FirstOrDefault();
                    obj.IDNumber = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id == obj.ClientId
                                    select p.Identification_Number).FirstOrDefault();
                    if (StatusIdChild != null) { 
                        obj.RACAPRecordStatusDescription = db.apl_RACAP_Record_Status.Find(StatusIdChild).Description;
                    }
                    obj.RACAP_Adoptive_Child_Id = item.RACAP_Adoptive_Child_Id;
                    obj.RACAP_Prospective_Parent_Id = item.RACAP_Prospective_Parent_Id;
                    caseViewModel.Add(obj);
                }
                return caseViewModel;
            }
            else
            {
                var ListofMatchesInProgress = (from matchesParent in db.RACAP_Matches

                                               join parent in db.RACAP_Adoptive_Child on matchesParent.RACAP_Adoptive_Child_Id equals parent.RACAP_Adoptive_Child_Id
                                               join caser in db.RACAP_Case_Details on parent.RACAP_Case_Id equals caser.RACAP_Case_Id
                                               join p in db.Intake_Assessments on caser.Intake_Assessment_Id equals p.Intake_Assessment_Id
                                               join client in db.Clients on p.Client_Id equals client.Client_Id
                                               join person in db.Persons on client.Person_Id equals person.Person_Id
                                               where ((matchesParent.RACAP_Adoptive_Child_Id == id) && ((caser.RACAP_Record_Status_Id == 2))
                                               )
                                               select matchesParent).ToList();


                foreach (var item in ListofMatchesInProgress)
                {
                    RACAPCaseGridMain obj = new RACAPCaseGridMain();
                    int? CiId = (from z in db.RACAP_Adoptive_Child
                                 join y in db.RACAP_Case_Details on z.RACAP_Case_Id equals y.RACAP_Case_Id
                                 where z.RACAP_Adoptive_Child_Id == id
                                 select y.Intake_Assessment_Id).FirstOrDefault();

                    int UseThisClientId = (from a in db.Intake_Assessments
                                           join b in db.Clients on a.Client_Id equals b.Client_Id
                                           where a.Intake_Assessment_Id == (CiId)
                                           select b.Client_Id).FirstOrDefault();

                    int? IntAssChild = db.RACAP_Adoptive_Child.Find(item.RACAP_Adoptive_Child_Id).Intake_Assessment_Id;
                    int RACAPCaseIdParent = (from z in db.RACAP_Case_Details
                                             where z.Intake_Assessment_Id == IntAssChild
                                             select z.RACAP_Case_Id).FirstOrDefault();
                    //test
                    
                    int? StatusIdChild = db.RACAP_Case_Details.Find(RACAPCaseIdParent).RACAP_Record_Status_Id;
                    
                    obj.IntakeAssessmentId = (from k in db.RACAP_Prospective_Parent
                                              where k.RACAP_Prospective_Parent_Id == (item.RACAP_Prospective_Parent_Id)
                                              select k.Intake_Assessment_Id).FirstOrDefault();
                    obj.ClientId = UseThisClientId;
                    obj.FirstName = (from c in db.Clients
                                     join p in db.Persons on c.Person_Id equals p.Person_Id
                                     where c.Client_Id == obj.ClientId
                                     select p.First_Name).FirstOrDefault();
                    obj.LastName = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id == obj.ClientId
                                    select p.Last_Name).FirstOrDefault();
                    obj.IDNumber = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id == obj.ClientId
                                    select p.Identification_Number).FirstOrDefault();
                    if (StatusIdChild != null) { 
                        obj.RACAPRecordStatusDescription = db.apl_RACAP_Record_Status.Find(StatusIdChild).Description;
                    }
                    obj.RACAP_Adoptive_Child_Id = item.RACAP_Adoptive_Child_Id;
                    obj.RACAP_Prospective_Parent_Id = item.RACAP_Prospective_Parent_Id;
                    caseViewModel.Add(obj);
                }
                return caseViewModel;
            }
        }

        // UPDATE RACAP RECORD STATUS

        public void UpdateRACAPRecordStatus(RACAPCaseViewModel cases, int Pid, int Cid, int option, int LoggedIn_User_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int ParentCaseid = (from i in db.RACAP_Prospective_Parent
                                        join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                        where i.RACAP_Prospective_Parent_Id == Pid
                                        select b.RACAP_Case_Id).FirstOrDefault();
                    int UseThisPid = (from l in db.RACAP_Adoptive_Child
                                      where l.Intake_Assessment_Id == Cid
                                      select l.RACAP_Adoptive_Child_Id).FirstOrDefault();

                    int ChildCaseid = (from i in db.RACAP_Adoptive_Child
                                       join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                       where i.RACAP_Adoptive_Child_Id == Cid
                                       select b.RACAP_Case_Id).FirstOrDefault();

                    if (ParentCaseid != 0)
                    {
                        RACAP_Case_Details editp = db.RACAP_Case_Details.Find(ParentCaseid);
                        if (option == 1)
                        {
                            editp.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            editp.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            editp.RACAP_Record_Status_Id = 4;
                        }
                    }
                    if (ChildCaseid != 0)
                    {

                        RACAP_Case_Details edit = db.RACAP_Case_Details.Find(ChildCaseid);

                        if (option == 1)
                        {
                            edit.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            edit.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            edit.RACAP_Record_Status_Id = 4;
                        }
                    }

                    db.SaveChanges();
                    //Update RACAP_Matches tbl
                    int RACAP_Match_Id = (from x in db.RACAP_Matches
                                          where (x.RACAP_Prospective_Parent_Id == UseThisPid && x.RACAP_Adoptive_Child_Id == Cid)
                                          select x.RACAP_Match_Id).FirstOrDefault();
                    if (RACAP_Match_Id == 0)
                    {
                        RACAP_Matches RMat = new RACAP_Matches();
                        RMat.RACAP_Prospective_Parent_Id = UseThisPid;
                        RMat.RACAP_Adoptive_Child_Id = Cid;
                        RMat.Date_Created = DateTime.Now;
                        RMat.Created_By = LoggedIn_User_Id;
                        db.RACAP_Matches.Add(RMat);
                        db.SaveChanges();
                    }
                    if (RACAP_Match_Id != 0)
                    {
                        RACAP_Matches RMat = db.RACAP_Matches.Find(RACAP_Match_Id);
                        RMat.RACAP_Prospective_Parent_Id = UseThisPid;
                        RMat.RACAP_Adoptive_Child_Id = Cid;
                        RMat.Date_Modified = DateTime.Now;
                        RMat.Modified_By = LoggedIn_User_Id;
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

        public int? GetRACAPAdoptChildId(int Cid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                int? PerId= (from a in db.RACAP_Adoptive_Child
                        join b in db.RACAP_Case_Details on a.RACAP_Case_Id equals b.RACAP_Case_Id
                        join c in db.Intake_Assessments on b.Intake_Assessment_Id equals c.Intake_Assessment_Id
                        join e in db.Clients on c.Client_Id equals e.Client_Id
                        where a.RACAP_Adoptive_Child_Id == Cid
                        select c.Intake_Assessment_Id).FirstOrDefault();

                return PerId;
            }
        }

        #endregion

        //Addition by Herman
        #region Other Function for Parent
        
        public void RemoveFileFromDB(int id)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var RecordOfFileToBeRemoved = db.RACAP_Supporting_Document.Find(id);
            db.RACAP_Supporting_Document.Remove(RecordOfFileToBeRemoved);
            db.SaveChanges();
        }
        public List<RACAPCaseGridMain> GetRACAPMATCHAssParent(int? Intake_Assessment_Id, int? AgeF,int? AgeT, int? raceid, int? raceid_2, int? Gendeid)
        {
            //raceid 1 = Caucasian
            //raceid 2 = Black
            //raceid 3 = white
            //raceid 4 = Asian
            //raceid 5 = Coloured
            //raceid 6 = Indian
            //raceid 7 = Any

            // initializing view model
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            List<RACAPCaseGridMain> caseViewModel = new List<RACAPCaseGridMain>();
            int raceS = 0;
            if (raceid == 1) raceS = 1;
            if (raceid == 2) raceS = 2;
            if (raceid == 3) raceS = 3;
            if (raceid == 4) raceS = 4;
            if (raceid == 5) raceS = 5;
            if (raceid == 6) raceS = 6;
            if (raceid == 7) raceS = 7;
            int raceS_2 = 0;
            if (raceid_2 == 1) raceS_2 = 1;
            if (raceid_2 == 2) raceS_2 = 2;
            if (raceid_2 == 3) raceS_2 = 3;
            if (raceid_2 == 4) raceS_2 = 4;
            if (raceid_2 == 5) raceS_2 = 5;
            if (raceid_2 == 6) raceS_2 = 6;
            if (raceid_2 == 7) raceS_2 = 7;

            // get Adoption cases that have been accepted on adoption module

            var clientAssessments = (from p in db.Intake_Assessments
                                     join client in db.Clients on p.Client_Id equals client.Client_Id
                                     join person in db.Persons on client.Person_Id equals person.Person_Id
                                     join racapcase in db.RACAP_Case_Details on p.Intake_Assessment_Id equals racapcase.Intake_Assessment_Id
                                     //join subcat in db.Problem_Sub_Categories on p.Problem_Sub_Category_Id equals subcat.Problem_Sub_Category_Id

                                     join Child in db.RACAP_Adoptive_Child on racapcase.RACAP_Case_Id equals Child.RACAP_Case_Id
                                     //DataGridUpdate
                                     join ROR in db.RACAP_OrgansationResponsible on racapcase.RACAP_Case_Id equals ROR.RACAP_Case_Id into ror
                                     from ROR in ror.DefaultIfEmpty()
                                     join RCW in db.RACAP_Case_WorkList on racapcase.Intake_Assessment_Id equals RCW.Intake_Assessment_Id into rcw
                                     from RCW in rcw.DefaultIfEmpty()
                                     //join Parent in db.RACAP_Prospective_Parent on racapcase.RACAP_Case_Id equals Parent.RACAP_Case_Id
                                     where (
                                     //Amendment4_20180713
                                         (p.Problem_Sub_Category_Id == 19)
                                         &&
                                                (person.Gender_Id == Gendeid)
                                                &&
                                                (person.Population_Group_Id == raceS || person.Population_Group_Id == raceS_2||person.Population_Group_Id==7)
                                                &&
                                                (person.Age >= AgeF) && (person.Age <= AgeT)
                                     //Amendment1_20180713
                                     &&
                                     (racapcase.RACAP_Record_Status_Id == 1 
                                     //|| racapcase.RACAP_Record_Status_Id == 3
                                     )
                                     ) // To be modified to check record Status

                                     group p by new 
                                     {
                                         client.Client_Id,
                                         person.First_Name,
                                         person.Last_Name,
                                         person.Identification_Number,
                                         p.Intake_Assessment_Id,
                                         person.Population_Group_Id,
                                         person.Gender_Id,
                                         person.Age,
                                         racapcase.RACAP_Record_Status_Id,
                                         Child.RACAP_Adoptive_Child_Id,
                                         ROR.Social_Worker_Id,
                                         RCW.Reference_Number,
                                         //Parent.RACAP_Prospective_Parent_Id,
                                     }

                                     into g
                                     select  g).ToList();

            
            var NewList = (from a in clientAssessments
                           select a).Distinct().ToList();
            foreach (var item in NewList)
            {
                RACAPCaseGridMain obj = new RACAPCaseGridMain();

                obj.IntakeAssessmentId = item.Key.Intake_Assessment_Id;
                obj.ClientId = item.Key.Client_Id;
                obj.FirstName = item.Key.First_Name;
                obj.LastName = item.Key.Last_Name;
                obj.IDNumber = item.Key.Identification_Number;
                obj.RACAPRecordStatusDescription = db.apl_RACAP_Record_Status.Find(item.Key.RACAP_Record_Status_Id).Description;
                obj.RACAP_Adoptive_Child_Id = item.Key.RACAP_Adoptive_Child_Id;
                obj.RACAP_Prospective_Parent_Id = (from a in db.Intake_Assessments
                                                  join b in db.Clients on a.Client_Id equals b.Client_Id
                                                  join c in db.Persons on b.Person_Id equals c.Person_Id
                                                  where a.Intake_Assessment_Id== Intake_Assessment_Id
                                                  select c.Person_Id).FirstOrDefault();
                obj.Race = db.Races.Find(item.Key.Population_Group_Id).Description;
                obj.Age = Convert.ToString(item.Key.Age);
                if (item.Key.Social_Worker_Id != null) {
                    obj.SocialWorkerName = db.Users.Find(item.Key.Social_Worker_Id).First_Name + " " + db.Users.Find(item.Key.Social_Worker_Id).Last_Name;
                    obj.MobileNumber = (from t in db.Employees
                                       where t.User_Id== (item.Key.Social_Worker_Id)
                                       select t.Mobile_Phone_Number).FirstOrDefault();
                    obj.EmailAddress = db.Users.Find(item.Key.Social_Worker_Id).Email_Address;
                }
                obj.RefNumber = item.Key.Reference_Number;
                caseViewModel.Add(obj);
            }
            return caseViewModel;
        }
        //AdvancedSearch addition Herman
        
        //End AdvancedSearch addition Herman

        // GET Match In Proccess
        public List<RACAPCaseGridMain> GetRACAPMATCHInProcessParent(int id)
        {
            // initializing view model
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            List<RACAPCaseGridMain> caseViewModel = new List<RACAPCaseGridMain>();

            // get Adoption cases that have been accepted on adoption module

            var CheckIfFinailised = (from r in db.RACAP_Matches
                                    where r.RACAP_Prospective_Parent_Id==id && r.Modified_By!=null
                                    select r).ToList();
            if (CheckIfFinailised.Count > 0)
            {
                var ListofMatchesInProgress = (from matchesParent in db.RACAP_Matches

                                               join parent in db.RACAP_Prospective_Parent on matchesParent.RACAP_Prospective_Parent_Id equals parent.RACAP_Prospective_Parent_Id
                                               join caser in db.RACAP_Case_Details on parent.RACAP_Case_Id equals caser.RACAP_Case_Id
                                               join p in db.Intake_Assessments on caser.Intake_Assessment_Id equals p.Intake_Assessment_Id
                                               join client in db.Clients on p.Client_Id equals client.Client_Id
                                               join person in db.Persons on client.Person_Id equals person.Person_Id
                                               where ((matchesParent.RACAP_Prospective_Parent_Id == id) && ((caser.RACAP_Record_Status_Id == 2||caser.RACAP_Record_Status_Id == 3))
                                               && matchesParent.Modified_By!=null)
                                               select matchesParent).ToList();


                foreach (var item in ListofMatchesInProgress)
                {
                    RACAPCaseGridMain obj = new RACAPCaseGridMain();
                    int? ClId = db.RACAP_Adoptive_Child.Find(item.RACAP_Adoptive_Child_Id).Intake_Assessment_Id;
                    int UseThisClientId = (from a in db.Intake_Assessments
                                           join b in db.Clients on a.Client_Id equals b.Client_Id
                                           where a.Intake_Assessment_Id == (ClId)
                                           select b.Client_Id).FirstOrDefault();

                    int? IntAssParent = db.RACAP_Prospective_Parent.Find(item.RACAP_Prospective_Parent_Id).Intake_Assessment_Id;
                    int RACAPCaseIdParent = (from z in db.RACAP_Case_Details
                                             where z.Intake_Assessment_Id == IntAssParent
                                             select z.RACAP_Case_Id).FirstOrDefault();
                    int? StatusIdParent = db.RACAP_Case_Details.Find(RACAPCaseIdParent).RACAP_Record_Status_Id;

                    obj.IntakeAssessmentId = (from k in db.RACAP_Adoptive_Child
                                              where k.RACAP_Adoptive_Child_Id == (item.RACAP_Adoptive_Child_Id)
                                              select k.Intake_Assessment_Id).FirstOrDefault();
                    obj.ClientId = UseThisClientId;
                    obj.FirstName = (from c in db.Clients
                                     join p in db.Persons on c.Person_Id equals p.Person_Id
                                     where c.Client_Id == obj.ClientId
                                     select p.First_Name).FirstOrDefault();
                    obj.LastName = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id == obj.ClientId
                                    select p.Last_Name).FirstOrDefault();
                    obj.IDNumber = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id == obj.ClientId
                                    select p.Identification_Number).FirstOrDefault();
                    obj.RACAPRecordStatusDescription = db.apl_RACAP_Record_Status.Find(StatusIdParent).Description;
                    obj.RACAP_Adoptive_Child_Id = item.RACAP_Adoptive_Child_Id;
                    obj.RACAP_Prospective_Parent_Id = item.RACAP_Prospective_Parent_Id;
                    caseViewModel.Add(obj);
                }
                return caseViewModel;
            }
            
            else
            { 

            var ListofMatchesInProgress = (from matchesParent in db.RACAP_Matches

                           join parent in db.RACAP_Prospective_Parent on matchesParent.RACAP_Prospective_Parent_Id equals parent.RACAP_Prospective_Parent_Id
                           join caser in db.RACAP_Case_Details on parent.RACAP_Case_Id equals caser.RACAP_Case_Id
                           join p in db.Intake_Assessments on caser.Intake_Assessment_Id equals p.Intake_Assessment_Id
                           join client in db.Clients on p.Client_Id equals client.Client_Id
                           join person in db.Persons on client.Person_Id equals person.Person_Id
                           where ((matchesParent.RACAP_Prospective_Parent_Id == id) && ((caser.RACAP_Record_Status_Id == 2) ))
                           select matchesParent).ToList();


                foreach (var item in ListofMatchesInProgress)
                {
                    RACAPCaseGridMain obj = new RACAPCaseGridMain();
                    int? ClId = db.RACAP_Adoptive_Child.Find(item.RACAP_Adoptive_Child_Id).Intake_Assessment_Id;
                    int UseThisClientId = (from a in db.Intake_Assessments
                                           join b in db.Clients on a.Client_Id equals b.Client_Id
                                           where a.Intake_Assessment_Id == (ClId)
                                           select b.Client_Id).FirstOrDefault();

                    int? IntAssParent = db.RACAP_Prospective_Parent.Find(item.RACAP_Prospective_Parent_Id).Intake_Assessment_Id;
                    int RACAPCaseIdParent = (from z in db.RACAP_Case_Details
                                             where z.Intake_Assessment_Id == IntAssParent
                                             select z.RACAP_Case_Id).FirstOrDefault();
                    int? StatusIdParent = db.RACAP_Case_Details.Find(RACAPCaseIdParent).RACAP_Record_Status_Id;

                    obj.IntakeAssessmentId = (from k in db.RACAP_Adoptive_Child
                                              where k.RACAP_Adoptive_Child_Id == (item.RACAP_Adoptive_Child_Id)
                                              select k.Intake_Assessment_Id).FirstOrDefault();
                    obj.ClientId = UseThisClientId;
                    obj.FirstName = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id==obj.ClientId
                                    select p.First_Name).FirstOrDefault();
                    obj.LastName = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id == obj.ClientId
                                    select p.Last_Name).FirstOrDefault();
                    obj.IDNumber = (from c in db.Clients
                                    join p in db.Persons on c.Person_Id equals p.Person_Id
                                    where c.Client_Id == obj.ClientId
                                    select p.Identification_Number).FirstOrDefault();
                    obj.RACAPRecordStatusDescription = db.apl_RACAP_Record_Status.Find(StatusIdParent).Description;
                    obj.RACAP_Adoptive_Child_Id = item.RACAP_Adoptive_Child_Id;
                    obj.RACAP_Prospective_Parent_Id = item.RACAP_Prospective_Parent_Id;
                    caseViewModel.Add(obj);
                }
                return caseViewModel;
            }
        }

        // UPDATE RACAP RECORD STATUS

        //public void UpdateRACAPRecordStatusParent(RACAPCaseViewModel cases, int Pid, int Cid, int option)
        public void UpdateRACAPRecordStatusParent(int? Pid, int? option, int LoggedIn_User_Id,int Cid)
        {
            //start here to check new var
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    //Amendment_20180717
                    int ParentCaseid = (from b in db.RACAP_Case_Details
                                        where b.Intake_Assessment_Id == Pid
                                        select b.RACAP_Case_Id).FirstOrDefault();
                    int RCWLId = (from x in db.RACAP_Case_WorkList
                                  join y in db.Intake_Assessments on x.Intake_Assessment_Id equals y.Intake_Assessment_Id
                                  where y.Intake_Assessment_Id == Pid
                                  select x.RACAP_CaseWoklist_Id).FirstOrDefault();
                    int UseThisPid = (from l in db.RACAP_Prospective_Parent
                                      where l.Intake_Assessment_Id == Pid
                                      select l.RACAP_Prospective_Parent_Id).FirstOrDefault();

                   

                    if (ParentCaseid != 0)
                    {
                        RACAP_Case_Details editp = db.RACAP_Case_Details.Find(ParentCaseid);
                        RACAP_Case_Details editc = db.RACAP_Case_Details.Find(Cid);
                        RACAP_Case_WorkList editRCWL = db.RACAP_Case_WorkList.Find(RCWLId);
                        if (option == 1)
                        {
                            editp.RACAP_Record_Status_Id = 2;
                            //editp.RACAP_Registration_Status_Id = 2;
                            editc.RACAP_Record_Status_Id = 2;
                            //editc.RACAP_Registration_Status_Id = 2;
                            db.SaveChanges();
                            editRCWL.RACAP_Record_Status_Id = 2;
                            db.SaveChanges();
                        }
                        if (option == 2)
                        {
                            editp.RACAP_Record_Status_Id = 3;
                            //editp.RACAP_Registration_Status_Id = 3;
                            editc.RACAP_Record_Status_Id = 3;
                            //editc.RACAP_Registration_Status_Id = 3;
                            db.SaveChanges();
                            editRCWL.RACAP_Record_Status_Id = 3;
                            db.SaveChanges();

                        }
                        if (option == 3)
                        {
                            editp.RACAP_Record_Status_Id = 4;
                            editc.RACAP_Record_Status_Id = 4;
                            db.SaveChanges();
                            editRCWL.RACAP_Record_Status_Id = 4;
                            db.SaveChanges();
                        }
                    }
                    
                    //Update RACAP_Matches tbl
                    int RACAP_Match_Id = (from x in db.RACAP_Matches
                                          where (x.RACAP_Prospective_Parent_Id == UseThisPid )
                                          select x.RACAP_Match_Id).FirstOrDefault();
                    if(RACAP_Match_Id==0)
                    { 
                    RACAP_Matches RMat = new RACAP_Matches();
                        RMat.RACAP_Prospective_Parent_Id = UseThisPid;
                        RMat.RACAP_Adoptive_Child_Id = Cid;
                        RMat.Date_Created = DateTime.Now;
                        RMat.Created_By = LoggedIn_User_Id;
                        db.RACAP_Matches.Add(RMat);
                    db.SaveChanges();
                    }
                    if (RACAP_Match_Id != 0)
                    {
                        RACAP_Matches RMat = db.RACAP_Matches.Find(RACAP_Match_Id);
                        RMat.RACAP_Prospective_Parent_Id = UseThisPid;
                        RMat.RACAP_Adoptive_Child_Id = Cid;
                        RMat.Date_Modified = DateTime.Now;
                        RMat.Modified_By = LoggedIn_User_Id;
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

        public void UpdateRACAPRecordStatusParent_1(RACAPCaseViewModel cases, int? Pid, int Cid, int option, int LoggedIn_User_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    //Amendment_20180717
                    int ParentCaseid = (from i in db.RACAP_Case_Details
                                        join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                                        join k in db.RACAP_Prospective_Parent on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                                        where k.RACAP_Prospective_Parent_Id == Pid
                                        select i.RACAP_Case_Id).FirstOrDefault();
                    int RCWLId = (from x in db.RACAP_Case_WorkList
                                  join y in db.Intake_Assessments on x.Intake_Assessment_Id equals y.Intake_Assessment_Id
                                  join z in db.RACAP_Prospective_Parent on y.Intake_Assessment_Id equals z.Intake_Assessment_Id                   
                                  where z.RACAP_Prospective_Parent_Id == Pid
                                  select x.RACAP_CaseWoklist_Id).FirstOrDefault();

                    int ChildCaseid = (from i in db.RACAP_Case_Details
                                       join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                                       join k in db.RACAP_Adoptive_Child on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                                       where k.RACAP_Adoptive_Child_Id == Cid
                                       select i.RACAP_Case_Id).FirstOrDefault();

                    var RacapMatch = (from n in db.RACAP_Matches
                                      where n.RACAP_Prospective_Parent_Id == Pid && n.RACAP_Adoptive_Child_Id == Cid
                                      select n).FirstOrDefault();
                    if (RacapMatch != null)
                    {
                        RacapMatch.Modified_By = LoggedIn_User_Id;
                        db.SaveChanges();
                    }

                    if (ParentCaseid != 0)
                    {
                        RACAP_Case_Details editp = db.RACAP_Case_Details.Find(ParentCaseid);
                        RACAP_Case_WorkList editRCWL = db.RACAP_Case_WorkList.Find(RCWLId);
                        if (option == 1)
                        {
                            editp.RACAP_Record_Status_Id = 2;
                            editRCWL.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            editp.RACAP_Record_Status_Id = 3;
                            editRCWL.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            editp.RACAP_Record_Status_Id = 4;
                            editRCWL.RACAP_Record_Status_Id = 4;
                        }
                        db.SaveChanges();
                    }
                    if (ChildCaseid != 0)
                    {

                        RACAP_Case_Details edit = db.RACAP_Case_Details.Find(ChildCaseid);
                        RACAP_Case_WorkList editRCWL = db.RACAP_Case_WorkList.Find(RCWLId);

                        if (option == 1)
                        {
                            edit.RACAP_Record_Status_Id = 2;
                            editRCWL.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            edit.RACAP_Record_Status_Id = 3;
                            editRCWL.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            edit.RACAP_Record_Status_Id = 4;
                            editRCWL.RACAP_Record_Status_Id = 4;
                        }
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

        public void UpdateRACAPRecordStatusParent_2(RACAPCaseViewModel cases, int? Pid, int Cid, int option, int LoggedIn_User_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    //Amendment_20180717
                    int ParentCaseid = (from i in db.RACAP_Case_Details
                                        join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                                        join k in db.RACAP_Prospective_Parent on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                                        where k.RACAP_Prospective_Parent_Id == Pid
                                        select i.RACAP_Case_Id).FirstOrDefault();
                    int RCWLId = (from x in db.RACAP_Case_WorkList
                                  join y in db.Intake_Assessments on x.Intake_Assessment_Id equals y.Intake_Assessment_Id
                                  join z in db.RACAP_Adoptive_Child on y.Intake_Assessment_Id equals z.Intake_Assessment_Id
                                  where z.RACAP_Adoptive_Child_Id == Cid
                                  select x.RACAP_CaseWoklist_Id).FirstOrDefault();

                    int ChildCaseid = (from i in db.RACAP_Case_Details
                                       join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                                       join k in db.RACAP_Adoptive_Child on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                                       where k.RACAP_Adoptive_Child_Id == Cid
                                       select i.RACAP_Case_Id).FirstOrDefault();

                    var RacapMatch = (from n in db.RACAP_Matches
                                      where n.RACAP_Prospective_Parent_Id == Pid && n.RACAP_Adoptive_Child_Id == Cid
                                      select n).FirstOrDefault();
                    if (RacapMatch != null)
                    {
                        RacapMatch.Modified_By = LoggedIn_User_Id;
                        db.SaveChanges();
                    }

                    if (ParentCaseid != 0)
                    {
                        RACAP_Case_Details editp = db.RACAP_Case_Details.Find(ParentCaseid);
                        RACAP_Case_WorkList editRCWL = db.RACAP_Case_WorkList.Find(RCWLId);

                        if (option == 1)
                        {
                            editp.RACAP_Record_Status_Id = 2;
                            editRCWL.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            editp.RACAP_Record_Status_Id = 3;
                            editRCWL.RACAP_Record_Status_Id = 3;

                        }
                        if (option == 3)
                        {
                            editp.RACAP_Record_Status_Id = 4;
                            editRCWL.RACAP_Record_Status_Id = 4;

                        }
                        db.SaveChanges();
                    }
                    if (ChildCaseid != 0)
                    {

                        RACAP_Case_Details edit = db.RACAP_Case_Details.Find(ChildCaseid);
                        RACAP_Case_WorkList editRCWL = db.RACAP_Case_WorkList.Find(RCWLId);

                        if (option == 1)
                        {
                            edit.RACAP_Record_Status_Id = 2;
                            editRCWL.RACAP_Record_Status_Id = 2;

                        }
                        if (option == 2)
                        {
                            edit.RACAP_Record_Status_Id = 3;
                            editRCWL.RACAP_Record_Status_Id = 3;

                        }
                        if (option == 3)
                        {
                            edit.RACAP_Record_Status_Id = 4;
                            editRCWL.RACAP_Record_Status_Id = 4;

                        }
                        db.SaveChanges();

                    }
                    //Update RACAP_Matches tbl
                    int RACAP_Match_Id = (from x in db.RACAP_Matches
                                          where (x.RACAP_Prospective_Parent_Id == Pid && x.RACAP_Adoptive_Child_Id == Cid)
                                          select x.RACAP_Match_Id).FirstOrDefault();
                    if (RACAP_Match_Id == 0)
                    {
                        RACAP_Matches RMat = new RACAP_Matches();
                        RMat.RACAP_Prospective_Parent_Id = Pid;
                        RMat.RACAP_Adoptive_Child_Id = Cid;
                        RMat.Date_Created = DateTime.Now;
                        RMat.Created_By = LoggedIn_User_Id;
                        db.RACAP_Matches.Add(RMat);
                        db.SaveChanges();
                    }
                    if (RACAP_Match_Id != 0)
                    {
                        RACAP_Matches RMat = db.RACAP_Matches.Find(RACAP_Match_Id);
                        RMat.RACAP_Prospective_Parent_Id = Pid;
                        RMat.RACAP_Adoptive_Child_Id = Cid;
                        RMat.Date_Modified = DateTime.Now;
                        RMat.Modified_By = LoggedIn_User_Id;
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

        public void UpdateRACAPRecordStatusParent_3(RACAPCaseViewModel cases, int? Pid, int Cid, int option, int LoggedIn_User_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    //Amendment_20180717
                    int ParentCaseid = (from i in db.RACAP_Case_Details
                                        join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                                        join k in db.RACAP_Prospective_Parent on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                                        where k.RACAP_Prospective_Parent_Id == Pid
                                        select i.RACAP_Case_Id).FirstOrDefault();


                    int ChildCaseid = (from i in db.RACAP_Case_Details
                                       join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                                       join k in db.RACAP_Adoptive_Child on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                                       where k.RACAP_Adoptive_Child_Id == Cid
                                       select i.RACAP_Case_Id).FirstOrDefault();

                    int RCWLId = (from x in db.RACAP_Case_WorkList
                                  join y in db.Intake_Assessments on x.Intake_Assessment_Id equals y.Intake_Assessment_Id
                                  join q in db.RACAP_Case_Details on y.Intake_Assessment_Id equals q.Intake_Assessment_Id
                                  where q.RACAP_Case_Id == ChildCaseid
                                  select x.RACAP_CaseWoklist_Id).FirstOrDefault();

                    var RacapMatch = (from n in db.RACAP_Matches
                                      where n.RACAP_Prospective_Parent_Id == Pid && n.RACAP_Adoptive_Child_Id == Cid
                                      select n).FirstOrDefault();
                    if (RacapMatch != null)
                    {
                        RacapMatch.Modified_By = LoggedIn_User_Id;
                        db.SaveChanges();
                    }

                    if (ParentCaseid != 0)
                    {
                        RACAP_Case_Details editp = db.RACAP_Case_Details.Find(ParentCaseid);
                        RACAP_Case_WorkList editRCWL = db.RACAP_Case_WorkList.Find(RCWLId);
                        if (option == 1)
                        {
                            editp.RACAP_Record_Status_Id = 2;
                            editRCWL.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            editp.RACAP_Record_Status_Id = 3;
                            editRCWL.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            editp.RACAP_Record_Status_Id = 4;
                            editRCWL.RACAP_Record_Status_Id = 4;
                        }
                        db.SaveChanges();
                    }
                    if (ChildCaseid != 0)
                    {

                        RACAP_Case_Details edit = db.RACAP_Case_Details.Find(ChildCaseid);
                        RACAP_Case_WorkList editRCWL = db.RACAP_Case_WorkList.Find(RCWLId);

                        if (option == 1)
                        {
                            edit.RACAP_Record_Status_Id = 2;
                            editRCWL.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            edit.RACAP_Record_Status_Id = 3;
                            editRCWL.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            edit.RACAP_Record_Status_Id = 4;
                            editRCWL.RACAP_Record_Status_Id = 4;
                        }
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

        public void UpdateRACAPRecordStatusChild(RACAPCaseViewModel cases, int Pid, int Cid, int option, int LoggedIn_User_Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int ParentCaseid = (from i in db.RACAP_Prospective_Parent
                                        join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                        where i.RACAP_Prospective_Parent_Id == Pid
                                        select b.RACAP_Case_Id).FirstOrDefault();

                    int ChildCaseid = (from i in db.RACAP_Adoptive_Child
                                       join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                       where i.RACAP_Adoptive_Child_Id == Cid
                                       select b.RACAP_Case_Id).FirstOrDefault();

                    if (ParentCaseid != 0)
                    {
                        RACAP_Case_Details editp = db.RACAP_Case_Details.Find(ParentCaseid);
                        if (option == 1)
                        {
                            editp.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            editp.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            editp.RACAP_Record_Status_Id = 4;
                        }
                    }
                    if (ChildCaseid != 0)
                    {

                        RACAP_Case_Details edit = db.RACAP_Case_Details.Find(ChildCaseid);

                        if (option == 1)
                        {
                            edit.RACAP_Record_Status_Id = 2;
                        }
                        if (option == 2)
                        {
                            edit.RACAP_Record_Status_Id = 3;
                        }
                        if (option == 3)
                        {
                            edit.RACAP_Record_Status_Id = 4;
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
            }
        }


        #endregion

        //End Addition by Herman

        //GET ADOPTION CASE BY ADOPTION CASE ID

        #region GET RACAP CASE DETAILS

        public int GetRACAPChildCaseDetailsByssId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        where i.Intake_Assessment_Id.Equals(intAssessmentId)
                        select Case.RACAP_Case_Id).FirstOrDefault();
            }
        }


        public RACAPCaseViewModel GetRACAPChildCaseDetails(int RacapcaseId)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int CaseId = (from i in db.RACAP_Case_Details
                                   where i.RACAP_Case_Id == RacapcaseId
                                   select i.RACAP_Case_Id).FirstOrDefault();

                    RACAP_Case_Details act = db.RACAP_Case_Details.Find(CaseId);

                    vm.RACAP_Case_Id = CaseId;

                    vm.Expiry_Date = act.Expiry_Date;
                    vm.Verified = act.Verified;
                    vm.DateVerified = act.DateVerified;
                    vm.Date_Registered = act.Date_Registered;
                    vm.Comments = act.Comments;
                    vm.Adoptions_Reason_Id = act.Adoptions_Reason_Id;
                    vm.Date_Captured = (from d in db.RACAP_Case_WorkList
                                       where d.Intake_Assessment_Id==act.Intake_Assessment_Id
                                       select d.Date_Allocated).FirstOrDefault();
                    if (act.RACAP_Record_Status_Id != null)
                    {
                        vm.RACAP_Record_Status_Id = act.RACAP_Record_Status_Id;
                    }
                    else {
                        vm.RACAP_Record_Status_Id = 1;
                    }
                    vm.RACAP_Registration_Status_Id = act.RACAP_Registration_Status_Id;
                    vm.Adoptions_Reason_Id = act.Adoptions_Reason_Id;
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

        public RACAPCaseViewModel GetRACAPCaseDetailsCreate(int Case_Id)
        {
            RACAPCaseViewModel AdoptCasevm = new RACAPCaseViewModel();
            //SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            //var act1 = db.ADOPT_Case_Details.Find(Adopt_Case_Id);


            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? CaseId = (from i in db.RACAP_Case_Details
                                   where i.RACAP_Case_Id == Case_Id
                                   select i.RACAP_Case_Id).FirstOrDefault();

                    RACAP_Case_Details act = db.RACAP_Case_Details.Find(CaseId);




                    //AdoptCasevm.Intake_Assessment_Id = Intake_Assessment_Id;
                    //AdoptCasevm.Adopt_Case_Id = CaseId;
                    //AdoptCasevm.OdoptionOrder_Date = act.OdoptionOrder_Date;
                    AdoptCasevm.Date_Registered = act.Date_Registered;
                    AdoptCasevm.Comments = act.Comments;
                    AdoptCasevm.Adoptions_Reason_Id = act.Adoptions_Reason_Id;
                    //AdoptCasevm.Legitimacy_Id = act.Legitimacy_Id;
                    //AdoptCasevm.Cross_Cultural_Id = act.Cross_Cultural_Id;
                    AdoptCasevm.RACAP_Record_Status_Id = act.RACAP_Record_Status_Id;
                    //AdoptCasevm.Adopting_Reason_Id = act.Adopting_Reason_Id;
                    //AdoptCasevm.Court_id = act.Court_Id;
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

                return AdoptCasevm;
            }
        }

        public int GetRACAPChildClientRefIdssId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join f in db.int_Client_Module_Registration on c.Client_Id equals f.Client_Id
                        where i.Intake_Assessment_Id.Equals(intAssessmentId)
                        select f.Client_Module_Id).FirstOrDefault();
            }
        }

        public string GetRACAPChildClientRef(int ClientRefid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join f in db.int_Client_Module_Registration on c.Client_Id equals f.Client_Id
                        where f.Client_Module_Id.Equals(ClientRefid)
                        select f.Client_Module_Ref_No).FirstOrDefault();
            }
        }

        #endregion

        #region RACAP CHILD FEATURES

        public int GetRACAPChildFeaturedByssId(int IntAss)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                var RacapCaseId = (from p in db.Persons
                                   join c in db.Clients on p.Person_Id equals c.Person_Id
                                   join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                                   join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                                   where i.Intake_Assessment_Id.Equals(IntAss)
                                   select Case.RACAP_Case_Id).FirstOrDefault();

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        join F in db.RACAP_Adoptive_Child on Case.RACAP_Case_Id equals F.RACAP_Case_Id
                        where Case.RACAP_Case_Id.Equals(RacapCaseId)
                        select F.RACAP_Adoptive_Child_Id).FirstOrDefault();
            }
        }

        public RACAPCaseViewModel GetRACAPChildFeaturedDetails(int AdoptiveChildId)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? Adoptive_Child_Id = (from i in db.RACAP_Case_Details
                                              join b in db.RACAP_Adoptive_Child on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                              where b.RACAP_Adoptive_Child_Id == AdoptiveChildId
                                              select b.RACAP_Adoptive_Child_Id).FirstOrDefault();

                    RACAP_Adoptive_Child act = db.RACAP_Adoptive_Child.Find(Adoptive_Child_Id);
                    if(act!=null) { 
                    vm.Eye_Color_Id = act.Eye_Color_Id;
                    vm.Skin_Colour_Id = act.Skin_Colour_Id;
                    vm.Body_Structure_Id = act.Body_Structure_Id;
                    vm.Ethnic_Cultural_Group_Id = act.Ethnic_Cultural_Group_Id;
                    vm.Special_Needs_Id = act.Special_Needs_Id;
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

        public void CreateRACAPChildFeature(RACAPCaseViewModel cases, int id, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    RACAP_Adoptive_Child newCase = new RACAP_Adoptive_Child();

                    newCase.RACAP_Case_Id = (from a in db.Intake_Assessments
                                             join b in db.RACAP_Case_Details on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                                             where a.Intake_Assessment_Id == id
                                             select b.RACAP_Case_Id).FirstOrDefault();
                    db.RACAP_Adoptive_Child.Add(newCase);
                    db.SaveChanges();
                    int idnew = newCase.RACAP_Adoptive_Child_Id;
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

        public RACAPCaseViewModel GetRACAPChildFeatureDetailsCreate(int AdoptiveChildid)
        {
            RACAPCaseViewModel AdoptCasevm = new RACAPCaseViewModel();


            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? childid = (from i in db.RACAP_Case_Details
                                    join b in db.RACAP_Adoptive_Child on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                    where b.RACAP_Adoptive_Child_Id == AdoptiveChildid
                                    select b.RACAP_Adoptive_Child_Id).FirstOrDefault();

                    RACAP_Adoptive_Child act = db.RACAP_Adoptive_Child.Find(childid);

                    if (AdoptCasevm.Eye_Color_Id != null)
                    {

                        AdoptCasevm.Eye_Color_Id = act.Eye_Color_Id;
                    }
                    if (AdoptCasevm.Special_Needs_Id != null)
                    {
                        AdoptCasevm.Special_Needs_Id = act.Special_Needs_Id;
                    }
                    if (AdoptCasevm.Body_Structure_Id != null)
                    {


                        AdoptCasevm.Body_Structure_Id = act.Body_Structure_Id;
                    }
                    if (AdoptCasevm.Ethnic_Cultural_Group_Id != null)
                    {


                        AdoptCasevm.Ethnic_Cultural_Group_Id = act.Ethnic_Cultural_Group_Id;
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

                return AdoptCasevm;
            }
        }

        public void UpdateRACAPChildFeatures(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int AdoptiveChildId1 = (from i in db.RACAP_Adoptive_Child
                                            join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                            join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                            where a.Intake_Assessment_Id == Assid
                                            select i.RACAP_Adoptive_Child_Id).FirstOrDefault();

                    int RACAP_Case_Id = (from j in db.RACAP_Case_Details
                                         where j.Intake_Assessment_Id == Assid
                                         select j.RACAP_Case_Id).FirstOrDefault();
                    if (AdoptiveChildId1 != 0) { 
                    RACAP_Adoptive_Child editCase = db.RACAP_Adoptive_Child.Find(AdoptiveChildId1);

                    editCase.RACAP_Adoptive_Child_Id = AdoptiveChildId1;


                    if (cases.RACAPCaseViewModel.Eye_Color_Id != null)
                    {
                        editCase.Eye_Color_Id = cases.RACAPCaseViewModel.Eye_Color_Id;
                    }

                    if (cases.RACAPCaseViewModel.Skin_Colour_Id != null)
                    {
                        editCase.Skin_Colour_Id = cases.RACAPCaseViewModel.Skin_Colour_Id;
                    }
                    if (cases.RACAPCaseViewModel.Skin_Colour_Id != null)
                    {
                        editCase.Population_Group_Id = cases.RACAPCaseViewModel.Skin_Colour_Id;
                    }

                    if (cases.RACAPCaseViewModel.Special_Needs_Id != null)
                    {
                        editCase.Special_Needs_Id = cases.RACAPCaseViewModel.Special_Needs_Id;
                    }

                    if (cases.RACAPCaseViewModel.Body_Structure_Id != null)
                    {
                        editCase.Body_Structure_Id = cases.RACAPCaseViewModel.Body_Structure_Id;
                    }

                    if (cases.RACAPCaseViewModel.Ethnic_Cultural_Group_Id != null)
                    {

                        editCase.Ethnic_Cultural_Group_Id = cases.RACAPCaseViewModel.Ethnic_Cultural_Group_Id;
                    }
                    db.SaveChanges();
                        //var racapSpecialNeeds = new Int_Person_SpecialNeed();
                        //UserModel userModel = new UserModel();
                        //foreach (var item in cases.RACAPCaseViewModel.SelectedSpecialNeedsIds)
                        //{
                        //    var ObjChecks = (from s in db.RACAP_SpecialNeed_Selection
                        //                     where s.SpecialNeed_Id == item &&
                        //                     s.RACAP_Case_Id == RACAP_Case_Id
                        //                     select s).FirstOrDefault();
                        //    if (ObjChecks != null)
                        //    {
                        //        racapSpecialNeeds = db.Int_Person_SpecialNeed.Find(ObjChecks.SpecialNeed_Selection_Id);
                        //        racapSpecialNeeds.RACAP_Case_Id = RACAP_Case_Id;
                        //        racapSpecialNeeds.SpecialNeed_Id = item;
                        //        racapSpecialNeeds.Modified_By = userId;
                        //        racapSpecialNeeds.Modified_Date = DateTime.Now;
                        //        racapSpecialNeeds.Is_Active = true;
                        //        racapSpecialNeeds.Is_Deleted = false;
                        //        db.SaveChanges();
                        //    }
                        //    if (ObjChecks == null)
                        //    {
                        //        //racapSpecialNeeds = new RACAP_SpecialNeed_Selection();
                        //        racapSpecialNeeds.RACAP_Case_Id = RACAP_Case_Id;
                        //        racapSpecialNeeds.SpecialNeed_Id = item;
                        //        racapSpecialNeeds.Created_By = userId;
                        //        racapSpecialNeeds.CreatedTimeStamp = DateTime.Now;
                        //        racapSpecialNeeds.Is_Active = true;
                        //        racapSpecialNeeds.Is_Deleted = false;
                        //        db.RACAP_SpecialNeed_Selection.Add(racapSpecialNeeds);
                        //        db.SaveChanges();
                        //    }
                        //}
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



        #endregion

        #region RACAP CHILD REMOVAL
        public int GetRACAPChildRemovalByssId(int IntAss)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                var RacapCaseId = (from p in db.Persons
                                   join c in db.Clients on p.Person_Id equals c.Person_Id
                                   join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                                   join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                                   where i.Intake_Assessment_Id.Equals(IntAss)
                                   select Case.RACAP_Case_Id).FirstOrDefault();

                return (from Case in db.RACAP_Case_Details
                        where Case.RACAP_Case_Id.Equals(RacapCaseId)
                        select Case.RACAP_Case_Id).FirstOrDefault();
            }
        }

        public RACAPCaseViewModel GetRACAPChildRemovalDetails(int Caseid)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? removeid = (from i in db.RACAP_Case_Details
                                     join b in db.RACAP_Removal on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                     where i.RACAP_Case_Id == Caseid
                                     select b.Removal_Id).FirstOrDefault();
                    if (removeid > 0)
                    {

                        RACAP_Removal act = db.RACAP_Removal.Find(removeid);

                        vm.Removal_Type_Id = act.Removal_Type_Id;
                        vm.Removal_Comments = act.Removal_Comments;
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

        public void UpdateRACAPChildRemoval(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int RACAP_Case_Id1 = (from i in db.RACAP_Adoptive_Child
                                          join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                          join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                          where a.Intake_Assessment_Id == Assid
                                          select b.RACAP_Case_Id).FirstOrDefault();

                    int RACAP_Case_Id1r = (from i in db.RACAP_Adoptive_Child
                                           join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                           join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                           join r in db.RACAP_Removal on b.RACAP_Case_Id equals r.RACAP_Case_Id
                                           where r.RACAP_Case_Id == RACAP_Case_Id1
                                           select b.RACAP_Case_Id).FirstOrDefault();
                    if (RACAP_Case_Id1r <= 0)
                    {
                        RACAP_Removal newremove = new RACAP_Removal();
                        newremove.RACAP_Case_Id = RACAP_Case_Id1;

                        newremove.Date_Removed = DateTime.Now;
                        db.RACAP_Removal.Add(newremove);
                        db.SaveChanges();
                    }


                    int RACAP_Case_Id1r2 = (from i in db.RACAP_Adoptive_Child
                                            join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                            join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                            join r in db.RACAP_Removal on b.RACAP_Case_Id equals r.RACAP_Case_Id
                                            where r.RACAP_Case_Id == RACAP_Case_Id1
                                            select b.RACAP_Case_Id).FirstOrDefault();

                    //Update.....................................................................
                    if (RACAP_Case_Id1r2 >= 0)
                    {
                        int Removalid = (from i in db.RACAP_Adoptive_Child
                                         join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                         join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                         join r in db.RACAP_Removal on b.RACAP_Case_Id equals r.RACAP_Case_Id
                                         where r.RACAP_Case_Id == RACAP_Case_Id1r2
                                         select r.Removal_Id).FirstOrDefault();

                        RACAP_Removal edit = db.RACAP_Removal.Find(Removalid);

                        edit.RACAP_Case_Id = RACAP_Case_Id1r2;

                        if (cases.RACAPCaseViewModel.Removal_Type_Id != null)
                        {
                            edit.Removal_Type_Id = cases.RACAPCaseViewModel.Removal_Type_Id;
                        }
                        if (cases.RACAPCaseViewModel.Removal_Comments != null)
                        {
                            edit.Removal_Comments = cases.RACAPCaseViewModel.Removal_Comments;
                            edit.Date_Removed = DateTime.Now;
                        }
                        db.SaveChanges();

                        var UpdateRACAPCaseDetails = db.RACAP_Case_Details.Find(RACAP_Case_Id1r2);
                        UpdateRACAPCaseDetails.RACAP_Record_Status_Id = 4;
                        UpdateRACAPCaseDetails.Modified_By = userId;
                        UpdateRACAPCaseDetails.Date_Modified = DateTime.Now;
                        db.SaveChanges();

                        var UpdateRACAPWorkList = (from g in db.RACAP_Case_WorkList
                                                   where g.Intake_Assessment_Id == Assid
                                                   select g).FirstOrDefault();
                        UpdateRACAPWorkList.RACAP_Record_Status_Id = 4;
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

        #endregion

        // GET ORGANISATION REPSONSIBLE FOR ADOPTION

        #region GET ORGANISATION REPSONSIBLE FOR RACAP


        public int GetRACAPChildOrganisationByssId(int IntAss)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                var RacapCaseId = (from p in db.Persons
                                   join c in db.Clients on p.Person_Id equals c.Person_Id
                                   join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                                   join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                                   where i.Intake_Assessment_Id.Equals(IntAss)
                                   select Case.RACAP_Case_Id).FirstOrDefault();

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        join F in db.RACAP_OrgansationResponsible on Case.RACAP_Case_Id equals F.RACAP_Case_Id
                        where Case.RACAP_Case_Id.Equals(RacapCaseId)
                        select F.RACAP_Responsible_Organisation).FirstOrDefault();
            }
        }

        public RACAPCaseViewModel GetRACAPChildOrganisationDetails(int OrganizationIdChild)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? OrganizationChild = (from i in db.RACAP_Case_Details
                                              join b in db.RACAP_OrgansationResponsible on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                              where b.RACAP_Responsible_Organisation == OrganizationIdChild
                                              select b.RACAP_Responsible_Organisation).FirstOrDefault();

                    RACAP_OrgansationResponsible act = db.RACAP_OrgansationResponsible.Find(OrganizationChild);
                    if (act.Organization_Id_Child != null)
                    {
                        vm.Organization_Id = act.Organization_Id_Child;
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


        public void CreateRACAPChildOrganisation(RACAPCaseViewModel cases, int id, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    RACAP_OrgansationResponsible newCase = new RACAP_OrgansationResponsible();

                    newCase.RACAP_Case_Id = (from a in db.Intake_Assessments
                                             join b in db.RACAP_Case_Details on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                                             where a.Intake_Assessment_Id == id
                                             select b.RACAP_Case_Id).FirstOrDefault();
                    db.RACAP_OrgansationResponsible.Add(newCase);
                    db.SaveChanges();
                    int idnew = newCase.RACAP_Responsible_Organisation;
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

        public RACAPCaseViewModel GetRACAPChildOrganisationDetailsCreate(int ResponsibleOrganisationid)
        {
            RACAPCaseViewModel AdoptCasevm = new RACAPCaseViewModel();


            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? ResponsibleOrganisation = (from i in db.RACAP_Case_Details
                                                    join b in db.RACAP_OrgansationResponsible on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                                    where b.RACAP_Responsible_Organisation == ResponsibleOrganisationid
                                                    select b.RACAP_Responsible_Organisation).FirstOrDefault();

                    RACAP_OrgansationResponsible act = db.RACAP_OrgansationResponsible.Find(ResponsibleOrganisation);


                    if (act.Organization_Id_Child != null)
                    {
                        AdoptCasevm.Organization_Id = act.Organization_Id_Child;
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

                return AdoptCasevm;
            }
        }

        public void UpdateRACAPChildOrganisation(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int ResponsibleOrganisation = (from i in db.RACAP_OrgansationResponsible
                                                   join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                                   join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                                   where a.Intake_Assessment_Id == Assid
                                                   select i.RACAP_Responsible_Organisation).FirstOrDefault();

                    RACAP_OrgansationResponsible editCase = db.RACAP_OrgansationResponsible.Find(ResponsibleOrganisation);

                    editCase.RACAP_Responsible_Organisation = ResponsibleOrganisation;
                    editCase.Social_Worker_Id = cases.RACAPOrgResponsibleVM.SocialWorkerId;
                    db.SaveChanges();
                    if (cases.RACAPCaseViewModel.Organization_Id != null)
                    {
                        RACAP_OrgansationResponsible editCase_1 = db.RACAP_OrgansationResponsible.Find(ResponsibleOrganisation);

                        editCase_1.Organization_Id_Child = cases.RACAPCaseViewModel.Organization_Id;
                        db.SaveChanges();
                    }

                   
                    if (editCase.RACAP_Responsible_Organisation == 0)
                    {
                        editCase.Organization_Id_Child = cases.RACAPCaseViewModel.Organization_Id_Child;
                        editCase.Intake_Assessment_Id = cases.RACAPCaseViewModel.Intake_Assessment_Id;
                        editCase.RACAP_Case_Id = cases.RACAPCaseViewModel.RACAP_Case_Id;
                        db.RACAP_OrgansationResponsible.Add(editCase);
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


        #endregion

        //ADD ADOPTION CASE DETAILS
        #region ADD RACAP CASE DETAILS 

        public void CreateRACAPChildCase(RACAPCaseViewModel cases, int id, int clientref, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    RACAP_Case_Details newCase = new RACAP_Case_Details();

                    newCase.Intake_Assessment_Id = id;
                    //newCase.Client_Module_Id = cases.Client_Module_Id;
                    newCase.Intake_Assessment_Id = (from a in db.Intake_Assessments
                                                    where a.Intake_Assessment_Id == id
                                                    select a.Intake_Assessment_Id).FirstOrDefault();
                    newCase.RACAP_Registration_Status_Id = 1;
                    db.RACAP_Case_Details.Add(newCase);
                    db.SaveChanges();
                    RACAP_Case_WorkList wlist = (from a in db.RACAP_Case_WorkList
                                                 where a.Intake_Assessment_Id == id
                                                 select a).FirstOrDefault();
                    wlist.RACAP_Record_Status_Id = 1;
                    db.SaveChanges();
                    int idnew = newCase.RACAP_Case_Id;
                    int RPPId = (from a in db.RACAP_Adoptive_Child
                                 where a.RACAP_Case_Id == idnew
                                 select a.RACAP_Adoptive_Child_Id).FirstOrDefault();
                    if (RPPId == 0)
                    {
                        RACAP_Adoptive_Child NewObj = new RACAP_Adoptive_Child();
                        NewObj.RACAP_Case_Id = idnew;
                        NewObj.Intake_Assessment_Id = id;
                        NewObj.Date_Created = DateTime.Now;
                        db.RACAP_Adoptive_Child.Add(NewObj);
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

        //Herman
        public void CreateRACAPOrgnanisationResponsible(RACAPCaseViewModel cases, int id, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    RACAP_OrgansationResponsible newCaseOrgRespon = new RACAP_OrgansationResponsible();

                    newCaseOrgRespon.Intake_Assessment_Id = id;
                    //newCase.Client_Module_Id = cases.Client_Module_Id;
                    newCaseOrgRespon.RACAP_Case_Id = (from a in db.RACAP_Case_Details
                                                      where a.Intake_Assessment_Id == id
                                                      select a.RACAP_Case_Id).FirstOrDefault();
                    //newCaseOrgRespon.Organization_Id_Child = cases.Organization_Id_Child;
                    //newCaseOrgRespon.Organization_Id_Parent = cases.Organization_Id_Parent;
                    db.RACAP_OrgansationResponsible.Add(newCaseOrgRespon);
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
        //End Herman

        #endregion

        //UPDATE CASE DETAILS
        #region UPDATE RACAP CASE DETAILS
        public void UpdateRACAPCase(MainPageModelRACAPVM cases, int userId, int AssId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? SubCategoryId = (from a in db.Intake_Assessments
                                         where a.Intake_Assessment_Id == AssId
                                         select a.Problem_Sub_Category_Id).FirstOrDefault();
                    int ? CaseId = (from i in db.RACAP_Case_Details
                                    where i.Intake_Assessment_Id == AssId
                                    select i.RACAP_Case_Id).FirstOrDefault();

                    RACAP_Case_Details editCase = db.RACAP_Case_Details.Find(CaseId);



                    editCase.Intake_Assessment_Id = AssId;


                    if (cases.RACAPCaseViewModel.Date_Registered != null)
                    {
                        editCase.Date_Registered = cases.RACAPCaseViewModel.Date_Registered;
                    }

                    if (cases.RACAPCaseViewModel.Date_Registered != null && SubCategoryId == 19)
                    {
  
                            DateTime startDate = Convert.ToDateTime(cases.RACAPCaseViewModel.Date_Registered);
                            DateTime expiryDate = startDate.AddDays(30);
                            editCase.Expiry_Date = expiryDate;
                    }

                
                    if (cases.RACAPCaseViewModel.Date_Registered != null && SubCategoryId == 20)
                    {
                        DateTime startDate = Convert.ToDateTime(cases.RACAPCaseViewModel.Date_Registered);
                        DateTime expiryDate = startDate.AddYears(3);
                        editCase.Expiry_Date = expiryDate;
                    }

                if (cases.RACAPCaseViewModel.Adoptions_Reason_Id != null)
                    {
                        editCase.Adoptions_Reason_Id = cases.RACAPCaseViewModel.Adoptions_Reason_Id;
                        db.SaveChanges();
                    }

                    if (cases.RACAPCaseViewModel.RACAP_Record_Status_Id != null)
                    {
                        editCase.RACAP_Record_Status_Id = cases.RACAPCaseViewModel.RACAP_Record_Status_Id;
                    }

                    if (cases.RACAPCaseViewModel.RACAP_Registration_Status_Id != null)
                    {

                        editCase.RACAP_Registration_Status_Id = cases.RACAPCaseViewModel.RACAP_Registration_Status_Id;
                    }
                    if (cases.RACAPCaseViewModel.RACAP_Record_Status_Id != null)
                    {

                        editCase.RACAP_Record_Status_Id = cases.RACAPCaseViewModel.RACAP_Record_Status_Id;
                    }

                    if (cases.RACAPCaseViewModel.Comments != null)
                    {

                        editCase.Comments = cases.RACAPCaseViewModel.Comments;
                    }
                    if (cases.RACAPCaseViewModel.Date_Captured != null)
                    {
                        editCase.DateCaptured = cases.RACAPCaseViewModel.Date_Captured;
                    }
                    if (cases.RACAPCaseViewModel.Verified != false)
                    {
                        editCase.Verified = true;
                    }
                    if (cases.RACAPCaseViewModel.DateVerified != null)
                    {
                        editCase.DateVerified = DateTime.Now;
                    }


                    //db.RACAP_Case_Details.Add(editCase);
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

        public void UpdateRACAPOrganisation(MainPageModelRACAPVM cases, int userId, int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? RACAPORGResponId = (from d in db.RACAP_OrgansationResponsible
                                             where d.Intake_Assessment_Id == Id
                                             select d.RACAP_Responsible_Organisation).FirstOrDefault();
                    RACAP_OrgansationResponsible editCase = db.RACAP_OrgansationResponsible.Find(RACAPORGResponId);
                    editCase.Intake_Assessment_Id = Id;
                    editCase.RACAP_Case_Id = editCase.RACAP_Case_Id;
                    editCase.Organization_Id_Child = cases.RACAPCaseViewModel.Organization_Id_Child;
                    editCase.Organization_Id_Parent = cases.RACAPCaseViewModel.Organization_Id_Parent;
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

        #endregion

        #endregion  


        #endregion

        // RACAP 


        #region  LOAD DROP DOWNS MODEL

        #region RACAP MODULE DROP DOWNS

        public List<RemovalTypeLookupRACAP> GetAllRemovalReason()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Removal = db.apl_RACAP_Client_Removal.Select(o => new RemovalTypeLookupRACAP
                {
                    Removal_Type_Id = o.Removal_Type_Id,
                    Description = o.Description
                }).ToList();

                return Removal;
            }
        }

        public List<CorrespondenceTypeLookupRACAP> GetAllCorrespondenceTypes()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Correspondence_Type = db.apl_RACAP_Correspondence_Type.Select(o => new CorrespondenceTypeLookupRACAP
                {
                    RACAP_Correspondence_Type_Id = o.RACAP_Correspondence_Type_Id,
                    Description = o.Description
                })
                .Where(o=>o.RACAP_Correspondence_Type_Id==4 || o.RACAP_Correspondence_Type_Id==9)
                .ToList();

                return Correspondence_Type;
            }
        }

        public List<ReasonForAdoptionLookupRACAP> GetAllReasonForAdoptionType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var AdoptionReasonType = db.apl_Adoption_Reason.Select(o => new ReasonForAdoptionLookupRACAP
                {
                    Adoptions_Reason_Id = o.Adoptions_Reason_Id,
                    Description = o.Description
                }).ToList();

                return AdoptionReasonType;
            }
        }

        public List<RACAPRegistrationStatusLookupAdopt> GetAllRegistration_StatusType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var registration_StatusType = db.apl_RACAP_Registration_Status.Select(o => new RACAPRegistrationStatusLookupAdopt
                {
                    RACAP_Registration_Status_Id = o.RACAP_Registration_Status_Id,
                    Description = o.Description
                }).ToList();

                return registration_StatusType;
            }
        }

        
        public List<EyeColorsLookupAdopt> GetAllEyeColors()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var EyeColors = db.Eye_Colors.Select(o => new EyeColorsLookupAdopt
                {
                    Eye_Color_Id = o.Eye_Color_Id,
                    Description = o.Description
                }).ToList();

                return EyeColors;
            }
        }

        public List<BodyStructureLookupAdopt> GetAllBodyStructure()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var BodyStructure = db.apl_Body_Structure.Select(o => new BodyStructureLookupAdopt
                {
                    Body_Structure_Id = o.Body_Structure_Id,
                    Description = o.Description
                }).ToList();

                return BodyStructure;
            }
        }


        public List<SpecialNeedsLookupAdopt> GetAllSpecialNeeds()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var SpecialNeeds = db.apl_Special_Need.Select(o => new SpecialNeedsLookupAdopt
                {
                    Special_Needs_Id = o.Special_Needs_Id,
                    Description = o.Description
                }).ToList();

                return SpecialNeeds;
            }
        }

        public List<SkinColourLookupAdopt> GetAllSkinColour()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var SkinColour = db.apl_Skin_Colour.Select(o => new SkinColourLookupAdopt
                {
                    Skin_Colour_Id = o.Skin_Colour_Id,
                    Description = o.Description
                }).ToList();

                return SkinColour;
            }
        }

        public List<EthnicCulturalLookupAdopt> GetAllEthnicCultural()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var EthnicCultural = db.apl_Ethnic_Cultural.Select(o => new EthnicCulturalLookupAdopt
                {
                    Ethnic_Cultural_Group_Id = o.Ethnic_Cultural_Id,
                    Description = o.Description
                }).ToList();

                return EthnicCultural;
            }
        }

        public List<RACAPRegistrationStatusLookupAdopt> GetAllRACAPRegistrationStatus()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var RegistrationStatus = db.apl_RACAP_Registration_Status.Select(o => new RACAPRegistrationStatusLookupAdopt
                {
                    RACAP_Registration_Status_Id = o.RACAP_Registration_Status_Id,
                    Description = o.Description
                }).ToList();

                return RegistrationStatus;
            }
        }

        public List<RACAPRecordStatusLookupAdopt> GetAllRACAPRecordStatus()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var RecordStatus = db.apl_RACAP_Record_Status.Select(o => new RACAPRecordStatusLookupAdopt
                {
                    RACAP_Record_Status_Id = o.RACAP_Record_Status_Id,
                    Description = o.Description
                }).ToList();

                return RecordStatus;
            }
        }

        public List<OrganisationTypeLookupRACAPChild> GetAllChildOrganisationType()
        {

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Child = db.apl_Organisation_Type.Select(o => new OrganisationTypeLookupRACAPChild
                {
                    IdType = o.IdType,
                    Description = o.Description
                }).ToList();

                return Child;
            }
        }

        #endregion


        public List<IdentificationTypeLookupRACAP> GetAllIdentificationType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var idType = db.Identification_Types.Select(o => new IdentificationTypeLookupRACAP
                {
                    Identification_Type_Id = o.Identification_Type_Id,
                    Description = o.Description
                }).ToList();

                return idType;
            }
        }

        public List<LanguageTypeLookupRACAP> GetAllLanguageType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var languageType = db.Languages.Select(o => new LanguageTypeLookupRACAP
                {
                    Language_Id = o.Language_Id,
                    Description = o.Description
                }).ToList();

                return languageType;
            }
        }


        public List<GenderTypeLookupRACAP> GetAllGenderType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var genderType = db.Genders.Select(o => new GenderTypeLookupRACAP
                {
                    Gender_Id = o.Gender_Id,
                    Description = o.Description
                }).ToList();

                return genderType;
            }
        }

        public List<MaritalTypeLookupRACAP> GetAllMaritalType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var maritalType = db.Marital_Statusses.Select(o => new MaritalTypeLookupRACAP
                {
                    Marital_Status_Id = o.Marital_Status_Id,
                    Description = o.Description
                }).ToList();

                return maritalType;
            }
        }

        public List<ContactTypeLookupRACAP> GetAllContactType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var contactType = db.Preferred_Contact_Types.Select(o => new ContactTypeLookupRACAP
                {
                    Preferred_Contact_Type_Id = o.Preferred_Contact_Type_Id,
                    Description = o.Description
                }).ToList();

                return contactType;
            }
        }

        public List<ReligionTypeLookupRACAP> GetAllReligionType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var religionType = db.Religions.Select(o => new ReligionTypeLookupRACAP
                {
                    Religion_Id = o.Religion_Id,
                    Description = o.Description
                }).ToList();

                return religionType;
            }
        }

        public List<Population_GroupTypeLookupRACAP> GetAllPopulationType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var populationType = db.Population_Groups.Select(o => new Population_GroupTypeLookupRACAP
                {
                    Population_Group_Id = o.Population_Group_Id,
                    Description = o.Description
                }).ToList();

                return populationType;
            }
        }

        public List<Special_NeedTypeLookupRACAP> GetAllSpecialNeedType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var specialNeedType = db.apl_Special_Need.Select(o => new Special_NeedTypeLookupRACAP
                {
                    Special_Need_Id = o.Special_Needs_Id,
                    Description = o.Description
                }).ToList();

                return specialNeedType;
            }
        }

        public List<Ethnic_CulturalTypeLookupRACAP> GetAllEthnicCulturalType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var ethnicCulturalType = db.apl_Ethnic_Cultural.Select(o => new Ethnic_CulturalTypeLookupRACAP
                {
                    Ethnic_Cultural_Id = o.Ethnic_Cultural_Id,
                    Description = o.Description
                }).ToList();

                return ethnicCulturalType;
            }
        }


        public List<RaceTypeLookupRACAP> GetAllRaceType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var raceType = db.Population_Groups.Select(o => new RaceTypeLookupRACAP
                {
                    Race_Id = o.Population_Group_Id,
                    Description = o.Description
                }).ToList();

                return raceType;
            }
        }

        public DateTime GetDate_Registered(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                
                return DateTime.Now.AddYears(3);
            }
        }

        public List<Body_StructureTypeLookupRACAP> GetAllBody_StructureType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var bodyStructurerType = db.apl_Body_Structure.Select(o => new Body_StructureTypeLookupRACAP
                {
                    Body_Structure_Id = o.Body_Structure_Id,
                    Description = o.Description
                }).ToList();

                return bodyStructurerType;
            }
        }

        //Dropdown_EyeColor_5
        public List<Eye_ColourTypeLookupRACAP> GetAllEye_ColourType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var eyeColourType = db.Eye_Colors.Select(o => new Eye_ColourTypeLookupRACAP
                {
                    Eye_Colour_Id = o.Eye_Color_Id,
                    Description = o.Description
                }).ToList();

                return eyeColourType;
            }
        }

        //Language
        public List<ParentLanguageTypeLookupRACAP> GetAllParentLanguageType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var parentLanguageType = db.Languages.Select(o => new ParentLanguageTypeLookupRACAP
                {
                    Language_Id = o.Language_Id,
                    Description = o.Description
                }).ToList();

                return parentLanguageType;
            }
        }

        //Gender

        public List<ParentGenderTypeLookupRACAP> GetAllParentGenderType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var parentGenderType = db.Genders.Select(o => new ParentGenderTypeLookupRACAP
                {
                    Gender_Id = o.Gender_Id,
                    Description = o.Description
                }).ToList();

                return parentGenderType;
            }
        }


        //PopulationGroup
        public List<ParentPopulationGroupTypeLookupRACAP> GetAllParentPopulationGroupType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var parentPopulationGroupType = db.Population_Groups.Select(o => new ParentPopulationGroupTypeLookupRACAP
                {
                    Population_Group_Id = o.Population_Group_Id,
                    Description = o.Description
                }).ToList();

                return parentPopulationGroupType;
            }
        }

        //Race
        public List<ParentRaceTypeLookupRACAP> GetAllParentRaceType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var parentRaceType = db.Races.Select(o => new ParentRaceTypeLookupRACAP
                {
                    Race_Id = o.Race_Id,
                    Description = o.Description
                }).ToList();

                return parentRaceType;
            }
        }

        //Religion
        public List<ParentReligionTypeLookupRACAP> GetAllParentReligionType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var parentReligionType = db.Religions.Select(o => new ParentReligionTypeLookupRACAP
                {
                    Religion_Id = o.Religion_Id,
                    Description = o.Description
                }).ToList();

                return parentReligionType;
            }
        }
        //Age from
        public int? Agefrom { get; set; }
        //Age To

        public int? AgeTo { get; set; }

        public List<NationalityTypeLookupRACAP> GetAllNationalityType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var nationalityType = db.Nationalities.Select(o => new NationalityTypeLookupRACAP
                {
                    Nationality_Id = o.Nationality_Id,
                    Description = o.Description
                }).ToList();

                return nationalityType;
            }
        }
        public List<DisabilityTypeLookupRACAP> GetAllDisabilityType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var disabilityType = db.Disabilities.Select(o => new DisabilityTypeLookupRACAP
                {
                    Disability_Type_Id = o.Disability_Id,
                    Description = o.Description
                }).ToList();

                return disabilityType;
            }
        }


        public List<ParentDisabilityTypeLookupRACAP> GetAllParentDisabilityType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var disabilityType = db.Disabilities.Select(o => new ParentDisabilityTypeLookupRACAP
                {
                    Disability_Id = o.Disability_Id,
                    Description = o.Description
                }).ToList();

                return disabilityType;
            }
        }


        public List<Cross_CulturalLookupRACAP> GetAllCross_CulturalType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var CrossCulturalType = db.apl_Cross_Cultural.Select(o => new Cross_CulturalLookupRACAP
                {
                    Cross_Cultural_Id = o.Cross_Cultural_Id,
                    Description = o.Description
                }).ToList();

                return CrossCulturalType;
            }
        }


        public List<Record_StatusLookupRACAP> GetAllRecord_StatuslType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var StatusType = db.apl_Record_Status.Select(o => new Record_StatusLookupRACAP
                {
                    Record_Status_Id = o.Record_Status_Id,
                    Description = o.Description
                }).ToList();

                return StatusType;
            }
        }


        public List<ReasonForAdoptingLookupAdopt> GetAllReasonForAdoptinglType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var AdoptingType = db.apl_Adopting_Reason.Select(o => new ReasonForAdoptingLookupAdopt
                {
                    Adopting_Reason_Id = o.Adopting_Reason_Id,
                    Description = o.Description
                }).ToList();

                return AdoptingType;
            }
        }
        public List<ProvinceLookupRACAP> GetAllProvinces()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Province_id = db.Provinces.Select(o => new ProvinceLookupRACAP
                {
                    Province_Id = o.Province_Id,
                    Description = o.Description
                }).ToList();

                return Province_id;
            }
        }


        public List<DistrictLookupRACAP> GetAllDistrict()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var District_id = db.Districts.Select(o => new DistrictLookupRACAP
                {
                    District_Id = o.District_Id,
                    Description = o.Description
                }).ToList();

                return District_id;


            }
        }



        public List<CourtLookupAdopt> GetAllCourt()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Court_id = db.Courts.Select(o => new CourtLookupAdopt
                {
                    Court_id = o.Court_Id,
                    Description = o.Description
                }).ToList();

                return Court_id;


            }
        }


        public List<OrganisationTypeLookupRACAPParent> GetAllParentOrganisationType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Parent = db.apl_Organisation_Type.Select(o => new OrganisationTypeLookupRACAPParent
                {
                    Organization_Type_Id = o.IdType,
                    Description = o.Description
                }).ToList();

                return Parent;
            }
        }

        public List<OrganisationTypeLookupSocialWorkerRACAP> GetAllSocialWorkerOrganisationType()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var SocialWorker = db.apl_Organisation_Type.Select(o => new OrganisationTypeLookupSocialWorkerRACAP
                {
                    Organization_Type_Id = o.IdType,
                    Description = o.Description
                }).ToList();

                return SocialWorker;
            }
        }
     
        public List<OrganisationChildLookupRACAP> GetAllChildOrganisation()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Child = db.Organizations.Select(o => new OrganisationChildLookupRACAP
                {
                    Organization_Id = o.Organization_Id,
                    Description = o.Description
                }).ToList();

                return Child;
            }
        }


        public List<OrganisationLookupRACAPParent> GetAllParentOrganisation()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var parent = db.Organizations.Select(o => new OrganisationLookupRACAPParent
                {
                    Organization_Id = o.Organization_Id,
                    Description = o.Description,
                    Telephone_Number = o.Telephone_Number,
                    Email_Address = o.Email_Address
                }).ToList();

                return parent;
            }
        }

        public List<SocialWorkerLookupRACAPParent> GetAllParentSocialWorkers()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var Obj = db.Social_Workers.Select(o => new SocialWorkerLookupRACAPParent
                {
                    SocialWorkerId = o.User_Id,
                    Social_Worker_Name = o.apl_User.First_Name,
                    Social_Worker_Surname = o.apl_User.First_Name +" "+o.apl_User.Last_Name,
                    Accreditation_Ref = o.SocialWorkerPracticeNumber,
                    Telephone_Number = o.Mobile_Phone_Number,
                    SocWorkFax = o.Phone_Number,
                    Accredited_Date = o.AccreditedDate,
                    Province = o.apl_Service_Office.apl_Local_Municipality.District.Province.Description

                }).ToList();


                return Obj;

            }
        }
        public List<OrganisationLookupSocialWorkerRACAP> GetAllSocialWorkerOrganisation()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var parent = db.Organizations.Select(o => new OrganisationLookupSocialWorkerRACAP
                {
                    Organization_Id = o.Organization_Id,
                    Description = o.Description,
                    Telephone_Number = o.Telephone_Number,
                    Email_Address = o.Email_Address
                }).ToList();

                return parent;
            }
        }


        #endregion


        #region RACAP CASE INFROMATION

        public int? GetRACAPCaseDetailsByssId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        where i.Intake_Assessment_Id.Equals(intAssessmentId)
                        select Case.RACAP_Case_Id).FirstOrDefault();
            }
        }

        public void CreateRACAPCase(RACAPCaseViewModel cases, int id, int ids, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    RACAP_Case_Details newCase = new RACAP_Case_Details();

                    newCase.Intake_Assessment_Id = id;
                    //newCase.Client_Module_Id = cases.Client_Module_Id;
                    newCase.Intake_Assessment_Id = (from a in db.Intake_Assessments
                                                    where a.Intake_Assessment_Id == id
                                                    select a.Intake_Assessment_Id).FirstOrDefault();
                    db.RACAP_Case_Details.Add(newCase);
                    db.SaveChanges();
                    int? idnew = newCase.RACAP_Case_Id;
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



        public RACAPCaseViewModel GetRACAPCaseDetails(int assid)
        {
            RACAPCaseViewModel AdoptCasevm = new RACAPCaseViewModel();
            //SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            //var act1 = db.ADOPT_Case_Details.Find(Adopt_Case_Id);


            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? CaseId = (from i in db.RACAP_Case_Details
                                   where i.Intake_Assessment_Id == assid
                                   select i.RACAP_Case_Id).FirstOrDefault();

                    RACAP_Case_Details act = db.RACAP_Case_Details.Find(CaseId);




                    //AdoptCasevm.Intake_Assessment_Id = Intake_Assessment_Id;
                    //AdoptCasevm.RACAP_Case_Id = CaseId;
                    //AdoptCasevm.OdoptionOrder_Date = act.OdoptionOrder_Date;
                    //AdoptCasevm.Date_Registered = act.Date_Registered;
                    //AdoptCasevm.Adoption_Surname = act.Adoption_Surname;
                    //AdoptCasevm.Comments = act.Comments;
                    //AdoptCasevm.Adoptions_Reason_Id = act.Adoptions_Reason_Id;
                    //AdoptCasevm.Legitimacy_Id = act.Legitimacy_Id;
                    //AdoptCasevm.Cross_Cultural_Id = act.Cross_Cultural_Id;
                    //AdoptCasevm.Record_Status_Id = act.Record_Status_Id;
                    //AdoptCasevm.Adopting_Reason_Id = act.Adopting_Reason_Id;
                    //AdoptCasevm.Court_id = act.Court_Id;
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

                return AdoptCasevm;
            }
        }



        //  Get Specific Adoption Case
        public RACAP_Case_Details GetSpecificAdoptionCase(int RACAP_Case_Id)
        {
            RACAP_Case_Details Adoptcase;

            var dbContext = new SDIIS_DatabaseEntities();
            try
            {
                var AdoptcaseList = (from r in dbContext.RACAP_Case_Details
                                     where r.RACAP_Case_Id.Equals(RACAP_Case_Id)
                                     select r).ToList();

                Adoptcase = (from r in AdoptcaseList
                             select r).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }


            return Adoptcase;
        }


        //  Get Specific Adoption Case Intake assement
        public RACAP_Case_Details GetSpecificAdoptionCaseByIntakeAssment(int Intake_Assessment_Id)
        {
            RACAP_Case_Details AdoptcaseIntAss;

            var dbContext = new SDIIS_DatabaseEntities();
            try
            {
                var AdoptcaseIntAssList = (from r in dbContext.RACAP_Case_Details
                                           where r.Intake_Assessment_Id.Equals(Intake_Assessment_Id)
                                           select r).ToList();

                AdoptcaseIntAss = (from r in AdoptcaseIntAssList
                                   select r).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }


            return AdoptcaseIntAss;
        }

        #endregion FOR RACAP CASE INFROMATION


        #endregion Child

        //Amended by Herman
        #region Parent

        #region PERSON DETAILS

        // GET A PERSON BY PERSON ID

        public RACAPPersonViewModel GetRACAPPersonParent(int personId)
        {
            RACAPPersonViewModel Adoptvm = new RACAPPersonViewModel();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            var act = db.Persons.Find(personId);
            var RACAPProPar = (from a in db.Clients
                               join b in db.Intake_Assessments on a.Client_Id equals b.Client_Id
                               join c in db.RACAP_Prospective_Parent on b.Intake_Assessment_Id equals c.Intake_Assessment_Id
                               where a.Person_Id == act.Person_Id
                               select c).FirstOrDefault();
            Adoptvm.Person_Id = personId;
            Adoptvm.First_Name = act.First_Name;
            Adoptvm.Last_Name = act.Last_Name;
            Adoptvm.Known_As = act.Known_As;
            Adoptvm.Identification_Type_Id = act.Identification_Type_Id;
            Adoptvm.Identification_Number = act.Identification_Number;
            Adoptvm.Date_Of_Birth = act.Date_Of_Birth;

            DateTime old = Convert.ToDateTime(Adoptvm.Date_Of_Birth);

            //int age = DateTime.Now.AddYears(-old.Year).Year;
            int age = new DateTime(DateTime.Now.Subtract(Convert.ToDateTime(Adoptvm.Date_Of_Birth)).Ticks).Year - 1;

            Adoptvm.Age = age;
            //Adoptvm.AgeFrom = RACAPProPar.Age_From;
            //Adoptvm.AgeTo = RACAPProPar.Age_To;

            Adoptvm.Is_Estimated_Age = act.Is_Estimated_Age;
            Adoptvm.Language_Id = act.Language_Id;
            Adoptvm.Gender_Id = act.Gender_Id;
            Adoptvm.Marital_Status_Id = act.Marital_Status_Id;
            Adoptvm.Preferred_Contact_Type_Id = act.Preferred_Contact_Type_Id;
            Adoptvm.Religion_Id = act.Religion_Id;
            Adoptvm.Phone_Number = act.Phone_Number;
            Adoptvm.Mobile_Phone_Number = act.Mobile_Phone_Number;
            Adoptvm.Email_Address = act.Email_Address;
            Adoptvm.Population_Group_Id = act.Population_Group_Id;
            Adoptvm.Nationality_Id = act.Nationality_Id;
            Adoptvm.Disability_Type_Id = act.Disability_Type_Id;

            return Adoptvm;
        }

        public RACAPPersonViewModel GetRACAPProspectiveParentChoice(int ProspectiveParentId)
        {
            RACAPPersonViewModel racapParentMatches = new RACAPPersonViewModel();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            var act = db.RACAP_Prospective_Parent.Find(ProspectiveParentId);
            if (act != null)
            {
                racapParentMatches.AgeTo = act.Age_To;
                racapParentMatches.AgeFrom = act.Age_From;
                racapParentMatches.Special_Need_Id = act.Special_Needs_Id;
                racapParentMatches.Gender_Id = act.Gender_Id;
                racapParentMatches.Population_Group_Id = act.Population_Group_Id;
                racapParentMatches.Population_Group_Id_Second_Choice = act.Race_Id_Option2;
            }
            return racapParentMatches;
        }



        public RACAPPersonViewModel GetRACAPChildFeatures(int id)
        {
            RACAPPersonViewModel Adoptvm = new RACAPPersonViewModel();
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            int personId = (from k in db.Intake_Assessments
                            join l in db.Clients on k.Client_Id equals l.Client_Id
                            join m in db.Persons on l.Person_Id equals m.Person_Id
                            where k.Intake_Assessment_Id == id
                            select m.Person_Id).FirstOrDefault();
            var act = db.Persons.Find(personId);
            var RACAPProPar = (from a in db.Clients
                               join b in db.Intake_Assessments on a.Client_Id equals b.Client_Id
                               join c in db.RACAP_Prospective_Parent on b.Intake_Assessment_Id equals c.Intake_Assessment_Id
                               where b.Intake_Assessment_Id==id
                               select c).FirstOrDefault();
            Adoptvm.Person_Id = personId;
            Adoptvm.First_Name = act.First_Name;
            Adoptvm.Last_Name = act.Last_Name;
            Adoptvm.Known_As = act.Known_As;
            Adoptvm.Identification_Type_Id = act.Identification_Type_Id;
            Adoptvm.Identification_Number = act.Identification_Number;
            Adoptvm.Date_Of_Birth = act.Date_Of_Birth;

            DateTime old = Convert.ToDateTime(Adoptvm.Date_Of_Birth);

            int age = DateTime.Now.AddYears(-old.Year).Year;

            Adoptvm.Age = age;
            Adoptvm.AgeFrom = RACAPProPar.Age_From;
            Adoptvm.AgeTo = RACAPProPar.Age_To;

            Adoptvm.Is_Estimated_Age = act.Is_Estimated_Age;
            Adoptvm.Language_Id = act.Language_Id;
            Adoptvm.Gender_Id = RACAPProPar.Gender_Id;
            Adoptvm.Marital_Status_Id = act.Marital_Status_Id;
            Adoptvm.Preferred_Contact_Type_Id = act.Preferred_Contact_Type_Id;
            Adoptvm.Religion_Id = act.Religion_Id;
            Adoptvm.Phone_Number = act.Phone_Number;
            Adoptvm.Mobile_Phone_Number = act.Mobile_Phone_Number;
            Adoptvm.Email_Address = act.Email_Address;
            Adoptvm.Population_Group_Id = RACAPProPar.Race_Id;
            Adoptvm.Nationality_Id = act.Nationality_Id;
            Adoptvm.Disability_Type_Id = act.Disability_Type_Id;

            return Adoptvm;

        }

        public bool CheckExist(int Id)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            int DbObj = (from a in db.int_Client_ProspectiveAdoptiveParents
                         where a.Client_Id == Id
                         select a.Client_ProspectiveAdoptiveParents_Id).FirstOrDefault();
            if (DbObj > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //UPDATE PERSON DETAILS 


        #endregion FOR PESRON DETAILS


        #region GET RACAP CASE DETAILS FOR PARENT

        //public int GetRACAPParentCaseDetailsByssId(int intAssessmentId)
        //{
        //    using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
        //    {

        //        return (from p in db.Persons
        //                join c in db.Clients on p.Person_Id equals c.Person_Id
        //                join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
        //                join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
        //                where i.Intake_Assessment_Id.Equals(intAssessmentId)
        //                select Case.RACAP_Case_Id).FirstOrDefault();
        //    }
        //}

        #region UpdateCaseDetailsStatus

        public void UpdateCaseDetailsStatusFirst(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                if (Id != 0)
                {
                    var RACAPCaseDetailsTbl = (from a in db.RACAP_Case_Details
                                               where a.Intake_Assessment_Id == Id
                                               select a).FirstOrDefault();
                    int RACAPWorkListId = (from a in db.RACAP_Case_WorkList
                                           where a.Intake_Assessment_Id == Id
                                           select a.RACAP_CaseWoklist_Id).FirstOrDefault();
                    var RACAPWorkList = db.RACAP_Case_WorkList.Find(RACAPWorkListId);
                    if (RACAPCaseDetailsTbl != null) { 
                    if (RACAPCaseDetailsTbl.RACAP_Record_Status_Id == null)
                    {
                        RACAPCaseDetailsTbl.RACAP_Record_Status_Id = 1;
                        RACAPWorkList.RACAP_Record_Status_Id = 1;
                        db.SaveChanges();

                    }
                    if (RACAPCaseDetailsTbl.RACAP_Record_Status_Id == 1 
                        //&& RACAPCaseDetailsTbl.Date_Created < DateTime.Today.AddDays(-1)
                        )
                    {
                        RACAPCaseDetailsTbl.RACAP_Record_Status_Id = 2;
                        RACAPWorkList.RACAP_Record_Status_Id = 2;
                        db.SaveChanges();
                    }
                    }
                    db.SaveChanges();
                }
            }
        }

        public void UpdateCaseDetailsStatus(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                if (Id != 0) { 
                    var RACAPCaseDetailsTbl = db.RACAP_Case_Details.Find(Id);
                    int RACAPWorkListId = (from a in db.RACAP_Case_WorkList
                                         where a.Intake_Assessment_Id == RACAPCaseDetailsTbl.Intake_Assessment_Id
                                         select a.RACAP_CaseWoklist_Id).FirstOrDefault();
                    var RACAPWorkList = db.RACAP_Case_WorkList.Find(RACAPWorkListId);
                    if (RACAPCaseDetailsTbl.RACAP_Registration_Status_Id == null)
                    {
                        RACAPCaseDetailsTbl.RACAP_Registration_Status_Id = 1;
                        RACAPCaseDetailsTbl.RACAP_Record_Status_Id = 1;
                        RACAPWorkList.RACAP_Record_Status_Id = 1;


                    }
                    if (RACAPCaseDetailsTbl.RACAP_Registration_Status_Id==1 && RACAPCaseDetailsTbl.Date_Created < DateTime.Today.AddDays(-1)) {
                        RACAPCaseDetailsTbl.RACAP_Registration_Status_Id = 2;
                        RACAPCaseDetailsTbl.RACAP_Record_Status_Id = 2;
                        RACAPWorkList.RACAP_Record_Status_Id = 2;
                    }
                    db.SaveChanges();
                }
            }
        }

        public void UpdateCaseDetailsStatusFinal(int Id)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                var RACAPCaseDetailsTbl = db.RACAP_Case_Details.Find(Id);
                    RACAPCaseDetailsTbl.RACAP_Registration_Status_Id = 3;
                db.SaveChanges();
                int RACAPWorkListId = (from a in db.RACAP_Case_WorkList
                                       where a.Intake_Assessment_Id == RACAPCaseDetailsTbl.Intake_Assessment_Id
                                       select a.RACAP_CaseWoklist_Id).FirstOrDefault();
                var RACAPWorkList = db.RACAP_Case_WorkList.Find(RACAPWorkListId);
                RACAPWorkList.RACAP_Record_Status_Id = 3;
                db.SaveChanges();
            }
        }

        #endregion
        public int GetRACAPParentCaseDetailsByssId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Intake_Assessments
                        join q in db.RACAP_Case_Details on p.Intake_Assessment_Id equals q.Intake_Assessment_Id
                        where p.Intake_Assessment_Id.Equals(intAssessmentId)
                        select q.RACAP_Case_Id).FirstOrDefault();
            }
        }


        public RACAPCaseViewModel GetRACAPParentCaseDetails(int RacapcaseId)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int CaseId = (from i in db.RACAP_Case_Details
                                  where i.RACAP_Case_Id == RacapcaseId
                                  select i.RACAP_Case_Id).FirstOrDefault();

                    RACAP_Case_Details act = db.RACAP_Case_Details.Find(CaseId);

                    vm.RACAP_Case_Id = CaseId;

                    vm.Expiry_Date = act.Expiry_Date;
                    vm.Verified = act.Verified;
                    vm.DateVerified = act.DateVerified;
                    vm.Date_Registered = act.Date_Registered;
                    vm.Date_Captured = (from s in db.RACAP_Case_WorkList
                                       where s.Intake_Assessment_Id==act.Intake_Assessment_Id
                                       select s.Date_Allocated).FirstOrDefault();
                    vm.Comments = act.Comments;

                    vm.Adoptions_Reason_Id = act.Adoptions_Reason_Id;

                if (act.RACAP_Record_Status_Id != null)
                {
                    vm.RACAP_Record_Status_Id = act.RACAP_Record_Status_Id;
                    }
                    else
                    {
                        vm.RACAP_Record_Status_Id = 1;
                    }
                    vm.RACAP_Registration_Status_Id = act.RACAP_Registration_Status_Id;
                    vm.Adoptions_Reason_Id = act.Adoptions_Reason_Id;
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

        public RACAPCaseViewModel GetRACAPCaseDetailsCreateParent(int Case_Id)
        {
            RACAPCaseViewModel AdoptCasevm = new RACAPCaseViewModel();
            //SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            //var act1 = db.ADOPT_Case_Details.Find(Adopt_Case_Id);


            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? CaseId = (from i in db.RACAP_Case_Details
                                   where i.RACAP_Case_Id == Case_Id
                                   select i.RACAP_Case_Id).FirstOrDefault();

                    RACAP_Case_Details act = db.RACAP_Case_Details.Find(CaseId);




                    //AdoptCasevm.Intake_Assessment_Id = Intake_Assessment_Id;
                    //AdoptCasevm.Adopt_Case_Id = CaseId;
                    //AdoptCasevm.OdoptionOrder_Date = act.OdoptionOrder_Date;
                    AdoptCasevm.Date_Registered = act.Date_Registered;
                    AdoptCasevm.Comments = act.Comments;
                    AdoptCasevm.Adoptions_Reason_Id = act.Adoptions_Reason_Id;
                    //AdoptCasevm.Legitimacy_Id = act.Legitimacy_Id;
                    //AdoptCasevm.Cross_Cultural_Id = act.Cross_Cultural_Id;
                    AdoptCasevm.RACAP_Record_Status_Id = act.RACAP_Record_Status_Id;
                    //AdoptCasevm.Adopting_Reason_Id = act.Adopting_Reason_Id;
                    //AdoptCasevm.Court_id = act.Court_Id;
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

                return AdoptCasevm;
            }
        }

        public int GetRACAPParentClientRefIdssId(int intAssessmentId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join f in db.int_Client_Module_Registration on c.Client_Id equals f.Client_Id
                        where i.Intake_Assessment_Id.Equals(intAssessmentId)
                        select f.Client_Module_Id).FirstOrDefault();
            }
        }

        public string GetRACAPParentClientRef(int ClientRefid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join f in db.int_Client_Module_Registration on c.Client_Id equals f.Client_Id
                        where f.Client_Module_Id.Equals(ClientRefid)
                        select f.Client_Module_Ref_No).FirstOrDefault();
            }
        }

        #endregion


        #region RACAP PARENT FEATURES

        public int GetRACAPParentFeaturedByssId(int IntAss)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                var RacapCaseId = (from p in db.Persons
                                   join c in db.Clients on p.Person_Id equals c.Person_Id
                                   join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                                   join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                                   where i.Intake_Assessment_Id.Equals(IntAss)
                                   select Case.RACAP_Case_Id).FirstOrDefault();

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        join F in db.RACAP_Prospective_Parent on Case.RACAP_Case_Id equals F.RACAP_Case_Id
                        where Case.RACAP_Case_Id.Equals(RacapCaseId)
                        select F.RACAP_Prospective_Parent_Id).FirstOrDefault();
            }
        }

        public RACAPCaseViewModel GetRACAPParentFeaturedDetails(int ProspectiveParentId)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? Prospective_Parent_Id = (from i in db.RACAP_Case_Details
                                              join b in db.RACAP_Prospective_Parent on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                              where b.RACAP_Prospective_Parent_Id == ProspectiveParentId
                                                  select b.RACAP_Prospective_Parent_Id).FirstOrDefault();

                    RACAP_Prospective_Parent act = db.RACAP_Prospective_Parent.Find(Prospective_Parent_Id);

                    vm.Eye_Color_Id = act.Eye_Color_Id;
                    vm.Skin_Colour_Id = act.Skin_Color_Id;
                    vm.Body_Structure_Id = act.Body_Structure_Id;
                    vm.Ethnic_Cultural_Group_Id = act.Ethnic_Cultural_Group_Id;
                    vm.Special_Needs_Id = act.Special_Needs_Id;
                    vm.Religion_Id = act.Religion_Id;
                    vm.Language_Id = act.Language_Id;
                    vm.Gender_Id = act.Gender_Id;
                    vm.Race_Id = act.Race_Id;
                    vm.Race_Id_Option2 = act.Race_Id_Option2;
                    vm.Population_Group_Id = act.Population_Group_Id;
                    vm.Disability_Id = act.Disability_Id;
                    vm.Age_From = act.Age_From;
                    vm.Age_To = act.Age_To;

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

        public void CreateRACAPParentFeature(RACAPCaseViewModel cases, int id, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    RACAP_Adoptive_Child newCase = new RACAP_Adoptive_Child();

                    newCase.RACAP_Case_Id = (from a in db.Intake_Assessments
                                             join b in db.RACAP_Case_Details on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                                             where a.Intake_Assessment_Id == id
                                             select b.RACAP_Case_Id).FirstOrDefault();
                    db.RACAP_Adoptive_Child.Add(newCase);
                    db.SaveChanges();
                    int idnew = newCase.RACAP_Adoptive_Child_Id;
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

        public RACAPCaseViewModel GetRACAPParentFeatureDetailsCreate(int ProspectiveParentid)
        {
            RACAPCaseViewModel AdoptCasevm = new RACAPCaseViewModel();


            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? parentid = (from i in db.RACAP_Case_Details
                                    join b in db.RACAP_Prospective_Parent on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                    where b.RACAP_Prospective_Parent_Id == ProspectiveParentid
                                     select b.RACAP_Prospective_Parent_Id).FirstOrDefault();

                    RACAP_Prospective_Parent act = db.RACAP_Prospective_Parent.Find(parentid);

                    if (AdoptCasevm.Eye_Color_Id != null)
                    {

                        AdoptCasevm.Eye_Color_Id = act.Eye_Color_Id;
                    }
                    if (AdoptCasevm.Special_Needs_Id != null)
                    {
                        AdoptCasevm.Special_Needs_Id = act.Special_Needs_Id;
                    }
                    if (AdoptCasevm.Body_Structure_Id != null)
                    {


                        AdoptCasevm.Body_Structure_Id = act.Body_Structure_Id;
                    }
                    if (AdoptCasevm.Ethnic_Cultural_Group_Id != null)
                    {


                        AdoptCasevm.Ethnic_Cultural_Group_Id = act.Ethnic_Cultural_Group_Id;
                    }
                    if (AdoptCasevm.Body_Structure_Id != null)
                    {


                        AdoptCasevm.Body_Structure_Id = act.Body_Structure_Id;
                    }
                    if (AdoptCasevm.Skin_Colour_Id != null)
                    {


                        AdoptCasevm.Skin_Colour_Id = act.Skin_Color_Id;
                    }
                    if (AdoptCasevm.Religion_Id != null)
                    {


                        AdoptCasevm.Religion_Id = act.Religion_Id;
                    }
                    if (AdoptCasevm.Language_Id != null)
                    {


                        AdoptCasevm.Language_Id = act.Language_Id;
                    }
                    if (AdoptCasevm.Gender_Id != null)
                    {


                        AdoptCasevm.Gender_Id = act.Gender_Id;
                    }
                    if (AdoptCasevm.Race_Id != null)
                    {


                        AdoptCasevm.Race_Id = act.Race_Id;
                    }
                    if (AdoptCasevm.Age_From != null)
                    {


                        AdoptCasevm.Age_From = act.Age_From;
                    }
                    if (AdoptCasevm.Age_To != null)
                    {


                        AdoptCasevm.Age_To = act.Age_To;
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

                return AdoptCasevm;
            }
        }

        public void UpdateRACAPParentFeatures(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int ProspectiveParent1 = (from i in db.RACAP_Prospective_Parent
                                              join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                              join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                              where a.Intake_Assessment_Id == Assid
                                              select i.RACAP_Prospective_Parent_Id).FirstOrDefault();
                    int RACAP_Case_Id = (from j in db.RACAP_Case_Details
                                         where j.Intake_Assessment_Id == Assid
                                         select j.RACAP_Case_Id).FirstOrDefault();

                    //RACAP_Adoptive_Child editCase = db.RACAP_Adoptive_Child.Find(AdoptiveChildId1);
                    RACAP_Prospective_Parent editCase = db.RACAP_Prospective_Parent.Find(ProspectiveParent1);
                    if (ProspectiveParent1 == 0)
                    {
                        RACAP_Prospective_Parent newCase = new RACAP_Prospective_Parent();
                        newCase.Intake_Assessment_Id = Assid;
                        newCase.RACAP_Case_Id = RACAP_Case_Id;
                        newCase.Created_By = userId;
                        newCase.Date_Created = DateTime.Now;
                        if (cases.RACAPCaseViewModel.Eye_Color_Id != null)
                        {
                            newCase.Eye_Color_Id = cases.RACAPCaseViewModel.Eye_Color_Id;
                        }

                        if (cases.RACAPCaseViewModel.Skin_Colour_Id != null)
                        {
                            newCase.Skin_Color_Id = cases.RACAPCaseViewModel.Skin_Colour_Id;
                        }

                        if (cases.RACAPCaseViewModel.Special_Needs_Id != null)
                        {
                            newCase.Special_Needs_Id = cases.RACAPCaseViewModel.Special_Needs_Id;
                        }

                        if (cases.RACAPCaseViewModel.Religion_Id != null)
                        {
                            newCase.Religion_Id = cases.RACAPCaseViewModel.Religion_Id;
                        }

                        if (cases.RACAPCaseViewModel.Language_Id != null)
                        {
                            newCase.Language_Id = cases.RACAPCaseViewModel.Language_Id;
                        }

                        if (cases.RACAPCaseViewModel.Gender_Id != null)
                        {
                            newCase.Gender_Id = cases.RACAPCaseViewModel.Gender_Id;
                        }
                        if (cases.RACAPCaseViewModel.Population_Group_Id != null)
                        {
                            newCase.Race_Id = cases.RACAPCaseViewModel.Race_Id;
                        }
                        if (cases.RACAPCaseViewModel.Race_Id_Option2 != null)
                        {
                            newCase.Race_Id_Option2 = cases.RACAPCaseViewModel.Race_Id_Option2;
                        }

                        if (cases.RACAPCaseViewModel.Body_Structure_Id != null)
                        {
                            newCase.Body_Structure_Id = cases.RACAPCaseViewModel.Body_Structure_Id;
                        }

                        if (cases.RACAPCaseViewModel.Ethnic_Cultural_Group_Id != null)
                        {

                            newCase.Ethnic_Cultural_Group_Id = cases.RACAPCaseViewModel.Ethnic_Cultural_Group_Id;
                        }
                        if (cases.RACAPCaseViewModel.Population_Group_Id != null)
                        {

                            newCase.Population_Group_Id = cases.RACAPCaseViewModel.Population_Group_Id;
                        }
                        if (cases.RACAPCaseViewModel.Disability_Id != null)
                        {

                            newCase.Disability_Id = cases.RACAPCaseViewModel.Disability_Id;
                        }
                        if (cases.RACAPCaseViewModel.Age_From != null)
                        {

                            newCase.Age_From = cases.RACAPCaseViewModel.Age_From;
                        }
                        if (cases.RACAPCaseViewModel.Age_To != null)
                        {

                            newCase.Age_To = cases.RACAPCaseViewModel.Age_To;
                        }
                        db.RACAP_Prospective_Parent.Add(newCase);
                        db.SaveChanges();

                        var racapSpecialNeeds = new RACAP_SpecialNeed_Selection();
                        UserModel userModel = new UserModel();
                        foreach (var item in cases.SelectedSpecialNeedsIds)
                        {
                            var ObjChecks = (from s in db.RACAP_SpecialNeed_Selection
                                             where s.SpecialNeed_Id == item &&
                                             s.RACAP_Case_Id == RACAP_Case_Id
                                             select s).FirstOrDefault();
                            if (ObjChecks != null)
                            {
                                racapSpecialNeeds = db.RACAP_SpecialNeed_Selection.Find(ObjChecks.SpecialNeed_Selection_Id);
                                racapSpecialNeeds.RACAP_Case_Id = RACAP_Case_Id;
                                racapSpecialNeeds.SpecialNeed_Id = item;
                                racapSpecialNeeds.Modified_By = userId;
                                racapSpecialNeeds.Modified_Date = DateTime.Now;
                                racapSpecialNeeds.Is_Active = true;
                                racapSpecialNeeds.Is_Deleted = false;
                                db.SaveChanges();
                            }
                            if (ObjChecks == null)
                            {
                                racapSpecialNeeds = new RACAP_SpecialNeed_Selection();
                                racapSpecialNeeds.RACAP_Case_Id = RACAP_Case_Id;
                                racapSpecialNeeds.SpecialNeed_Id = item;
                                racapSpecialNeeds.Created_By = userId;
                                racapSpecialNeeds.CreatedTimeStamp = DateTime.Now;
                                racapSpecialNeeds.Is_Active = true;
                                racapSpecialNeeds.Is_Deleted = false;
                                db.RACAP_SpecialNeed_Selection.Add(racapSpecialNeeds);
                                db.SaveChanges();
                            }
                        }
                    }
                    if (ProspectiveParent1 != 0)
                    {
                        editCase.RACAP_Prospective_Parent_Id = ProspectiveParent1;

                        editCase.Modified_By = userId;
                        editCase.Date_Modified = DateTime.Now;
                        if (cases.RACAPCaseViewModel.Eye_Color_Id != null)
                        {
                            editCase.Eye_Color_Id = cases.RACAPCaseViewModel.Eye_Color_Id;
                        }

                        if (cases.RACAPCaseViewModel.Skin_Colour_Id != null)
                        {
                            editCase.Skin_Color_Id = cases.RACAPCaseViewModel.Skin_Colour_Id;
                        }

                        if (cases.RACAPCaseViewModel.Special_Needs_Id != null)
                        {
                            editCase.Special_Needs_Id = cases.RACAPCaseViewModel.Special_Needs_Id;
                        }

                        if (cases.RACAPCaseViewModel.Religion_Id != null)
                        {
                            editCase.Religion_Id = cases.RACAPCaseViewModel.Religion_Id;
                        }

                        if (cases.RACAPCaseViewModel.Language_Id != null)
                        {
                            editCase.Language_Id = cases.RACAPCaseViewModel.Language_Id;
                        }

                        if (cases.RACAPCaseViewModel.Gender_Id != null)
                        {
                            editCase.Gender_Id = cases.RACAPCaseViewModel.Gender_Id;
                        }
                        if (cases.RACAPCaseViewModel.Population_Group_Id != null)
                        {
                            editCase.Race_Id = cases.RACAPCaseViewModel.Race_Id;
                        }
                        if (cases.RACAPCaseViewModel.Race_Id_Option2 != null)
                        {
                            editCase.Race_Id_Option2 = cases.RACAPCaseViewModel.Race_Id_Option2;
                        }

                        if (cases.RACAPCaseViewModel.Body_Structure_Id != null)
                        {
                            editCase.Body_Structure_Id = cases.RACAPCaseViewModel.Body_Structure_Id;
                        }

                        if (cases.RACAPCaseViewModel.Ethnic_Cultural_Group_Id != null)
                        {

                            editCase.Ethnic_Cultural_Group_Id = cases.RACAPCaseViewModel.Ethnic_Cultural_Group_Id;
                        }
                        if (cases.RACAPCaseViewModel.Population_Group_Id != null)
                        {

                            editCase.Population_Group_Id = cases.RACAPCaseViewModel.Population_Group_Id;
                        }
                        if (cases.RACAPCaseViewModel.Disability_Id != null)
                        {

                            editCase.Disability_Id = cases.RACAPCaseViewModel.Disability_Id;
                        }
                        if (cases.RACAPCaseViewModel.Age_From != null)
                        {

                            editCase.Age_From = cases.RACAPCaseViewModel.Age_From;
                        }
                        if (cases.RACAPCaseViewModel.Age_To != null)
                        {

                            editCase.Age_To = cases.RACAPCaseViewModel.Age_To;
                        }
                        var racapSpecialNeeds = new RACAP_SpecialNeed_Selection();
                        UserModel userModel = new UserModel();
                        if (cases.RACAPCaseViewModel.SelectedSpecialNeedsIds != null) { 
                        foreach (var item in cases.RACAPCaseViewModel.SelectedSpecialNeedsIds)
                        {
                            var ObjChecks = (from s in db.RACAP_SpecialNeed_Selection
                                             where s.SpecialNeed_Id == item &&
                                             s.RACAP_Case_Id == RACAP_Case_Id
                                             select s).FirstOrDefault();
                            if (ObjChecks != null)
                            {
                                racapSpecialNeeds = db.RACAP_SpecialNeed_Selection.Find(ObjChecks.SpecialNeed_Selection_Id);
                                racapSpecialNeeds.RACAP_Case_Id = RACAP_Case_Id;
                                racapSpecialNeeds.SpecialNeed_Id = item;
                                racapSpecialNeeds.Modified_By = userId;
                                racapSpecialNeeds.Modified_Date = DateTime.Now;
                                racapSpecialNeeds.Is_Active = true;
                                racapSpecialNeeds.Is_Deleted = false;
                                db.SaveChanges();
                            }
                            if (ObjChecks == null)
                            {
                                racapSpecialNeeds = new RACAP_SpecialNeed_Selection();
                                racapSpecialNeeds.RACAP_Case_Id = RACAP_Case_Id;
                                racapSpecialNeeds.SpecialNeed_Id = item;
                                racapSpecialNeeds.Created_By = userId;
                                racapSpecialNeeds.CreatedTimeStamp = DateTime.Now;
                                racapSpecialNeeds.Is_Active = true;
                                racapSpecialNeeds.Is_Deleted = false;
                                db.RACAP_SpecialNeed_Selection.Add(racapSpecialNeeds);
                                db.SaveChanges();
                            }
                        }

                        }
                    }
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

        #endregion

        #region GET ORGANISATION REPSONSIBLE FOR RACAP


        public int GetRACAPParentOrganisationByssId(int IntAss)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                var RacapCaseId = (from p in db.Persons
                                   join c in db.Clients on p.Person_Id equals c.Person_Id
                                   join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                                   join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                                   where i.Intake_Assessment_Id.Equals(IntAss)
                                   select Case.RACAP_Case_Id).FirstOrDefault();

                return (from p in db.Persons
                        join c in db.Clients on p.Person_Id equals c.Person_Id
                        join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                        join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                        join F in db.RACAP_OrgansationResponsible on Case.RACAP_Case_Id equals F.RACAP_Case_Id
                        where Case.RACAP_Case_Id.Equals(RacapCaseId)
                        select F.RACAP_Responsible_Organisation).FirstOrDefault();
            }
        }

        public RACAPCaseViewModel GetRACAPParentOrganisationDetails(int OrganizationIdChild)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? OrganizationChild = (from i in db.RACAP_Case_Details
                                              join b in db.RACAP_OrgansationResponsible on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                              where b.RACAP_Responsible_Organisation == OrganizationIdChild
                                              select b.RACAP_Responsible_Organisation).FirstOrDefault();

                    RACAP_OrgansationResponsible act = db.RACAP_OrgansationResponsible.Find(OrganizationChild);
                    if (act.Organization_Id_Parent!= null)
                    {
                        vm.OrganizationP_Id = act.Organization_Id_Parent;
                        vm.Organisation_Type_Id = db.Organizations.Find(db.RACAP_OrgansationResponsible.Find(OrganizationChild).Organization_Id_Parent).Organisation_Type_Id;
                                                   
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


        public void CreateRACAPParentOrganisation(RACAPCaseViewModel cases, int id, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    RACAP_OrgansationResponsible newCase = new RACAP_OrgansationResponsible();

                    newCase.RACAP_Case_Id = (from a in db.Intake_Assessments
                                             join b in db.RACAP_Case_Details on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                                             where a.Intake_Assessment_Id == id
                                             select b.RACAP_Case_Id).FirstOrDefault();
                    db.RACAP_OrgansationResponsible.Add(newCase);
                    db.SaveChanges();
                    int idnew = newCase.RACAP_Responsible_Organisation;
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

        public RACAPCaseViewModel GetRACAPParentOrganisationDetailsCreate(int ResponsibleOrganisationid)
        {
            RACAPCaseViewModel AdoptCasevm = new RACAPCaseViewModel();


            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? ResponsibleOrganisation = (from i in db.RACAP_Case_Details
                                                    join b in db.RACAP_OrgansationResponsible on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                                    where b.RACAP_Responsible_Organisation == ResponsibleOrganisationid
                                                    select b.RACAP_Responsible_Organisation).FirstOrDefault();

                    RACAP_OrgansationResponsible act = db.RACAP_OrgansationResponsible.Find(ResponsibleOrganisation);


                    if (act.Organization_Id_Child != null)
                    {
                        AdoptCasevm.Organization_Id = act.Organization_Id_Child;
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

                return AdoptCasevm;
            }
        }

        public void UpdateRACAPParentOrganisation(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int ResponsibleOrganisation = (from i in db.RACAP_OrgansationResponsible
                                                   join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                                   join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                                   where a.Intake_Assessment_Id == Assid
                                                   select i.RACAP_Responsible_Organisation).FirstOrDefault();
                    if (ResponsibleOrganisation == 0)
                    {
                        RACAP_OrgansationResponsible newCase = new RACAP_OrgansationResponsible();
                        newCase.Organization_Id_Parent = cases.RACAPCaseViewModel.OrganizationP_Id;
                        newCase.Social_Worker_Id = cases.RACAPOrgResponsibleVM.EmployeeId;
                        newCase.Intake_Assessment_Id = Assid;
                        newCase.Social_Worker_Id = cases.RACAPOrgResponsibleVM.SocialWorkerId;
                        db.RACAP_OrgansationResponsible.Add(newCase);
                        db.SaveChanges();   

                    }
                    if (ResponsibleOrganisation != 0) { 

                    RACAP_OrgansationResponsible editCase = db.RACAP_OrgansationResponsible.Find(ResponsibleOrganisation);

                        editCase.RACAP_Responsible_Organisation = ResponsibleOrganisation;
                        editCase.Social_Worker_Id = cases.RACAPOrgResponsibleVM.SocialWorkerId;

                        if (cases.RACAPCaseViewModel.OrganizationP_Id != null)
                    {
                            editCase.Organization_Id_Parent = cases.RACAPCaseViewModel.OrganizationP_Id;
                            editCase.Social_Worker_Id = cases.RACAPOrgResponsibleVM.SocialWorkerId;
                            editCase.Intake_Assessment_Id = Assid;
                            db.SaveChanges();
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
            }
        }


        #endregion

        public void CreateRACAPParentCase(RACAPCaseViewModel cases, int id, int clientref, int userId)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {

                    RACAP_Case_Details newCase = new RACAP_Case_Details();

                    newCase.Intake_Assessment_Id = id;
                    //newCase.Client_Module_Id = cases.Client_Module_Id;
                    newCase.Intake_Assessment_Id = (from a in db.Intake_Assessments
                                                    where a.Intake_Assessment_Id == id
                                                    select a.Intake_Assessment_Id).FirstOrDefault();
                    newCase.RACAP_Registration_Status_Id = 1;
                    db.RACAP_Case_Details.Add(newCase);
                    db.SaveChanges();
                    RACAP_Case_WorkList wlist = (from a in db.RACAP_Case_WorkList
                                                 where a.Intake_Assessment_Id == id
                                                 select a).FirstOrDefault();
                    wlist.RACAP_Record_Status_Id = 1;
                    db.SaveChanges();

                    int idnew = newCase.RACAP_Case_Id;
                    int RPPId = (from a in db.RACAP_Prospective_Parent
                                 where a.RACAP_Case_Id == idnew
                                 select a.RACAP_Prospective_Parent_Id).FirstOrDefault();
                    if (RPPId == 0)
                    {
                        RACAP_Prospective_Parent NewObj = new RACAP_Prospective_Parent();
                        NewObj.RACAP_Case_Id = idnew;
                        NewObj.Intake_Assessment_Id = id;
                        NewObj.Date_Created = DateTime.Now;
                        db.RACAP_Prospective_Parent.Add(NewObj);
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


        #region RACAP PARENT REMOVAL
        public int GetRACAPParentRemovalByssId(int IntAss)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {

                var RacapCaseId = (from p in db.Persons
                                   join c in db.Clients on p.Person_Id equals c.Person_Id
                                   join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                                   join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                                   where i.Intake_Assessment_Id.Equals(IntAss)
                                   select Case.RACAP_Case_Id).FirstOrDefault();

                return (from Case in db.RACAP_Case_Details
                        where Case.RACAP_Case_Id.Equals(RacapCaseId)
                        select Case.RACAP_Case_Id).FirstOrDefault();
            }
        }

        public RACAPCaseViewModel GetRACAPParentRemovalDetails(int Caseid)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? removeid = (from i in db.RACAP_Case_Details
                                     join b in db.RACAP_Removal on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                     where i.RACAP_Case_Id == Caseid
                                     select b.Removal_Id).FirstOrDefault();
                    if (removeid > 0)
                    {

                        RACAP_Removal act = db.RACAP_Removal.Find(removeid);

                        vm.Removal_Type_Id = act.Removal_Type_Id;
                        vm.Removal_Comments = act.Removal_Comments;
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

        public void UpdateRACAPParentRemoval(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int RACAP_Case_Id1 = (from i in db.RACAP_Prospective_Parent
                                          join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                          join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                          where a.Intake_Assessment_Id == Assid
                                          select b.RACAP_Case_Id).FirstOrDefault();

                    int RACAP_Case_Id1r = (from i in db.RACAP_Prospective_Parent
                                           join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                           join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                           join r in db.RACAP_Removal on b.RACAP_Case_Id equals r.RACAP_Case_Id
                                           where r.RACAP_Case_Id == RACAP_Case_Id1
                                           select b.RACAP_Case_Id).FirstOrDefault();
                    if (RACAP_Case_Id1r <= 0)
                    {
                        RACAP_Removal newremove = new RACAP_Removal();
                        newremove.RACAP_Case_Id = RACAP_Case_Id1;

                        newremove.Date_Removed = DateTime.Now;
                        db.RACAP_Removal.Add(newremove);
                        db.SaveChanges();
                    }


                    int RACAP_Case_Id1r2 = (from i in db.RACAP_Prospective_Parent
                                            join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                            join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                            join r in db.RACAP_Removal on b.RACAP_Case_Id equals r.RACAP_Case_Id
                                            where r.RACAP_Case_Id == RACAP_Case_Id1
                                            select b.RACAP_Case_Id).FirstOrDefault();

                    //Update.....................................................................
                    if (RACAP_Case_Id1r2 >= 0)
                    {
                        int Removalid = (from i in db.RACAP_Prospective_Parent
                                         join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                                         join a in db.Intake_Assessments on b.Intake_Assessment_Id equals a.Intake_Assessment_Id
                                         join r in db.RACAP_Removal on b.RACAP_Case_Id equals r.RACAP_Case_Id
                                         where r.RACAP_Case_Id == RACAP_Case_Id1r2
                                         select r.Removal_Id).FirstOrDefault();

                        RACAP_Removal edit = db.RACAP_Removal.Find(Removalid);

                        edit.RACAP_Case_Id = RACAP_Case_Id1r2;

                        if (cases.RACAPCaseViewModel.Removal_Type_Id != null)
                        {
                            edit.Removal_Type_Id = cases.RACAPCaseViewModel.Removal_Type_Id;
                        }
                        if (cases.RACAPCaseViewModel.Removal_Comments != null)
                        {
                            edit.Removal_Comments = cases.RACAPCaseViewModel.Removal_Comments;
                            edit.Date_Removed = DateTime.Now;
                        }
                        db.SaveChanges();
                        var UpdateRACAPCaseDetails = db.RACAP_Case_Details.Find(RACAP_Case_Id1r2);
                        UpdateRACAPCaseDetails.RACAP_Record_Status_Id = 4;
                        UpdateRACAPCaseDetails.Modified_By = userId;
                        UpdateRACAPCaseDetails.Date_Modified = DateTime.Now;
                        db.SaveChanges();

                        var UpdateRACAPWorkList = (from g in db.RACAP_Case_WorkList
                                                   where g.Intake_Assessment_Id == Assid
                                                   select g).FirstOrDefault();
                        UpdateRACAPWorkList.RACAP_Record_Status_Id = 4;
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

        #endregion

        #region ParentPopup

        public List<RACAPPersonViewModel> GetListOfParents()
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                List<Person> NewObj = new List<Person>();

                var ListAsFromDb = (from a in db.Persons
                                    join c in db.Clients on a.Person_Id equals c.Person_Id
                                    join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                                    join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id

                                    where i.Intake_Assessment_Id== 28387
                                    select new
                                    {
                                        a.Person_Id,
                                        a.First_Name,
                                        a.Last_Name,
                                        a.Identification_Number
                                    }).ToList();
                List<RACAPPersonViewModel> RPVM = new List<RACAPPersonViewModel>();

                foreach (var item in ListAsFromDb)
                {
                    RACAPPersonViewModel Obj = new RACAPPersonViewModel();
                    Obj.Person_Id = item.Person_Id;
                    Obj.First_Name = item.First_Name;
                    Obj.Last_Name = item.Last_Name;
                    Obj.Identification_Number = item.Identification_Number;
                    RPVM.Add(Obj);
                }
                return RPVM;
             }
        }

        #endregion

        #region Correspondence

        public RACAPCaseViewModel GetRACAPCorrespondences(int Caseid)
        {
            RACAPCaseViewModel vm = new RACAPCaseViewModel();

            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int? correspondenceid = (from i in db.RACAP_Correspondence
                                     where i.RACAP_Case_Id == Caseid
                                     select i.RACAP_Correspondence_Id).FirstOrDefault();
                    if (correspondenceid > 0)
                    {

                        RACAP_Correspondence act = db.RACAP_Correspondence.Find(correspondenceid);

                        vm.RACAP_Correspondence_Type_Id = act.RACAP_Correspondence_Type_Id;
                        vm.RACAP_Correspondence_Comments = act.RACAP_Correspondence_Comments;
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

        //public List<RACAPCaseViewModel> GetRACAPCorrespondencesList(int Caseid)
        //{
        //    List<RACAPCaseViewModel> ListedObj = new List<RACAPCaseViewModel>();

        //    using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
        //    {
        //        var RCor = (from i in db.RACAP_Correspondence
        //                    where i.RACAP_Case_Id == Caseid
        //                    select i).ToList();
        //        if (RCor != null)
        //        {

        //            RACAPCaseViewModel act = new RACAPCaseViewModel();
        //            foreach (var item in RCor)
        //            {
        //                act.RACAP_Correspondence_Id = item.RACAP_Correspondence_Id;
        //                act.RACAP_Case_Id = item.RACAP_Case_Id;
        //                act.RACAP_Correspondence_Comments = item.RACAP_Correspondence_Comments;
        //                act.RACAP_Correspondence_Created_By = item.RACAP_Correspondence_Created_By;
        //                act.RACAP_Correspondence_Date_Created = item.RACAP_Correspondence_Date_Created;
        //                act.RACAP_Correspondence_Date_Modified = item.RACAP_Correspondence_Date_Modified;
        //                act.RACAP_Correspondence_FileName = item.RACAP_Correspondence_FileName;
        //                act.RACAP_Correspondence_FilePath = item.RACAP_Correspondence_FilePath;
        //                act.RACAP_Correspondence_Modified_By = item.RACAP_Correspondence_Modified_By;
        //                act.RACAP_Correspondence_Type_Id = item.RACAP_Correspondence_Type_Id;
        //                if (item.RACAP_Correspondence_Type_Id != null)
        //                {
        //                    act.RACAP_Correspondenc_Type = db.apl_RACAP_Correspondence_Type.Find(item.RACAP_Correspondence_Type_Id).Description;

        //                }
        //                ListedObj.Add(act);
        //            }

        //        }
        //        return ListedObj;
        //    }
        //}

        public List<RACAP_Correspondence> GetRACAPCorrespondencesList(int Caseid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                return (from a in db.RACAP_Correspondence
                        where a.RACAP_Case_Id == Caseid
                        select a).ToList();
            }
        }
        public void UpdateRACAPParentCorrespondence(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int Case_Id = (from s in db.RACAP_Case_Details
                                   where s.Intake_Assessment_Id == Assid
                                   select s.RACAP_Case_Id).FirstOrDefault();
                    if (cases.RACAPCaseViewModel != null) { 
                    RACAP_Correspondence RCorr = (from c in db.RACAP_Correspondence
                                                  where c.RACAP_Case_Id == Case_Id && c.RACAP_Correspondence_Type_Id== cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id
                                                  select c).FirstOrDefault();

                    int? TypeOfLetter = (from c in db.RACAP_Correspondence
                                        where c.RACAP_Case_Id == Case_Id
                                        select c.RACAP_Correspondence_Type_Id).FirstOrDefault();
                    if (RCorr != null && TypeOfLetter==cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id)
                    {
                        RCorr.RACAP_Case_Id = Case_Id;
                        RCorr.RACAP_Correspondence_Comments = cases.RACAPCaseViewModel.RACAP_Correspondence_Comments;

                            RCorr.RACAP_Correspondence_Modified_By = userId;
                            RCorr.RACAP_Correspondence_Date_Modified = DateTime.Now;
                  
                        RCorr.RACAP_Correspondence_Type_Id = cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id;
                        db.SaveChanges();
                    }
                    if (RCorr == null)
                    {
                        RACAP_Correspondence NewRCorr = new RACAP_Correspondence();
                        NewRCorr.RACAP_Case_Id = Case_Id;
                        NewRCorr.RACAP_Correspondence_Comments = cases.RACAPCaseViewModel.RACAP_Correspondence_Comments;

                            NewRCorr.RACAP_Correspondence_Created_By = userId;
                            NewRCorr.RACAP_Correspondence_Date_Created = DateTime.Now;

                        NewRCorr.RACAP_Correspondence_Type_Id = cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id;
                        db.RACAP_Correspondence.Add(NewRCorr);
                        db.SaveChanges();
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
            }
        }

        public void UpdateRACAPChildCorrespondence(MainPageModelRACAPVM cases, int userId, int Assid)
        {
            using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
            {
                try
                {
                    int Case_Id = (from s in db.RACAP_Case_Details
                                   where s.Intake_Assessment_Id == Assid
                                   select s.RACAP_Case_Id).FirstOrDefault();
                    if (cases.RACAPCaseViewModel != null) { 
                    RACAP_Correspondence RCorr = (from c in db.RACAP_Correspondence
                                                  where c.RACAP_Case_Id == Case_Id && c.RACAP_Correspondence_Type_Id == cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id
                                                  select c).FirstOrDefault();
                    
                    int? TypeOfLetter = (from c in db.RACAP_Correspondence
                                         where c.RACAP_Case_Id == Case_Id
                                         select c.RACAP_Correspondence_Type_Id).FirstOrDefault();
                    if (RCorr != null && TypeOfLetter == cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id)
                    {
                        RCorr.RACAP_Case_Id = Case_Id;
                        RCorr.RACAP_Correspondence_Comments = cases.RACAPCaseViewModel.RACAP_Correspondence_Comments;

                        RCorr.RACAP_Correspondence_Modified_By = userId;
                        RCorr.RACAP_Correspondence_Date_Modified = DateTime.Now;

                        RCorr.RACAP_Correspondence_Type_Id = cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id;
                        db.SaveChanges();
                    }
                    if (RCorr == null)
                    {
                        RACAP_Correspondence NewRCorr = new RACAP_Correspondence();
                        NewRCorr.RACAP_Case_Id = Case_Id;
                        NewRCorr.RACAP_Correspondence_Comments = cases.RACAPCaseViewModel.RACAP_Correspondence_Comments;

                        NewRCorr.RACAP_Correspondence_Created_By = userId;
                        NewRCorr.RACAP_Correspondence_Date_Created = DateTime.Now;

                        NewRCorr.RACAP_Correspondence_Type_Id = cases.RACAPCaseViewModel.RACAP_Correspondence_Type_Id;
                        db.RACAP_Correspondence.Add(NewRCorr);
                        db.SaveChanges();
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
            }
        }

        //public void CreateWordLetter(MainPageModelRACAPVM cases, int userId, int Assid)
        //{

        //    RACAPLetterTemplateModel Rep = new RACAPLetterTemplateModel();

        //    Rep.ExportToWord(cases, userId, Assid);

        //}

        //public bool CheckIfCorExists(MainPageModelRACAPVM UpdateRACAPCorrespondence, int userId, int Id)
        //{
        //    using (SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities())
        //    {
        //        var test = (from a in db.Intake_Assessments
        //                    join b in db.RACAP_Case_Details on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
        //                    join c in db.RACAP_Correspondence on b.RACAP_Case_Id equals c.RACAP_Case_Id
        //                    where b.Intake_Assessment_Id == Id
        //                    select c).ToList();
        //        if (test.Count() > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //}





        public string PreviewWordLetter(int id, int? TypeId)
        {
            SDIIS_DatabaseEntities dbF = new SDIIS_DatabaseEntities();
            var data = (from a in dbF.RACAP_Correspondence
                        join b in dbF.RACAP_Case_Details on a.RACAP_Case_Id equals b.RACAP_Case_Id
                        where b.Intake_Assessment_Id == id && a.RACAP_Correspondence_Type_Id== TypeId
                        select new { b.Intake_Assessment_Id}).ToList();
            string FileExtention = "";
            if (TypeId==1)
            {
                FileExtention = "General_letter_English";
            }
            if (TypeId == 2)
            {
                FileExtention = "Reminder_letter_English";
            }
            if (TypeId == 3)
            {
                FileExtention = "Fault_letter_English";
            }
            if (TypeId == 4)
            {
                FileExtention = "Acknowledgment_letter_English";
            }
            if (TypeId == 5)
            {
                FileExtention = "Cover_letter_English";
            }
            if (TypeId == 6)
            {
                FileExtention = "Home_Affairs_letter_English";
            }
            if (TypeId == 7)
            {
                FileExtention = "Enquiries_letter_English";
            }
            if (TypeId == 8)
            {
                FileExtention = "Regret_letter_English";
            }
            //string File__ = (from a in data
            //                 select a.Intake_Assessment_Id).FirstOrDefault() + FileExtention;
            string File__ = id +"_"+TypeId;
            return File__;
        }


        #endregion

        public void UpdateRegistrationDate(int id, string Username)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var RegStatusUpdate = db.RACAP_Case_Details.Find(id);
            RegStatusUpdate.Date_Registered = DateTime.Now;
            RegStatusUpdate.Date_Modified = DateTime.Now;
            RegStatusUpdate.Modified_By = (from a in db.Users
                                           where a.User_Name == Username
                                           select a.User_Id).FirstOrDefault();
            db.SaveChanges();

        }

        public void UpdateRegistrationStatus(int id, string Username)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var RegStatusUpdate = db.RACAP_Case_Details.Find(id);
            RegStatusUpdate.RACAP_Registration_Status_Id = 3;
            RegStatusUpdate.Verified = true;
            RegStatusUpdate.DateVerified = DateTime.Now;
            RegStatusUpdate.VerifiedBy = (from a in db.Users
                                          where a.User_Name == Username
                                          select a.User_Id).FirstOrDefault(); ;
            RegStatusUpdate.Date_Registered = DateTime.Now;
            RegStatusUpdate.RACAP_Record_Status_Id = 2;
            RegStatusUpdate.Date_Modified = DateTime.Now;
            RegStatusUpdate.Modified_By = (from a in db.Users
                                           where a.User_Name == Username
                                           select a.User_Id).FirstOrDefault();
           db.SaveChanges();
            var WorkListUpdate = (from a in db.RACAP_Case_WorkList
                                  where a.Intake_Assessment_Id == RegStatusUpdate.Intake_Assessment_Id
                                  select a).FirstOrDefault();
            WorkListUpdate.RACAP_Record_Status_Id = 2;
            db.SaveChanges(); 
        }

        public string GetPersonId(int? id)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            string Result = Convert.ToString((from a in db.RACAP_Prospective_Parent
                          join b in db.Intake_Assessments on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                          join c in db.Clients on b.Client_Id equals c.Client_Id
                          where a.RACAP_Prospective_Parent_Id == id
                          select c.Person_Id).FirstOrDefault());

            return Result;
 
        }
        #endregion Parent
        //End Amended by Herman

        #region ForBoth
        public string GetReferenceNumberRACAP(int? Id)
            {
                SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

                return (from a in db.Intake_Assessments
                        join b in db.RACAP_Case_WorkList on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                        where a.Intake_Assessment_Id == Id
                        select b.Reference_Number).FirstOrDefault();
            }

        public List<RACAPSpecialNeedsVM> GetListOfSpecialNeeds(int Id)
        {
            using (SDIIS_DatabaseEntities dbcontext = new SDIIS_DatabaseEntities())
            {
                var specialNeeds = (from a in dbcontext.RACAP_SpecialNeed_Selection
                                    where a.RACAP_Case_Id == Id
                                    select a).ToList();
                List<RACAPSpecialNeedsVM> NewModelList = new List<RACAPSpecialNeedsVM>();
                foreach (var item in specialNeeds)
                {
                    RACAPSpecialNeedsVM NewModel = new RACAPSpecialNeedsVM();
                    NewModel.Racap_CaseId = Id;
                    NewModel.SpecialNeedId = item.SpecialNeed_Id;
                    if (item.SpecialNeed_Id != 0) { 
                    NewModel.SpecialNeedDescription = dbcontext.apl_Special_Need.Find(item.SpecialNeed_Id).Description;
                    }
                    NewModelList.Add(NewModel);
                }
                return NewModelList.ToList();
            }
        }

        public void Delete_SpecialNeed(int? Id, int? RACAP_Case_Id)
        {
            using (SDIIS_DatabaseEntities dbContext = new SDIIS_DatabaseEntities())
            {
                var DelSpecialNeed = (from a in dbContext.RACAP_SpecialNeed_Selection
                                      where a.SpecialNeed_Id == Id && a.RACAP_Case_Id == RACAP_Case_Id
                                      select a).FirstOrDefault();
                dbContext.RACAP_SpecialNeed_Selection.Remove(DelSpecialNeed);
                dbContext.SaveChanges();
            }
        }

        public int GetRACAPCaseId(int? id)
        {
            //SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            return (from j in db.RACAP_Case_Details
                    where j.Intake_Assessment_Id == id
                    select j.RACAP_Case_Id).FirstOrDefault();
        }

        public int GetRACAPCaseIdChildDetailsIndex(int id)
        {
            return (from p in db.Persons
                    join c in db.Clients on p.Person_Id equals c.Person_Id
                    join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                    join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                    where i.Intake_Assessment_Id.Equals(id)
                    select Case.RACAP_Case_Id).FirstOrDefault();
        }

        public int GetRACAPCaseIdParentDetailsIndex(int id)
        {
            return (from p in db.Persons
                    join c in db.Clients on p.Person_Id equals c.Person_Id
                    join i in db.Intake_Assessments on c.Client_Id equals i.Client_Id
                    join Case in db.RACAP_Case_Details on i.Intake_Assessment_Id equals Case.Intake_Assessment_Id
                    where i.Intake_Assessment_Id.Equals(id)
                    select Case.RACAP_Case_Id).FirstOrDefault();
        }

        public int? GetFromWhere(int Id)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            int? Result = (from t in db.RACAP_Case_Details
                          join u in db.Intake_Assessments on t.Intake_Assessment_Id equals u.Intake_Assessment_Id
                          where t.RACAP_Case_Id == Id
                          select u.Problem_Sub_Category_Id).FirstOrDefault();
                return Result;
        }

        public int GetIntakeId(int Id)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            int Result = (from t in db.RACAP_Case_Details
                           join u in db.Intake_Assessments on t.Intake_Assessment_Id equals u.Intake_Assessment_Id
                           where t.RACAP_Case_Id == Id
                           select u.Intake_Assessment_Id).FirstOrDefault();
            return Result;

        }

        public Social_Worker GetSocialWorkerDetails(int userId)
        {

            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var Retrieve = (from a in db.Employees
                           join b in db.Users on a.User_Id equals b.User_Id
                           join c in db.Service_Offices on a.Service_Office_Id equals c.Service_Office_Id
                           join d in db.Local_Municipalities on c.Local_Municipality_Id equals d.Local_Municipality_Id
                           join e in db.Districts on d.District_Municipality_Id equals e.District_Id
                           join f in db.Provinces on e.Province_Id equals f.Province_Id
                           join g in db.Social_Workers on b.User_Id equals g.User_Id
                           where a.User_Id== userId
                           select new { a.User_Id,
                                        a.Employee_Id,
                                        b.Email_Address,
                                        a.Mobile_Phone_Number,
                                        f.Description,
                                        g.SocialWorkerPracticeNumber,
                                        g.AccreditedDate
                          }).FirstOrDefault();
            if (Retrieve != null)
            {
                Social_Worker NewObject = new Social_Worker();
                NewObject.User_Id = Retrieve.User_Id;
                NewObject.SocialWorkerPracticeNumber = Retrieve.SocialWorkerPracticeNumber;
                NewObject.AccreditedDate = Retrieve.AccreditedDate;
                NewObject.Phone_Number = Retrieve.Mobile_Phone_Number;
                NewObject.E_Mail = Retrieve.Email_Address;
                NewObject.Province = Retrieve.Description;
                return NewObject;
            }
            else
            {
                Social_Worker NewObject = new Social_Worker();
                return NewObject;
            }

        }

        public RACAPOrgResponsibleVM GetSocialWorkerDetails_1(int? userId)
        {

            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            var Retrieve = (from a in db.Employees
                            join b in db.Users on a.User_Id equals b.User_Id
                            join c in db.Service_Offices on a.Service_Office_Id equals c.Service_Office_Id
                            join d in db.Local_Municipalities on c.Local_Municipality_Id equals d.Local_Municipality_Id
                            join e in db.Districts on d.District_Municipality_Id equals e.District_Id
                            join f in db.Provinces on e.Province_Id equals f.Province_Id
                            join g in db.Social_Workers on b.User_Id equals g.User_Id
                            where a.User_Id == userId
                            select new
                            {
                                a.User_Id,
                                a.Employee_Id,
                                b.First_Name,
                                b.Last_Name,
                                b.Email_Address,
                                a.Mobile_Phone_Number,
                                f.Description,
                                g.SocialWorkerPracticeNumber,
                                g.AccreditedDate,
                                a.ID_Number,
                                a.Race_Id
                            }).FirstOrDefault();

                RACAPOrgResponsibleVM NewObject1 = new RACAPOrgResponsibleVM();
                    NewObject1.Accreditation_Ref = Retrieve.SocialWorkerPracticeNumber;
                    NewObject1.Social_Worker_Surname = Retrieve.First_Name + " " + Retrieve.Last_Name;
                    NewObject1.Accredited_Date = Retrieve.AccreditedDate;
                    NewObject1.SocWorkTelephone = Retrieve.Mobile_Phone_Number;
                    NewObject1.SocWorkEmail = Retrieve.Email_Address;
                    NewObject1.SocWorkProvince = Retrieve.Description;
                    NewObject1.IdNumber = Retrieve.ID_Number;
                    NewObject1.Race = db.Races.Find(Retrieve.Race_Id).Description;
            return NewObject1;
        }

        public bool CheckIfSocialWorkerIsAdded(int OrganizationIdParent)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            int? Result =(from a in db.RACAP_OrgansationResponsible
                         where a.RACAP_Responsible_Organisation == OrganizationIdParent
                         select a.Social_Worker_Id).FirstOrDefault();
            if (Result != 0 && Result != null) {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int? GetSoWId(int? Result)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            int? Results = (from a in db.RACAP_OrgansationResponsible
                           where a.RACAP_Responsible_Organisation == Result
                          select a.Social_Worker_Id).FirstOrDefault();

                return Results;


        }

        public string GetRACAPRefNumber(int Id)
        {
            return (from z in db.RACAP_Case_WorkList
                    where z.Intake_Assessment_Id == ((from a in db.Persons
                                                      join b in db.Clients on a.Person_Id equals b.Person_Id
                                                      join c in db.Intake_Assessments on b.Client_Id equals c.Client_Id
                                                      where a.Person_Id == Id
                                                      select c.Intake_Assessment_Id).FirstOrDefault())
                    select z.Reference_Number).FirstOrDefault();
        }

        public string GetPersonName(int Id)
        {
            return db.Persons.Find(Id).First_Name;
        }
        public string GetPersonSurName(int Id)
        {
            return db.Persons.Find(Id).Last_Name;
        }

        public string GetDateCreated(int Id)
        {
            return (from d in db.RACAP_Case_Details
                    join e in db.Intake_Assessments on d.Intake_Assessment_Id equals e.Intake_Assessment_Id
                    join f in db.Clients on e.Client_Id equals f.Client_Id
                    where f.Person_Id == Id
                    select e.Date_Created).FirstOrDefault().ToShortDateString();
        }

        public IList<Common_Objects.Models.Organization> GetOrganisation(int IdType)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Organizations.Where(m => m.Organisation_Type_Id == IdType).ToList();
        }

        public IList<Common_Objects.Models.Social_Worker> GetSocial_Worker(int OrgabisationId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();

            return db.Social_Workers.Where(m => m.Organization_Id == OrgabisationId).ToList();
        }

        public IList<Common_Objects.Models.Social_Worker> GetRACAPSocial_Worker(int userId)
        {
            SDIIS_DatabaseEntities db = new SDIIS_DatabaseEntities();
            RACAPCaseDetailsModel Model = new RACAPCaseDetailsModel();
            var retrieve = Model.GetSocialWorkerDetails(userId);


            var test = db.Social_Workers.Where(m => m.User_Id == userId).ToList();
            return test;
        }

        public int GetPersonId(int id)
        {
            return (from a in db.Persons
                    join b in db.Clients on a.Person_Id equals b.Person_Id
                    join c in db.Intake_Assessments on b.Client_Id equals c.Client_Id
                    where c.Intake_Assessment_Id == id
                    select a.Person_Id).FirstOrDefault();
        }

        public string GetPersonIdAsString(int Id)
        {
            return Convert.ToString((from a in db.Persons
                    join b in db.Clients on a.Person_Id equals b.Person_Id
                    join c in db.Intake_Assessments on b.Client_Id equals c.Client_Id
                    where c.Intake_Assessment_Id == Id
                    select a.Person_Id).FirstOrDefault());
        }

        public Person GetPersonDetails(int PersonId)
        {
            return db.Persons.Find(PersonId);
        }

        public string GetOrganisationDescription(int Orgtype)
        {
            return db.Organizations.Find(Orgtype).Description;
        }

        public int GetOrganisationTypeId(int Orgtype)
        {
            return (from k in db.apl_Organisation_Type
                    join k1 in db.Organizations on k.IdType equals k1.Organisation_Type_Id
                    where k1.Organization_Id == Orgtype
                    select k.IdType).FirstOrDefault();
        }

        public int GetIntakeAssessMentId(int PersonId)
        {
            return (from t in db.Intake_Assessments
                    join u in db.Clients on t.Client_Id equals u.Client_Id
                    join v in db.Persons on u.Person_Id equals v.Person_Id
                    where v.Person_Id == PersonId
                    select t.Intake_Assessment_Id).FirstOrDefault();
        }

        public List<RACAP_Supporting_Document> GetRACAPSupportDocs(int Racapcase)
        {
            return (from docs in db.RACAP_Supporting_Document
                    where (docs.RACAP_Case_Id == Racapcase)
                    select docs).ToList();
        }

        public void AddDocument(RACAP_Supporting_Document fuelReceipts)
        {
            db.RACAP_Supporting_Document.Add(fuelReceipts);
        }
        public void SaveDocuments()
        {
            db.SaveChanges();
        }

        public string GetGenderDescription(int? Id)
        {
            return db.Genders.Find(Id).Description;
        }
        public int GetRACAP_Adoptive_Child_Id(int? id)
        {
            return (from t in db.RACAP_Adoptive_Child
                    where t.Intake_Assessment_Id == id
                    select t.RACAP_Adoptive_Child_Id).FirstOrDefault();
        }

        public int? GetRaceId(int id)
        {
            return db.RACAP_Adoptive_Child.Find(id).Population_Group_Id;
        }

        public string GetSkin_Colour(int? id)
        {
            return db.apl_Skin_Colour.Find(id).Description;
        }
        public int? GetPopulationGroupId(int Id)
        {
            return db.RACAP_Adoptive_Child.Find(Id).Population_Group_Id;
        }

        public int GetUserId(string userName)
        {
            return (from u in db.Users
                    where u.User_Name == userName
                    select u.User_Id).FirstOrDefault();
        }

        public int GetRACAP_Prospective_Parent_Id(int id)
        {
            return (from a in db.RACAP_Prospective_Parent
                    where a.RACAP_Case_Id == id
                    select a.RACAP_Prospective_Parent_Id).FirstOrDefault();
        }

        public void AddRACAP_ProspectiveParent(int RacapcaseId,int id)
        {
            RACAP_Prospective_Parent NewObj = new RACAP_Prospective_Parent();
            NewObj.RACAP_Case_Id = RacapcaseId;
            NewObj.Intake_Assessment_Id = id;
            NewObj.Date_Created = DateTime.Now;
            db.RACAP_Prospective_Parent.Add(NewObj);
            db.SaveChanges();
        }

        public SelectList GetOrganisationType(int OrgtypId)
        {
            return new SelectList(db.apl_Organisation_Type, "IdType", "Description", OrgtypId);
        }

        public int? GetIntakeAssessmentIdFromSupDocs(int id)
        {
            return db.RACAP_Case_Details.Find(db.RACAP_Supporting_Document.Find(id).RACAP_Case_Id).Intake_Assessment_Id;
        }

        public int? GetGenderId(int id)
        {
            return (from a in db.RACAP_Prospective_Parent
                   where a.Intake_Assessment_Id == id
                   select a.Gender_Id).FirstOrDefault();
        }

        public int? GetRaceIdOfParent(int id)
        {
            return (from a in db.RACAP_Prospective_Parent
                    where a.Intake_Assessment_Id == id
                    select a.Race_Id).FirstOrDefault();
        }

        public string GetRaceDescription(int? Raceid)
        {
            return db.Races.Find(Raceid).Description;
        }
        public int? GetRacse_Id_2fromParent(int id)
        {
            return (from a in db.RACAP_Prospective_Parent
                    where a.Intake_Assessment_Id == id
                    select a.Race_Id_Option2).FirstOrDefault();
        }

        public string GetpopulationGroupDescrition(int? Race_Id_Option2)
        {
            return db.Population_Groups.Find(Race_Id_Option2).Description;
        }

        public int? GetProsParentAgeFrom(int RACAP_Prospective_Parent_Id)
        {
            return db.RACAP_Prospective_Parent.Find(RACAP_Prospective_Parent_Id).Age_From;
        }        
        public int? GetProsParentAge_To(int RACAP_Prospective_Parent_Id)
        {
            return db.RACAP_Prospective_Parent.Find(RACAP_Prospective_Parent_Id).Age_To;
        }
        public int? GetProsParentRace_Id(int RACAP_Prospective_Parent_Id)
        {
            return db.RACAP_Prospective_Parent.Find(RACAP_Prospective_Parent_Id).Race_Id;
        }
        public int? GetProsParentRace_Id_Option2(int RACAP_Prospective_Parent_Id)
        {
            return db.RACAP_Prospective_Parent.Find(RACAP_Prospective_Parent_Id).Race_Id_Option2;
        }
        public int? GetProsParentGender_Id(int RACAP_Prospective_Parent_Id)
        {
            return db.RACAP_Prospective_Parent.Find(RACAP_Prospective_Parent_Id).Gender_Id;
        }
        public int GetClientId(int id)
        {
            return db.Intake_Assessments.Find(id).Client_Id;
        }
        public int GetProvId()
        {
            return (from k in db.Provinces
                    select k.Province_Id).FirstOrDefault();
        }

        public SelectList GetProvinces(int ProvId)
        {
            return new SelectList(db.Provinces, "Province_Id", "Description", ProvId);
        }
        public int GetDistrictId()
        {
            return (from k in db.Districts
                    select k.District_Id).FirstOrDefault();
        }

        public SelectList GetDistricts(int? DistrictId)
        {
            return new SelectList(db.Districts, "District_Id", "Description", DistrictId);
        }
        public int GetLocal_MunicipalitiesId()
        {
            return (from k in db.Local_Municipalities
                    select k.Local_Municipality_Id).FirstOrDefault();
        }

        public SelectList GetLocal_Municipalities(int? LocalMuniId)
        {
            return new SelectList(db.Local_Municipalities, "Local_Municipality_Id", "Description", LocalMuniId);
        }
        public int GetTownsId()
        {
            return (from k in db.Towns
                    select k.Town_Id).FirstOrDefault();
        }

        public SelectList GetTowns(int? TownId)
        {
            return new SelectList(db.Towns, "Town_Id", "Description", TownId);
        }

        public int? GetParentIntakeAssessmentId(int? Pid)
        {
            return db.RACAP_Prospective_Parent.Find(Pid).Intake_Assessment_Id;
        }

        public int? GetChildIntakeAssessmentId(int? Cid)
        {
            return db.RACAP_Adoptive_Child.Find(Cid).Intake_Assessment_Id;
        }

        public int GetCase_Id(int Id)
        {
            return (from a in db.Intake_Assessments
                    join b in db.RACAP_Case_Details on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                    join c in db.RACAP_Correspondence on b.RACAP_Case_Id equals c.RACAP_Case_Id
                    where b.Intake_Assessment_Id == Id
                    select b.RACAP_Case_Id).FirstOrDefault();
        }
        #endregion

        public string GetReferenceNumberRACAP(int Id)
        {
            return (from a in db.Intake_Assessments
                    join b in db.RACAP_Case_WorkList on a.Intake_Assessment_Id equals b.Intake_Assessment_Id
                    where a.Intake_Assessment_Id == Id
                    select b.Reference_Number).FirstOrDefault();
        }

        public int GetRACAP_Prospective_Parent_Id(int? IAid)
        {
            return (from i in db.RACAP_Prospective_Parent
                    where i.Intake_Assessment_Id == IAid
                    select i.RACAP_Prospective_Parent_Id).FirstOrDefault();
        }

        public int GetRACAP_CaseWoklist_Id(int? IAid)
        {
            return (from x in db.RACAP_Case_WorkList
                    where x.Intake_Assessment_Id == IAid
                    select x.RACAP_CaseWoklist_Id).FirstOrDefault();
        }

        public int GetRACAP_Case_IdForParentCase(int IAid)
        {
            return (from i in db.RACAP_Case_Details
                    join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                    join k in db.RACAP_Prospective_Parent on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                    where k.Intake_Assessment_Id == IAid
                    select i.RACAP_Case_Id).FirstOrDefault();
        }

        public RACAP_Matches GetRACAP_Matches(int RACId,int Pid)
        {
            return (from n in db.RACAP_Matches
                    where n.RACAP_Prospective_Parent_Id == RACId && n.RACAP_Prospective_Parent_Id == Pid
                    select n).FirstOrDefault();
        }

        public int GetRACAP_MatchesChild(int RACId, int Pid)
        {
            return (from x in db.RACAP_Matches
                    where (x.RACAP_Adoptive_Child_Id == RACId && x.RACAP_Prospective_Parent_Id == Pid)
                    select x.RACAP_Match_Id).FirstOrDefault();
        }

        public RACAP_Case_Details GetRACAP_Case_Details(int ParentCaseid)
        {
            return db.RACAP_Case_Details.Find(ParentCaseid);
        }

        public RACAP_Case_WorkList GetRACAP_Case_WorkList(int RCWLIdForParent)
        {
            return db.RACAP_Case_WorkList.Find(RCWLIdForParent);
        }

        public void AddRACAP_Matches(RACAP_Matches RMat)
        {
            db.RACAP_Matches.Add(RMat);
        }

        public RACAP_Matches GetRACAP_Matches(int RACAP_Match_Id)
        {
            return db.RACAP_Matches.Find(RACAP_Match_Id);
        }

        public string GetRACAPSuportingDocuments(int? newId)
        {
            return db.RACAP_Supporting_Document.Find(newId).FileName;
        }


        public int GetRACAPCase_IdForParentCase(int? Pid)
        {
            return (from i in db.RACAP_Case_Details
                    join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                    join k in db.RACAP_Prospective_Parent on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                    where k.RACAP_Prospective_Parent_Id == Pid
                    select i.RACAP_Case_Id).FirstOrDefault();
        }

        public int GetRACAP_CaseWoklist_IdForRCW(int? Pid)
        {
            return (from x in db.RACAP_Case_WorkList
                    join y in db.Intake_Assessments on x.Intake_Assessment_Id equals y.Intake_Assessment_Id
                    join z in db.RACAP_Prospective_Parent on y.Intake_Assessment_Id equals z.Intake_Assessment_Id
                    where z.RACAP_Prospective_Parent_Id == Pid
                    select x.RACAP_CaseWoklist_Id).FirstOrDefault();
        }

        public int GetRACAP_Case_IdForChildCase(int? Cid)
        {
            return (from i in db.RACAP_Case_Details
                    join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                    join k in db.RACAP_Adoptive_Child on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                    where k.RACAP_Adoptive_Child_Id == Cid
                    select i.RACAP_Case_Id).FirstOrDefault();
        }

        public RACAP_Matches GetRACAP_MatchesForParent_1_C(int? Pid, int Cid)
        {
            return (from n in db.RACAP_Matches
                    where n.RACAP_Prospective_Parent_Id == Pid && n.RACAP_Adoptive_Child_Id == Cid
                    select n).FirstOrDefault();
        }
        public int GetRACAP_Adoptive_Child_Id(int IAid)
        {
            return (from i in db.RACAP_Adoptive_Child
                    where i.Intake_Assessment_Id == IAid
                    select i.RACAP_Adoptive_Child_Id).FirstOrDefault();
        }

        public int GetRACAP_Case_IdForParent_2_P(int? Pid)
        {
            return (from i in db.RACAP_Case_Details
                    join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                    join k in db.RACAP_Prospective_Parent on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                    where k.Intake_Assessment_Id == Pid
                    select i.RACAP_Case_Id).FirstOrDefault();
        }

        public int GetRACAP_CaseWoklist_IDForParent_2_P(int IAid)
        {
            return (from x in db.RACAP_Case_WorkList
                    where x.Intake_Assessment_Id == IAid
                    select x.RACAP_CaseWoklist_Id).FirstOrDefault();
        }

        public int GetRACAP_Case_IdForRCWLIdForParent(int? Pid)
        {
            return (from x in db.RACAP_Case_WorkList
                    where x.Intake_Assessment_Id == Pid
                    select x.RACAP_CaseWoklist_Id).FirstOrDefault();
        }

        public int GetRACAP_Case_IdForChildCaseid(int IAid)
        {
            return (from i in db.RACAP_Case_Details
                    join j in db.Intake_Assessments on i.Intake_Assessment_Id equals j.Intake_Assessment_Id
                    join k in db.RACAP_Adoptive_Child on j.Intake_Assessment_Id equals k.Intake_Assessment_Id
                    where k.Intake_Assessment_Id == IAid
                    select i.RACAP_Case_Id).FirstOrDefault();
        }

        public int GetRACAP_Prospective_Parent_IdForRPPId(int? Pid)
        {
            return (from x in db.RACAP_Prospective_Parent
                    where x.Intake_Assessment_Id == Pid
                    select x.RACAP_Prospective_Parent_Id).FirstOrDefault();
        }

        public RACAP_Matches GetRACAPMatchesForParent_2_P(int RPPId, int Cid)
        {
            return (from n in db.RACAP_Matches
                    where n.RACAP_Prospective_Parent_Id == RPPId && n.RACAP_Adoptive_Child_Id == Cid
                    select n).FirstOrDefault();
        }

        public int GetRACAP_MatchesParent(int RPPId, int Cid)
        {
            return (from x in db.RACAP_Matches
                    where (x.RACAP_Prospective_Parent_Id == RPPId && x.RACAP_Adoptive_Child_Id == Cid)
                    select x.RACAP_Match_Id).FirstOrDefault();
        }

        public string GetRaceDesciptionFormSkin_Colour(int? Raceid)
        {
            return db.apl_Skin_Colour.Find(Raceid).Description;
        }

        public int GetRACAP_Case_IdForParent_P(int? Pid)
        {
            return (from i in db.RACAP_Prospective_Parent
                    join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                    where b.Intake_Assessment_Id == Pid
                    select b.RACAP_Case_Id).FirstOrDefault();
        }

        public int GetRACAP_CaseWoklist_IdForParent_P(int? Pid)
        {
            return (from x in db.RACAP_Case_WorkList
                    join y in db.Intake_Assessments on x.Intake_Assessment_Id equals y.Intake_Assessment_Id
                    where y.Intake_Assessment_Id == Pid
                    select x.RACAP_CaseWoklist_Id).FirstOrDefault();
        }

        public int GetRACAP_Prospective_Parent_IdForParent_P(int? Pid)
        {
            return (from l in db.RACAP_Prospective_Parent
                    where l.Intake_Assessment_Id == Pid
                    select l.RACAP_Prospective_Parent_Id).FirstOrDefault();
        }
        public int GetRACAP_Case_IdForParent_P(int Cid)
        {
            return (from i in db.RACAP_Adoptive_Child
                    join b in db.RACAP_Case_Details on i.RACAP_Case_Id equals b.RACAP_Case_Id
                    where i.RACAP_Adoptive_Child_Id == Cid
                    select b.RACAP_Case_Id).FirstOrDefault();
        }

        public int GetRACAP_Match_IdForParent_2(int UseThisPid, int Cid)
        {
            return (from x in db.RACAP_Matches
                    where (x.RACAP_Prospective_Parent_Id == UseThisPid && x.RACAP_Adoptive_Child_Id == Cid)
                    select x.RACAP_Match_Id).FirstOrDefault();
        }
        public int? GetGender_IdForParentMatches(int id)
        {
            return (from a in db.RACAP_Prospective_Parent
                    where a.Intake_Assessment_Id == id
                    select a.Gender_Id).FirstOrDefault();
        }

        public int? GetRace_IdForParentMatches(int id)
        {
            return (from a in db.RACAP_Prospective_Parent
                    where a.Intake_Assessment_Id == id
                    select a.Race_Id).FirstOrDefault();
        }

        public int? GetRace_Id_Option2ForParentMatches(int id)
        {
            return (from a in db.RACAP_Prospective_Parent
                    where a.Intake_Assessment_Id == id
                    select a.Race_Id_Option2).FirstOrDefault();
        }
    }

}