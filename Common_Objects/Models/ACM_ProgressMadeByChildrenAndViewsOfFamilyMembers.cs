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
    
    public partial class ACM_ProgressMadeByChildrenAndViewsOfFamilyMembers
    {
        public int ProgressMadeByChildrenAndViewsOfFamilyMembers_Id { get; set; }
        public string ProgressMadeByTheChildrenSinceThePlacement { get; set; }
        public string ReasonsForTheRemovalOfTheChildrenStillExist { get; set; }
        public string ContactBetweenCaregiverParentsOrFamilyMemberAndTheConcernedChildren { get; set; }
        public string EducatorECDPractionerImpressionsOfTheChildrenProgressAndAdjustment { get; set; }
        public string ViewsOfTheParentFamilyMember { get; set; }
        public int ExtensionOfAlternativeCareOrder_Id { get; set; }
    
        public virtual ACM_ExtensionOfAlternativeCareOrder ACM_ExtensionOfAlternativeCareOrder { get; set; }
    }
}
