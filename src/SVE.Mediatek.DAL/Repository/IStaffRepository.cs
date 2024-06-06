using SVE.Mediatek.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Repository
{
    public interface IStaffRepository : IRepository<StaffEntity>
    {
        Task DeleteStaff(int id);
    }
}
