using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.Model
{
    public class ManagerModel : StaffModel
    {
        public required string Password { get; set; }
        public required string Salt { get; set; }
        public ManagerModel()
        {         
        }
    }
}
