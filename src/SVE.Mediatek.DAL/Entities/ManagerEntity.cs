using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Entities
{
    public class ManagerEntity : StaffEntity
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public ManagerEntity(string name, string firsName, string email, string phone, DepartmentEntity department, string login, string password)
        : base(name, firsName, email, phone, department)
        {         
            Login = "pouet"; //TODO Login = mail et passwod = sha2machin
            Password = "password";
        }
    }
}
