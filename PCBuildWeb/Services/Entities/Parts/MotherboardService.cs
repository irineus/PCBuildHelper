using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;
using PCBuildWeb.Services.Interfaces;
using PCBuildWeb.Utils.Filters;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class MotherboardService : IBuildPartService<Motherboard>
    {
        private readonly PCBuildWebContext _context;

        public MotherboardService(PCBuildWebContext context)
        {
            _context = context;
        }

        public async Task<List<Motherboard>> FindAllAsync()
        {
            return await _context.Motherboard
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MoboChipset)
                .Include(m => m.Size)
                .Include(m => m.MultiGPUs)
                .ToListAsync();
        }

        public async Task<Motherboard?> FindByIdAsync(int id)
        {
            return await _context.Motherboard
                .Include(m => m.CPUSocket)
                .Include(m => m.Manufacturer)
                .Include(m => m.MoboChipset)
                .Include(m => m.Size)
                .Include(m => m.MultiGPUs)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best Motherboard for the build parameters
        public async Task<Motherboard?> FindBestMotherboard(Build build, double budgetValue, 
            CPUService _cpuService, CPUCoolerService _cpuCoolerService, WC_CPU_BlockService _wcCPUBlockService, 
            CaseService _caseService, MemoryService _memoryService, StorageService _storageService, GPUService _gpuService)
        {
            List<Motherboard> bestMobo = await FindAllAsync();
            bestMobo = bestMobo.Where(c => c.Price <= budgetValue)
                            .Where(c => c.LevelUnlock < build.Parameter.CurrentLevel)
                            .OrderByDescending(c => c.MoboChipset.Name) // higher chipset are better
                            .ThenByDescending(c => c.Size.Id)
                            .ThenByDescending(c => c.M2SlotsSupportingHeatsinks)
                            .ThenByDescending(c => c.M2Slots)
                            .ThenByDescending(c => c.MaxRamSpeed)
                            .ThenByDescending(c => c.Price)
                            .ToList();


            // Filter Motherboard socket by the socket of the selected CPU
            CPU? prerequisiteCPU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Motherboard, PartType.CPU, _cpuService);
            bestMobo = prerequisiteCPU is null ? bestMobo : BuildFilters.SimpleFilter(bestMobo, m => m.CPUSocket  == prerequisiteCPU.CPUSocket).ToList();
            
            // Filter Motherboard socket by the socket of the selected CPUCooler
            CPUCooler? prerequisiteCPUCooler = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Motherboard, PartType.CPUCooler, _cpuCoolerService);
            bestMobo = prerequisiteCPUCooler is null ? bestMobo : BuildFilters.SimpleFilter(bestMobo, m => prerequisiteCPUCooler.CPUSockets.Contains(m.CPUSocket)).ToList();

            // Filter Motherboard socket by the socket of the selected WC CPU Socket
            WC_CPU_Block? prerequisiteWCCPUBlock = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Motherboard, PartType.WC_CPU_Block, _wcCPUBlockService);
            bestMobo = prerequisiteWCCPUBlock is null ? bestMobo : BuildFilters.SimpleFilter(bestMobo, m => prerequisiteWCCPUBlock.CPUSockets.Contains(m.CPUSocket)).ToList();

            // Filter Motherboard form factor by the form factor of the selected Case
            Case? prerequisiteCase = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Motherboard, PartType.Case, _caseService);
            bestMobo = prerequisiteCase is null ? bestMobo : BuildFilters.SimpleFilter(bestMobo, m => prerequisiteCase.MoboSizes.Contains(m.Size)).ToList();

            // Filter Motherboard by the selected Memory Frequency
            Memory? prerequisiteMemory = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Motherboard, PartType.Memory, _memoryService);
            bestMobo = prerequisiteMemory is null ? bestMobo : BuildFilters.SimpleFilter(bestMobo, m => (m.MinRamSpeed <= prerequisiteMemory.Frequency) && (prerequisiteMemory.Frequency <= m.MaxRamSpeed)).ToList();

            // Filter Motherboard by the selected Storage, if it's a M.2
            Storage? prerequisiteStorage = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Motherboard, PartType.Storage, _storageService);
            if (prerequisiteStorage is not null)
            {
                if (prerequisiteStorage.Type == StorageType.M_2)
                {
                    bestMobo = BuildFilters.SimpleFilter(bestMobo, m => (m.M2Slots > 0)).ToList();
                    bestMobo = prerequisiteStorage.IncludesHeatsink ? BuildFilters.SimpleFilter(bestMobo, m => (m.M2SlotsSupportingHeatsinks > 0)).ToList() : bestMobo;
                }
            }

            // Check for Memory Channels support
            bestMobo = BuildFilters.SimpleFilter(bestMobo, m => m.RamSlots >= build.Parameter.MemoryChannels).ToList();

            // Check for Dual GPU support if is required
            if (build.Parameter.MustHaveDualGPU)
            {
                bestMobo = BuildFilters.SimpleFilter(bestMobo, m => m.MultiGPUs.Any()).ToList();
                // Check GPU slot size and get only Motherboards that support it
                GPU? prerequisiteGPU = await BuildFilters.FindPrerequisitePartAsync(build.Components, PartType.Motherboard, PartType.GPU, _gpuService);
                bestMobo = prerequisiteGPU is null ? bestMobo : BuildFilters.SimpleFilter(bestMobo, m => m.DualGPUMaxSlotSize >= prerequisiteGPU.SlotSize).ToList();
            }

            // Check for Manufacturer preference
            bestMobo = BuildFilters.IfAnyFilter(bestMobo, c => c.Manufacturer == build.Parameter.PreferredManufacturer).ToList();

            return bestMobo.FirstOrDefault();
        }

        public bool MotherboardExists(int id)
        {
            return _context.Motherboard.Any(e => e.Id == id);
        }
    }
}
