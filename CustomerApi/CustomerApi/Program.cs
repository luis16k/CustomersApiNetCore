using Customer.Domain.Service.Contracts;
using Customer.Domain.Service.Implementations;
using Microsoft.EntityFrameworkCore;
using static Customer.Api.IoC.DependencyInjection;
using Customer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

LoadDI(builder);
Setup.AddDBContext(builder.Services, "TestDB");

var app = builder.Build();

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

public partial class Program { }

