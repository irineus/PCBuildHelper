using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Models.Entities.Properties;

namespace PCBuildWeb.Data
{
    public class PCBuildWebContext : DbContext
    {
        public PCBuildWebContext (DbContextOptions<PCBuildWebContext> options)
            : base(options)
        {
        }
        
        public DbSet<CPUSeries> CPUSeries { get; set; }
        public DbSet<CPUSocket> CPUSocket { get; set; }
        public DbSet<GPUChipsetSeries> GPUChipsetSeries { get; set; }
        public DbSet<GPUChipset> GPUChipset { get; set; }
        public DbSet<Manufacturer> Manufacturer { get; set; }
        public DbSet<MoboChipset> MoboChipset { get; set; }

    }
}
