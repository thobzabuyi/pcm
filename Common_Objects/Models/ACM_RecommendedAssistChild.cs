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
    
    public partial class ACM_RecommendedAssistChild
    {
        public int AssistChild_Id { get; set; }
        public int ChildrensCourtReport_Id { get; set; }
        public bool IsTherapeutic { get; set; }
        public string Therapeutic { get; set; }
        public bool IsEducational { get; set; }
        public string Eductational { get; set; }
        public bool IsCultural { get; set; }
        public string Cultural { get; set; }
        public bool IsLinguistic { get; set; }
        public string Linguistic { get; set; }
        public bool IsDevelopmental { get; set; }
        public string Developmental { get; set; }
        public bool IsSocioEconomical { get; set; }
        public string SocioEconomical { get; set; }
        public bool IsSpiritual { get; set; }
        public string Spiritual { get; set; }
        public bool IsOther { get; set; }
        public string Other { get; set; }
    
        public virtual ACM_ChildrensCourtReport ACM_ChildrensCourtReport { get; set; }
    }
}
