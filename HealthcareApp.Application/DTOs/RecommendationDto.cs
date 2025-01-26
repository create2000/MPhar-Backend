namespace HealthcareApp.Application.DTOs
{
    public class RecommendationDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
