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
    
    public partial class ACM_BoardOfManagement
    {
        public int Id { get; set; }
        public int ManagementInspectionReport_Id { get; set; }
        public string Name { get; set; }
        public string OfficePosition { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string Occupation { get; set; }
    
        public virtual ACM_ManagementInspectionReport ACM_ManagementInspectionReport { get; set; }
    }
}
