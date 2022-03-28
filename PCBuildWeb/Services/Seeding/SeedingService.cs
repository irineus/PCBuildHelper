using PCBuildWeb.Data;
using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;

namespace PCBuildWeb.Services.Seeding
{
    public class SeedingService
    {
        private readonly PCBuildWebContext _context;

        public SeedingService(PCBuildWebContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            #region CPUSeries Seed
            List<CPUSeries> cpuSeriesList = new List<CPUSeries>();
            CPUSeries AMD_Ryzen_3 = new CPUSeries() { Id = 1, Name = "AMD Ryzen 3" }; cpuSeriesList.Add(AMD_Ryzen_3);
            CPUSeries AMD_Ryzen_5 = new CPUSeries() { Id = 2, Name = "AMD Ryzen 5" }; cpuSeriesList.Add(AMD_Ryzen_5);
            CPUSeries AMD_Ryzen_7 = new CPUSeries() { Id = 3, Name = "AMD Ryzen 7" }; cpuSeriesList.Add(AMD_Ryzen_7);
            CPUSeries AMD_Ryzen_9 = new CPUSeries() { Id = 4, Name = "AMD Ryzen 9" }; cpuSeriesList.Add(AMD_Ryzen_9);
            CPUSeries AMD_Ryzen_Threadripper = new CPUSeries() { Id = 5, Name = "AMD Ryzen Threadripper" }; cpuSeriesList.Add(AMD_Ryzen_Threadripper);
            CPUSeries Intel_Celeron = new CPUSeries() { Id = 6, Name = "Intel Celeron" }; cpuSeriesList.Add(Intel_Celeron);
            CPUSeries Intel_Core_i3 = new CPUSeries() { Id = 7, Name = "Intel Core i3" }; cpuSeriesList.Add(Intel_Core_i3);
            CPUSeries Intel_Core_i5 = new CPUSeries() { Id = 8, Name = "Intel Core i5" }; cpuSeriesList.Add(Intel_Core_i5);
            CPUSeries Intel_Core_i7 = new CPUSeries() { Id = 9, Name = "Intel Core i7" }; cpuSeriesList.Add(Intel_Core_i7);
            CPUSeries Intel_Core_i9 = new CPUSeries() { Id = 10, Name = "Intel Core i9" }; cpuSeriesList.Add(Intel_Core_i9);
            CPUSeries Intel_Pentium = new CPUSeries() { Id = 11, Name = "Intel Pentium" }; cpuSeriesList.Add(Intel_Pentium);
            #endregion
            if (!_context.CPUSeries.Any())
            {
                _context.CPUSeries.AddRange(cpuSeriesList);
            }
            #region CPUSocket Seed
            List<CPUSocket> cpuSockets = new List<CPUSocket>();
            CPUSocket AM4 = new CPUSocket() { Id = 1, Name = "AM4" }; cpuSockets.Add(AM4);
            CPUSocket TR4 = new CPUSocket() { Id = 2, Name = "TR4" }; cpuSockets.Add(TR4);
            CPUSocket sTRX4 = new CPUSocket() { Id = 3, Name = "sTRX4" }; cpuSockets.Add(sTRX4);
            CPUSocket LGA_1151__Skylake_ = new CPUSocket() { Id = 4, Name = "LGA 1151 (Skylake)" }; cpuSockets.Add(LGA_1151__Skylake_);
            CPUSocket LGA_1151__Kaby_Lake_ = new CPUSocket() { Id = 5, Name = "LGA 1151 (Kaby Lake)" }; cpuSockets.Add(LGA_1151__Kaby_Lake_);
            CPUSocket LGA_1200 = new CPUSocket() { Id = 6, Name = "LGA 1200" }; cpuSockets.Add(LGA_1200);
            CPUSocket LGA_1151__Coffee_Lake_ = new CPUSocket() { Id = 7, Name = "LGA 1151 (Coffee Lake)" }; cpuSockets.Add(LGA_1151__Coffee_Lake_);
            CPUSocket LGA_2066 = new CPUSocket() { Id = 8, Name = "LGA 2066" }; cpuSockets.Add(LGA_2066);
            #endregion
            if (!_context.CPUSocket.Any())
            {
                _context.CPUSocket.AddRange(cpuSockets);
            }
            #region GPUChipsetSeries Seed
            List<GPUChipsetSeries> gpuChipsetSeriesList = new List<GPUChipsetSeries>();
            GPUChipsetSeries GTX_700 = new GPUChipsetSeries() { Id = 1, Name = "GTX 700" }; gpuChipsetSeriesList.Add(GTX_700);
            GPUChipsetSeries GTX_900 = new GPUChipsetSeries() { Id = 2, Name = "GTX 900" }; gpuChipsetSeriesList.Add(GTX_900);
            GPUChipsetSeries GTX_1000 = new GPUChipsetSeries() { Id = 3, Name = "GTX 1000" }; gpuChipsetSeriesList.Add(GTX_1000);
            GPUChipsetSeries GTX_1600 = new GPUChipsetSeries() { Id = 4, Name = "GTX 1600" }; gpuChipsetSeriesList.Add(GTX_1600);
            GPUChipsetSeries RTX_2000 = new GPUChipsetSeries() { Id = 5, Name = "RTX 2000" }; gpuChipsetSeriesList.Add(RTX_2000);
            GPUChipsetSeries RTX_3000 = new GPUChipsetSeries() { Id = 6, Name = "RTX 3000" }; gpuChipsetSeriesList.Add(RTX_3000);
            GPUChipsetSeries R5_R7_R9_200 = new GPUChipsetSeries() { Id = 7, Name = "R5/R7/R9 200" }; gpuChipsetSeriesList.Add(R5_R7_R9_200);
            GPUChipsetSeries R5_R7_R9_300 = new GPUChipsetSeries() { Id = 8, Name = "R5/R7/R9 300" }; gpuChipsetSeriesList.Add(R5_R7_R9_300);
            GPUChipsetSeries RX_400 = new GPUChipsetSeries() { Id = 9, Name = "RX 400" }; gpuChipsetSeriesList.Add(RX_400);
            GPUChipsetSeries RX_500 = new GPUChipsetSeries() { Id = 10, Name = "RX 500" }; gpuChipsetSeriesList.Add(RX_500);
            GPUChipsetSeries RX_Vega = new GPUChipsetSeries() { Id = 11, Name = "RX Vega" }; gpuChipsetSeriesList.Add(RX_Vega);
            GPUChipsetSeries VII = new GPUChipsetSeries() { Id = 12, Name = "VII" }; gpuChipsetSeriesList.Add(VII);
            GPUChipsetSeries RX_5000 = new GPUChipsetSeries() { Id = 13, Name = "RX 5000" }; gpuChipsetSeriesList.Add(RX_5000);
            GPUChipsetSeries RX_6000 = new GPUChipsetSeries() { Id = 14, Name = "RX 6000" }; gpuChipsetSeriesList.Add(RX_6000);
            #endregion
            if (!_context.GPUChipsetSeries.Any())
            {
                _context.GPUChipsetSeries.AddRange(gpuChipsetSeriesList);
            }
            #region GPUChipset Seed
            List<GPUChipset> gpuChipsets = new List<GPUChipset>();
            GPUChipset GeForce_GTX_750_Ti = new GPUChipset() { Id = 1, Name = "GeForce GTX 750 Ti", ChipsetSeries = GTX_700 }; gpuChipsets.Add(GeForce_GTX_750_Ti);
            GPUChipset GeForce_GTX_950 = new GPUChipset() { Id = 2, Name = "GeForce GTX 950", ChipsetSeries = GTX_900 }; gpuChipsets.Add(GeForce_GTX_950);
            GPUChipset GeForce_GTX_960 = new GPUChipset() { Id = 3, Name = "GeForce GTX 960", ChipsetSeries = GTX_900 }; gpuChipsets.Add(GeForce_GTX_960);
            GPUChipset GeForce_GTX_970 = new GPUChipset() { Id = 4, Name = "GeForce GTX 970", ChipsetSeries = GTX_900 }; gpuChipsets.Add(GeForce_GTX_970);
            GPUChipset GeForce_GTX_980 = new GPUChipset() { Id = 5, Name = "GeForce GTX 980", ChipsetSeries = GTX_900 }; gpuChipsets.Add(GeForce_GTX_980);
            GPUChipset GeForce_GTX_980_Ti = new GPUChipset() { Id = 6, Name = "GeForce GTX 980 Ti", ChipsetSeries = GTX_900 }; gpuChipsets.Add(GeForce_GTX_980_Ti);
            GPUChipset GeForce_GT_1030 = new GPUChipset() { Id = 7, Name = "GeForce GT 1030", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GT_1030);
            GPUChipset GeForce_GTX_1050 = new GPUChipset() { Id = 8, Name = "GeForce GTX 1050", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GTX_1050);
            GPUChipset GeForce_GTX_1050_Ti = new GPUChipset() { Id = 9, Name = "GeForce GTX 1050 Ti", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GTX_1050_Ti);
            GPUChipset GeForce_GTX_1060 = new GPUChipset() { Id = 10, Name = "GeForce GTX 1060", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GTX_1060);
            GPUChipset GeForce_GTX_1070 = new GPUChipset() { Id = 11, Name = "GeForce GTX 1070", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GTX_1070);
            GPUChipset GeForce_GTX_1070_Ti = new GPUChipset() { Id = 12, Name = "GeForce GTX 1070 Ti", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GTX_1070_Ti);
            GPUChipset GeForce_GTX_1080 = new GPUChipset() { Id = 13, Name = "GeForce GTX 1080", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GTX_1080);
            GPUChipset GeForce_GTX_1080_Ti = new GPUChipset() { Id = 14, Name = "GeForce GTX 1080 Ti", ChipsetSeries = GTX_1000 }; gpuChipsets.Add(GeForce_GTX_1080_Ti);
            GPUChipset GeForce_GTX_1650 = new GPUChipset() { Id = 15, Name = "GeForce GTX 1650", ChipsetSeries = GTX_1600 }; gpuChipsets.Add(GeForce_GTX_1650);
            GPUChipset GeForce_GTX_1660_Super = new GPUChipset() { Id = 16, Name = "GeForce GTX 1660 Super", ChipsetSeries = GTX_1600 }; gpuChipsets.Add(GeForce_GTX_1660_Super);
            GPUChipset GeForce_GTX_1660_Ti = new GPUChipset() { Id = 17, Name = "GeForce GTX 1660 Ti", ChipsetSeries = GTX_1600 }; gpuChipsets.Add(GeForce_GTX_1660_Ti);
            GPUChipset Geforce_RTX_2060 = new GPUChipset() { Id = 18, Name = "Geforce RTX 2060", ChipsetSeries = RTX_2000 }; gpuChipsets.Add(Geforce_RTX_2060);
            GPUChipset GeForce_RTX_2060_Super = new GPUChipset() { Id = 19, Name = "GeForce RTX 2060 Super", ChipsetSeries = RTX_2000 }; gpuChipsets.Add(GeForce_RTX_2060_Super);
            GPUChipset Geforce_RTX_2070 = new GPUChipset() { Id = 20, Name = "Geforce RTX 2070", ChipsetSeries = RTX_2000 }; gpuChipsets.Add(Geforce_RTX_2070);
            GPUChipset GeForce_RTX_2070_Super = new GPUChipset() { Id = 21, Name = "GeForce RTX 2070 Super", ChipsetSeries = RTX_2000 }; gpuChipsets.Add(GeForce_RTX_2070_Super);
            GPUChipset GeForce_RTX_2080 = new GPUChipset() { Id = 22, Name = "GeForce RTX 2080", ChipsetSeries = RTX_2000 }; gpuChipsets.Add(GeForce_RTX_2080);
            GPUChipset GeForce_RTX_2080_Super = new GPUChipset() { Id = 23, Name = "GeForce RTX 2080 Super", ChipsetSeries = RTX_2000 }; gpuChipsets.Add(GeForce_RTX_2080_Super);
            GPUChipset GeForce_RTX_2080_Ti = new GPUChipset() { Id = 24, Name = "GeForce RTX 2080 Ti", ChipsetSeries = RTX_2000 }; gpuChipsets.Add(GeForce_RTX_2080_Ti);
            GPUChipset GeForce_RTX_3060 = new GPUChipset() { Id = 25, Name = "GeForce RTX 3060", ChipsetSeries = RTX_3000 }; gpuChipsets.Add(GeForce_RTX_3060);
            GPUChipset GeForce_RTX_3060_Ti = new GPUChipset() { Id = 26, Name = "GeForce RTX 3060 Ti", ChipsetSeries = RTX_3000 }; gpuChipsets.Add(GeForce_RTX_3060_Ti);
            GPUChipset GeForce_RTX_3070 = new GPUChipset() { Id = 27, Name = "GeForce RTX 3070", ChipsetSeries = RTX_3000 }; gpuChipsets.Add(GeForce_RTX_3070);
            GPUChipset GeForce_RTX_3070_Ti = new GPUChipset() { Id = 28, Name = "GeForce RTX 3070 Ti", ChipsetSeries = RTX_3000 }; gpuChipsets.Add(GeForce_RTX_3070_Ti);
            GPUChipset GeForce_RTX_3080 = new GPUChipset() { Id = 29, Name = "GeForce RTX 3080", ChipsetSeries = RTX_3000 }; gpuChipsets.Add(GeForce_RTX_3080);
            GPUChipset GeForce_RTX_3080_Ti = new GPUChipset() { Id = 30, Name = "GeForce RTX 3080 Ti", ChipsetSeries = RTX_3000 }; gpuChipsets.Add(GeForce_RTX_3080_Ti);
            GPUChipset GeForce_RTX_3090 = new GPUChipset() { Id = 31, Name = "GeForce RTX 3090", ChipsetSeries = RTX_3000 }; gpuChipsets.Add(GeForce_RTX_3090);
            GPUChipset Radeon_R9_280 = new GPUChipset() { Id = 32, Name = "Radeon R9 280", ChipsetSeries = R5_R7_R9_200 }; gpuChipsets.Add(Radeon_R9_280);
            GPUChipset Radeon_R9_290 = new GPUChipset() { Id = 33, Name = "Radeon R9 290", ChipsetSeries = R5_R7_R9_200 }; gpuChipsets.Add(Radeon_R9_290);
            GPUChipset Radeon_R9_370 = new GPUChipset() { Id = 34, Name = "Radeon R9 370", ChipsetSeries = R5_R7_R9_300 }; gpuChipsets.Add(Radeon_R9_370);
            GPUChipset Radeon_R9_380 = new GPUChipset() { Id = 35, Name = "Radeon R9 380", ChipsetSeries = R5_R7_R9_300 }; gpuChipsets.Add(Radeon_R9_380);
            GPUChipset Radeon_R9_390 = new GPUChipset() { Id = 36, Name = "Radeon R9 390", ChipsetSeries = R5_R7_R9_300 }; gpuChipsets.Add(Radeon_R9_390);
            GPUChipset Radeon_R9_390X = new GPUChipset() { Id = 37, Name = "Radeon R9 390X", ChipsetSeries = R5_R7_R9_300 }; gpuChipsets.Add(Radeon_R9_390X);
            GPUChipset Radeon_RX_470 = new GPUChipset() { Id = 38, Name = "Radeon RX 470", ChipsetSeries = RX_400 }; gpuChipsets.Add(Radeon_RX_470);
            GPUChipset Radeon_RX_480 = new GPUChipset() { Id = 39, Name = "Radeon RX 480", ChipsetSeries = RX_400 }; gpuChipsets.Add(Radeon_RX_480);
            GPUChipset Radeon_RX560 = new GPUChipset() { Id = 40, Name = "Radeon RX560", ChipsetSeries = RX_500 }; gpuChipsets.Add(Radeon_RX560);
            GPUChipset Radeon_RX570 = new GPUChipset() { Id = 41, Name = "Radeon RX570", ChipsetSeries = RX_500 }; gpuChipsets.Add(Radeon_RX570);
            GPUChipset Radeon_RX580 = new GPUChipset() { Id = 42, Name = "Radeon RX580", ChipsetSeries = RX_500 }; gpuChipsets.Add(Radeon_RX580);
            GPUChipset Radeon_RX590 = new GPUChipset() { Id = 43, Name = "Radeon RX590", ChipsetSeries = RX_500 }; gpuChipsets.Add(Radeon_RX590);
            GPUChipset Radeon_RX_VEGA_56 = new GPUChipset() { Id = 44, Name = "Radeon RX VEGA 56", ChipsetSeries = RX_Vega }; gpuChipsets.Add(Radeon_RX_VEGA_56);
            GPUChipset Radeon_RX_VEGA_64 = new GPUChipset() { Id = 45, Name = "Radeon RX VEGA 64", ChipsetSeries = RX_Vega }; gpuChipsets.Add(Radeon_RX_VEGA_64);
            GPUChipset Radeon_VII = new GPUChipset() { Id = 46, Name = "Radeon VII", ChipsetSeries = VII }; gpuChipsets.Add(Radeon_VII);
            GPUChipset Radeon_RX_5500_XT = new GPUChipset() { Id = 47, Name = "Radeon RX 5500 XT", ChipsetSeries = RX_5000 }; gpuChipsets.Add(Radeon_RX_5500_XT);
            GPUChipset Radeon_RX_5600_XT = new GPUChipset() { Id = 48, Name = "Radeon RX 5600 XT", ChipsetSeries = RX_5000 }; gpuChipsets.Add(Radeon_RX_5600_XT);
            GPUChipset Radeon_RX5700 = new GPUChipset() { Id = 49, Name = "Radeon RX5700", ChipsetSeries = RX_5000 }; gpuChipsets.Add(Radeon_RX5700);
            GPUChipset Radeon_RX5700_XT = new GPUChipset() { Id = 50, Name = "Radeon RX5700 XT", ChipsetSeries = RX_5000 }; gpuChipsets.Add(Radeon_RX5700_XT);
            GPUChipset Radeon_RX_6700_XT = new GPUChipset() { Id = 51, Name = "Radeon RX 6700 XT", ChipsetSeries = RX_6000 }; gpuChipsets.Add(Radeon_RX_6700_XT);
            GPUChipset Radeon_RX_6800 = new GPUChipset() { Id = 52, Name = "Radeon RX 6800", ChipsetSeries = RX_6000 }; gpuChipsets.Add(Radeon_RX_6800);
            GPUChipset Radeon_RX_6800_XT = new GPUChipset() { Id = 53, Name = "Radeon RX 6800 XT", ChipsetSeries = RX_6000 }; gpuChipsets.Add(Radeon_RX_6800_XT);
            GPUChipset Radeon_RX_6900_XT = new GPUChipset() { Id = 54, Name = "Radeon RX 6900 XT", ChipsetSeries = RX_6000 }; gpuChipsets.Add(Radeon_RX_6900_XT);
            #endregion
            if (!_context.GPUChipset.Any())
            {
                _context.GPUChipset.AddRange(gpuChipsets);
            }
            #region Manufacturers Seed
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            Manufacturer Acer = new Manufacturer() { Id = 1, Name = "Acer" }; manufacturers.Add(Acer);
            Manufacturer ADATA = new Manufacturer() { Id = 2, Name = "ADATA" }; manufacturers.Add(ADATA);
            Manufacturer Alphacool = new Manufacturer() { Id = 3, Name = "Alphacool" }; manufacturers.Add(Alphacool);
            Manufacturer AMD = new Manufacturer() { Id = 4, Name = "AMD" }; manufacturers.Add(AMD);
            Manufacturer Antec = new Manufacturer() { Id = 5, Name = "Antec" }; manufacturers.Add(Antec);
            Manufacturer ARCTIC = new Manufacturer() { Id = 6, Name = "ARCTIC" }; manufacturers.Add(ARCTIC);
            Manufacturer ASRock = new Manufacturer() { Id = 7, Name = "ASRock" }; manufacturers.Add(ASRock);
            Manufacturer ASUS = new Manufacturer() { Id = 8, Name = "ASUS" }; manufacturers.Add(ASUS);
            Manufacturer be_quiet_ = new Manufacturer() { Id = 9, Name = "be quiet!" }; manufacturers.Add(be_quiet_);
            Manufacturer BenQ = new Manufacturer() { Id = 10, Name = "BenQ" }; manufacturers.Add(BenQ);
            Manufacturer Colorful = new Manufacturer() { Id = 11, Name = "Colorful" }; manufacturers.Add(Colorful);
            Manufacturer Cooler_Master = new Manufacturer() { Id = 12, Name = "Cooler Master" }; manufacturers.Add(Cooler_Master);
            Manufacturer CORSAIR = new Manufacturer() { Id = 13, Name = "CORSAIR" }; manufacturers.Add(CORSAIR);
            Manufacturer Cryorig = new Manufacturer() { Id = 14, Name = "Cryorig" }; manufacturers.Add(Cryorig);
            Manufacturer Deepcool = new Manufacturer() { Id = 15, Name = "Deepcool" }; manufacturers.Add(Deepcool);
            Manufacturer DFL = new Manufacturer() { Id = 16, Name = "DFL" }; manufacturers.Add(DFL);
            Manufacturer EKWB = new Manufacturer() { Id = 17, Name = "EKWB" }; manufacturers.Add(EKWB);
            Manufacturer EVGA = new Manufacturer() { Id = 18, Name = "EVGA" }; manufacturers.Add(EVGA);
            Manufacturer Fractal_Design = new Manufacturer() { Id = 19, Name = "Fractal Design" }; manufacturers.Add(Fractal_Design);
            Manufacturer FSP = new Manufacturer() { Id = 20, Name = "FSP" }; manufacturers.Add(FSP);
            Manufacturer G_SKILL = new Manufacturer() { Id = 21, Name = "G.SKILL" }; manufacturers.Add(G_SKILL);
            Manufacturer GamerStorm = new Manufacturer() { Id = 22, Name = "GamerStorm" }; manufacturers.Add(GamerStorm);
            Manufacturer GIGABYTE = new Manufacturer() { Id = 23, Name = "GIGABYTE" }; manufacturers.Add(GIGABYTE);
            Manufacturer HyperX = new Manufacturer() { Id = 24, Name = "HyperX" }; manufacturers.Add(HyperX);
            Manufacturer Intel = new Manufacturer() { Id = 25, Name = "Intel" }; manufacturers.Add(Intel);
            Manufacturer InWin = new Manufacturer() { Id = 26, Name = "InWin" }; manufacturers.Add(InWin);
            Manufacturer Kingston = new Manufacturer() { Id = 27, Name = "Kingston" }; manufacturers.Add(Kingston);
            Manufacturer Kolink = new Manufacturer() { Id = 28, Name = "Kolink" }; manufacturers.Add(Kolink);
            Manufacturer Lian_Li = new Manufacturer() { Id = 29, Name = "Lian Li" }; manufacturers.Add(Lian_Li);
            Manufacturer Mediatonic = new Manufacturer() { Id = 30, Name = "Mediatonic" }; manufacturers.Add(Mediatonic);
            Manufacturer Mortoni = new Manufacturer() { Id = 31, Name = "Mortoni" }; manufacturers.Add(Mortoni);
            Manufacturer MSI = new Manufacturer() { Id = 32, Name = "MSI" }; manufacturers.Add(MSI);
            Manufacturer NIMBUS_Data = new Manufacturer() { Id = 33, Name = "NIMBUS Data" }; manufacturers.Add(NIMBUS_Data);
            Manufacturer NVIDIA = new Manufacturer() { Id = 34, Name = "NVIDIA" }; manufacturers.Add(NVIDIA);
            Manufacturer NZXT = new Manufacturer() { Id = 35, Name = "NZXT" }; manufacturers.Add(NZXT);
            Manufacturer OCUK = new Manufacturer() { Id = 36, Name = "OCUK" }; manufacturers.Add(OCUK);
            Manufacturer Open_Benchtable = new Manufacturer() { Id = 37, Name = "Open Benchtable" }; manufacturers.Add(Open_Benchtable);
            Manufacturer Patriot = new Manufacturer() { Id = 38, Name = "Patriot" }; manufacturers.Add(Patriot);
            Manufacturer Raijintek = new Manufacturer() { Id = 39, Name = "Raijintek" }; manufacturers.Add(Raijintek);
            Manufacturer Razer = new Manufacturer() { Id = 40, Name = "Razer" }; manufacturers.Add(Razer);
            Manufacturer ROG = new Manufacturer() { Id = 41, Name = "ROG" }; manufacturers.Add(ROG);
            Manufacturer Sabrent = new Manufacturer() { Id = 42, Name = "Sabrent" }; manufacturers.Add(Sabrent);
            Manufacturer Seagate = new Manufacturer() { Id = 43, Name = "Seagate" }; manufacturers.Add(Seagate);
            Manufacturer Shean = new Manufacturer() { Id = 44, Name = "Shean" }; manufacturers.Add(Shean);
            Manufacturer SilverStone = new Manufacturer() { Id = 45, Name = "SilverStone" }; manufacturers.Add(SilverStone);
            Manufacturer SteelSeries = new Manufacturer() { Id = 46, Name = "SteelSeries" }; manufacturers.Add(SteelSeries);
            Manufacturer Team_Group = new Manufacturer() { Id = 47, Name = "Team Group" }; manufacturers.Add(Team_Group);
            Manufacturer Thermaltake = new Manufacturer() { Id = 48, Name = "Thermaltake" }; manufacturers.Add(Thermaltake);
            Manufacturer ZOTAC = new Manufacturer() { Id = 49, Name = "ZOTAC" }; manufacturers.Add(ZOTAC);
            #endregion
            if (!_context.Manufacturer.Any())
            {
                _context.Manufacturer.AddRange(manufacturers);
            }
            #region MoboChipset
            List<MoboChipset> moboChipsets = new List<MoboChipset>();
            MoboChipset B450 = new MoboChipset() { Id = 1, Name = "B450" }; moboChipsets.Add(B450);
            MoboChipset B550 = new MoboChipset() { Id = 2, Name = "B550" }; moboChipsets.Add(B550);
            MoboChipset TRX40 = new MoboChipset() { Id = 3, Name = "TRX40" }; moboChipsets.Add(TRX40);
            MoboChipset X570 = new MoboChipset() { Id = 4, Name = "X570" }; moboChipsets.Add(X570);
            MoboChipset Z390 = new MoboChipset() { Id = 5, Name = "Z390" }; moboChipsets.Add(Z390);
            MoboChipset Z590 = new MoboChipset() { Id = 6, Name = "Z590" }; moboChipsets.Add(Z590);
            MoboChipset X299 = new MoboChipset() { Id = 7, Name = "X299" }; moboChipsets.Add(X299);
            MoboChipset X470 = new MoboChipset() { Id = 8, Name = "X470" }; moboChipsets.Add(X470);
            MoboChipset Z370 = new MoboChipset() { Id = 9, Name = "Z370" }; moboChipsets.Add(Z370);
            MoboChipset X399 = new MoboChipset() { Id = 10, Name = "X399" }; moboChipsets.Add(X399);
            MoboChipset B365 = new MoboChipset() { Id = 11, Name = "B365" }; moboChipsets.Add(B365);
            MoboChipset A320 = new MoboChipset() { Id = 12, Name = "A320" }; moboChipsets.Add(A320);
            MoboChipset H170 = new MoboChipset() { Id = 13, Name = "H170" }; moboChipsets.Add(H170);
            MoboChipset Z270 = new MoboChipset() { Id = 14, Name = "Z270" }; moboChipsets.Add(Z270);
            MoboChipset Z490 = new MoboChipset() { Id = 15, Name = "Z490" }; moboChipsets.Add(Z490);
            MoboChipset B360 = new MoboChipset() { Id = 16, Name = "B360" }; moboChipsets.Add(B360);
            MoboChipset B350 = new MoboChipset() { Id = 17, Name = "B350" }; moboChipsets.Add(B350);
            MoboChipset X370 = new MoboChipset() { Id = 18, Name = "X370" }; moboChipsets.Add(X370);
            MoboChipset B250 = new MoboChipset() { Id = 19, Name = "B250" }; moboChipsets.Add(B250);
            MoboChipset H370 = new MoboChipset() { Id = 20, Name = "H370" }; moboChipsets.Add(H370);
            MoboChipset B460 = new MoboChipset() { Id = 21, Name = "B460" }; moboChipsets.Add(B460);
            #endregion
            if (!_context.MoboChipset.Any())
            {
                _context.MoboChipset.AddRange(moboChipsets);
            }
            #region MoboSize
            List<MoboSize> moboSizes = new List<MoboSize>();
            MoboSize Mini_ITX = new MoboSize() { Id = 1, Name = "Mini-ITX" }; moboSizes.Add(Mini_ITX);
            MoboSize Micro_ATX = new MoboSize() { Id = 2, Name = "Micro-ATX" }; moboSizes.Add(Micro_ATX);
            MoboSize S_ATX = new MoboSize() { Id = 3, Name = "S-ATX" }; moboSizes.Add(S_ATX);
            MoboSize E_ATX = new MoboSize() { Id = 4, Name = "E-ATX" }; moboSizes.Add(E_ATX);
            MoboSize XL_ATX = new MoboSize() { Id = 5, Name = "XL-ATX" }; moboSizes.Add(XL_ATX);
            MoboSize SSI_EEB = new MoboSize() { Id = 6, Name = "SSI-EEB" }; moboSizes.Add(SSI_EEB);
            #endregion
            if (!_context.MoboSize.Any())
            {
                _context.MoboSize.AddRange(moboSizes);
            }
            #region MultiGPU
            List<MultiGPU> multiGPUs = new List<MultiGPU>();
            MultiGPU CrossFire = new MultiGPU() { Id = 1, Name = "CrossFire" }; multiGPUs.Add(CrossFire);
            MultiGPU SLI = new MultiGPU() { Id = 2, Name = "SLI" }; multiGPUs.Add(SLI);
            #endregion
            if (!_context.MultiGPU.Any())
            {
                _context.MultiGPU.AddRange(multiGPUs);
            }
            #region PowerConnector
            List<PowerConnector> powerConnectors = new List<PowerConnector>();
            PowerConnector Six_Pin = new PowerConnector() { Id = 1, Name = "Six Pin" }; powerConnectors.Add(Six_Pin);
            PowerConnector Eight_Pin = new PowerConnector() { Id = 2, Name = "Eight Pin" }; powerConnectors.Add(Eight_Pin);
            #endregion
            if (!_context.PowerConnector.Any())
            {
                _context.PowerConnector.AddRange(powerConnectors);
            }
            #region PSUSize
            List<PSUSize> psuSizes = new List<PSUSize>();
            PSUSize SFX = new PSUSize() { Id = 1, Name = "SFX" }; psuSizes.Add(SFX);
            PSUSize ATX = new PSUSize() { Id = 2, Name = "ATX" }; psuSizes.Add(ATX);
            #endregion
            if (!_context.PSUSize.Any())
            {
                _context.PSUSize.AddRange(psuSizes);
            }
            #region CaseFan
            List<CaseFan> caseFans = new List<CaseFan>();
            CaseFan XPG_VENTO_120 = new CaseFan() { Id = 1, Name = "XPG VENTO 120", PartType = PartType.CaseFan, Manufacturer = ADATA, Price = 30, SellPrice = 10, LevelUnlock = 31, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 45.3, Size = 120, AirPressure = 0.68 }; caseFans.Add(XPG_VENTO_120);
            CaseFan XPG_VENTO_PRO_120_PWM = new CaseFan() { Id = 2, Name = "XPG VENTO PRO 120 PWM", PartType = PartType.CaseFan, Manufacturer = ADATA, Price = 30, SellPrice = 10, LevelUnlock = 30, LevelPercent = 50, Lighting = null, AirFlow = 75, Size = 120, AirPressure = 3.15 }; caseFans.Add(XPG_VENTO_PRO_120_PWM);
            CaseFan Prizm_120_ARGB = new CaseFan() { Id = 3, Name = "Prizm 120 ARGB", PartType = PartType.CaseFan, Manufacturer = Antec, Price = 20, SellPrice = 7, LevelUnlock = 30, LevelPercent = 50, Lighting = Color.RGB, AirFlow = 45.03, Size = 120, AirPressure = 2.56 }; caseFans.Add(Prizm_120_ARGB);
            CaseFan BioniX_F120__Green_ = new CaseFan() { Id = 4, Name = "BioniX F120 (Green)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 4, LevelUnlock = 3, LevelPercent = 60, Lighting = null, AirFlow = 69, Size = 120, AirPressure = 3 }; caseFans.Add(BioniX_F120__Green_);
            CaseFan BioniX_F120__Grey___White_ = new CaseFan() { Id = 5, Name = "BioniX F120 (Grey + White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 4, LevelUnlock = 3, LevelPercent = 60, Lighting = null, AirFlow = 69, Size = 120, AirPressure = 3 }; caseFans.Add(BioniX_F120__Grey___White_);
            CaseFan BioniX_F120__Grey_ = new CaseFan() { Id = 6, Name = "BioniX F120 (Grey)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 4, LevelUnlock = 3, LevelPercent = 60, Lighting = null, AirFlow = 69, Size = 120, AirPressure = 3 }; caseFans.Add(BioniX_F120__Grey_);
            CaseFan BioniX_F120__Red_ = new CaseFan() { Id = 7, Name = "BioniX F120 (Red)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 4, LevelUnlock = 3, LevelPercent = 60, Lighting = null, AirFlow = 69, Size = 120, AirPressure = 3 }; caseFans.Add(BioniX_F120__Red_);
            CaseFan BioniX_F120__White_ = new CaseFan() { Id = 8, Name = "BioniX F120 (White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 4, LevelUnlock = 3, LevelPercent = 60, Lighting = null, AirFlow = 69, Size = 120, AirPressure = 3 }; caseFans.Add(BioniX_F120__White_);
            CaseFan BioniX_F120__Yellow_ = new CaseFan() { Id = 9, Name = "BioniX F120 (Yellow)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 4, LevelUnlock = 3, LevelPercent = 60, Lighting = null, AirFlow = 69, Size = 120, AirPressure = 3 }; caseFans.Add(BioniX_F120__Yellow_);
            CaseFan BioniX_F140__Green_ = new CaseFan() { Id = 10, Name = "BioniX F140 (Green)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 5, LevelPercent = 1, Lighting = null, AirFlow = 104, Size = 140, AirPressure = 2 }; caseFans.Add(BioniX_F140__Green_);
            CaseFan BioniX_F140__Grey___White_ = new CaseFan() { Id = 11, Name = "BioniX F140 (Grey + White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 5, LevelPercent = 1, Lighting = null, AirFlow = 104, Size = 140, AirPressure = 2 }; caseFans.Add(BioniX_F140__Grey___White_);
            CaseFan BioniX_F140__Grey_ = new CaseFan() { Id = 12, Name = "BioniX F140 (Grey)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 5, LevelPercent = 1, Lighting = null, AirFlow = 104, Size = 140, AirPressure = 2 }; caseFans.Add(BioniX_F140__Grey_);
            CaseFan BioniX_F140__Red_ = new CaseFan() { Id = 13, Name = "BioniX F140 (Red)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 5, LevelPercent = 1, Lighting = null, AirFlow = 104, Size = 140, AirPressure = 2 }; caseFans.Add(BioniX_F140__Red_);
            CaseFan BioniX_F140__White_ = new CaseFan() { Id = 14, Name = "BioniX F140 (White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 5, LevelPercent = 1, Lighting = null, AirFlow = 104, Size = 140, AirPressure = 2 }; caseFans.Add(BioniX_F140__White_);
            CaseFan BioniX_F140__Yellow_ = new CaseFan() { Id = 15, Name = "BioniX F140 (Yellow)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 5, LevelPercent = 1, Lighting = null, AirFlow = 104, Size = 140, AirPressure = 2 }; caseFans.Add(BioniX_F140__Yellow_);
            CaseFan BioniX_P120__Green_ = new CaseFan() { Id = 16, Name = "BioniX P120 (Green)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 16, SellPrice = 5, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 67.56, Size = 120, AirPressure = 2.75 }; caseFans.Add(BioniX_P120__Green_);
            CaseFan BioniX_P120__Grey___White_ = new CaseFan() { Id = 17, Name = "BioniX P120 (Grey + White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 16, SellPrice = 5, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 67.56, Size = 120, AirPressure = 2.75 }; caseFans.Add(BioniX_P120__Grey___White_);
            CaseFan BioniX_P120__Grey_ = new CaseFan() { Id = 18, Name = "BioniX P120 (Grey)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 16, SellPrice = 5, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 67.56, Size = 120, AirPressure = 2.75 }; caseFans.Add(BioniX_P120__Grey_);
            CaseFan BioniX_P120__Red_ = new CaseFan() { Id = 19, Name = "BioniX P120 (Red)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 16, SellPrice = 5, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 67.56, Size = 120, AirPressure = 2.75 }; caseFans.Add(BioniX_P120__Red_);
            CaseFan BioniX_P120__White_ = new CaseFan() { Id = 20, Name = "BioniX P120 (White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 16, SellPrice = 5, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 67.56, Size = 120, AirPressure = 2.75 }; caseFans.Add(BioniX_P120__White_);
            CaseFan BioniX_P120__Yellow_ = new CaseFan() { Id = 21, Name = "BioniX P120 (Yellow)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 16, SellPrice = 5, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 67.56, Size = 120, AirPressure = 2.75 }; caseFans.Add(BioniX_P120__Yellow_);
            CaseFan BioniX_P120_ARGB = new CaseFan() { Id = 22, Name = "BioniX P120 ARGB", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 30, SellPrice = 20, LevelUnlock = 1, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 48, Size = 120, AirPressure = 2.1 }; caseFans.Add(BioniX_P120_ARGB);
            CaseFan BioniX_P140__Grey___White_ = new CaseFan() { Id = 23, Name = "BioniX P140 (Grey + White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 18, SellPrice = 6, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 77.6, Size = 140, AirPressure = 2.85 }; caseFans.Add(BioniX_P140__Grey___White_);
            CaseFan BioniX_P140__Grey_ = new CaseFan() { Id = 24, Name = "BioniX P140 (Grey)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 18, SellPrice = 6, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 77.6, Size = 140, AirPressure = 2.85 }; caseFans.Add(BioniX_P140__Grey_);
            CaseFan BioniX_P140__Red_ = new CaseFan() { Id = 25, Name = "BioniX P140 (Red)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 18, SellPrice = 6, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 77.6, Size = 140, AirPressure = 2.85 }; caseFans.Add(BioniX_P140__Red_);
            CaseFan BioniX_P140__White_ = new CaseFan() { Id = 26, Name = "BioniX P140 (White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 18, SellPrice = 6, LevelUnlock = 13, LevelPercent = 10, Lighting = null, AirFlow = 77.6, Size = 140, AirPressure = 2.85 }; caseFans.Add(BioniX_P140__White_);
            CaseFan F12_PWM_PST = new CaseFan() { Id = 27, Name = "F12 PWM PST", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 11, LevelPercent = 1, Lighting = null, AirFlow = 53, Size = 120, AirPressure = 0.87 }; caseFans.Add(F12_PWM_PST);
            CaseFan F12_PWM_PST_CO = new CaseFan() { Id = 28, Name = "F12 PWM PST CO", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 12, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 53, Size = 120, AirPressure = 2 }; caseFans.Add(F12_PWM_PST_CO);
            CaseFan F12_Silent = new CaseFan() { Id = 29, Name = "F12 Silent", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 37, Size = 120, AirPressure = 2 }; caseFans.Add(F12_Silent);
            CaseFan F14_PWM_PST = new CaseFan() { Id = 30, Name = "F14 PWM PST", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 11, LevelPercent = 1, Lighting = null, AirFlow = 74, Size = 140, AirPressure = 2.4 }; caseFans.Add(F14_PWM_PST);
            CaseFan F14_PWM_PST_CO = new CaseFan() { Id = 31, Name = "F14 PWM PST CO", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 74, Size = 140, AirPressure = 2 }; caseFans.Add(F14_PWM_PST_CO);
            CaseFan F14_Silent = new CaseFan() { Id = 32, Name = "F14 Silent", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 11, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 46, Size = 140, AirPressure = 2 }; caseFans.Add(F14_Silent);
            CaseFan P12 = new CaseFan() { Id = 33, Name = "P12", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12);
            CaseFan P12_PWM__Black_Transparent_ = new CaseFan() { Id = 34, Name = "P12 PWM (Black Transparent)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM__Black_Transparent_);
            CaseFan P12_PWM__Black_ = new CaseFan() { Id = 35, Name = "P12 PWM (Black)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM__Black_);
            CaseFan P12_PWM__White_Transparent_ = new CaseFan() { Id = 36, Name = "P12 PWM (White Transparent)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM__White_Transparent_);
            CaseFan P12_PWM__White_ = new CaseFan() { Id = 37, Name = "P12 PWM (White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM__White_);
            CaseFan P12_PWM_PST__Black_Transparent_ = new CaseFan() { Id = 38, Name = "P12 PWM PST (Black Transparent)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 11, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM_PST__Black_Transparent_);
            CaseFan P12_PWM_PST__Black_ = new CaseFan() { Id = 39, Name = "P12 PWM PST (Black)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 11, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM_PST__Black_);
            CaseFan P12_PWM_PST__White_Transparent_ = new CaseFan() { Id = 40, Name = "P12 PWM PST (White Transparent)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 11, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM_PST__White_Transparent_);
            CaseFan P12_PWM_PST__White_ = new CaseFan() { Id = 41, Name = "P12 PWM PST (White)", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 11, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM_PST__White_);
            CaseFan P12_PWM_PST_CO = new CaseFan() { Id = 42, Name = "P12 PWM PST CO", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 11, SellPrice = 4, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_PWM_PST_CO);
            CaseFan P12_Silent = new CaseFan() { Id = 43, Name = "P12 Silent", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 10, SellPrice = 3, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 24.1, Size = 120, AirPressure = 2 }; caseFans.Add(P12_Silent);
            CaseFan P12_Slim_PWM_PST = new CaseFan() { Id = 44, Name = "P12 Slim PWM PST", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 13, SellPrice = 20, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 42.1, Size = 120, AirPressure = 1.45 }; caseFans.Add(P12_Slim_PWM_PST);
            CaseFan P12_TC = new CaseFan() { Id = 45, Name = "P12 TC", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 20, SellPrice = 7, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 56.3, Size = 120, AirPressure = 2 }; caseFans.Add(P12_TC);
            CaseFan P14 = new CaseFan() { Id = 46, Name = "P14", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 72.8, Size = 140, AirPressure = 2 }; caseFans.Add(P14);
            CaseFan P14_PWM = new CaseFan() { Id = 47, Name = "P14 PWM", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 72.8, Size = 140, AirPressure = 2 }; caseFans.Add(P14_PWM);
            CaseFan P14_PWM_PST = new CaseFan() { Id = 48, Name = "P14 PWM PST", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 72.8, Size = 140, AirPressure = 2 }; caseFans.Add(P14_PWM_PST);
            CaseFan P14_PWM_PST_CO = new CaseFan() { Id = 49, Name = "P14 PWM PST CO", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 72.8, Size = 140, AirPressure = 2 }; caseFans.Add(P14_PWM_PST_CO);
            CaseFan P14_Silent = new CaseFan() { Id = 50, Name = "P14 Silent", PartType = PartType.CaseFan, Manufacturer = ARCTIC, Price = 14, SellPrice = 5, LevelUnlock = 16, LevelPercent = 1, Lighting = null, AirFlow = 29.8, Size = 140, AirPressure = 2 }; caseFans.Add(P14_Silent);
            CaseFan Pure_Wings_2_120mm = new CaseFan() { Id = 51, Name = "Pure Wings 2 120mm", PartType = PartType.CaseFan, Manufacturer = be_quiet_, Price = 10, SellPrice = 3, LevelUnlock = 27, LevelPercent = 30, Lighting = null, AirFlow = 65.51, Size = 120, AirPressure = 2.23 }; caseFans.Add(Pure_Wings_2_120mm);
            CaseFan Pure_Wings_2_140mm = new CaseFan() { Id = 52, Name = "Pure Wings 2 140mm", PartType = PartType.CaseFan, Manufacturer = be_quiet_, Price = 13, SellPrice = 4, LevelUnlock = 27, LevelPercent = 30, Lighting = null, AirFlow = 82.4, Size = 140, AirPressure = 1.82 }; caseFans.Add(Pure_Wings_2_140mm);
            CaseFan Shadow_Wings_2_120mm = new CaseFan() { Id = 53, Name = "Shadow Wings 2 120mm", PartType = PartType.CaseFan, Manufacturer = be_quiet_, Price = 16, SellPrice = 5, LevelUnlock = 27, LevelPercent = 70, Lighting = null, AirFlow = 38.5, Size = 120, AirPressure = 0.82 }; caseFans.Add(Shadow_Wings_2_120mm);
            CaseFan Shadow_Wings_2_140mm = new CaseFan() { Id = 54, Name = "Shadow Wings 2 140mm", PartType = PartType.CaseFan, Manufacturer = be_quiet_, Price = 19, SellPrice = 6, LevelUnlock = 27, LevelPercent = 70, Lighting = null, AirFlow = 49.8, Size = 140, AirPressure = 0.58 }; caseFans.Add(Shadow_Wings_2_140mm);
            CaseFan Silent_Wings_3_120mm = new CaseFan() { Id = 55, Name = "Silent Wings 3 120mm", PartType = PartType.CaseFan, Manufacturer = be_quiet_, Price = 7, SellPrice = 2, LevelUnlock = 27, LevelPercent = 1, Lighting = null, AirFlow = 50.5, Size = 120, AirPressure = 1.79 }; caseFans.Add(Silent_Wings_3_120mm);
            CaseFan Silent_Wings_3_140mm = new CaseFan() { Id = 56, Name = "Silent Wings 3 140mm", PartType = PartType.CaseFan, Manufacturer = be_quiet_, Price = 8, SellPrice = 3, LevelUnlock = 27, LevelPercent = 1, Lighting = null, AirFlow = 59.5, Size = 140, AirPressure = 1.08 }; caseFans.Add(Silent_Wings_3_140mm);
            CaseFan MasterCase_H500P_200mm_RGB = new CaseFan() { Id = 57, Name = "MasterCase H500P 200mm RGB", PartType = PartType.CaseFan, Manufacturer = Cooler_Master, Price = 10, SellPrice = 3, LevelUnlock = 3, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 90, Size = 200, AirPressure = 0.88 }; caseFans.Add(MasterCase_H500P_200mm_RGB);
            CaseFan SickleFlow_120mm = new CaseFan() { Id = 58, Name = "SickleFlow 120mm", PartType = PartType.CaseFan, Manufacturer = Cooler_Master, Price = 30, SellPrice = 10, LevelUnlock = 32, LevelPercent = 50, Lighting = null, AirFlow = 55, Size = 120, AirPressure = 2.5 }; caseFans.Add(SickleFlow_120mm);
            CaseFan SILENCIO_FP_120_PWM = new CaseFan() { Id = 59, Name = "SILENCIO FP 120 PWM", PartType = PartType.CaseFan, Manufacturer = Cooler_Master, Price = 30, SellPrice = 10, LevelUnlock = 31, LevelPercent = 1, Lighting = null, AirFlow = 52.02, Size = 120, AirPressure = 2 }; caseFans.Add(SILENCIO_FP_120_PWM);
            CaseFan HD120_RGB_LED = new CaseFan() { Id = 60, Name = "HD120 RGB LED", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 30, SellPrice = 10, LevelUnlock = 18, LevelPercent = 40, Lighting = Color.RGB, AirFlow = 54.4, Size = 120, AirPressure = 2.25 }; caseFans.Add(HD120_RGB_LED);
            CaseFan HD140_RGB_LED = new CaseFan() { Id = 61, Name = "HD140 RGB LED", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 33, SellPrice = 11, LevelUnlock = 18, LevelPercent = 40, Lighting = Color.RGB, AirFlow = 74, Size = 140, AirPressure = 1.85 }; caseFans.Add(HD140_RGB_LED);
            CaseFan iCUE_QL120_RGB_120mm_PWM__Black_ = new CaseFan() { Id = 62, Name = "iCUE QL120 RGB 120mm PWM (Black)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 30, SellPrice = 10, LevelUnlock = 30, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 41.8, Size = 120, AirPressure = 2 }; caseFans.Add(iCUE_QL120_RGB_120mm_PWM__Black_);
            CaseFan iCUE_QL120_RGB_120mm_PWM__White_ = new CaseFan() { Id = 63, Name = "iCUE QL120 RGB 120mm PWM (White)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 30, SellPrice = 10, LevelUnlock = 30, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 41.8, Size = 120, AirPressure = 2 }; caseFans.Add(iCUE_QL120_RGB_120mm_PWM__White_);
            CaseFan iCUE_QL140_RGB_140mm_PWM__Black_ = new CaseFan() { Id = 64, Name = "iCUE QL140 RGB 140mm PWM (Black)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 35, SellPrice = 12, LevelUnlock = 30, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 52.02, Size = 140, AirPressure = 2 }; caseFans.Add(iCUE_QL140_RGB_140mm_PWM__Black_);
            CaseFan iCUE_QL140_RGB_140mm_PWM__White_ = new CaseFan() { Id = 65, Name = "iCUE QL140 RGB 140mm PWM (White)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 35, SellPrice = 12, LevelUnlock = 30, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 52.02, Size = 140, AirPressure = 2 }; caseFans.Add(iCUE_QL140_RGB_140mm_PWM__White_);
            CaseFan LL120_RGB_LED = new CaseFan() { Id = 66, Name = "LL120 RGB LED", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 35, SellPrice = 12, LevelUnlock = 18, LevelPercent = 80, Lighting = Color.RGB, AirFlow = 44, Size = 120, AirPressure = 1.61 }; caseFans.Add(LL120_RGB_LED);
            CaseFan LL120_RGB_LED__White_ = new CaseFan() { Id = 67, Name = "LL120 RGB LED (White)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 35, SellPrice = 12, LevelUnlock = 18, LevelPercent = 80, Lighting = Color.RGB, AirFlow = 44, Size = 120, AirPressure = 1.61 }; caseFans.Add(LL120_RGB_LED__White_);
            CaseFan LL140_RGB_LED = new CaseFan() { Id = 68, Name = "LL140 RGB LED", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 38, SellPrice = 13, LevelUnlock = 18, LevelPercent = 80, Lighting = Color.RGB, AirFlow = 52, Size = 140, AirPressure = 1.52 }; caseFans.Add(LL140_RGB_LED);
            CaseFan ML120 = new CaseFan() { Id = 69, Name = "ML120", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 25, SellPrice = 8, LevelUnlock = 2, LevelPercent = 1, Lighting = null, AirFlow = 75, Size = 120, AirPressure = 4.2 }; caseFans.Add(ML120);
            CaseFan ML140 = new CaseFan() { Id = 70, Name = "ML140", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 28, SellPrice = 9, LevelUnlock = 2, LevelPercent = 1, Lighting = null, AirFlow = 97, Size = 140, AirPressure = 3 }; caseFans.Add(ML140);
            CaseFan SP120__Black_ = new CaseFan() { Id = 71, Name = "SP120 (Black)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 20, SellPrice = 7, LevelUnlock = 28, LevelPercent = 1, Lighting = null, AirFlow = 45, Size = 120, AirPressure = 1.46 }; caseFans.Add(SP120__Black_);
            CaseFan SP120__White_ = new CaseFan() { Id = 72, Name = "SP120 (White)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 20, SellPrice = 7, LevelUnlock = 28, LevelPercent = 1, Lighting = null, AirFlow = 45, Size = 120, AirPressure = 1.46 }; caseFans.Add(SP120__White_);
            CaseFan SP120_RGB_Elite__Black_ = new CaseFan() { Id = 73, Name = "SP120 RGB Elite (Black)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 25, SellPrice = 8, LevelUnlock = 28, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 45, Size = 120, AirPressure = 1.46 }; caseFans.Add(SP120_RGB_Elite__Black_);
            CaseFan SP120_RGB_Elite__White_ = new CaseFan() { Id = 74, Name = "SP120 RGB Elite (White)", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 25, SellPrice = 8, LevelUnlock = 28, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 45, Size = 120, AirPressure = 1.46 }; caseFans.Add(SP120_RGB_Elite__White_);
            CaseFan SP120_RGB_LED = new CaseFan() { Id = 75, Name = "SP120 RGB LED", PartType = PartType.CaseFan, Manufacturer = CORSAIR, Price = 35, SellPrice = 12, LevelUnlock = 10, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 52, Size = 120, AirPressure = 1.61 }; caseFans.Add(SP120_RGB_LED);
            CaseFan QF120___Performance = new CaseFan() { Id = 76, Name = "QF120 - Performance", PartType = PartType.CaseFan, Manufacturer = Cryorig, Price = 12, SellPrice = 4, LevelUnlock = 30, LevelPercent = 1, Lighting = null, AirFlow = 83, Size = 120, AirPressure = 3.33 }; caseFans.Add(QF120___Performance);
            CaseFan QF140___Performance = new CaseFan() { Id = 77, Name = "QF140 - Performance", PartType = PartType.CaseFan, Manufacturer = Cryorig, Price = 15, SellPrice = 5, LevelUnlock = 30, LevelPercent = 1, Lighting = null, AirFlow = 128, Size = 140, AirPressure = 2.12 }; caseFans.Add(QF140___Performance);
            CaseFan XF140 = new CaseFan() { Id = 78, Name = "XF140", PartType = PartType.CaseFan, Manufacturer = Cryorig, Price = 20, SellPrice = 7, LevelUnlock = 29, LevelPercent = 1, Lighting = null, AirFlow = 76, Size = 140, AirPressure = 1.44 }; caseFans.Add(XF140);
            CaseFan XT140 = new CaseFan() { Id = 79, Name = "XT140", PartType = PartType.CaseFan, Manufacturer = Cryorig, Price = 60, SellPrice = 20, LevelUnlock = 29, LevelPercent = 1, Lighting = null, AirFlow = 65, Size = 140, AirPressure = 1.49 }; caseFans.Add(XT140);
            CaseFan RF_120 = new CaseFan() { Id = 80, Name = "RF 120", PartType = PartType.CaseFan, Manufacturer = Deepcool, Price = 20, SellPrice = 7, LevelUnlock = 14, LevelPercent = 25, Lighting = Color.RGB, AirFlow = 56.5, Size = 120, AirPressure = null }; caseFans.Add(RF_120);
            CaseFan RF_140 = new CaseFan() { Id = 81, Name = "RF 140", PartType = PartType.CaseFan, Manufacturer = Deepcool, Price = 25, SellPrice = 8, LevelUnlock = 14, LevelPercent = 25, Lighting = Color.RGB, AirFlow = 64.13, Size = 140, AirPressure = null }; caseFans.Add(RF_140);
            CaseFan EK_Vardar_EVO_120ER_RGB = new CaseFan() { Id = 82, Name = "EK-Vardar EVO 120ER RGB", PartType = PartType.CaseFan, Manufacturer = EKWB, Price = 30, SellPrice = 10, LevelUnlock = 25, LevelPercent = 30, Lighting = Color.RGB, AirFlow = 77, Size = 120, AirPressure = 3.16 }; caseFans.Add(EK_Vardar_EVO_120ER_RGB);
            CaseFan EK_Vardar_EVO_140S_BB = new CaseFan() { Id = 83, Name = "EK-Vardar EVO 140S BB", PartType = PartType.CaseFan, Manufacturer = EKWB, Price = 30, SellPrice = 10, LevelUnlock = 25, LevelPercent = 1, Lighting = null, AirFlow = 98, Size = 140, AirPressure = 3.15 }; caseFans.Add(EK_Vardar_EVO_140S_BB);
            CaseFan EK_Vardar_F4_120ER = new CaseFan() { Id = 84, Name = "EK-Vardar F4-120ER", PartType = PartType.CaseFan, Manufacturer = EKWB, Price = 25, SellPrice = 8, LevelUnlock = 25, LevelPercent = 1, Lighting = null, AirFlow = 77, Size = 120, AirPressure = 3.16 }; caseFans.Add(EK_Vardar_F4_120ER);
            CaseFan Dynamic_X2_GP_12 = new CaseFan() { Id = 85, Name = "Dynamic X2 GP-12", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 13, SellPrice = 4, LevelUnlock = 21, LevelPercent = 1, Lighting = null, AirFlow = 52, Size = 120, AirPressure = 0.88 }; caseFans.Add(Dynamic_X2_GP_12);
            CaseFan Dynamic_X2_GP_12__Black_ = new CaseFan() { Id = 86, Name = "Dynamic X2 GP-12 (Black)", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 13, SellPrice = 4, LevelUnlock = 21, LevelPercent = 1, Lighting = null, AirFlow = 52, Size = 120, AirPressure = 0.88 }; caseFans.Add(Dynamic_X2_GP_12__Black_);
            CaseFan Dynamic_X2_GP_12__White_ = new CaseFan() { Id = 87, Name = "Dynamic X2 GP-12 (White)", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 13, SellPrice = 4, LevelUnlock = 21, LevelPercent = 1, Lighting = null, AirFlow = 52, Size = 120, AirPressure = 0.88 }; caseFans.Add(Dynamic_X2_GP_12__White_);
            CaseFan Dynamic_X2_GP_14 = new CaseFan() { Id = 88, Name = "Dynamic X2 GP-14", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 15, SellPrice = 5, LevelUnlock = 21, LevelPercent = 1, Lighting = null, AirFlow = 68, Size = 140, AirPressure = 0.71 }; caseFans.Add(Dynamic_X2_GP_14);
            CaseFan Dynamic_X2_GP_14__Black_ = new CaseFan() { Id = 89, Name = "Dynamic X2 GP-14 (Black)", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 15, SellPrice = 5, LevelUnlock = 21, LevelPercent = 1, Lighting = null, AirFlow = 68, Size = 140, AirPressure = 0.71 }; caseFans.Add(Dynamic_X2_GP_14__Black_);
            CaseFan Dynamic_X2_GP_14__White_ = new CaseFan() { Id = 90, Name = "Dynamic X2 GP-14 (White)", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 15, SellPrice = 5, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 68, Size = 140, AirPressure = 0.71 }; caseFans.Add(Dynamic_X2_GP_14__White_);
            CaseFan Dynamic_X2_GP_18_PWM = new CaseFan() { Id = 91, Name = "Dynamic X2 GP-18 PWM", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 1, SellPrice = 0, LevelUnlock = 21, LevelPercent = 1, Lighting = null, AirFlow = 44.9, Size = 180, AirPressure = 0.4 }; caseFans.Add(Dynamic_X2_GP_18_PWM);
            CaseFan Prisma_AL_12 = new CaseFan() { Id = 92, Name = "Prisma AL-12", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 25, SellPrice = 8, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 50, Size = 120, AirPressure = 0.8 }; caseFans.Add(Prisma_AL_12);
            CaseFan Prisma_AL_14 = new CaseFan() { Id = 93, Name = "Prisma AL-14", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 25, SellPrice = 8, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 63, Size = 140, AirPressure = 0.8 }; caseFans.Add(Prisma_AL_14);
            CaseFan Prisma_AL_18_PWM = new CaseFan() { Id = 94, Name = "Prisma AL-18 PWM", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 1, SellPrice = 0, LevelUnlock = 21, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 43.1, Size = 180, AirPressure = 0.44 }; caseFans.Add(Prisma_AL_18_PWM);
            CaseFan Silent_Series_LL_Blue_120mm = new CaseFan() { Id = 95, Name = "Silent Series LL Blue 120mm", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 15, SellPrice = 5, LevelUnlock = 26, LevelPercent = 1, Lighting = Color.Blue, AirFlow = 41.8, Size = 120, AirPressure = 0.87 }; caseFans.Add(Silent_Series_LL_Blue_120mm);
            CaseFan Silent_Series_LL_Red_120mm = new CaseFan() { Id = 96, Name = "Silent Series LL Red 120mm", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 15, SellPrice = 5, LevelUnlock = 26, LevelPercent = 1, Lighting = Color.Red, AirFlow = 41.8, Size = 120, AirPressure = 0.87 }; caseFans.Add(Silent_Series_LL_Red_120mm);
            CaseFan Silent_Series_LL_White_120mm = new CaseFan() { Id = 97, Name = "Silent Series LL White 120mm", PartType = PartType.CaseFan, Manufacturer = Fractal_Design, Price = 15, SellPrice = 5, LevelUnlock = 26, LevelPercent = 1, Lighting = Color.White, AirFlow = 41.8, Size = 120, AirPressure = 0.87 }; caseFans.Add(Silent_Series_LL_White_120mm);
            CaseFan CMT510 = new CaseFan() { Id = 98, Name = "CMT510", PartType = PartType.CaseFan, Manufacturer = FSP, Price = 15, SellPrice = 5, LevelUnlock = 14, LevelPercent = 30, Lighting = Color.RGB, AirFlow = 45, Size = 120, AirPressure = 1.5 }; caseFans.Add(CMT510);
            CaseFan CMT520 = new CaseFan() { Id = 99, Name = "CMT520", PartType = PartType.CaseFan, Manufacturer = FSP, Price = 15, SellPrice = 5, LevelUnlock = 14, LevelPercent = 30, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 1.5 }; caseFans.Add(CMT520);
            CaseFan MF120 = new CaseFan() { Id = 100, Name = "MF120", PartType = PartType.CaseFan, Manufacturer = GamerStorm, Price = 60, SellPrice = 20, LevelUnlock = 30, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 45, Size = 120, AirPressure = 1.67 }; caseFans.Add(MF120);
            CaseFan MF120_GT = new CaseFan() { Id = 101, Name = "MF120 GT", PartType = PartType.CaseFan, Manufacturer = GamerStorm, Price = 40, SellPrice = 13, LevelUnlock = 28, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 40, Size = 120, AirPressure = 1.67 }; caseFans.Add(MF120_GT);
            CaseFan TF120S__Black_ = new CaseFan() { Id = 102, Name = "TF120S (Black)", PartType = PartType.CaseFan, Manufacturer = GamerStorm, Price = 40, SellPrice = 13, LevelUnlock = 28, LevelPercent = 1, Lighting = null, AirFlow = 40, Size = 120, AirPressure = 1.67 }; caseFans.Add(TF120S__Black_);
            CaseFan TF120S__White_ = new CaseFan() { Id = 103, Name = "TF120S (White)", PartType = PartType.CaseFan, Manufacturer = GamerStorm, Price = 40, SellPrice = 13, LevelUnlock = 28, LevelPercent = 1, Lighting = null, AirFlow = 40, Size = 120, AirPressure = 1.67 }; caseFans.Add(TF120S__White_);
            CaseFan CROWN_AC120 = new CaseFan() { Id = 104, Name = "CROWN AC120", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 20, SellPrice = 7, LevelUnlock = 24, LevelPercent = 30, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(CROWN_AC120);
            CaseFan CROWN_AC140 = new CaseFan() { Id = 105, Name = "CROWN AC140", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 30, SellPrice = 10, LevelUnlock = 24, LevelPercent = 30, Lighting = Color.RGB, AirFlow = 70, Size = 140, AirPressure = 2.64 }; caseFans.Add(CROWN_AC140);
            CaseFan EGO_AE120 = new CaseFan() { Id = 106, Name = "EGO AE120", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 30, SellPrice = 10, LevelUnlock = 25, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(EGO_AE120);
            CaseFan Luna_AL120 = new CaseFan() { Id = 107, Name = "Luna AL120", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 20, SellPrice = 7, LevelUnlock = 30, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 2.31 }; caseFans.Add(Luna_AL120);
            CaseFan Polaris_LED__Blue_ = new CaseFan() { Id = 108, Name = "Polaris LED (Blue)", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 25, SellPrice = 8, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.Blue, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(Polaris_LED__Blue_);
            CaseFan Polaris_LED__Green_ = new CaseFan() { Id = 109, Name = "Polaris LED (Green)", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 25, SellPrice = 8, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.Green, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(Polaris_LED__Green_);
            CaseFan Polaris_LED__Red_ = new CaseFan() { Id = 110, Name = "Polaris LED (Red)", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 25, SellPrice = 8, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.Red, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(Polaris_LED__Red_);
            CaseFan Polaris_LED__White_ = new CaseFan() { Id = 111, Name = "Polaris LED (White)", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 25, SellPrice = 8, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.White, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(Polaris_LED__White_);
            CaseFan Polaris_RGB = new CaseFan() { Id = 112, Name = "Polaris RGB", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 40, SellPrice = 13, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(Polaris_RGB);
            CaseFan Polaris_RGB_Aluminium = new CaseFan() { Id = 113, Name = "Polaris RGB Aluminium", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 50, SellPrice = 17, LevelUnlock = 24, LevelPercent = 50, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(Polaris_RGB_Aluminium);
            CaseFan Saturn_ASN120 = new CaseFan() { Id = 114, Name = "Saturn ASN120", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 10, SellPrice = 3, LevelUnlock = 20, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 50, Size = 120, AirPressure = 2 }; caseFans.Add(Saturn_ASN120);
            CaseFan Sirius_Loop_ASL120 = new CaseFan() { Id = 115, Name = "Sirius Loop ASL120", PartType = PartType.CaseFan, Manufacturer = InWin, Price = 12, SellPrice = 4, LevelUnlock = 24, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 2.24 }; caseFans.Add(Sirius_Loop_ASL120);
            CaseFan Fan_120 = new CaseFan() { Id = 116, Name = "Fan 120", PartType = PartType.CaseFan, Manufacturer = Kolink, Price = 11, SellPrice = 4, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 60, Size = 120, AirPressure = 1.61 }; caseFans.Add(Fan_120);
            CaseFan Bora_Digital_ARGB__Black_ = new CaseFan() { Id = 117, Name = "Bora Digital ARGB (Black)", PartType = PartType.CaseFan, Manufacturer = Lian_Li, Price = 20, SellPrice = 7, LevelUnlock = 15, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 57.97, Size = 120, AirPressure = 1.46 }; caseFans.Add(Bora_Digital_ARGB__Black_);
            CaseFan Bora_Digital_ARGB__Silver_ = new CaseFan() { Id = 118, Name = "Bora Digital ARGB (Silver)", PartType = PartType.CaseFan, Manufacturer = Lian_Li, Price = 20, SellPrice = 7, LevelUnlock = 15, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 57.97, Size = 120, AirPressure = 1.46 }; caseFans.Add(Bora_Digital_ARGB__Silver_);
            CaseFan Bora_Digital_ARGB__Space_Grey_ = new CaseFan() { Id = 119, Name = "Bora Digital ARGB (Space Grey)", PartType = PartType.CaseFan, Manufacturer = Lian_Li, Price = 20, SellPrice = 7, LevelUnlock = 15, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 57.97, Size = 120, AirPressure = 1.46 }; caseFans.Add(Bora_Digital_ARGB__Space_Grey_);
            CaseFan Heat_Away_120 = new CaseFan() { Id = 120, Name = "Heat Away 120", PartType = PartType.CaseFan, Manufacturer = Mortoni, Price = 7, SellPrice = 2, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 40, Size = 120, AirPressure = 1.61 }; caseFans.Add(Heat_Away_120);
            CaseFan Heat_Away_140 = new CaseFan() { Id = 121, Name = "Heat Away 140", PartType = PartType.CaseFan, Manufacturer = Mortoni, Price = 7, SellPrice = 2, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 60, Size = 140, AirPressure = 1.61 }; caseFans.Add(Heat_Away_140);
            CaseFan Heat_Away_90 = new CaseFan() { Id = 122, Name = "Heat Away 90", PartType = PartType.CaseFan, Manufacturer = Mortoni, Price = 5, SellPrice = 2, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 25, Size = 90, AirPressure = 1.2 }; caseFans.Add(Heat_Away_90);
            CaseFan Heat_Away_Pro_RGB_120 = new CaseFan() { Id = 123, Name = "Heat Away Pro RGB 120", PartType = PartType.CaseFan, Manufacturer = Mortoni, Price = 12, SellPrice = 4, LevelUnlock = 10, LevelPercent = 50, Lighting = Color.RGB, AirFlow = 42, Size = 120, AirPressure = 1.2 }; caseFans.Add(Heat_Away_Pro_RGB_120);
            CaseFan Heat_Away_RGB_120 = new CaseFan() { Id = 124, Name = "Heat Away RGB 120", PartType = PartType.CaseFan, Manufacturer = Mortoni, Price = 10, SellPrice = 3, LevelUnlock = 10, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 40, Size = 120, AirPressure = 1.61 }; caseFans.Add(Heat_Away_RGB_120);
            CaseFan Heat_Away_RGB_140 = new CaseFan() { Id = 125, Name = "Heat Away RGB 140", PartType = PartType.CaseFan, Manufacturer = Mortoni, Price = 10, SellPrice = 3, LevelUnlock = 10, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 60, Size = 140, AirPressure = 1.61 }; caseFans.Add(Heat_Away_RGB_140);
            CaseFan N20mm = new CaseFan() { Id = 126, Name = "120mm", PartType = PartType.CaseFan, Manufacturer = MSI, Price = 40, SellPrice = 13, LevelUnlock = 8, LevelPercent = 1, Lighting = null, AirFlow = 50, Size = 120, AirPressure = 2 }; caseFans.Add(N20mm);
            CaseFan N20mm_ARGB__Black_Logo_ = new CaseFan() { Id = 127, Name = "120mm ARGB (Black Logo)", PartType = PartType.CaseFan, Manufacturer = MSI, Price = 20, SellPrice = 7, LevelUnlock = 8, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 50, Size = 120, AirPressure = 2 }; caseFans.Add(N20mm_ARGB__Black_Logo_);
            CaseFan N20mm_ARGB__Silver_Logo_ = new CaseFan() { Id = 128, Name = "120mm ARGB (Silver Logo)", PartType = PartType.CaseFan, Manufacturer = MSI, Price = 20, SellPrice = 7, LevelUnlock = 8, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 50, Size = 120, AirPressure = 2 }; caseFans.Add(N20mm_ARGB__Silver_Logo_);
            CaseFan N20mm_RGB = new CaseFan() { Id = 129, Name = "120mm RGB", PartType = PartType.CaseFan, Manufacturer = MSI, Price = 40, SellPrice = 13, LevelUnlock = 8, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 50, Size = 120, AirPressure = 2 }; caseFans.Add(N20mm_RGB);
            CaseFan N00mm = new CaseFan() { Id = 130, Name = "200mm", PartType = PartType.CaseFan, Manufacturer = MSI, Price = 40, SellPrice = 13, LevelUnlock = 8, LevelPercent = 1, Lighting = null, AirFlow = 90, Size = 200, AirPressure = 2 }; caseFans.Add(N00mm);
            CaseFan MAG_MAX_F20A_1 = new CaseFan() { Id = 131, Name = "MAG MAX F20A-1", PartType = PartType.CaseFan, Manufacturer = MSI, Price = 40, SellPrice = 13, LevelUnlock = 8, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 90, Size = 200, AirPressure = 2 }; caseFans.Add(MAG_MAX_F20A_1);
            CaseFan Aer_P_120mm__Black_ = new CaseFan() { Id = 132, Name = "Aer P 120mm (Black)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 73, Size = 120, AirPressure = 2.93 }; caseFans.Add(Aer_P_120mm__Black_);
            CaseFan Aer_P_120mm__Blue_ = new CaseFan() { Id = 133, Name = "Aer P 120mm (Blue)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 73, Size = 120, AirPressure = 2.93 }; caseFans.Add(Aer_P_120mm__Blue_);
            CaseFan Aer_P_120mm__Red_ = new CaseFan() { Id = 134, Name = "Aer P 120mm (Red)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 73, Size = 120, AirPressure = 2.93 }; caseFans.Add(Aer_P_120mm__Red_);
            CaseFan Aer_P_120mm__White_ = new CaseFan() { Id = 135, Name = "Aer P 120mm (White)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 73, Size = 120, AirPressure = 2.93 }; caseFans.Add(Aer_P_120mm__White_);
            CaseFan Aer_P_140mm__Black_ = new CaseFan() { Id = 136, Name = "Aer P 140mm (Black)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 98, Size = 140, AirPressure = 2.71 }; caseFans.Add(Aer_P_140mm__Black_);
            CaseFan Aer_P_140mm__Blue_ = new CaseFan() { Id = 137, Name = "Aer P 140mm (Blue)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 98, Size = 140, AirPressure = 2.71 }; caseFans.Add(Aer_P_140mm__Blue_);
            CaseFan Aer_P_140mm__Red_ = new CaseFan() { Id = 138, Name = "Aer P 140mm (Red)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 98, Size = 140, AirPressure = 2.71 }; caseFans.Add(Aer_P_140mm__Red_);
            CaseFan Aer_P_140mm__White_ = new CaseFan() { Id = 139, Name = "Aer P 140mm (White)", PartType = PartType.CaseFan, Manufacturer = NZXT, Price = 17, SellPrice = 6, LevelUnlock = 9, LevelPercent = 1, Lighting = null, AirFlow = 98, Size = 140, AirPressure = 2.71 }; caseFans.Add(Aer_P_140mm__White_);
            CaseFan IRIS_12 = new CaseFan() { Id = 140, Name = "IRIS 12", PartType = PartType.CaseFan, Manufacturer = Raijintek, Price = 20, SellPrice = 7, LevelUnlock = 10, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 42, Size = 120, AirPressure = 1.7 }; caseFans.Add(IRIS_12);
            CaseFan IRIS_14_RBW_ADD = new CaseFan() { Id = 141, Name = "IRIS 14 RBW ADD", PartType = PartType.CaseFan, Manufacturer = Raijintek, Price = 25, SellPrice = 8, LevelUnlock = 10, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 70, Size = 140, AirPressure = 2.56 }; caseFans.Add(IRIS_14_RBW_ADD);
            CaseFan MACULA_12_RAINBOW_RGB = new CaseFan() { Id = 142, Name = "MACULA 12 RAINBOW RGB", PartType = PartType.CaseFan, Manufacturer = Raijintek, Price = 30, SellPrice = 10, LevelUnlock = 12, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 45, Size = 120, AirPressure = 1.7 }; caseFans.Add(MACULA_12_RAINBOW_RGB);
            CaseFan SKLERA_12_RBW_ADD = new CaseFan() { Id = 143, Name = "SKLERA 12 RBW ADD", PartType = PartType.CaseFan, Manufacturer = Raijintek, Price = 30, SellPrice = 10, LevelUnlock = 12, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 45, Size = 120, AirPressure = 1.7 }; caseFans.Add(SKLERA_12_RBW_ADD);
            CaseFan Air_Blazer_120R = new CaseFan() { Id = 144, Name = "Air Blazer 120R", PartType = PartType.CaseFan, Manufacturer = SilverStone, Price = 35, SellPrice = 12, LevelUnlock = 34, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 3.53 }; caseFans.Add(Air_Blazer_120R);
            CaseFan Air_Blazer_120RW = new CaseFan() { Id = 145, Name = "Air Blazer 120RW", PartType = PartType.CaseFan, Manufacturer = SilverStone, Price = 35, SellPrice = 12, LevelUnlock = 34, LevelPercent = 1, Lighting = Color.RGB, AirFlow = 60, Size = 120, AirPressure = 3.53 }; caseFans.Add(Air_Blazer_120RW);
            CaseFan AP182__Black_ = new CaseFan() { Id = 146, Name = "AP182 (Black)", PartType = PartType.CaseFan, Manufacturer = SilverStone, Price = 40, SellPrice = 13, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 150, Size = 180, AirPressure = 6.1 }; caseFans.Add(AP182__Black_);
            CaseFan AP182__White_ = new CaseFan() { Id = 147, Name = "AP182 (White)", PartType = PartType.CaseFan, Manufacturer = SilverStone, Price = 40, SellPrice = 13, LevelUnlock = 1, LevelPercent = 1, Lighting = null, AirFlow = 150, Size = 180, AirPressure = 6.1 }; caseFans.Add(AP182__White_);
            CaseFan Pure_Plus_12_LED_RGB = new CaseFan() { Id = 148, Name = "Pure Plus 12 LED RGB", PartType = PartType.CaseFan, Manufacturer = Thermaltake, Price = 20, SellPrice = 7, LevelUnlock = 28, LevelPercent = 20, Lighting = Color.RGB, AirFlow = 56.45, Size = 120, AirPressure = 1.59 }; caseFans.Add(Pure_Plus_12_LED_RGB);
            CaseFan Riing_Plus_12_LED_RGB = new CaseFan() { Id = 149, Name = "Riing Plus 12 LED RGB", PartType = PartType.CaseFan, Manufacturer = Thermaltake, Price = 20, SellPrice = 7, LevelUnlock = 28, LevelPercent = 20, Lighting = Color.RGB, AirFlow = 48.34, Size = 120, AirPressure = 1.54 }; caseFans.Add(Riing_Plus_12_LED_RGB);
            CaseFan Riing_Plus_14_LED_RGB = new CaseFan() { Id = 150, Name = "Riing Plus 14 LED RGB", PartType = PartType.CaseFan, Manufacturer = Thermaltake, Price = 25, SellPrice = 8, LevelUnlock = 28, LevelPercent = 20, Lighting = Color.RGB, AirFlow = 63.19, Size = 140, AirPressure = 1.53 }; caseFans.Add(Riing_Plus_14_LED_RGB);
            #endregion
            if (!_context.CaseFan.Any())
            {
                _context.CaseFan.AddRange(caseFans);
            }
            #region Case
            List<Case> cases = new List<Case>();
            #endregion
            //if (!_context.Case.Any())
            //{
            //    _context.Case.AddRange(cases);
            //}
            _context.SaveChanges();
        }
    }
}
