using Core.Persistance;
using Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Web.Persistance;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped(typeof(BookDomainService));
builder.Services.AddAuthentication();
builder.Services.
    AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        
    }).
    AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.DisableBuiltInModelValidation = true;
} );

builder.Services
    .AddDbContext<ApplicationDbContext>(opt=> 
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.MapGroup("api/v1/users").MapIdentityApi<IdentityUser>();

app.Run();