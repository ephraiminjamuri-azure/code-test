using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_azure_test.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
        public string Department { get; set; }
        public string SkillSets { get; set; }
        public string Date_of_Birth { get; set; }
        public string Date_of_Joining { get; set; }
        public Boolean IsActive { get; set; }
    }

}
