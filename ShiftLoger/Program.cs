using Microsoft.EntityFrameworkCore;
using ShiftLoger.Contexts;
using ShiftLoger.Data;
using ShiftLoger.Factories;
using ShiftLoger.Interfaces;
using ShiftLoger.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ShiftLogerContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ShiftLogerContext")
    ?? throw new InvalidOperationException("Connection string 'MoviesContext' not found.")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddScoped();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logFactory = services.GetRequiredService<ILogFactory>();
    SeedData.InitializeLogData(services, logFactory);
}


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void AddScoped()
{
    builder.Services.AddScoped<ILogRepository, LogRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<ILogFactory, LogFactory>();
}