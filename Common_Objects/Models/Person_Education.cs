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
    
    public partial class Person_Education
    {
        public int Person_Education_Id { get; set; }
        public int Person_Id { get; set; }
        public int School_Id { get; set; }
        public Nullable<int> Grade_Completed_Id { get; set; }
        public string Year_Completed { get; set; }
        public Nullable<System.DateTime> Date_Last_Attended { get; set; }
        public string Additional_Information { get; set; }
        public System.DateTime Date_Created { get; set; }
        public string Created_By { get; set; }
        public Nullable<System.DateTime> Date_Last_Modified { get; set; }
        public string Modified_By { get; set; }
        public bool Is_Active { get; set; }
        public bool Is_Deleted { get; set; }
    
        public virtual Person Person { get; set; }
        public virtual School School { get; set; }
        public virtual Grade Grade { get; set; }
    }
}
