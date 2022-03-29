using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Entities.Parts;

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
        public DbSet<CaseFan> CaseFan { get; set; }
        public DbSet<MoboSize> MoboSize { get; set; }
        public DbSet<MultiGPU> MultiGPU { get; set; }
        public DbSet<PowerConnector> PowerConnector { get; set; }
        public DbSet<PSUSize> PSUSize { get; set; }
        public DbSet<Case> Case { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.CPU> CPU { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.CPUCooler> CPUCooler { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.GPU> GPU { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.Memory> Memory { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.Storage> Storage { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.Motherboard> Motherboard { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.PSU> PSU { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.WC_CPU_Block> WC_CPU_Block { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.WC_Radiator> WC_Radiator { get; set; }
        public DbSet<PCBuildWeb.Models.Entities.Parts.WC_Reservoir> WC_Reservoir { get; set; }
    }
}
