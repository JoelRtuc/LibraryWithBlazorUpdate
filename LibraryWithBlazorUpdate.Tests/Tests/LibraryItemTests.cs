using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace LibraryWithBlazorUpdate.Tests
{
    public class LibraryItemTests
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
        public async System.Threading.Tasks.Task CreateLibraryItem_ShouldAddLibraryItemToDatabaseAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var item = new Book("978-0-00-000000-1", "Test Book", "A test book", "Test Author", 2024, true);

            context.LibraryItems.Add(item);
            await context.SaveChangesAsync();

            //Act
            var savedItem = await context.LibraryItems.FirstOrDefaultAsync(i => i.Id == item.Id);

            // Assert
            Assert.NotNull(savedItem);
            Assert.Equal("978-0-00-000000-1", item.ISBN);
            Assert.Equal("Test Book", savedItem.title);
            Assert.Equal(2024, savedItem.publishedYear);
            Assert.True(savedItem.isAvailable);
        }

        /// <summary>
        /// Test READ & UPDATE: Modifies an existing loan and persists the changes
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task UpdateLibraryItem_ShouldModifyLibraryItemPropertiesAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-3", "Update Test Book", "desc", "Author", 2024, true);

            context.LibraryItems.Add(book);
            await context.SaveChangesAsync();

            // Act
            var itemToUpdate = await context.LibraryItems.FirstOrDefaultAsync(l => l.Id == book.Id);
            itemToUpdate.isAvailable = false;
            itemToUpdate.description = "Updated description";
            await context.SaveChangesAsync();

            // Assert
            var updatedItem = await context.LibraryItems.FirstOrDefaultAsync(l => l.Id == book.Id);
            Assert.NotNull(updatedItem);
            Assert.False(updatedItem.isAvailable);
            Assert.Equal("Updated description", updatedItem.description);
        }

        /// <summary>
        /// Test DELETE: Removes a loan from the database
        /// </summary>
        [Fact]
        public async System.Threading.Tasks.Task DeleteLibraryItem_ShouldRemoveLibraryItemFromDatabaseAsync()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var book = new Book("978-0-00-000000-4", "Delete Test Book", "desc", "Author", 2024, true);

            context.LibraryItems.Add(book);
            await context.SaveChangesAsync();

            // Act
            var itemToDelete = await context.LibraryItems.FirstOrDefaultAsync(l => l.Id == book.Id);
            context.LibraryItems.Remove(itemToDelete);
            await context.SaveChangesAsync();

            // Assert
            var deletedItem = await context.LibraryItems.FirstOrDefaultAsync(l => l.Id == book.Id);
            Assert.Null(deletedItem);
        }
    }
}
