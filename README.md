# SimJudge - Online Judge System

A modern online judge system built with .NET 8 and clean architecture principles.

## Architecture Overview

This project follows Clean Architecture principles with clear separation of concerns:

```
SimJudge/
├── SimJudge.Domain/          # Core business entities and interfaces
├── SimJudge.Application/     # Application services and DTOs
├── SimJudge.Infrastructure/  # Data access and external services
├── SimJudge.WebAPI/         # Web API controllers and configuration
├── SimJudge.Frontend/       # Frontend application (HTML/JS)
└── Test/                    # Legacy .NET Framework 4.7.2 project
```

## Project Structure

### Domain Layer (`SimJudge.Domain`)
- **Entities**: Core business objects (User, Problem, Contest, Submission, etc.)
- **Interfaces**: Repository and service contracts
- **Services**: Domain service interfaces

### Application Layer (`SimJudge.Application`)
- **DTOs**: Data Transfer Objects for API communication
- **Services**: Application service implementations
- **Mappings**: AutoMapper profiles for entity-DTO mapping

### Infrastructure Layer (`SimJudge.Infrastructure`)
- **Data**: Entity Framework Core DbContext and configurations
- **Repositories**: Generic repository pattern implementation
- **Unit of Work**: Transaction management

### Web API Layer (`SimJudge.WebAPI`)
- **Controllers**: RESTful API endpoints
- **Configuration**: Dependency injection and middleware setup
- **Authentication**: JWT-based authentication (planned)

### Frontend (`SimJudge.Frontend`)
- **HTML/JS**: Modern responsive web interface
- **Bootstrap**: UI framework for styling
- **API Integration**: JavaScript fetch calls to backend

## Key Features

- **Clean Architecture**: Separation of concerns with dependency inversion
- **Entity Framework Core**: Modern ORM with .NET 8
- **AutoMapper**: Object-to-object mapping
- **Repository Pattern**: Generic repository with Unit of Work
- **Dependency Injection**: Built-in .NET DI container
- **RESTful API**: Clean API design with proper HTTP verbs
- **Responsive Frontend**: Modern web interface

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server or SQL Server LocalDB
- Visual Studio 2022 or VS Code

### Running the Application

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd SimJudge
   ```

2. **Restore packages**
   ```bash
   dotnet restore
   ```

3. **Update connection string** in `SimJudge.WebAPI/appsettings.json`
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your connection string here"
     }
   }
   ```

4. **Run the Web API**
   ```bash
   cd SimJudge.WebAPI
   dotnet run
   ```

5. **Open the frontend**
   - Open `SimJudge.Frontend/index.html` in your browser
   - Or serve it through a web server

### API Endpoints

- **Problems**: `GET/POST/PUT/DELETE /api/problems`
- **Contests**: `GET/POST/PUT/DELETE /api/contests`
- **Submissions**: `GET/POST/PUT/DELETE /api/submissions`
- **Users**: `GET/POST/PUT/DELETE /api/users`

## Migration from Legacy System

This project represents a complete modernization of the original SimJudge system:

### What Changed
- **Framework**: .NET Framework 4.7.2 → .NET 8
- **Architecture**: Monolithic MVC → Clean Architecture
- **ORM**: Entity Framework 6 → Entity Framework Core
- **Frontend**: Server-side rendering → SPA with API
- **Dependency Management**: Manual → Built-in DI container

### What Stayed the Same
- **Core Business Logic**: Problem solving, contest management
- **Data Model**: Similar entity relationships
- **User Experience**: Familiar interface design

## Development Guidelines

### Adding New Features
1. Define entities in `SimJudge.Domain`
2. Create DTOs in `SimJudge.Application`
3. Implement services in `SimJudge.Application`
4. Add repositories in `SimJudge.Infrastructure`
5. Create controllers in `SimJudge.WebAPI`
6. Update frontend in `SimJudge.Frontend`

### Code Style
- Follow C# naming conventions
- Use async/await for I/O operations
- Implement proper error handling
- Write unit tests for business logic
- Use dependency injection throughout

## Future Enhancements

- [ ] JWT Authentication and Authorization
- [ ] Real-time submission processing
- [ ] Code editor integration
- [ ] Contest management features
- [ ] User ranking system
- [ ] Docker containerization
- [ ] CI/CD pipeline

## Contributing

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if applicable
5. Submit a pull request

## License

This project is licensed under the MIT License - see the LICENSE file for details.
