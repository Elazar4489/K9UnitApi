using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTOs
{
    public class CreateDogDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Breed { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string MicrochipId { get; set; } = string.Empty;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Specialty { get; set; } = string.Empty;

    }

}
