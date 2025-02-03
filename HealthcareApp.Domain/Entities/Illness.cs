using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareApp.Domain.Entities
{
    public class Illness
    {
        [Key]
        public string Id { get; set; } // Id is a string (GUID)

        public string Name { get; set; }
        public string Description { get; set; }

        public string PatientId { get; set; }  // Foreign Key (int) - DO NOT CHANGE
        public AppUser Patient { get; set; }

        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        public int? AssignedHealthProfessionalId { get; set; }  // Foreign Key (nullable int) - DO NOT CHANGE
        public HealthProfessional AssignedHealthProfessional { get; set; }

        public string Recommendation { get; set; }
        public DateTime? RecommendationDate { get; set; }
    }
}