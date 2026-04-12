using Scalar.AspNetCore;
using Microsoft.AspNetCore.HttpLogging;
using SurvStation.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddDbContexts();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestPath;
});
builder.Services.AddLogging();
builder.Services.AddOpenApi();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/_docs/scalar", options =>
    {
        options.WithTitle("Survey Station").DisableMcp().DisableAgent().WithTheme(ScalarTheme.DeepSpace);
    });
}

app.Run();
