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
    
    public partial class ACM_SocialHistory
    {
        public int SocialHistory_Id { get; set; }
        public int ChildAssessment_Id { get; set; }
        public string FamilyHistory { get; set; }
        public string SupportSystems { get; set; }
        public string RoleChanges { get; set; }
        public string ProblemSolving { get; set; }
        public string EmploymentHistory { get; set; }
        public string EnvironmentalFactors { get; set; }
        public string InterpersonalRelationship { get; set; }
        public string CriticalEvent { get; set; }
        public string CulturalReligionFactors { get; set; }
        public string SocialHabits { get; set; }
    
        public virtual ACM_ChildAssessment ACM_ChildAssessment { get; set; }
    }
}