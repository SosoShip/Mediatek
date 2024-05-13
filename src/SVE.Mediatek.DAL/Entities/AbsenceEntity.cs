using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Entities
{
    public class AbsenceEntity : EntityBase
    {
        public required DateOnly BeginDate { get; set; }
        public required DateOnly EndDate { get; set; }
        public required Reason Reason { get; set; }

        public AbsenceEntity() 
        {
        }
    }
}
