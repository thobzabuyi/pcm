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
    
    public partial class ACM_EvaluationRecommendationAndConclusion
    {
        public int EvaluationRecommendationAndConclusion_Id { get; set; }
        public string Evaluation { get; set; }
        public string CarePlanShortTerm { get; set; }
        public string CarePlanLongTerm { get; set; }
        public string Conclusion { get; set; }
        public string Recommendation { get; set; }
        public int ExtensionOfAlternativeCareOrder_Id { get; set; }
    
        public virtual ACM_ExtensionOfAlternativeCareOrder ACM_ExtensionOfAlternativeCareOrder { get; set; }
    }
}
