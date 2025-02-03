# MPhar-Backend

# Backend Documentation

## Project Overview
The backend of this project is built using **.NET Core** with **Entity Framework Core** for database management and **ASP.NET Identity** for authentication. It provides APIs to handle user authentication, patient health issue submissions, assignment to health professionals, and recommendation management.

## Technologies Used
- **.NET Core**
- **Entity Framework Core** (EF Core)
- **ASP.NET Identity**
- **SQL Server**
- **Swagger** for API documentation
- **JWT Authentication**

---

## Getting Started

### Prerequisites
Ensure you have the following installed:
1. **.NET SDK** (latest stable version) - [Download here](https://dotnet.microsoft.com/en-us/download/dotnet)
2. **SQL Server** (Express or any edition) - [Download here](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
3. **Visual Studio** or **VS Code** (with C# extensions)
4. **Postman** (for API testing, optional)

### Setting Up the Project
1. **Clone the repository:**
   ```sh
   git clone <repository_url>
   cd <backend-folder>
   ```

2. **Set up environment variables:**
   - Copy `appsettings.example.json` and rename it to `appsettings.json`.
   - Configure your **database connection string** under `ConnectionStrings`.

3. **Restore dependencies:**
   ```sh
   dotnet restore
   ```

4. **Apply database migrations:**
   ```sh
   dotnet ef database update
   ```

5. **Run the application:**
   ```sh
   dotnet run
   ```

6. **Access Swagger API documentation:**
   - Open your browser and navigate to `https://localhost:<port>/swagger/index.html`

---

## API Overview
### Authentication
- **POST** `/api/auth/register` → Register a new user
- **POST** `/api/auth/login` → Login and receive JWT token

### Patients
- **POST** `/api/patient/submit-issue` → Submit a health issue
- **GET** `/api/patient/issues` → Get list of submitted issues

### Admin
- **GET** `/api/admin/issues` → Get all patient health issues
- **POST** `/api/admin/assign` → Assign health issue to a professional

### Health Professionals
- **GET** `/api/professional/issues` → View assigned issues
- **POST** `/api/professional/recommend` → Submit recommendations

---

## Workflow Explanation
1. A **patient** submits a health issue.
2. The **admin** sees the issue on their dashboard and assigns it to a **health professional**.
3. The **health professional** reviews the issue and provides a recommendation.
4. The **recommendation is sent back** to the patient for review.

### Data Not Displaying on Dashboards
During implementation, there was an issue where submitted data was not appearing in the respective dashboards. Possible reasons:
- Missing **proper entity relationships** in the database.
- Issues with **Eager vs Lazy loading** in EF Core.
- Queries **not properly filtering** assigned issues.
- Debugging might require adding explicit `Include()` statements for related entities in queries.

A suggested fix is to review queries used in controllers to ensure they correctly retrieve related data and update the API endpoints accordingly.

---

## Deployment
1. **Build the application:**
   ```sh
   dotnet publish -c Release -o out
   ```

---

## Conclusion
This documentation provides a guide to setting up and running the backend, explains the API workflow, and highlights the data issue encountered. Further improvements can be made by debugging queries and ensuring proper data retrieval for dashboards.

