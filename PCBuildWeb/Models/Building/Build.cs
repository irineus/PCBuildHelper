using PCBuildWeb.Models.Entities.Parts;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Building
{
    public class Build
    {
        public Build()
        {
        }

        public Parameter Parameter { get; set; } = new Parameter();
        public List<Component>? Components { get; set; }
        public BuildType? BuildType { get; set; }
        public int TotalBasicScore { get; set; }
        public int TotalOCScore { get; set; }
        public int TotalRankingScore { get; set; }
    }
}
