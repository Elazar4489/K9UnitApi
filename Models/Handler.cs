using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.Models
{
    [Index(nameof(PersonalNumber), IsUnique = true)]
    public class Handler
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [MaxLength(10)]
        public string PersonalNumber { get; set; } = string.Empty;
        [Required]
        [MaxLength(30)]
        public string Rank { get; set; } = string.Empty;
        [Required]
        [Range(0,40)]
        public int YearsOfExperience { get; set; }
        [Required]
        [MaxLength(100)]
        public string BaseAssigned { get; set; } = string.Empty;
        public Dog? Dog { get; set; }
    }
}
