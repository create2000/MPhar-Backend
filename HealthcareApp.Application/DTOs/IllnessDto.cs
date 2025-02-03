namespace HealthcareApp.Application.DTOs
{
    public class IllnessDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PatientId { get; set; }  // Foreign Key (int)
        public int? AssignedHealthProfessionalId { get; set; }  // Foreign Key (nullable int)
        public string Recommendation { get; set; }
        public DateTime? RecommendationDate { get; set; }
    }
}