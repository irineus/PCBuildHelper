using System.ComponentModel.DataAnnotations;

namespace PCBuildWeb.Models.Entities.Bases
{
    public abstract class PartProperty
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Part Property Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(256, ErrorMessage = "{0} should have between {2} and {1} characters", MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;
    }
}
