using Microsoft.EntityFrameworkCore;
using MyMicroservice.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Add services to the container.
builder.Services.AddControllers();

// --- 1. Add Swagger Services ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// -------------------------------

// Register the Database Context with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- 2. Configure the HTTP request pipeline. ---{
app.UseSwagger();
app.UseSwaggerUI();

// -------
app.MapControllers(); // <--- THIS WAS MISSING!

// --- AUTO-CREATE DATABASE ON STARTUP ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // This creates the app.db file if it's missing
    db.Database.EnsureCreated();
}
// ---------------------------------------

app.Run();