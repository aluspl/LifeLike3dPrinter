using Microsoft.AspNetCore.Mvc;
using PrinterService.Handlers;
using PrinterService.Models.Filament;
using Wolverine;

namespace Printer.Extensions;

public static class MapExtensions
{
    public static IEndpointRouteBuilder MapPrinterActions(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/filament/create", async ([FromBody]CreateFilamentCommand request, [FromServices]IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentCreated>(request);
                return filament;
            })
            .WithName("CreateFilament")
            .WithOpenApi();

        endpoints.MapPost("/filament/refill", async ([FromBody] RefillFilamentCommand request, [FromServices]IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentRefilled>(request);
                return filament;
            })
            .WithName("RefillFilament")
            .WithOpenApi();

        endpoints.MapGet("/filament", async ([FromBody]FilamentQuery request, [FromServices]IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentRefilled>(request);
                return filament;
            })
            .WithName("GetFilament")
            .WithOpenApi();

        return endpoints;
    }
}