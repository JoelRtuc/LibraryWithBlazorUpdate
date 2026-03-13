# LibraryWithBlazorUpdate And Tests

## Integration and Database

- **This project uses a dbContext class to communicate with a database, it has a connection string and uses migrations to convert c# into SQL**
- **This project uses EntityFrameworksCore for integration**

## Usage

- **Open Files and run**
- **Blazor app will oven with interactive server**
- **Navigate Left Menu to find Library items, Loans, Members**
- **You can loan in the loan tab**
- **Or you can loan directly in the library items list or in the indevidual edit page**
- **You can return it the same way as loading, a return button will appear if the item is available, No admin privleges**
- **In loan tab press delete to return**
- **In all edit pages you can CRUD**

Comprehensive test suite for the Library Management Blazor application using **xUnit**, **bUnit**, and **Moq**.

## Structure

### Tests/
Unit tests for models and business logic:

- **LibraryItemTests.cs** - LibraryItem model tests
- **MemberTests.cs** - Member model tests
- **LoanTests.cs** - Loan model tests

### Integration/
Integration tests with database:

- **DbContextTests.cs** - Entity Framework Core DbContext tests
- **EFIntegrationTests.cs** - Loan creation/deletion integration tests

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

## Screenshots if they can't be opened check the files

![alt text](<Skärmbild 2026-03-13 150459.png>)
![alt text](<Skärmbild 2026-03-13 150507.png>)