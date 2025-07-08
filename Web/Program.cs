using Core.Persistance;
using Core.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Web.Persistance;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped(typeof(BookService));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services
    .AddDbContext<ApplicationDbContext>(opt=> 
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();