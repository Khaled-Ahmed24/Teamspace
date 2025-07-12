# Team Space Web Application API

## Description

The Team Space Web Application is a comprehensive digital platform designed specifically for the Faculty of Computer Science and Information Technology at Assiut University. This system serves as an integrated solution that replaces manual processes and provides a centralized platform for academic activities, communication, and course management.

The application supports three main user roles:
- **Faculty Admin**: Manages user accounts, general news, schedules, and system administration
- **Professors & TAs**: Manage subjects, upload materials, create exams, conduct live lectures, and communicate with students
- **Students**: Access course materials, take exams, attend live lectures, and communicate with faculty

### Key Features

- **User Management**: Role-based access control with secure authentication
- **Subject Management**: Subject selection for professors and enrollment for students
- **Course Materials**: Upload, download, and manage study materials and assignments
- **Exam System**: Create, take, and automatically evaluate exams with AI integration
- **Live Lectures**: Schedule and conduct virtual classroom sessions
- **Real-time Communication**: Chat system for all users
- **News Management**: General faculty news and subject-specific announcements
- **Schedule Management**: Lecture schedules and faculty office hours
- **Guidance System**: Indoor navigation and faculty location assistance
- **Faculty Regulations**: Access to current faculty regulations and policies

## Technology Stack

### Backend
- **Framework**: .NET Core 8.0
- **Database**: SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Architecture**: Repository Pattern
- **API Documentation**: Swagger/OpenAPI

### Frontend (Separate Repository)
- **Framework**: React.js
- **Communication**: RESTful API calls

### External Integrations
- **AI Services**: For automatic exam evaluation
- **Email Services**: For notifications and communications
- **Live Lecture Services**: For virtual classroom functionality

## Prerequisites

Before running the project, ensure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (Local or Remote)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (Optional)

## Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/Khaled-Ahmed24/Teamspace.git
cd Teamspace
```

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Database Configuration

1. **Update Connection String**: 
   - Open `appsettings.json`
   - Update the `ConnectionStrings` section with your SQL Server connection details:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=TeamSpaceDB;Trusted_Connection=true;TrustServerCertificate=true;"
  }
}
```

2. **Apply Migrations**:
   
```bash
# Add migration (if needed)
dotnet ef migrations add InitialCreate

# Update database
dotnet ef database update
```

### 4. Configuration Settings

Configure the following settings in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR_CONNECTION_STRING"
  },
  "JwtSettings": {
    "SecretKey": "YOUR_JWT_SECRET_KEY",
    "Issuer": "TeamSpaceAPI",
    "Audience": "TeamSpaceUsers",
    "ExpiryMinutes": 60
  },
  "EmailSettings": {
    "SmtpServer": "YOUR_SMTP_SERVER",
    "Port": 587,
    "Username": "YOUR_EMAIL",
    "Password": "YOUR_EMAIL_PASSWORD"
  },
  "AISettings": {
    "ApiKey": "YOUR_AI_SERVICE_API_KEY",
    "EndpointUrl": "YOUR_AI_SERVICE_ENDPOINT"
  },
  "LiveLectureSettings": {
    "ServiceUrl": "YOUR_LIVE_LECTURE_SERVICE_URL",
    "ApiKey": "YOUR_LIVE_LECTURE_API_KEY"
  }
}
```

### 5. Run the Application

```bash
dotnet run
```

The API will be available at:
- **HTTP**: `http://localhost:5000`
- **HTTPS**: `https://localhost:5001`
- **Swagger Documentation**: `https://localhost:5001/swagger`

## Default Admin Account

The system comes with a pre-configured admin account for initial setup:

- **Email**: `admin@aun.edu.eg`
- **Password**: `Admin123!`

⚠️ **Important**: Change the default admin password after first login for security purposes.

## Project Structure

```
TeamSpace/
├── Controllers/           # API Controllers
├── Models/               # Data Models/Entities
├── DTOs/                 # Data Transfer Objects
├── Services/             # Business Logic Services
├── Repositories/         # Data Access Layer (Repository Pattern)
├── Interfaces/           # Service and Repository Interfaces
├── Data/                 # Database Context and Configurations
├── Migrations/           # Entity Framework Migrations
├── Middleware/           # Custom Middleware
├── Helpers/              # Utility Classes and Extensions
├── appsettings.json      # Configuration Settings
└── Program.cs            # Application Entry Point
```

## API Documentation

Once the application is running, you can access the interactive API documentation at:

- **Swagger UI**: `https://localhost:5001/swagger`
- **OpenAPI Specification**: `https://localhost:5001/swagger/v1/swagger.json`

## Database Schema

The application uses the following main entities:

- **Users**: Base user information (Admin, Professor, Student)
- **Subjects**: Course subjects and their details
- **Channels**: Subject-specific communication channels
- **Materials**: Study materials and assignments
- **Exams**: Exam questions and configurations
- **News**: General and channel-specific announcements
- **Messages**: Chat and communication data
- **Schedules**: Lecture schedules and office hours
- **LiveLectures**: Virtual classroom sessions

## User Roles & Permissions

### Faculty Admin
- Manage all user accounts (Create, Read, Update, Delete)
- Manage general faculty news and announcements
- Upload and manage lecture schedules and office hours
- Access to all system features and data

### Professors & TAs
- Select and manage subjects
- Create and manage subject channels
- Upload study materials and assignments
- Create and evaluate exams
- Conduct live lectures
- Manage channel-specific news
- Chat with students and other faculty

### Students
- Enroll in subjects
- Access course materials and assignments
- Take online exams
- Attend live lectures
- Chat with professors and TAs
- View faculty news and announcements
- Access guidance and schedule information

## Security Features

- **JWT Authentication**: Secure token-based authentication
- **Role-based Authorization**: Different access levels for different user types
- **Password Hashing**: Secure password storage using bcrypt
- **HTTPS Enforcement**: Secure data transmission
- **Input Validation**: Comprehensive data validation and sanitization
- **Rate Limiting**: API rate limiting to prevent abuse

## Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Team Members

- **CS 162021097** – Hatem Mahmoud Ahmed
- **CS 162021027** – Ahmed Mohamed Abdelaziz
- **CS 162021180** – Abdelrahman Saad Ahmed
- **CS 162021113** – Khaled Ahmed Mohamed
- **CS 162021112** – Khaled Ahmed Kamal

## Supervisor

- **Prof. Noha Mostafa** - Faculty of Computer & Information Technology, Assiut University

## License

This project is developed as part of the academic requirements for the Faculty of Computer Science and Information Technology at Assiut University.

## Support

For support or questions about the project, please contact the development team or refer to the project documentation.

---

**Note**: This is the backend API for the Team Space Web Application. The frontend React application is maintained in a separate repository.
