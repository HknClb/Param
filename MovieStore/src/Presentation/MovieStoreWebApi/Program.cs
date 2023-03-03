using Application;
using MovieStoreWebApi.Constants;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPersistenceServices(builder.Configuration)
    .AddApplicationServices();

builder.Services
    .AddControllers(options => options.ModelValidatorProviders.Clear())
    .AddNewtonsoftJson(options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(CustomSwaggerGenOptions.SecurityOptions);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();