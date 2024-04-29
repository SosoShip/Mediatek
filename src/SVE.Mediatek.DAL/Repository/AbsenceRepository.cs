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
    public class AbsenceRepository : Repository<AbsenceEntity>, IRepository<AbsenceEntity>
    {
        /// <summary>
        /// Constructor that initializes the DbContext instance.
        /// </summary>
        /// <param name="context"></param>
        public AbsenceRepository(MediatekContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves an absence by its id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<AbsenceEntity> GetAbsence(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Retrieves all absences from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<AbsenceEntity>> GetAllAbsence()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Adds a new absence to the database.
        /// </summary>
        /// <param name="abscenceEntity"></param>
        /// <returns></returns>
        public async Task AddAbsence(AbsenceEntity abscenceEntity)
        {
            await base.Add(abscenceEntity);
        }

        
        /// <summary>
        /// Updates an existing absence in the database
        /// </summary>
        /// <param name="abscenceEntity"></param>
        /// <returns></returns>
        public async Task UpdateAbsence(AbsenceEntity abscenceEntity)
        {
            await base.Update(abscenceEntity);
        }

        /// <summary>
        /// Deletes a absence from the database by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAbsence(int id)
        {
            await base.Delete(id);
        }
    }
}
