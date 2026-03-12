using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibraryWithBlazorUpdate.Data
{
    /// <summary>
    /// Design-time factory for DbContext. Used by EF Core tools (migrations, scaffolding)
    /// to create the DbContext without executing the full application startup.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryWithBlazorUpdateContext>
    {
        public LibraryWithBlazorUpdateContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryWithBlazorUpdateContext>();

            // SQL Server connection string for design-time use.
            // Adjust the connection string as needed for your environment.
            // (localdb)\\mssqllocaldb is the default local SQL Server Express instance.
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=LibraryWithBlazorUpdate;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);

            return new LibraryWithBlazorUpdateContext(optionsBuilder.Options);
        }
    }
}
