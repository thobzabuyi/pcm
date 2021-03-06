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
    
    public partial class Community_Profiling_Instance
    {
        public Community_Profiling_Instance()
        {
            this.Community_Profiling_Answers = new HashSet<Community_Profiling_Answer>();
        }
    
        public int Community_Profiling_Instance_Id { get; set; }
        public System.DateTime Profiling_Date { get; set; }
        public int Profiling_Tool_Id { get; set; }
        public int Captured_By_UserId { get; set; }
        public int NISIS_Site_EA_Id { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public System.DateTime Date_Created { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Date_Last_Modified { get; set; }
        public string Modified_By { get; set; }
        public int QA_Status_Item_Id { get; set; }
    
        public virtual ICollection<Community_Profiling_Answer> Community_Profiling_Answers { get; set; }
        public virtual Profiling_Tool NISIS_Profiling_Tool { get; set; }
        public virtual NISIS_Site_EA NISIS_Site_EA { get; set; }
        public virtual NISIS_QA_Status_Item NISIS_QA_Status_Item { get; set; }
        public virtual User CapturedByUser { get; set; }
    }
}
