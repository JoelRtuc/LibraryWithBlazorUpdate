using System;
using System.Linq;
using System.Threading.Tasks;
using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibraryWithBlazorUpdate.Tests.Integration
{
    public class EfIntegrationTests
    {
        private LibraryWithBlazorUpdateContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<LibraryWithBlazorUpdateContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new LibraryWithBlazorUpdateContext(options);
        }

        [Fact]
        public async Task DeleteItem_ShouldCascadeRemoveLoans()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-1", "Cascade Book", "desc", "Author", 2024, true);
            var member = new Member("X01", "Alice", "alice@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, null);
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Act
            context.LibraryItems.Remove(book);
            await context.SaveChangesAsync();

            // Assert: loans referencing the deleted item should no longer exist
            var loans = await context.Loans.Where(l => l.LibraryItemId == book.Id).ToListAsync();
            Assert.Empty(loans);
        }

        [Fact]
        public async Task DeleteMember_WithActiveLoans_ShouldThrowOrLeaveDatabaseConsistent()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-2", "MemberLock Book", "desc", "Author", 2024, true);
            var member = new Member("X02", "Bob", "bob@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, null);
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Act
            context.Members.Remove(member);

            try
            {
                await context.SaveChangesAsync();

                // InMemory provider may allow deletion. Ensure database remains consistent: member deleted and no loans reference it
                var memberStill = await context.Members.FindAsync(member.memberId);
                Assert.Null(memberStill);

                var loansReferencing = await context.Loans.Where(l => l.MemberId == member.memberId).ToListAsync();
                Assert.Empty(loansReferencing);
            }
            catch (DbUpdateException)
            {
                // Expected for relational providers enforcing Restrict
                Assert.True(true);
            }
        }

        [Fact]
        public async Task ReturnLoan_MarksReturned_And_SetsItemAvailable()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-3", "Returnable Book", "desc", "Author", 2024, true);
            var member = new Member("X03", "Carol", "carol@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, null);
            context.Loans.Add(loan);
            await context.SaveChangesAsync();

            // Pre-assert
            Assert.False(loan.isReturned);
            Assert.False(context.LibraryItems.First(i => i.Id == book.Id).isAvailable);

            // Act
            loan.Return(book, null);
            context.Loans.Update(loan);
            // ensure book availability updated in tracked entity
            context.LibraryItems.Update(book);
            await context.SaveChangesAsync();

            // Assert
            var storedLoan = await context.Loans.FindAsync(loan.Id);
            var storedBook = await context.LibraryItems.FindAsync(book.Id);

            Assert.True(storedLoan.isReturned);
            Assert.True(storedBook.isAvailable);
        }
    }
}
