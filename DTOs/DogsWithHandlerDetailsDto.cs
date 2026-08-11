using K9UnitApi.Enums;
using System.ComponentModel.DataAnnotations;

namespace K9UnitApi.DTOs
{
    public class DogsWithHandlerDetailsDto
    {
        public int Id { get; set; }
       
        public string Name { get; set; } = string.Empty;
       
        public string Breed { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
       
        public string Status { get; set; } = string.Empty;
        public string? FullName { get; set; }
        public string? Rank { get; set; }
    }
}
