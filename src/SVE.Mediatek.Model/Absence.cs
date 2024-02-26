using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.Model
{
    public class Absence
    {
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Reason Reason { get; set; }

        public Absence(DateOnly beginDate, DateOnly endDate, Reason reason) 
        {
            BeginDate = beginDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}
