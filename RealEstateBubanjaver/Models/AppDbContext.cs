using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realestateBubanjaEF.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Estate> Estate { get; set; }
        public DbSet<Type> Type { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server = den1.mssql8.gear.host;Database=realestatedbef;Uid=realestatedbef;Pwd=Ke0kd3r~?hCU;");
        }
    }
}
