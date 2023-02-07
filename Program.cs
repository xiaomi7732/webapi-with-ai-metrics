using Microsoft.ApplicationInsights.Extensibility.EventCounterCollector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureTelemetryModule<EventCounterCollectionModule>((module, _) =>
{
    // This removes all default counters, if any.
    module.Counters.Clear();

    // This adds the system counter "gen-0-size" from "System.Runtime"
    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "threadpool-queue-length"));
    module.Counters.Add(new EventCounterCollectionRequest("System.Runtime", "threadpool-thread-count"));
});
builder.Services.AddApplicationInsightsTelemetry();

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