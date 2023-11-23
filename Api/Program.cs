using Database.Infrastructure.Seeding;
using Oakton;
using Oakton.Resources;
using Printer.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ApplyOaktonExtensions();
builder.Services.AddHostedService<DatabaseInitializer>();

var connectionString = builder.Configuration.GetConnectionString(CommonConsts.ConnectionString);

// Database.Infrastructure
builder.Services.UseDatabase(connectionString);
builder.Services.UseSwagger();
builder.Services.UseJson();

// Add Wolverine to project
builder.Host.UseWolverine(connectionString);
builder.Host.UseResourceSetupOnStartup();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPrinterActions();
await app.RunOaktonCommands(args);