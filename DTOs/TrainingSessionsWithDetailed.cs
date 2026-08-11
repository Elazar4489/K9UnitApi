using K9UnitApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTOs
{
    public class TrainingSessionsWithDetailed
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public string TrainingType { get; set; } = string.Empty;
        public int PerformanceScore { get; set; }
        public bool Passed { get; set; }
        public string Evaluator { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string? FullName { get; set; }

    }
}
