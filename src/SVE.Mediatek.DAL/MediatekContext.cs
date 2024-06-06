using Microsoft.EntityFrameworkCore;
using SVE.Mediatek.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVE.Mediatek.Dal
{
    /// <summary>
    /// Interaction with DataBase
    /// </summary>
    public class MediatekContext : DbContext
    {
        public DbSet<StaffEntity> Staffs { get; set; }
        public DbSet<ManagerEntity> Manager { get; set; }
        public DbSet<AbsenceEntity> Absence { get; set; }


        public MediatekContext()
        {
        }

        /// <summary>
        /// The constructor parameter DbContextOptions carries the configuration options for the DbContext, 
        /// here the connection string to connect to the SQL Server database. 
        /// The call to: base(options) in the constructor passes these options to the constructor of the base class 
        /// (DbContext), which uses these options to configure the DbContext.
        /// </summary>
        /// <param name="options"></param>
        public MediatekContext(DbContextOptions<MediatekContext> options) : base(options)
        {
        }

        // Uncomment to use CodeFirst migration. 
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=.\\" +
        //        "SQLEXPRESS;Initial Catalog=Mediatek;Integrated Security=true;TrustServerCertificate=true;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
