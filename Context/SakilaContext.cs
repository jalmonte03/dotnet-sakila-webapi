using Microsoft.EntityFrameworkCore;
using Sakila.App.WebAPI.Model;

namespace Sakila.App.WebAPI.Context;

#pragma warning disable CS1591
public class SakilaContext : DbContext
{
    public IConfiguration configuration;

    public DbSet<Film> Films { get; set; }
    public DbSet<Customer> Customers {  get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<FilmCategory> FilmCategories { get; set; }

    public SakilaContext(IConfiguration config)
    {
        configuration = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SQLServerConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Add current time to LastUpdate as default in Film Table
        modelBuilder.Entity<Film>()
            .Property(film => film.LastUpdate)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Customer>()
            .Property(customer => customer.CreateDate)
            .HasDefaultValueSql("getdate()");

        modelBuilder.Entity<Customer>()
            .Property(customer => customer.LastUpdate)
            .HasDefaultValueSql("getdate()");
        
        modelBuilder.Entity<Film>()
            .HasMany(e => e.Categories)
            .WithMany(e => e.Films)
            .UsingEntity<FilmCategory>();
    }
}
#pragma warning restore CS1591