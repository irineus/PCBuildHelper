using Microsoft.EntityFrameworkCore;
using PCBuildWeb.Data;
using PCBuildWeb.Models.Building;
using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Entities.Parts
{
    public class CaseService
    {
        private readonly PCBuildWebContext _context;
        private readonly CPUCoolerService _cpuCoolerService;
        private readonly GPUService _gpuService;
        private readonly MotherboardService _motherboardSerice;
        private readonly PSUService _psuService;
        private readonly WC_RadiatorService _wcRadiatorSerice;

        public CaseService(PCBuildWebContext context, CPUCoolerService cpuCoolerService,
            GPUService gpuService, MotherboardService motherboardSerice, PSUService psuService,
            WC_RadiatorService wcRadiatorSerice)
        {
            _context = context;
            _cpuCoolerService = cpuCoolerService;
            _gpuService = gpuService;
            _motherboardSerice = motherboardSerice;
            _psuService = psuService;
            _wcRadiatorSerice = wcRadiatorSerice;
        }

        public async Task<List<Case>> FindAllAsync()
        {
            return await _context.Case
                .Include(c => c.Manufacturer)
                .Include(c => c.SupportedMoboSizes)
                .Include(c => c.SupportedPSUSizes)
                .ToListAsync();
        }

        public async Task<Case?> FindByIdAsync(int id)
        {
            return await _context.Case
                .Include(c => c.Manufacturer)
                .Include(c => c.SupportedMoboSizes)
                .Include(c => c.SupportedPSUSizes)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        //Find best Case for the build parameters
        public async Task<Case?> FindBestCase(Build build, Component component)
        {
            List<Case> bestCase = await FindAllAsync();
            bestCase = bestCase
                .Where(c => c.Price <= component.BudgetValue)
                .Where(c => c.LevelUnlock <= build.CurrentLevel)
                .Where(c => c.LevelPercent <= build.CurrentLevelPercent)
                .OrderByDescending(c => c.Price)
                .ToList();

            // Check for Manufator preference
            if (build.PreferredManufacturer != null)
            {
                if (bestCase.Where(c => c.Manufacturer == build.PreferredManufacturer).Any())
                {
                    bestCase = bestCase
                        .Where(c => c.Manufacturer == build.PreferredManufacturer)
                        .OrderByDescending(c => c.Price)
                        .ToList();
                }
            }

            // Check mobo size
            Component? preRequisiteComponent = build.Components.Where(c => c.Type == PartType.Motherboard).FirstOrDefault();
            if (preRequisiteComponent != null)
            {
                ComputerPart? preRequisiteComputerPart = null;
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                if (preRequisiteComputerPart != null)
                {
                    Motherboard? selectedMobo = await _motherboardSerice.FindByIdAsync(preRequisiteComputerPart.Id);
                    if (selectedMobo != null)
                    {
                        bestCase = bestCase
                            .Where(c => c.SupportedMoboSizes.Contains(selectedMobo.Size))
                            .OrderByDescending(c => c.Price)
                            .ToList();
                    }
                }
            }            

            // Check CPUCooler specs against case
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.CPUCooler).FirstOrDefault();
            if (preRequisiteComponent != null)
            {
                ComputerPart? preRequisiteComputerPart = null;
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                if (preRequisiteComputerPart != null)
                {
                    CPUCooler? selectedCPUCooler = await _cpuCoolerService.FindByIdAsync(preRequisiteComputerPart.Id);
                    if (selectedCPUCooler != null)
                    {
                        if (selectedCPUCooler.WaterCooler)
                        {
                            // Watercooler should check for radiator slots
                            int? radiatorFanSize = selectedCPUCooler.RadiatorSize / selectedCPUCooler.RadiatorSlots;
                            if (radiatorFanSize == 120)
                            {
                                bestCase = bestCase
                                    .Where(c => c.Number120mmSlots >= selectedCPUCooler.RadiatorSlots)
                                    .OrderByDescending(c => c.Price)
                                    .ToList();
                            }
                            if (radiatorFanSize == 140)
                            {
                                bestCase = bestCase
                                    .Where(c => c.Number140mmSlots >= selectedCPUCooler.RadiatorSlots)
                                    .OrderByDescending(c => c.Price)
                                    .ToList();
                            }
                        }
                        // Always check for height
                        bestCase = bestCase
                            .Where(c => c.MaxCPUFanHeight > selectedCPUCooler.Height)
                            .OrderByDescending(c => c.Price)
                            .ToList();
                    }
                }
            }            

            // Check WC Radiator specs against case
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.WC_Radiator).FirstOrDefault();
            if (preRequisiteComponent != null)
            {
                ComputerPart? preRequisiteComputerPart = null;
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                if (preRequisiteComputerPart != null)
                {
                    WC_Radiator? selectedWC_Radiator = await _wcRadiatorSerice.FindByIdAsync(preRequisiteComputerPart.Id);
                    if (selectedWC_Radiator != null)
                    {
                        int? radiatorFanSize = selectedWC_Radiator.RadiatorSize / selectedWC_Radiator.RadiatorSlots;
                        if (radiatorFanSize == 120)
                        {
                            bestCase = bestCase
                                .Where(c => c.Number120mmSlots >= selectedWC_Radiator.RadiatorSlots)
                                .OrderByDescending(c => c.Price)
                                .ToList();
                        }
                        if (radiatorFanSize == 140)
                        {
                            bestCase = bestCase
                                .Where(c => c.Number140mmSlots >= selectedWC_Radiator.RadiatorSlots)
                                .OrderByDescending(c => c.Price)
                                .ToList();
                        } 
                    }
                }
            }            

            // Check PSU Length and FormFactor
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.PSU).FirstOrDefault();
            if (preRequisiteComponent != null)
            {
                ComputerPart? preRequisiteComputerPart = null;
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                if (preRequisiteComputerPart != null)
                {
                    PSU? selectedPSU = await _psuService.FindByIdAsync(preRequisiteComputerPart.Id);
                    if (selectedPSU != null)
                    {
                        bestCase = bestCase
                            .Where(c => c.SupportedPSUSizes.Contains(selectedPSU.PSUSize))
                            .Where(c => c.MaxPsuLength > selectedPSU.Length)
                            .OrderByDescending(c => c.Price)
                            .ToList(); 
                    }
                }
            }            

            // Check GPU Length
            preRequisiteComponent = build.Components.Where(c => c.Type == PartType.GPU).FirstOrDefault();
            if (preRequisiteComponent != null)
            {
                ComputerPart? preRequisiteComputerPart = null;
                preRequisiteComputerPart = preRequisiteComponent.BuildPart;
                if (preRequisiteComputerPart != null)
                {
                    GPU? selectedGPU = await _gpuService.FindByIdAsync(preRequisiteComputerPart.Id);
                    if (selectedGPU != null)
                    {
                        bestCase = bestCase
                            .Where(c => c.MaxGPULength > selectedGPU.Length)
                            .OrderByDescending(c => c.Price)
                            .ToList();
                    }
                }
            }            

            return bestCase.FirstOrDefault();
        }

        public bool CaseExists(int id)
        {
            return _context.Case.Any(e => e.Id == id);
        }
    }
}
