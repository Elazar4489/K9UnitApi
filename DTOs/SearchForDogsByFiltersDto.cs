namespace K9UnitApi.DTOs
{
    public class SearchForDogsByFiltersDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

    }
}
