using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Entities
{
    public class StaffEntity : EntityBase
    {
        public string Name { get; set; }
        public string FirsName { get; set; }
        public string Email {  get; set; }
        public string Phone { get; set; }
        public DepartmentEntity Department { get; set; }
        public List<AbsenceEntity> AbsenceList { get; set; }

        public StaffEntity(string name, string firsName, string email, string phone, DepartmentEntity department)
        {
            Name = name;
            FirsName = firsName;
            Email = email;
            Phone = phone;
            Department = department;
            AbsenceList = new List<AbsenceEntity>();
        }
    }
}
