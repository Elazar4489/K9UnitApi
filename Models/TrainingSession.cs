using K9UnitApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.Models
{
    public class TrainingSession
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [PastDate]
        public DateTime SessionDate { get; set; }
        [Required]
        [Range(1, 300)]
        public int DurationMinutes { get; set; }
        [Required]
        [EnumDataType(typeof(TrainingTypeEnum))]
        public string TrainingType { get; set; } = string.Empty;
        [Required]
        [Range(0, 100)]
        public int PerformanceScore { get; set; }
        public bool Passed { get; set; }
        [Required]
        [MaxLength(100)]
        public string Evaluator { get; set; } = string.Empty;
        public Dog Dog { get; set; } = null!;
        public int? DogId { get; set; }
    }
}
