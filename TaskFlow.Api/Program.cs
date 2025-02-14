using Microsoft.EntityFrameworkCore;
using Serilog;
using TaskFlow.Api.Data;
using TaskFlow.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
// Configure logging
builder.Logging.ConfigureLogging();

// Add services to the container.
// Configure services
builder.Services.ConfigureServices(builder.Configuration);

// Configure database
builder.Services.ConfigureDatabase(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    //app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
