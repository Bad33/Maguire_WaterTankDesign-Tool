using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WaterTankTool_WFA.Entity;


namespace WaterTankTool_WFA
{
    public class WaterTankDbContext : DbContext
    {
        public DbSet<SegmentProperties> SegmentProperties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Union Loaner\\WaterTank.db");
        }
    }
}