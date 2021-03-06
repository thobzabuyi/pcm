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
    
    public partial class NISIS_Site
    {
        public NISIS_Site()
        {
            this.NISIS_Site_EAs = new HashSet<NISIS_Site_EA>();
            this.NISIS_Profiling_Tools = new HashSet<Profiling_Tool>();
            this.NISIS_Profiling_Methods = new HashSet<NISIS_Profiling_Method>();
            this.NISIS_Grouping_Flags = new HashSet<NISIS_Grouping_Flag>();
            this.Registered_Programmes = new HashSet<NISIS_Programme>();
        }
    
        public int Site_Id { get; set; }
        public int NISIS_Ward_Id { get; set; }
        public string Site_Name { get; set; }
        public string Site_Alternative_Name { get; set; }
        public string GPS_Coordinates_Lat { get; set; }
        public string GPS_Coordinates_Long { get; set; }
        public Nullable<int> Registered_Programme_Id { get; set; }
        public Nullable<int> Registered_Programme_Status_Id { get; set; }
        public string Responsible_Organization { get; set; }
        public string Prioritization_Group { get; set; }
        public Nullable<System.DateTime> Target_Start_Date { get; set; }
        public Nullable<System.DateTime> Target_End_Date { get; set; }
        public string Primary_Contact { get; set; }
        public Nullable<int> Listing_Method_Id { get; set; }
        public Nullable<int> Responsible_Programme_Id { get; set; }
        public Nullable<int> Estimated_Population { get; set; }
        public string Source_of_Population_Estimate { get; set; }
        public Nullable<decimal> Budget_Committed { get; set; }
        public int QA_Status_Item_Id { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public string Created_By { get; set; }
        public System.DateTime Created_Date { get; set; }
        public string Modified_By { get; set; }
        public Nullable<System.DateTime> Modified_Date { get; set; }
    
        public virtual NISIS_Ward Ward { get; set; }
        public virtual NISIS_Programme_Status Registered_Programme_Status { get; set; }
        public virtual NISIS_Listing_Method Listing_Method { get; set; }
        public virtual NISIS_Programme Responsible_Programme { get; set; }
        public virtual ICollection<NISIS_Site_EA> NISIS_Site_EAs { get; set; }
        public virtual ICollection<Profiling_Tool> NISIS_Profiling_Tools { get; set; }
        public virtual ICollection<NISIS_Profiling_Method> NISIS_Profiling_Methods { get; set; }
        public virtual ICollection<NISIS_Grouping_Flag> NISIS_Grouping_Flags { get; set; }
        public virtual NISIS_QA_Status_Item NISIS_QA_Status_Item { get; set; }
        public virtual ICollection<NISIS_Programme> Registered_Programmes { get; set; }
    }
}
