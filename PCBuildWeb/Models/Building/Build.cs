using PCBuildWeb.Models.Entities.Properties;
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
    }
}
