using System;
using System.Collections.Generic;
using System.Text;

namespace Module06MVVM.Model
{
    public class EmployeeModel
    {
        public int Id { get; set; }  
        public string ActionCode { get; set; }
        public string Description { get; set; }
        public string ImpactLevel { get; set; }
        public string Category { get; set; }
        public string Frequency { get; set; }
    }
}
