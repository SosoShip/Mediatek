using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.Model
{
    public class Absence
    {
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public Reason Reason { get; set; }

        public Absence(string beginDate, string endDate, Reason reason) 
        {
            BeginDate = beginDate;
            EndDate = endDate;
            Reason = reason;
        }
    }
}
