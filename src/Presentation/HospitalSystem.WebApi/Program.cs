using HospitalSystem.Application;
using HospitalSystem.Domain.Primitives;
using HospitalSystem.Infrastructure;
using HospitalSystem.WebApi;
using HospitalSystem.WebApi.Common;
using HospitalSystem.WebApi.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddProcurementApplication();
builder.Services.AddProcurementInfrastructure(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "Hospital Management System API", Version = "v1" });
});

builder.Services.AddProblemDetails();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeature?.Error;

        var (statusCode, title) = exception switch
        {
            DomainException domainEx => (StatusCodes.Status409Conflict, "Domain rule violated"),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred"),
        };

        context.Response.StatusCode = statusCode;
        await Results.Problem(title: title, detail: exception?.Message, statusCode: statusCode)
            .ExecuteAsync(context);
    });
});

app.UseHttpsRedirection();


app.MapSchedulingEndpoints();
app.MapProcurementEndpoints();



app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestampUtc = DateTime.UtcNow }))
   .WithTags("Health");

app.Run();