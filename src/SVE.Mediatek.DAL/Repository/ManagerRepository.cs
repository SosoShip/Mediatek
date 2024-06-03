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
    public class ManagerRepository : IManagerRepository<ManagerEntity>
    {

        // DbContext instance for database session
        protected readonly MediatekContext Context;

        // Constructor that initializes the DbContext instance.
        public ManagerRepository(MediatekContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Retrieves a entity manager from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entity manager</returns>
        public async Task<ManagerEntity> GetManager(Department department)
        {
            return await Context.Set<ManagerEntity>().FirstOrDefaultAsync(m => m.Department == department);
        }
    }
}
