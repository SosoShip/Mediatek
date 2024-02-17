using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.Model
{
    public class Staff
    {
        public string Name { get; set; }
        public string FirsName { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public Department Department { get; set; }

        public Staff(string name, string firsName, string email, string phone, Department department)
        {
            Name = name;
            FirsName = firsName;
            Email = email;
            Phone = phone;
            Department = department;
        }
    }
}
