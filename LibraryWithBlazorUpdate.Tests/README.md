# LibraryWithBlazorUpdate.Tests

Comprehensive test suite for the Library Management Blazor application using **xUnit**, **bUnit**, and **Moq**.

## Structure

### Tests/
Unit tests for models and business logic:

- **LibraryItemTests.cs** - LibraryItem model tests
- **BookTests.cs** - Book model tests
- **MovieTests.cs** - Movie model tests
- **MagazineTests.cs** - Magazine model tests
- **MemberTests.cs** - Member model tests
- **LoanTests.cs** - Loan model tests
- **ClassManagerTests.cs** - ClassManager business logic tests

### Components/
Blazor component tests using bUnit:

- **LibraryItemIndexComponentTests.cs** - Library items list component
- **LibraryItemCreateComponentTests.cs** - Create library item component
- **MemberIndexComponentTests.cs** - Members list component
- **MemberCreateComponentTests.cs** - Create member component
- **LoanIndexComponentTests.cs** - Loans list component

### Integration/
Integration tests with database:

- **DbContextTests.cs** - Entity Framework Core DbContext tests
- **LoanIntegrationTests.cs** - Loan creation/deletion integration tests

## Running Tests

Run all tests:
```bash
dotnet test
```

Run tests with verbose output:
```bash
dotnet test --verbosity detailed
```

Run specific test class:
```bash
dotnet test --filter "ClassName"
```

List all available tests:
```bash
dotnet test --list-tests
```

## Test Status

All test functions are marked with **EMPTY** and ready for implementation. Each test follows the **AAA pattern** (Arrange, Act, Assert).

## Dependencies

- **xunit** - Test framework
- **xunit.runner.visualstudio** - Visual Studio test runner integration
- **bunit** - Blazor component testing library
- **Moq** - Mocking framework
- **Microsoft.EntityFrameworkCore.InMemory** - In-memory database for testing

## Notes

- Component tests use bUnit's TestContext for component rendering
- Integration tests use Entity Framework Core InMemory database
- All tests are isolated and can run independently
- Follow AAA pattern: Arrange ? Act ? Assert
