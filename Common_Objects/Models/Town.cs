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
    
    public partial class Town
    {
        public Town()
        {
            this.Addresses = new HashSet<Address>();
            this.SAPS_Stations = new HashSet<SAPS_Station>();
            this.CPR_Unsuitability = new HashSet<CPR_Unsuitability>();
        }
    
        public int Town_Id { get; set; }
        public int Local_Municipality_Id { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<SAPS_Station> SAPS_Stations { get; set; }
        public virtual Local_Municipality Local_Municipality { get; set; }
        public virtual ICollection<CPR_Unsuitability> CPR_Unsuitability { get; set; }
    }
}