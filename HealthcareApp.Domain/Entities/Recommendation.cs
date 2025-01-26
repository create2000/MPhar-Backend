namespace HealthcareApp.Domain.Entities
{
    public class Recommendation
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Type { get; set; } = string.Empty; // e.g., Allergy Check, Screening
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public Patient? Patient { get; set; } // Navigation property
    }
}
