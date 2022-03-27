using PCBuildWeb.Data;
using PCBuildWeb.Models.Entities.Properties;

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
            _context.SaveChanges();
        }
    }
}
