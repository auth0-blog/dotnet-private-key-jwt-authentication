var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

builder.Services.AddSwaggerService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/healthchecks", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new HealthCheckData
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(-index)),
            Random.Shared.Next(90, 150),
            Random.Shared.Next(50, 90),
            Random.Shared.Next(10, 18),
            Random.Shared.Next(60, 200),
            Random.Shared.Next(180, 250)
        ))
        .ToArray();
    return forecast;
})
.WithName("GetHealthChecks")
.RequireAuthorization()
.WithOpenApi();

app.MapFallbackToFile("index.html");

app.Run();

record HealthCheckData(DateOnly Date, int MaxBloodPressure, int MinBloodPressure, int Hemoglobin, int BloodGlucose, int Cholesterol);
