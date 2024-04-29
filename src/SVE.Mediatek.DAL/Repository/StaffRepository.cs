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
    /// Staff management from the DataBase
    /// </summary>
    public class StaffRepository : Repository<StaffEntity>, IRepository<StaffEntity>
    {
        /// <summary>
        /// Constructor that initializes the DbContext instance.
        /// </summary>
        /// <param name="context"></param>
        public StaffRepository(MediatekContext context) : base(context)       
        { 
        }

        /// <summary>
        /// Retrieves an staff by its id from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StaffEntity> GetStaff(int id)
        {
            return await base.Get(id);
        }

        /// <summary>
        /// Retrieves all staffs from the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StaffEntity>> GetAllStaff()
        {
            return await base.GetAll();
        }

        /// <summary>
        /// Adds a new staff to the database.
        /// </summary>
        /// <param name="staffEntity"></param>
        /// <returns></returns>
        public async Task AddStaff(StaffEntity staffEntity)
        {
            await base.Add(staffEntity);
        }

        /// <summary>
        /// Updates an existing staff in the database
        /// </summary>
        /// <param name="staffEntity"></param>
        /// <returns></returns>
        public async Task UpdateStaff(StaffEntity staffEntity)
        {
            await base.Update(staffEntity); 
        }

        /// <summary>
        /// Deletes a staff from the database by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteStaff(int id)
        {
            await base.Delete(id);
        }
    }
}
