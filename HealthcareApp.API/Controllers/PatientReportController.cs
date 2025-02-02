using HealthcareApp.Application.DTOs; // Ensure this is the correct namespace for LoginDto
using HealthcareApp.Application.Interfaces; // For IPatientReportService
using HealthcareApp.Domain.Entities; // For AppUser
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientReportController : ControllerBase
    {
        private readonly IPatientReportService _patientReportService;

        public PatientReportController(IPatientReportService patientReportService)
        {
            _patientReportService = patientReportService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientReportDto>>> GetPatientReports()
        {
            var reports = await _patientReportService.GetAllReportsAsync(); // Updated method name
            return Ok(reports);
        }

        [HttpPost]
        public async Task<ActionResult<PatientReportDto>> CreatePatientReport([FromBody] CreatePatientReportDto reportDTO)
        {
            // Read the raw request body
            using (var reader = new StreamReader(Request.Body))
            {
                var body = await reader.ReadToEndAsync();
                Console.WriteLine($"Raw Request Body: {body}");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid:");
                foreach (var error in ModelState)
                {
                    Console.WriteLine($"{error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                return BadRequest(ModelState);
            }

            Console.WriteLine($"Received Report Details: {reportDTO.ReportDetails}");

            var report = await _patientReportService.CreateReportAsync(reportDTO);
            return CreatedAtAction(nameof(GetPatientReport), new { id = report.Id }, report);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<PatientReportDto>> GetPatientReport(int id)
        {
            var report = await _patientReportService.GetReportByIdAsync(id); // Updated method name
            if (report == null)
            {
                return NotFound();
            }
            return Ok(report);
        }
    }
}
