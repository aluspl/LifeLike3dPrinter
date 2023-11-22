using Microsoft.AspNetCore.Mvc;
using PrinterService.Handlers;
using PrinterService.Models.Filament;
using Wolverine;

namespace Printer.Extensions;

public static class MapExtensions
{
    public static IEndpointRouteBuilder MapPrinterActions(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/filament/create", async ([FromBody]CreateFilament request, [FromServices]IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentCreated>(request);
                return filament;
            })
            .WithName("CreateFilament")
            .WithOpenApi();

        endpoints.MapPost("/filament/refill", async ([FromBody] RefillFilament request, [FromServices]IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentRefilled>(request);
                return filament;
            })
            .WithName("RefillFilament")
            .WithOpenApi();
        //
        // endpoints.MapGet("/filament", async ([FromQuery]QueryFilament request, [FromServices]IMessageBus bus) =>
        //     {
        //         var filament = await bus.InvokeAsync<FilamentModel>(request);
        //         return filament;
        //     })
        //     .WithName("GetFilament")
        //     .WithOpenApi();
        //
        // endpoints.MapGet("/filament/list", async ([FromQuery]QueryFilaments request, [FromServices]IMessageBus bus) =>
        //     {
        //         var filament = await bus.InvokeAsync<IEnumerable<FilamentModel>>(request);
        //         return filament;
        //     })
        //     .WithName("GetFilaments")
        //     .WithOpenApi();
        //
        return endpoints;
    }
}