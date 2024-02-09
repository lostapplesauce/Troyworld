using API.Extensions;
using API.Middleware;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//services are things we use within our application logic
//Shall use dependency injection
builder.Services.AddControllers(opt => {
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //Build at the end puts all this to work
    opt.Filters.Add(new AuthorizeFilter(policy)); //Every controller endpoint will now require authorization
});
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

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

app.UseAuthentication();
//This won't do anything til i create login
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope(); //using keyword automatically deletes the service after it is used
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager);
} 
catch(Exception ex){
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex,"An error ocurred during migration");
}

app.Run();
