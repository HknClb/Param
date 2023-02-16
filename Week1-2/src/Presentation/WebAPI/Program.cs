using Application;
using Application.Logging;
using CrossCuttingConcerns.Exceptions;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistenceServices(builder.Configuration)
    .AddApplicationServices();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddLogging();

// .AddNewtonsoftJson is using for JSON Patching. Also this method is overriding the default json format of the project.
builder.Services.AddControllers()
    .AddOData(options => options.EnableQueryFeatures()) // Enabling all OData features.
    .AddNewtonsoftJson()
    .AddODataNewtonsoftJson(); // Required for serializing OData response. So, you will get error if you didn't enable this.

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

#region Warning
/*
The order of Exception and Logging middleware is important. 
Because errors are logging in exception middleware. if we use first Logging Middleware then we will get double log for result.
*/
#endregion
app.UseExceptionMiddleware(); // Custom Exception Middleware
app.UseLoggingMiddleware(); // Custom Logging Middleware

app.MapControllers();

app.Run();