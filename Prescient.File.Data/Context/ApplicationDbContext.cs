using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Prescient.File.Domain.Models;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Prescient.File.Data.Context
{
    public partial class ApplicationDbContext : DbContext
    {
      
        public DbSet<DailyMtm> DailyMTM => Set<DailyMtm>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlServer()
            optionsBuilder.UseSqlServer("Data Source =.;Initial Catalog=DailyMTMDb;User Id=UserID;Password=************;TrustServerCertificate=true;");
        }

        
    }
}
