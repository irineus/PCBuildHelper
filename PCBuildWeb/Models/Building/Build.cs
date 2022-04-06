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

        public Parameter Parameter { get; set; }

        public ICollection<Component> Components { get; set; }

        public int GetTotalBasicScore()
        {
            return CalculateBuildTotalScore("basic");
        }

        public int GetTotalOCScore()
        {
            return CalculateBuildTotalScore("overclocked");
        }

        public int GetTotalRankingScore()
        {
            return CalculateBuildTotalScore("ranking");
        }

        //Calculate build total scores
        private int CalculateBuildTotalScore(string scoreType)
        {
            int totalBasicScore = 0;
            int totalOCScore = 0;
            int totalRankingScore = 0;
            if (Components.Any())
            {
                foreach (var component in Components.Where(c => c.BuildPart!.PartType == PartType.CPU || c.BuildPart.PartType == PartType.GPU).ToList())
                {
                    switch (component.BuildPart!.PartType)
                    {
                        case PartType.CPU:
                            CPU cpuComponent = (CPU)component.BuildPart;
                            totalBasicScore += cpuComponent.BasicCPUScore;
                            totalOCScore += cpuComponent.OverclockedCPUScore;
                            totalRankingScore += cpuComponent.RankingScore;
                            break;
                        case PartType.GPU:
                            GPU gpuComponent = (GPU)component.BuildPart;
                            totalBasicScore += gpuComponent.SingleGPUScore;
                            totalOCScore += gpuComponent.OverclockedSingleGPUScore ?? 0;
                            totalRankingScore += gpuComponent.RankingScore;
                            break;
                    }
                }
            }
            return scoreType switch
            {
                "basic" => totalBasicScore,
                "overclocked" => totalOCScore,
                "ranking" => totalRankingScore,
                _ => totalBasicScore,
            };
        }
    }
}
