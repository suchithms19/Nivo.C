# Nivo.API

A .NET API for managing appointments and patients.

## Folder Structure

```
Nivo.API/
├── Controllers/          # API controllers (HTTP endpoints)
├── Data/                 # Database context (AppDbContext)
├── Extensions/           # Extension methods (OpenAPI/Swagger)
├── Migrations/           # Entity Framework migrations
├── Models/
│   ├── Domain/          # Domain models (Appointment, Patient)
│   └── DTO/             # Data Transfer Objects (request/response)
├── Repositories/
│   ├── Interface/       # Repository interfaces
│   └── Implementation/  # Repository implementations
└── Services/            # Business logic services
```

## API Routes

Base URL: `/api/appointments`

### Book Appointment
- **POST** `/api/appointments/{userId}/book`
  - Creates a new appointment and patient
  - Request Body: `BookAppointmentDto` (StartTime, Name, PhoneNumber, Age)
  - Response: Success message

### Get All Appointments
- **GET** `/api/appointments`
  - Returns list of all appointments with patient details
  - Response: `AppointmentDto[]`

### Get Appointment by ID
- **GET** `/api/appointments/{id}`
  - Returns a single appointment by GUID
  - Response: `AppointmentDto` or 404 if not found

## Architecture

- **Controllers**: Handle HTTP requests/responses
- **Services**: Business logic and orchestration
- **Repositories**: Data access layer (EF Core)
- **DTOs**: Data transfer objects for API contracts

