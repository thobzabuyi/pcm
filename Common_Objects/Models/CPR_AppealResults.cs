//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Common_Objects.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CPR_AppealResults
    {
        public int AppealResult_Id { get; set; }
        public Nullable<System.DateTime> DateOfApplication { get; set; }
        public bool AppealAproved { get; set; }
        public string CourtCaseNumber { get; set; }
        public string CourtReferenceNumber { get; set; }
        public Nullable<System.DateTime> CourtDecisionDate { get; set; }
        public string OtherCourtInfo { get; set; }
        public Nullable<System.DateTime> DateOfOutcome { get; set; }
        public string OutcomeSummary { get; set; }
        public bool Form32 { get; set; }
        public bool IDDocument { get; set; }
        public bool PoliceClearance { get; set; }
        public bool IDPhoto { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string ContactNo { get; set; }
    }
}
