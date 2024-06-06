using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.Model
{
    public class AbsenceModel : ModelBase
    {
        public int? StaffEntityId { get; set; }
        public required DateOnly BeginDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required ReasonModel Reason { get; set; }

        public AbsenceModel() 
        {
        }
    }
}
