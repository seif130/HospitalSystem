using HospitalSystem.Application;
using HospitalSystem.Application.Modules.Administration.Departments.Commands.CreateDepartment;
using HospitalSystem.Infrastructure;
using HospitalSystem.WebApi.Endpoints.Administration;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference(options =>
    {
        options.Title = "Hospital System API";
        options.Theme = ScalarTheme.Purple; 
    });


}

app.UseHttpsRedirection();

app.MapDepartmentsEndpoints();

app.Run();


