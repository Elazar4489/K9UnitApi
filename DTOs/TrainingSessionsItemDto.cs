namespace K9UnitApi.DTOs
{
    public class TrainingSessionsItemDto
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }
        public int PerformanceScore { get; set; }
        public string DogName { get; set; } = string.Empty;
    }
}
