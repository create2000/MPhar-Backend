using HealthcareApp.Infrastructure.Data; // For AppDbContext
using HealthcareApp.Domain.Interfaces; // For IPatientService and IPatientRepository
using HealthcareApp.Infrastructure.Repositories; // For PatientRepository
using HealthcareApp.Application.Services; // For PatientService and AuthService
using HealthcareApp.Application.Interfaces; // For IPatientService 
using Microsoft.EntityFrameworkCore; // For UseSqlServer
using Microsoft.AspNetCore.Identity; // For IdentityRole and AppUser
using HealthcareApp.Infrastructure.Configuration; // For JwtSettings
using HealthcareApp.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Register Password Hasher
builder.Services.AddScoped<IPasswordHasher<AppUser>, PasswordHasher<AppUser>>();

// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add Repositories and Services
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
