using K9UnitApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTOs
{
    public class CreateTrainigDto
    {
        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public string TrainingType { get; set; } = string.Empty;
        public int PerformanceScore { get; set; }
        public string Evaluator { get; set; } = string.Empty;
    }
}
