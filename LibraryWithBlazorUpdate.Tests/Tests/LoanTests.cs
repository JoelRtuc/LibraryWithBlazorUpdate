using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace LibraryWithBlazorUpdate.Tests
{
    public class LoanTests
    {
        private LibraryWithBlazorUpdateContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<LibraryWithBlazorUpdateContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new LibraryWithBlazorUpdateContext(options);
        }

        /// <summary>
        /// Test CREATE: Creates a new loan and verifies it's persisted in the database
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task CreateLoan_ShouldAddLoanToDatabaseAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-1", "Test Book", "A test book", "Test Author", 2024, true);
            var member = new Member("M001", "John Doe", "john@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            // Act
            var loan = new Loan(book, member, DateTime.Now, null);
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Assert
            var savedLoan = await context.Loans.FirstOrDefaultAsync(l => l.Id == loan.Id);
            Assert.NotNull(savedLoan);
            Assert.Equal(book.Id, savedLoan.LibraryItemId);
            Assert.Equal(member.memberId, savedLoan.MemberId);
            Assert.Equal(DateTime.Now.Date, savedLoan.loanDate.Date);
            Assert.False(savedLoan.isReturned);
        }

        /// <summary>
        /// Test READ: Retrieves an existing loan from the database
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task ReadLoan_ShouldRetrieveLoanFromDatabaseAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-2", "Another Book", "desc", "Author", 2024, true);
            var member = new Member("M002", "Jane Smith", "jane@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, null);
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Act
            var retrievedLoan = await context.Loans
                .Include(l => l.item)
                .Include(l => l.member)
                .FirstOrDefaultAsync(l => l.Id == loan.Id);

            // Assert
            Assert.NotNull(retrievedLoan);
            Assert.Equal(loan.Id, retrievedLoan.Id);
            Assert.NotNull(retrievedLoan.item);
            Assert.NotNull(retrievedLoan.member);
            Assert.Equal("Another Book", retrievedLoan.item.title);
            Assert.Equal("Jane Smith", retrievedLoan.member.memberName);
        }

        /// <summary>
        /// Test UPDATE: Modifies an existing loan and persists the changes
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task UpdateLoan_ShouldModifyLoanPropertiesAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-3", "Update Test Book", "desc", "Author", 2024, true);
            var member = new Member("M003", "Bob Wilson", "bob@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, null);
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Act
            var loanToUpdate = await context.Loans.FirstOrDefaultAsync(l => l.Id == loan.Id);
            loanToUpdate.isOverdue = true;
            loanToUpdate.dueDate = DateTime.Now.AddDays(-5);
            context.Loans.Update(loanToUpdate);
            await context.SaveChangesAsync();

            // Assert
            var updatedLoan = await context.Loans.FirstOrDefaultAsync(l => l.Id == loan.Id);
            Assert.NotNull(updatedLoan);
            Assert.True(updatedLoan.isOverdue);
            Assert.True(updatedLoan.dueDate < DateTime.Now);
        }

        /// <summary>
        /// Test DELETE: Removes a loan from the database
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task DeleteLoan_ShouldRemoveLoanFromDatabaseAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-4", "Delete Test Book", "desc", "Author", 2024, true);
            var member = new Member("M004", "Alice Johnson", "alice@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, null);
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            var loanId = loan.Id;

            // Act
            var loanToDelete = await context.Loans.FirstOrDefaultAsync(l => l.Id == loanId);
            context.Loans.Remove(loanToDelete);
            await context.SaveChangesAsync();

            // Assert
            var deletedLoan = await context.Loans.FirstOrDefaultAsync(l => l.Id == loanId);
            Assert.Null(deletedLoan);
        }
    }
}
