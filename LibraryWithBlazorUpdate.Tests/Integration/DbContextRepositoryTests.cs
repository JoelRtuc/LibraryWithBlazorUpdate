using LibraryWithBlazorUpdate.Data;
using LibraryWithBlazorUpdate.Components.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace LibraryWithBlazorUpdate.Tests
{
    public class DbContextRepositoryTests
    {
        private LibraryWithBlazorUpdateContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<LibraryWithBlazorUpdateContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new LibraryWithBlazorUpdateContext(options);
        }

        [Fact]
        public async Task AddLibraryItem_ShouldInsertIntoDatabase_AndRetrievable()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var item = new Book("978-3-16-148410-0", "Test Book", "A test book", "Test Author", 2024, true);

            // Act
            context.LibraryItems.Add(item);
            await context.SaveChangesAsync();

            // Assert
            var retrievedItem = await context.LibraryItems.FirstOrDefaultAsync(li => li.title == "Test Book");
            Assert.NotNull(retrievedItem);
            Assert.Equal("Test Book", retrievedItem.title);
            Assert.Equal("Test Author", retrievedItem.author);
        }

        [Fact]
        public async Task AddMember_ShouldInsertIntoDatabase_AndRetrievable()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var member = new Member("M001", "John Doe", "john@example.com", DateTime.Now);

            // Act
            context.Members.Add(member);
            await context.SaveChangesAsync();

            // Assert
            var retrievedMember = await context.Members.FirstOrDefaultAsync(m => m.memberId == "M001");
            Assert.NotNull(retrievedMember);
            Assert.Equal("John Doe", retrievedMember.memberName);
            Assert.Equal("john@example.com", retrievedMember.email);
        }

        [Fact]
        public async Task AddLoan_ShouldInsertIntoDatabase_WithForeignKeyRelationships()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var item = new Book("978-3-16-148410-0", "Test Book", "A test book", "Test Author", 2024, true);
            var member = new Member("M001", "John Doe", "john@example.com", DateTime.Now);
            
            context.LibraryItems.Add(item);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan(item, member, DateTime.Now, null);

            // Act
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Assert
            var retrievedLoan = await context.Loans
                .Include(l => l.item)
                .Include(l => l.member)
                .FirstOrDefaultAsync(l => l.Id == loan.Id);
            
            Assert.NotNull(retrievedLoan);
            Assert.NotNull(retrievedLoan.item);
            Assert.NotNull(retrievedLoan.member);
            Assert.Equal("Test Book", retrievedLoan.item.title);
            Assert.Equal("John Doe", retrievedLoan.member.memberName);
        }

        [Fact]
        public async Task QueryWithInclude_ShouldLoadRelatedEntities_Successfully()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var item1 = new Book("978-3-16-148410-0", "Test Book 1", "A test book", "Test Author", 2024, true);
            var item2 = new Book("978-3-16-148410-1", "Test Book 2", "A test book", "Test Author", 2024, true);
            var member = new Member("M001", "John Doe", "john@example.com", DateTime.Now);
            
            context.LibraryItems.Add(item1);
            context.LibraryItems.Add(item2);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan1 = new Loan(item1, member, DateTime.Now, null);
            
            // Need to refresh item2 since item1 was marked unavailable
            var freshItem2 = await context.LibraryItems.FirstOrDefaultAsync(i => i.Id == item2.Id);
            var loan2 = new Loan(freshItem2, member, DateTime.Now.AddDays(1), null);
            
            context.Loans.Add(loan1);
            context.Loans.Add(loan2);
            await context.SaveChangesAsync();

            // Act
            var memberWithLoans = await context.Members
                .Include(m => m.loans)
                .ThenInclude(l => l.item)
                .FirstOrDefaultAsync(m => m.memberId == "M001");

            // Assert
            Assert.NotNull(memberWithLoans);
            Assert.Equal(2, memberWithLoans.loans.Count);
            Assert.Contains(memberWithLoans.loans, loan => loan.item.title == "Test Book 1");
            Assert.Contains(memberWithLoans.loans, loan => loan.item.title == "Test Book 2");
        }
    }
}
