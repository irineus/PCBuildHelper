using PCBuildWeb.Models.Entities.Bases;
using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Parts
{
    public class PSU : ComputerPart, ICloneable
    {
        [Display(Name = "Max Power")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 2000, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Wattage { get; set; }
        [Display(Name = "Length")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(100, 300, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int Length { get; set; }
        [Display(Name = "Type")]
        [Required(ErrorMessage = "{0} is required")]
        [EnumDataType(typeof(PSUType))]
        public PSUType Type { get; set; }
        [ForeignKey("PSUSizeId")]
        public PSUSize PSUSize { get; set; } = new PSUSize();
        [Display(Name = "Form Factor")]
        public int PSUSizeId { get; set; }

        public new object Clone()
        {
            var psuClone = (PSU)MemberwiseClone();
            if (PSUSize is not null)
            {
                psuClone.PSUSize = (PSUSize)PSUSize.Clone();
            }
            return psuClone;
        }
    }
}
