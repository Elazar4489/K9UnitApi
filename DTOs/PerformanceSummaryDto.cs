namespace K9UnitApi.DTOs
{
    public class PerformanceSummaryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public int TrainingNum { get; set; }
        public double? Avarage { get; set; } = null;
    }
}
