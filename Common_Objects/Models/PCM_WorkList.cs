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
    
    public partial class PCM_WorkList
    {
        public int PCMCaseWoklist_Id { get; set; }
        public Nullable<int> Intake_Assessment_Id { get; set; }
        public Nullable<int> Allocated_By { get; set; }
        public Nullable<int> Manager { get; set; }
        public Nullable<System.DateTime> Date_Allocated { get; set; }
        public string Reference_Number { get; set; }
        public Nullable<int> PCM_Record_Status_Id { get; set; }
        public Nullable<System.DateTime> Date_Acknowledged { get; set; }
        public Nullable<System.DateTime> Date_Accepted { get; set; }
        public Nullable<int> Accepted_By { get; set; }
    
        public virtual apl_PCM_Record_Status apl_PCM_Record_Status { get; set; }
        public virtual Intake_Assessment int_Intake_Assessment { get; set; }
    }
}
