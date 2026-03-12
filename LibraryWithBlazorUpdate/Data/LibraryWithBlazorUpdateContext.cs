using Microsoft.EntityFrameworkCore;
using LibraryWithBlazorUpdate.Components.Models;

namespace LibraryWithBlazorUpdate.Data
{
    /// <summary>
    /// Entity Framework Core DbContext for the Library Management System.
    /// Manages all database operations for LibraryItem (Book, Movie, Magazine), Member, and Loan entities.
    /// </summary>
    public class LibraryWithBlazorUpdateContext : DbContext
    {
        public LibraryWithBlazorUpdateContext(DbContextOptions<LibraryWithBlazorUpdateContext> options)
            : base(options)
        {
        }

        // DbSets for all entities
        public DbSet<LibraryItem> LibraryItems { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure LibraryItem (TPH - Table Per Hierarchy inheritance)
            modelBuilder.Entity<LibraryItem>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<LibraryItem>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Configure Book (derived from LibraryItem)
            modelBuilder.Entity<Book>()
                .HasBaseType<LibraryItem>();

            modelBuilder.Entity<Book>()
                .Property(x => x.ISBN)
                .HasMaxLength(17);

            // Configure Movie (derived from LibraryItem)
            modelBuilder.Entity<Movie>()
                .HasBaseType<LibraryItem>();

            modelBuilder.Entity<Movie>()
                .Property(x => x.type)
                .HasConversion<string>(); // Store enum as string in database

            // Configure Magazine (derived from LibraryItem)
            modelBuilder.Entity<Magazine>()
                .HasBaseType<LibraryItem>();

            modelBuilder.Entity<Magazine>()
                .Property(x => x.company)
                .HasMaxLength(255);

            // Configure Member
            modelBuilder.Entity<Member>()
                .HasKey(x => x.memberId);

            modelBuilder.Entity<Member>()
                .Property(x => x.memberName)
                .IsRequired()
                .HasMaxLength(255);

            modelBuilder.Entity<Member>()
                .Property(x => x.email)
                .HasMaxLength(255);

            // One-to-many: Member has many Loans
            modelBuilder.Entity<Member>()
                .HasMany(m => m.loans)
                .WithOne(l => l.member)
                .HasForeignKey(l => l.MemberId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting member with active loans

            // Configure Loan
            modelBuilder.Entity<Loan>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Loan>()
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            // Foreign keys
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.item)
                .WithMany(li => li.loans)
                .HasForeignKey(l => l.LibraryItemId)
                .OnDelete(DeleteBehavior.Cascade); // Delete loans when item is deleted

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.member)
                .WithMany(m => m.loans)
                .HasForeignKey(l => l.MemberId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting member with loans

            // Index for common queries
            modelBuilder.Entity<Loan>()
                .HasIndex(l => l.loanDate);

            modelBuilder.Entity<Loan>()
                .HasIndex(l => l.isReturned);

            modelBuilder.Entity<LibraryItem>()
                .HasIndex(li => li.title);

            modelBuilder.Entity<Member>()
                .HasIndex(m => m.email)
                .IsUnique();
        }
    }
}
