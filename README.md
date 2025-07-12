# DitLibrary - Library Management System

## 🎯 Development Approach

# Technical Project Overview

## Core Architecture

The `Core` layer of this project was built using Test-Driven Development (TDD) methodology, following rich domain model patterns. I implemented `DomainServices` to handle key operations such as borrowing and returning books. The project's core architecture is straightforward and well-structured, with comprehensive test coverage ensuring reliability.

While the Web project requires additional refactoring to meet optimal standards, time constraints prevented me from completing this refactoring process entirely. The foundational architecture remains solid, with the domain logic properly separated and thoroughly tested.

## Validation and API Documentation

I implemented FluentValidation for DTO validation and utilized an AutoValidation mechanism to adhere to the DRY (Don't Repeat Yourself) principle. The validation logic is documented through OpenAPI configuration, ensuring comprehensive API documentation.

Any DTO that implements `AbstractValidator` is automatically registered with the OpenAPI specification, providing seamless integration between validation rules and API documentation. This approach eliminates manual documentation maintenance while ensuring that validation constraints are clearly communicated to API consumers.

## Authorization System

For the authorization implementation, I designed a four-table structure consisting of `User`, `Role`, `Permission`, and `RolePermissions`. The `Role` and `Permission` entities are connected through the `RolePermissions` junction table, while each user is directly assigned to a role in the `Role` table.

Permissions and roles are stored as data records rather than being hardcoded as specific table columns, providing flexibility for dynamic permission management. This data-driven approach allows for easy addition, modification, or removal of roles and permissions without requiring schema changes. Although roles and permissions are currently defined as enums in the codebase to ensure type safety and follow secure object-oriented programming principles, this approach allows for easy extension and modification without requiring significant architectural changes.

The data-driven design enables seamless addition, modification, or removal of roles and permissions while maintaining the benefits of compile-time safety through enum validation.

Although I could have implemented a many-to-many relationship between `User` and `Role` tables using an additional junction table, the current project scope doesn't require this level of complexity. The direct user-to-role assignment approach is sufficient for the current requirements and maintains simplicity without over-engineering the solution.

## Controller-Level Permission Management

For controller-level permission management, I implemented a comprehensive authorization system using `AuthorizationHandler`, `Policy AuthorizationHandler`, and a custom `HasPermission` attribute. This approach ensures well-maintained and well-structured code while providing flexibility for future extensions and modifications.

The authorization framework leverages ASP.NET Core's built-in policy-based authorization system, allowing for declarative permission checks at the controller and action levels. The `HasPermission` attribute provides a clean, readable way to specify required permissions directly on controller methods, promoting code clarity and maintainability.

This architecture separates authorization concerns from business logic while maintaining a consistent and extensible permission validation mechanism throughout the application.

Currently, the `AuthorizationHandler` queries the database on every request to verify permissions. While this ensures real-time permission validation, I could optimize performance by embedding permissions directly in the JWT token, reducing database calls and improving response times. However, since the project doesn't implement a sign-out mechanism, the database query approach ensures immediate permission changes take effect without requiring users to re-authenticate.

## Data Persistence

I implemented Entity Framework Core's Fluent Configuration to maintain clear separation between data persistence and domain logic. The domain models are designed as Plain Old CLR Objects (POCOs) to ensure maximum testability and framework independence.

## CI/CD Pipeline

I configured GitHub Actions CI/CD pipelines that automatically enforce code formatting standards and execute comprehensive tests on each commit, maintaining code quality standards and streamlining the development workflow.


## 🚀 Quick Start

### Prerequisites
- .NET 9.0 SDK
- PostgreSQL database
- IDE (Visual Studio, VS Code, or Rider)

### 1. Clone and Setup
```bash
git clone <repository-url>
cd DitLibrary
```

### 2. Database Configuration
1. Install PostgreSQL
2. Create a new database
3. Update `Web/appsettings.json` with your database connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ditlibrary;Username=your_username;Password=your_password"
  },
  "Jwt": {
    "Key": "your-super-secret-key-here-make-it-long-and-secure",
    "Issuer": "DitLibrary",
    "Audience": "DitLibraryUsers"
  }
}
```

### 3. Run Database Migrations
```bash
cd Web
dotnet ef database update
```

### 4. Seed Initial Data (Optional)
The application includes seed data for testing and development:

```bash
# The database will be automatically seeded with:
# - Default roles (Member, Admin, Librarian)
# - Default permissions (CanCreateBooks, CanUpdateBooks, etc.)
# - Sample categories (Fiction, Non-Fiction, Science, etc.)
# - Sample books for testing
```

**Note**: Seed data is automatically created when the database is first initialized. You can modify the seed data in the `Web/Persistence/ApplicationDbContext.cs` file.

### 5. Start the Application
```bash
# From solution root
dotnet run --project Web

# Or from Web directory
cd Web
dotnet run
```

### 6. Access the Application
- **API**: https://localhost:5094
- **Scalar UI**: https://localhost:5094/scalar


## 🧪 Run Tests
```bash
dotnet test
```

## 📚 API Features
- User registration and authentication
- Book management (CRUD operations)
- Book borrowing and returning
- Search and filtering
- Role-based access control

## 🔐 Default Roles
- **Member**: Can borrow and return books
- **Admin**: Full access to all features
- **Librarian**: Can manage books and borrowing

---




**Built with ASP.NET Core 9.0**
