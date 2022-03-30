using PCBuildWeb.Data;

namespace PCBuildWeb.Services.Building
{
    public class BuildService
    {
        public readonly PCBuildWebContext _context;

        public BuildService(PCBuildWebContext context)
        {
            _context = context;
        }


    }
}
