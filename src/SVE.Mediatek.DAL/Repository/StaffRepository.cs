using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.Dal;
using SVE.Mediatek.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Repository
{
    /// <summary>
    /// Absence management from the DataBase
    /// </summary>
    public class StaffRepository : Repository<StaffEntity>, IStaffRepository
    {
        /// <summary>
        /// Constructor that initializes the DbContext instance.
        /// </summary>
        /// <param name="context"></param>
        public StaffRepository(MediatekContext context) : base(context)
        {
        }

        public async Task DeleteStaff(int id)
        {
            var staff = await Context.Set<StaffEntity>()
                .Include(a => a.AbsenceList)
                .FirstAsync(s => s.Id == id);

            Context.Set<StaffEntity>().Remove(staff);
            await Context.SaveChangesAsync();
        }
    }
}
