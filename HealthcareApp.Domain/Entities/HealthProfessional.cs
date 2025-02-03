using System.Collections.Generic;

namespace HealthcareApp.Domain.Entities
{
    public class HealthProfessional
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string ContactInfo { get; set; }
        public string Role { get; set; }

        public ICollection<Illness> Illnesses { get; set; } = new List<Illness>(); // Navigation property
    }
}