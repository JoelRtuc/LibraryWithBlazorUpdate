using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace LibraryWithBlazorUpdate.Tests
{
    public class MemberTests
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
        public async System.Threading.Tasks.Task CreateMember_ShouldAddMemberToDatabaseAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var member = new Member("M001", "John Doe", "john@example.com", DateTime.Now.Date);

            context.Members.Add(member);
            await context.SaveChangesAsync();

            // Act
            var savedMember = await context.Members.FirstOrDefaultAsync(m => m.memberId == member.memberId);

            // Assert
            Assert.NotNull(savedMember);
            Assert.Equal("M001", savedMember.memberId);
            Assert.Equal("John Doe", savedMember.memberName);
            Assert.Equal("john@example.com", savedMember.email);
            Assert.Equal(DateTime.Now.Date, savedMember.memberSince);
        }

        /// <summary>
        /// Test READ & UPDATE: Retrieves an existing member from the database
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task UpdateMember_ShouldModifyMemberPropertiesAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-2", "Another Book", "desc", "Author", 2024, true);
            var member = new Member("M002", "Adam Smith", "adam@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            member.memberName = "Jane Smith"; // Update member name before saving
            member.email = "jane@example.com"; // Update member name before saving
            await context.SaveChangesAsync();

            var loan = new Loan(book, member, DateTime.Now, null);
            await context.SaveChangesAsync();

            // Act
            var retrievedLoan = await context.Loans
                .Include(l => l.item)
                .Include(l => l.member)
                .FirstOrDefaultAsync(l => l.Id == loan.Id);
            var savedMember = await context.Members.FirstOrDefaultAsync(m => m.memberId == member.memberId);


            // Assert
            Assert.Single(savedMember.loans);
            Assert.Equal("Jane Smith", savedMember.memberName);
            Assert.Equal("jane@example.com", savedMember.email);
        }

        /// <summary>
        /// Test DELETE: Removes a member from the database
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task DeleteMember_ShouldRemoveMemberFromDatabaseAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-4", "Delete Test Book", "desc", "Author", 2024, true);
            var member = new Member("M004", "Alice Johnson", "alice@example.com", DateTime.Now);

            context.LibraryItems.Add(book);
            context.Members.Add(member);
            await context.SaveChangesAsync();

            // Act
            var memToDelete = await context.Members.FirstOrDefaultAsync(l => l.memberId == member.memberId);
            context.Members.Remove(member);
            var savedMember = await context.Members.FirstOrDefaultAsync(m => m.memberId == member.memberId);
            await context.SaveChangesAsync();

            // Assert
            var deletedMem = await context.Members.FirstOrDefaultAsync(l => l.memberId == member.memberId);
            Assert.Null(deletedMem);
        }
    }
}
