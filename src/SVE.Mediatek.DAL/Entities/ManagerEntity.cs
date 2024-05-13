using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Entities
{
    public class ManagerEntity : StaffEntity
    {
        public required string Password { get; set; }

        public ManagerEntity()
        {         
           // Password = "password";//TODO Login = mail et passwod = sha2machin
        }
    }
}
