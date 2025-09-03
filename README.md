# 🕒 Employee Time Tracking Application

![.NET](https://img.shields.io/badge/.NET-8.0-blue?style=flat-square&logo=dotnet)
![Build](https://img.shields.io/badge/build-passing-brightgreen?style=flat-square)
![Tests](https://img.shields.io/badge/tests-44%20passing-brightgreen?style=flat-square)
![License](https://img.shields.io/badge/license-MIT-blue?style=flat-square)

A modern **ASP.NET Core MVC** application for tracking employee working hours, built with **CQRS architecture** and **Clean Code principles**.

## ✨ Key Features

- 👥 **Employee Management** - Complete CRUD operations for employee data
- ⏱️ **Time Tracking** - Record arrival/departure times with multiple intervals
- 🔄 **Overlap Handling** - Intelligent algorithm to handle overlapping work periods
- 📊 **Automatic Calculation** - Real-time total hours computation
- ✅ **Comprehensive Validation** - Data integrity with FluentValidation
- 🧪 **Full Test Coverage** - 44 unit and integration tests

## 🛠️ Technology Stack

- **Framework:** ASP.NET Core 8.0 MVC
- **Architecture:** CQRS with MediatR
- **Database:** Entity Framework Core + SQL Server
- **Mapping:** AutoMapper
- **Validation:** FluentValidation
- **Testing:** xUnit, Moq
- **UI:** Bootstrap, Razor Views

## 🏗️ Architecture Overview

This application implements **CQRS (Command Query Responsibility Segregation)** pattern, separating read and write operations for better scalability and maintainability.

### Benefits of CQRS:

- **Scalability:** Independent optimization for reading and writing operations
- **Clear Separation of Concerns:** Distinct models and logic for commands and queries
- **Flexibility:** Easy to extend and modify without affecting other parts
- **Testability:** Each handler can be tested in isolation

The application follows **SOLID principles** and **Clean Architecture** patterns.

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full version)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/sasa-yovanovicc/CQRSExample.git
   cd CQRSExample
   ```

2. **Update connection string**
   Edit `appsettings.json` in the `EmployeeTimeTracking` project:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EmployeeTimeTrackingDb;Trusted_Connection=true;"
     }
   }
   ```

3. **Apply database migrations**
   ```bash
   cd EmployeeTimeTracking
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

5. **Run tests**
   ```bash
   cd ../EmployeeTimeTracking.Test
   dotnet test
   ```

The application will be available at `https://localhost:5001`

## 📋 Usage Examples

### Adding an Employee
1. Navigate to the Employees page
2. Click "Add New Employee"
3. Fill in: First Name, Last Name, Position, and Hire Date
4. Submit the form

### Recording Work Time
1. Select an employee from the list
2. Click "Check-In" to start a work interval
3. Click "Check-Out" to end the interval
4. Total hours are automatically calculated

### Handling Overlapping Periods
The system automatically merges overlapping work intervals:

**Example:**
- Interval 1: 9:00 AM - 1:00 PM (4 hours)
- Interval 2: 11:00 AM - 5:00 PM (6 hours)
- **Result:** 9:00 AM - 5:00 PM (8 hours total)

## 🔒 Security Note

Connection strings and sensitive configurations are stored in `appsettings.json` for development purposes. 

**For production deployments:**
- Use **Azure Key Vault** or **AWS Secrets Manager**
- Implement **environment variables** for sensitive data
- Enable **application insights** for monitoring
- Configure **HTTPS** enforcement

## 📁 Project Structure

```
EmployeeTimeTracking/
├── 📂 Commands/              # CQRS Commands for data modification
├── 📂 Controllers/           # MVC Controllers handling HTTP requests
├── 📂 Data/                  # Data Access Layer
│   ├── 📂 Entities/          # Domain entities (Employee, WorkInterval)
│   └── 📂 Repositories/      # Repository pattern implementations
├── 📂 Handlers/              # CQRS Command & Query handlers
├── 📂 Interfaces/            # Service contracts and abstractions
├── 📂 Mapping/               # AutoMapper profiles
├── 📂 Migrations/            # EF Core database migrations
├── 📂 Models/                # Data Transfer Objects (DTOs)
├── 📂 Queries/               # CQRS Queries for data retrieval
├── 📂 Services/              # Business logic services
├── 📂 Validators/            # FluentValidation rules
├── 📂 Views/                 # Razor view templates
└── 📂 wwwroot/               # Static files (CSS, JS, images)

EmployeeTimeTracking.Test/
├── 📂 UnitTest/              # Unit tests
│   ├── 📂 Controllers/       # Controller tests
│   ├── 📂 Mapping/           # AutoMapper tests
│   ├── 📂 Repositories/      # Repository tests
│   ├── 📂 Services/          # Service layer tests
│   └── 📂 Validators/        # Validation tests
└── 📂 IntegrationTests/      # Integration tests
```

## 🧪 Testing

The application includes **comprehensive testing coverage** with **44 tests** covering all critical components:

| Test Category | Coverage | Description |
|---------------|----------|-------------|
| **Controllers** | ✅ Complete | HTTP request/response handling |
| **Services** | ✅ Complete | Business logic validation |
| **Repositories** | ✅ Complete | Data access operations |
| **Mappers** | ✅ Complete | Object mapping verification |
| **Validators** | ✅ Complete | Data validation rules |

### Test Results
```bash
Test summary: total: 44, failed: 0, succeeded: 44, skipped: 0
```

### Key Test Scenarios
- ✅ **Overlap handling algorithm** with various interval combinations
- ✅ **CRUD operations** for employees and work intervals
- ✅ **Validation rules** for business constraints
- ✅ **Edge cases** and error handling
- ✅ **Integration tests** for end-to-end workflows

## 🎯 Requirements Implementation

✅ **Employee Management**
- Display employee list with First Name, Last Name, Job Title, Date of Joining, Total Hours

✅ **Time Tracking**
- Record arrival and departure times with date and time precision
- Support multiple work intervals per employee

✅ **Overlap Resolution**
- Automatically merge overlapping time periods
- Calculate accurate total worked hours

### Example Overlap Calculation:
```
Interval 1: 09:00 - 13:00 (4 hours)
Interval 2: 11:00 - 17:00 (6 hours)
Result:     09:00 - 17:00 (8 hours total)
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Saša Jovanović**
- GitHub: [@sasa-yovanovicc](https://github.com/sasa-yovanovicc)
- LinkedIn: [Your LinkedIn Profile](https://linkedin.com/in/your-profile)

---

⭐ **Star this repository if you find it helpful!**

