using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.DataAccess;

var runningMigrationMode = CommandLineParser.IsRunningMigrationMode(args);

if (runningMigrationMode)
{
    var connectionString = CommandLineParser.GetConnectionString(args);

    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlite(connectionString);

    await using var dbContext = new AppDbContext(optionsBuilder.Options);
    await dbContext.Database.MigrateAsync();

    return;
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
