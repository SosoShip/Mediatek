using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Entities
{
    public class StaffEntity : EntityBase
    {
        public required string Name { get; set; }
        public required string FirsName { get; set; }
        public required string Email {  get; set; }
        public required string Phone { get; set; }
        public required Department Department { get; set; }
        public  List<AbsenceEntity> AbsenceList { get; set; } = new List<AbsenceEntity>();

        public StaffEntity()
        {
        }
    }
}
