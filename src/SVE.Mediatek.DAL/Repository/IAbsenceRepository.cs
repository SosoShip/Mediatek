using SVE.Mediatek.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.DAL.Repository
{
    public interface IAbsenceRepository : IRepository<AbsenceEntity>
    {
        Task AddAbsence(int id, AbsenceEntity entity);
        Task<bool> IsAbsenceDateExistsAsync(DateOnly beginDate, DateOnly endDate);
    }
}
