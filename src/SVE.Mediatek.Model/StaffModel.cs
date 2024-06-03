using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.Model
{
    public class StaffModel : ModelBase
    {
        public required string Name { get; set; }
        public required string FirsName { get; set; }
        public required string Email {  get; set; }
        public required string Phone { get; set; }
        public required DepartmentModel Department { get; set; }
        public List<AbsenceModel> AbsenceList { get; set; } = new List<AbsenceModel>();

        public StaffModel()
        {
        }
    }
}
