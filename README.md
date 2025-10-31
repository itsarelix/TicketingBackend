Ticketing Backend API

Backend test built with .NET 8, ASP.NET Core Web API and Entity Framework Core.
Uses SQLite as a local database and implements JWT Authentication following RESTful best practices.

پHow to Run
How to Run

Clone the repository:
git clone https://github.com/<your-username>/<repo-name>.git
cd <repo-name>

Restore dependencies and setup the database:
dotnet restore
dotnet ef database update

Run the project:
dotnet run

Open Swagger:
https://localhost:5001/swagger


Seed Data

Automatically adds:
admin => admin@example.com / Admin@123
employee => employee@example.com / Employee@123


Focused on clean code and RESTful API structure
JWT used for authentication
No frontend included
Runs locally without external dependencies


Testing

Use Swagger or Postman to test endpoints.
Examples:
POST /api/auth/login
GET /api/tickets
POST /api/tickets
