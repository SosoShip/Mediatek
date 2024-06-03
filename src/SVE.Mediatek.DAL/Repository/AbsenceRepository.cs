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
    public class AbsenceRepository : Repository<AbsenceEntity>, IAbsenceRepository
    {
        /// <summary>
        /// Constructor that initializes the DbContext instance.
        /// </summary>
        /// <param name="context"></param>
        public AbsenceRepository(MediatekContext context) : base(context)
        {
        }

        /// <summary>
        /// Adds a new entity to the database with id of staff.
        /// </summary>
        /// <param name="absenceEntity"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public async Task AddAbsence(int staffId, AbsenceEntity absenceEntity)
        {
            // Find the corresponding staff
            var staff = await Context.Staffs.FindAsync(staffId);
            if (staff == null) throw new Exception("Staff not found");

            // Add staff' absence
            staff.AbsenceList.Add(absenceEntity);

            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Verification that the absence dates have already been saved.
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<bool> IsAbsenceDateExistsAsync(DateOnly beginDate, DateOnly endDate)
        {
            var isBeginDate = await Context.Absence.AnyAsync(a => a.BeginDate == beginDate);
            var isEndDate = await Context.Absence.AnyAsync(b => b.EndDate == endDate);

            if (isBeginDate && isEndDate) return true;
            return false;
        }
    }
}
