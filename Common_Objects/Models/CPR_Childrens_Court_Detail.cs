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
    
    public partial class CPR_Childrens_Court_Detail
    {
        public CPR_Childrens_Court_Detail()
        {
            this.Court_Outcomes = new HashSet<Court_Outcome_Item>();
            this.Section_173_Items = new HashSet<Section_173_Item>();
            this.Statutory_Outcome_Items = new HashSet<Statutory_Outcome_Item>();
            this.Need_for_Care_Reasons = new HashSet<Need_for_Care_Reason_Item>();
        }
    
        public int Childrens_Court_Detail_Id { get; set; }
        public int Incident_Id { get; set; }
        public bool Is_Form4_Issued { get; set; }
        public Nullable<System.DateTime> Date_Form4_Issued { get; set; }
        public Nullable<System.DateTime> Date_Court_Order_Issued { get; set; }
        public Nullable<int> Court_Id { get; set; }
        public string Court_Order_Number { get; set; }
    
        public virtual CPR_Incident CPR_Incident { get; set; }
        public virtual ICollection<Court_Outcome_Item> Court_Outcomes { get; set; }
        public virtual ICollection<Section_173_Item> Section_173_Items { get; set; }
        public virtual ICollection<Statutory_Outcome_Item> Statutory_Outcome_Items { get; set; }
        public virtual ICollection<Need_for_Care_Reason_Item> Need_for_Care_Reasons { get; set; }
        public virtual Court Court { get; set; }
    }
}
