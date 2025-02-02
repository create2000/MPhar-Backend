namespace HealthcareApp.Application.DTOs
{
    public class CreateRecommendationDto
    {
        public int IllnessId { get; set; }
        public int PatientId { get; set; }  // ✅ Ensure this property exists
        public int HealthProfessionalId { get; set; }  // ✅ Ensure this property exists
        public string RecommendationText { get; set; } 
    }
}
