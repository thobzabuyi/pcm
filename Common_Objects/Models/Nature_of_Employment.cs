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
    
    public partial class Nature_of_Employment
    {
        public Nature_of_Employment()
        {
            this.int_Person_Employment = new HashSet<Person_Employment>();
        }
    
        public int Nature_of_Employment_Id { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string Definition { get; set; }
    
        public virtual ICollection<Person_Employment> int_Person_Employment { get; set; }
    }
}
