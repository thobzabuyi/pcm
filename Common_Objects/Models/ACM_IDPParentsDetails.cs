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
    
    public partial class ACM_IDPParentsDetails
    {
        public int ParentDetails_Id { get; set; }
        public int IndividualDevelopmentPlan_Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<int> RelationshipType_Id { get; set; }
        public string ResidentialAddress { get; set; }
        public string WorkAddress { get; set; }
        public string OfficeFhone { get; set; }
        public string Cellular { get; set; }
        public string Email { get; set; }
        public string DurationOfInvolvement { get; set; }
    
        public virtual ACM_IndividualDevelopmentPlan ACM_IndividualDevelopmentPlan { get; set; }
    }
}