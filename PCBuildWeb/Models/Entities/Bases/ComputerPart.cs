using PCBuildWeb.Models.Entities.Properties;
using PCBuildWeb.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PCBuildWeb.Models.Entities.Bases
{
    public class ComputerPart : ICloneable
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Part Name/Model")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(256, ErrorMessage = "{0} should have between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; }
        [Display(Name = "Part Type")]
        [EnumDataType(typeof(PartType))]
        public PartType PartType { get; set; }
        [ForeignKey("ManufacturerId")]
        public Manufacturer? Manufacturer { get; set; }
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Buy Price (new)")]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Currency)]
        [Range(0.01, 9999.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double Price { get; set; }
        [Display(Name = "Sell Price (used)")]
        [Required(ErrorMessage = "{0} is required")]
        [DataType(DataType.Currency)]
        [Range(0.01, 9999.99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public double SellPrice { get; set; }
        [Display(Name = "Unlocked at Level")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int LevelUnlock { get; set; }
        [Display(Name = "Unlocked at Level Percent")]
        [Required(ErrorMessage = "{0} is required")]
        [Range(1, 99, ErrorMessage = "{0} should be a value between {1} and {2}")]
        public int LevelPercent { get; set; }
        [EnumDataType(typeof(Color))]
        public Color? Lighting { get; set; }
        [Display(Name = "Commit Part?")]


        public object Clone()
        {
            var computerPart = (ComputerPart)MemberwiseClone();
            if (Manufacturer is not null)
            {
                computerPart.Manufacturer = (Manufacturer)Manufacturer.Clone();
            }
            return computerPart;
        }
    }
}
