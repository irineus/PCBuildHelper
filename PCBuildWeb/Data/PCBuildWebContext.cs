#nullable disable
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
        public PCBuildWebContext(DbContextOptions<PCBuildWebContext> options)
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
        public DbSet<CPU> CPU { get; set; }
        public DbSet<CPUCooler> CPUCooler { get; set; }
        public DbSet<GPU> GPU { get; set; }
        public DbSet<Memory> Memory { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<Motherboard> Motherboard { get; set; }
        public DbSet<PSU> PSU { get; set; }
        public DbSet<WC_CPU_Block> WC_CPU_Block { get; set; }
        public DbSet<WC_Radiator> WC_Radiator { get; set; }
        public DbSet<WC_Reservoir> WC_Reservoir { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Framework: Many to Many Relations

            modelBuilder
                .Entity<Case>()
                .HasMany(e => e.MoboSizes)
                .WithMany(e => e.Cases)
                .UsingEntity<Dictionary<string, object>>(
                    "CaseMoboSize",
                    b => b.HasOne<MoboSize>().WithMany().HasForeignKey("MoboSizesId"),
                    b => b.HasOne<Case>().WithMany().HasForeignKey("CasesId"));

            modelBuilder
                .Entity<Case>()
                .HasMany(e => e.PSUSizes)
                .WithMany(e => e.Cases)
                .UsingEntity<Dictionary<string, object>>(
                    "CasePSUSize",
                    b => b.HasOne<PSUSize>().WithMany().HasForeignKey("PSUSizesId"),
                    b => b.HasOne<Case>().WithMany().HasForeignKey("CasesId"));

            modelBuilder
                .Entity<Case>()
                .HasMany(e => e.CaseFans)
                .WithMany(e => e.Cases)
                .UsingEntity<Dictionary<string, object>>(
                    "CaseIncludedFan",
                    b => b.HasOne<CaseFan>().WithMany().HasForeignKey("CaseFansId"),
                    b => b.HasOne<Case>().WithMany().HasForeignKey("CasesId"));

            modelBuilder
                .Entity<CPUCooler>()
                .HasMany(e => e.CPUSockets)
                .WithMany(e => e.CPUCoolers)
                .UsingEntity<Dictionary<string, object>>(
                    "CPUCoolerSocket",
                    b => b.HasOne<CPUSocket>().WithMany().HasForeignKey("CPUSocketsId"),
                    b => b.HasOne<CPUCooler>().WithMany().HasForeignKey("CPUCoolersId"));

            modelBuilder
                .Entity<GPU>()
                .HasMany(e => e.PowerConnectors)
                .WithMany(e => e.GPUs)
                .UsingEntity<Dictionary<string, object>>(
                    "GPUPowerConnector",
                    b => b.HasOne<PowerConnector>().WithMany().HasForeignKey("PowerConnectorsId"),
                    b => b.HasOne<GPU>().WithMany().HasForeignKey("GPUsId"));

            modelBuilder
                .Entity<Motherboard>()
                .HasMany(e => e.MultiGPUs)
                .WithMany(e => e.Motherboards)
                .UsingEntity<Dictionary<string, object>>(
                    "MotherboardMultiGPU",
                    b => b.HasOne<MultiGPU>().WithMany().HasForeignKey("MultiGPUsId"),
                    b => b.HasOne<Motherboard>().WithMany().HasForeignKey("MotherboardsId"));

            modelBuilder
                .Entity<WC_CPU_Block>()
                .HasMany(e => e.CPUSockets)
                .WithMany(e => e.WC_CPU_Blocks)
                .UsingEntity<Dictionary<string, object>>(
                    "WC_CPU_BlockSocket",
                    b => b.HasOne<CPUSocket>().WithMany().HasForeignKey("CPUSocketsId"),
                    b => b.HasOne<WC_CPU_Block>().WithMany().HasForeignKey("WC_CPU_BlocksId"));

            #endregion
        }

        public DbSet<PCBuildWeb.Models.Entities.Properties.BuildType> BuildType { get; set; }
    }
}
