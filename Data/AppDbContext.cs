// Data/AppDbContext.cs
using Microsoft.EntityFrameworkCore;
using MyMicroservice.Api.Models;

namespace MyMicroservice.Api.Data;



public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Product> Products => Set<Product>();
}