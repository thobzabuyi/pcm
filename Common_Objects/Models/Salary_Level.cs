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
    
    public partial class Salary_Level
    {
        public Salary_Level()
        {
            this.Employees = new HashSet<Employee>();
        }
    
        public int Salary_Level_Id { get; set; }
        public int Salary_Level_Number { get; set; }
        public string Description { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
        public string Source { get; set; }
        public string Definition { get; set; }
    
        public virtual ICollection<Employee> Employees { get; set; }
    }
}