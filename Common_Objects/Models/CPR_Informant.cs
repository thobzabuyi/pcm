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
    
    public partial class CPR_Informant
    {
        public int CPR_Informant_Id { get; set; }
        public int Incident_Id { get; set; }
        public int Person_Id { get; set; }
        public Nullable<int> District_Id { get; set; }
        public Nullable<int> Informant_Capacity_Type_Id { get; set; }
        public Nullable<int> Relationship_Type_Id { get; set; }
    
        public virtual Informant_Capacity_Type apl_Informant_Capacity_Type { get; set; }
        public virtual CPR_Incident CPR_Incident { get; set; }
        public virtual Person Person { get; set; }
    }
}