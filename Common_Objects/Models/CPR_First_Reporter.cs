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
    
    public partial class CPR_First_Reporter
    {
        public int CPR_First_Reporter_Id { get; set; }
        public int Incident_Id { get; set; }
        public Nullable<int> Social_Worker_Id { get; set; }
        public Nullable<int> Person_Id { get; set; }
        public Nullable<int> District_Id { get; set; }
        public Nullable<int> Child_Relationship_Type_Id { get; set; }
    
        public virtual Relationship_Type Child_Relationship_Type { get; set; }
        public virtual CPR_Incident CPR_Incident { get; set; }
        public virtual Person Person { get; set; }
        public virtual Social_Worker apl_Social_Worker { get; set; }
    }
}