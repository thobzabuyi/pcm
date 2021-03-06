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
    
    public partial class CPR_Incident
    {
        public CPR_Incident()
        {
            this.Incident_Abuse_Types = new HashSet<Incident_Abuse_Type>();
            this.Abuse_Indicators = new HashSet<Abuse_Indicator>();
            this.Medical_Detail_Items = new HashSet<Medical_Detail>();
            this.Alleged_Offenders = new HashSet<Alleged_Offender>();
            this.First_Reporters = new HashSet<CPR_First_Reporter>();
            this.SAPS_Detail_Items = new HashSet<CPR_SAPS_Detail>();
            this.Addresses = new HashSet<Address>();
            this.Incident_Monitoring_Items = new HashSet<Incident_Monitoring_Item>();
            this.Actions_Taken = new HashSet<Action_Taken>();
            this.Case_Notes = new HashSet<Case_Note>();
            this.Childrens_Court_Detail_Items = new HashSet<CPR_Childrens_Court_Detail>();
            this.CPR_Incident_Correspondence = new HashSet<Incident_Correspondence>();
            this.Informants = new HashSet<CPR_Informant>();
        }
    
        public int Incident_Id { get; set; }
        public int Intake_Assessment_Id { get; set; }
        public string Reference_Number { get; set; }
        public int Incident_Status_Id { get; set; }
        public Nullable<System.DateTime> Incident_Date { get; set; }
        public Nullable<System.DateTime> Notification_Date { get; set; }
        public Nullable<int> Incident_Location_Id { get; set; }
        public Nullable<int> Incident_Location_Address_Id { get; set; }
        public Nullable<int> Incident_District_Id { get; set; }
        public bool Is_Abuse_Confirmed { get; set; }
        public Nullable<System.DateTime> Abuse_Confirmed_Date { get; set; }
        public Nullable<int> Risk_Indicator_Id { get; set; }
        public string Abuse_Circumstances { get; set; }
        public bool Is_Multiple_Abuse { get; set; }
        public bool Is_Case_Closed { get; set; }
        public Nullable<int> Case_Closure_Reason_Id { get; set; }
        public string Case_Closure_Reason_Other { get; set; }
        public string Case_Closure_Motivation { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public System.DateTime Date_Created { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Date_Last_Modified { get; set; }
        public string Modified_By { get; set; }
        public Nullable<int> Assigned_Social_Worker_Id { get; set; }
    
        public virtual CPR_Incident_Status Incident_Status { get; set; }
        public virtual Incident_Location Incident_Location { get; set; }
        public virtual ICollection<Incident_Abuse_Type> Incident_Abuse_Types { get; set; }
        public virtual ICollection<Abuse_Indicator> Abuse_Indicators { get; set; }
        public virtual Risk_Indicator Risk_Indicator { get; set; }
        public virtual ICollection<Medical_Detail> Medical_Detail_Items { get; set; }
        public virtual ICollection<Alleged_Offender> Alleged_Offenders { get; set; }
        public virtual ICollection<CPR_First_Reporter> First_Reporters { get; set; }
        public virtual ICollection<CPR_SAPS_Detail> SAPS_Detail_Items { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Incident_Monitoring_Item> Incident_Monitoring_Items { get; set; }
        public virtual ICollection<Action_Taken> Actions_Taken { get; set; }
        public virtual ICollection<Case_Note> Case_Notes { get; set; }
        public virtual District District { get; set; }
        public virtual ICollection<CPR_Childrens_Court_Detail> Childrens_Court_Detail_Items { get; set; }
        public virtual ICollection<Incident_Correspondence> CPR_Incident_Correspondence { get; set; }
        public virtual Case_Closure_Reason Case_Closure_Reason { get; set; }
        public virtual Intake_Assessment Intake_Assessment { get; set; }
        public virtual ICollection<CPR_Informant> Informants { get; set; }
        public virtual Social_Worker apl_Social_Worker { get; set; }
    }
}
