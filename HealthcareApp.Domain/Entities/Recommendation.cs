namespace HealthcareApp.Domain.Entities
{
    // HealthcareApp.Domain/Entities/Recommendation.cs
public class Recommendation
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int HealthProfessionalId { get; set; }
    public string RecommendationText { get; set; }
    public bool IsCompleted { get; set; } // Whether the patient has marked it as completed
    public DateTime DateCreated { get; set; }
}

}
