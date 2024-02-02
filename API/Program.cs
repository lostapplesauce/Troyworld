using API.Extensions;
using API.Middleware;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//services are things we use within our application logic
//Shall use dependency injection
builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
//Anything with app in front is middleware

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//string has to match name within AddCors method
//CorsPolicy within ApplicationServiceExtensions
app.UseCors("CorsPolicy");
//This won't do anything til i create login
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope(); //using keyword automatically deletes the service after it is used
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
} 
catch(Exception ex){
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"An error ocurred during migration");
}

app.Run();
