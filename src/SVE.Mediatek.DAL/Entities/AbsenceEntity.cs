using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Entities
{
    public class AbsenceEntity : EntityBase
    {
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ReasonEntity Reason { get; set; }

        public AbsenceEntity(DateOnly beginDate, DateOnly endDate, ReasonEntity reason) 
        {
            BeginDate = beginDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}
