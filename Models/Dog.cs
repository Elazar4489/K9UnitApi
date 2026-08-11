using K9UnitApi.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace K9UnitApi.Models
{
    [Index(nameof(MicrochipId), IsUnique = true)]
    public class Dog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Breed { get; set; } = string.Empty;
        [Required]
        [MaxLength(15)]
        public string MicrochipId { get; set; } = string.Empty;
        [Required]
        //חייב להיות תאריך בעבר )לא היום ולאבעתיד(
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EnumDataType(typeof(SpecialtyEnum))]
        public string Specialty { get; set; } = string.Empty;
        [Required]
        [EnumDataType(typeof(StatusDogEnum))]
        public string Status { get; set; } = string.Empty;
        public Handler? Handler { get; set; }
        public int? HandlerId { get; set; }
        public List<TrainingSession> TrainingSessions { get; set; } = new();

    }
}
