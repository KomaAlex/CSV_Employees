//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSV_Employees.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Employees
    {
        public int Id { get; set; }
        public string Payroll_number { get; set; }
        public string Forenames { get; set; }
        public string Surname { get; set; }
        public Nullable<System.DateTime> Date_of_Birth { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string Address_2 { get; set; }
        public string Postcode { get; set; }
        public string EMail_Home { get; set; }
        public Nullable<System.DateTime> Start_Date { get; set; }
    }
}
