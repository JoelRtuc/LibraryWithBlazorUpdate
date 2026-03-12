using LibraryWithBlazorUpdate.Components.Models;
using LibraryWithBlazorUpdate.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryWithBlazorUpdate.Components
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
            ClassManager classManager = new ClassManager();
            LibraryItem[] items = {new Book("12345678901", "Book Title", "Author Authorsson", "hahabook", 1984, true),
        new Book("12345678901", "Book Title sequel", "new newberry", "hahabook", 1984, true),
        new Movie(0, "Movie Title", "hahamovie", "Scorsese", 1985, false, 128, Movie.MovieType.DVD),
        new Magazine(0, "Magazine Title", "hahamagazine", "Carl", 1986, true, 2, "Sports")
        };
            Member[] allMembers = { new Member("ab123", "John Jonas", "John@Jonas", Convert.ToDateTime("05/01/1996")),
        new Member("cd123", "Joel Ringh", "joel.Ringh@", Convert.ToDateTime("05/02/2001")),
        new Member("ef123", "Erin Svensson", "Erin.wa@wa", Convert.ToDateTime("02/06/2006")), };

            Loan JohnsLoan1 = new Loan(items[0], allMembers[0], DateTime.Now, classManager);
            Loan JohnsLoan2 = new Loan(items[1], allMembers[0], DateTime.Now, classManager);
            Loan ErinsLoan = new Loan(items[2], allMembers[2], DateTime.Now, classManager);

            Console.WriteLine("\nGet Info");
            foreach (LibraryItem item in items)
            {
                Console.WriteLine(item.GetInfo());
            }

            Console.WriteLine("\nSearchClass");
            classManager.SearchClass(items, "Scorsese");

            Console.WriteLine("\nProlific Loaner");
            Console.WriteLine(classManager.ProlificLoaner(allMembers).memberName);

            Console.WriteLine("\nSort List By year");
            foreach (LibraryItem item in classManager.SortListByYear(items.ToList()))
            {
                Console.WriteLine(item.title);
            }

            Console.WriteLine("\nAll Books");
            Console.WriteLine(classManager.AllBooks(items));

            Console.WriteLine("\nWhat Books Loaned");
            Console.WriteLine(classManager.WhatBooksLoaned(items));

            Console.WriteLine("\nMatch Returns true or false");
            foreach (LibraryItem item in items)
            {
                Console.WriteLine(item.Matches("Book Title"));//Two are true
            }
            foreach (Member item in allMembers)
            {
                Console.WriteLine(item.Matches("Joel"));//One is true
            }
            */

            var builder = WebApplication.CreateBuilder(args);

            // Add Entity Framework Core with SQL Server
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? "Server=(localdb)\\mssqllocaldb;Database=LibraryWithBlazorUpdate;Trusted_Connection=True;";

            builder.Services.AddDbContext<LibraryWithBlazorUpdateContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
