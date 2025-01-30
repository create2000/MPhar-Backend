namespace HealthcareApp.Application.DTOs
{

public class RecommendationDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int HealthProfessionalId { get; set; }
    public string RecommendationText { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime DateCreated { get; set; }
}

}
