using Commons.Enums;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models.Orders;
using PrinterService.Models.Filament;
using Wolverine;

namespace Printer.Extensions;

public static class MapExtensions
{
    public static IEndpointRouteBuilder MapFilamentActions(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/filament/create",
                async ([FromBody] CreateFilament request, [FromServices] IMessageBus bus) =>
                {
                    var filament = await bus.InvokeAsync<FilamentCreated>(request);
                    return filament;
                })
            .WithName("CreateFilament")
            .WithOpenApi();

        endpoints.MapPost("/filament/refill",
                async ([FromBody] RefillFilament request, [FromServices] IMessageBus bus) =>
                {
                    var filament = await bus.InvokeAsync<FilamentRefilled>(request);
                    return filament;
                })
            .WithName("RefillFilament")
            .WithOpenApi();

        endpoints.MapPost("/filament/use", async ([FromBody] UseFilament request, [FromServices] IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentUsed>(request);
                return filament;
            })
            .WithName("UseFilament")
            .WithOpenApi();

        endpoints.MapGet("/filament/{id}", async (Guid id, [FromServices] IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentModel>(new QueryFilamentItem(id));
                return filament;
            })
            .WithName("GetFilament")
            .WithOpenApi();

        endpoints.MapGet("/filament/list", async ([FromServices] IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<IEnumerable<FilamentModel>>(new QueryFilamentList());
                return filament;
            })
            .WithName("GetFilaments")
            .WithOpenApi();

        endpoints.MapGet("/filament/list/all", async ([FromServices] IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<IEnumerable<FilamentModel>>(new QueryFilamentList(false));
                return filament;
            })
            .WithName("GetAllFilaments")
            .WithOpenApi();

        endpoints.MapGet("/filament/list/type", async (FilamentType filamentType, [FromServices] IMessageBus bus) =>
            {
                var filament =
                    await bus.InvokeAsync<IEnumerable<FilamentModel>>(new QueryFilamentList(true, filamentType));
                return filament;
            })
            .WithName("GetFilamentsByType")
            .WithOpenApi();

        return endpoints;
    }

    public static IEndpointRouteBuilder MapOrderActions(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/order/create", async ([FromBody] CreateOrder request, [FromServices] IMessageBus bus) =>
            {
                var orderCreated = await bus.InvokeAsync<OrderCreated>(request);
                foreach (var requestFilament in request.filaments)
                {
                    
                    await bus.InvokeAsync<FilamentUsed>(new UseFilament(requestFilament.id, requestFilament.weight,
                        orderCreated.id));
                }
                return orderCreated;
            })
            .WithName("CreateFilament")
            .WithOpenApi();

        endpoints.MapPost("/order/update", async ([FromBody] UpdateOrder request, [FromServices] IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentUsed>(request);
                return filament;
            })
            .WithName("UseFilament")
            .WithOpenApi();

        endpoints.MapGet("/order/{id}", async (Guid id, [FromServices] IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<FilamentModel>(new QueryFilamentItem(id));
                return filament;
            })
            .WithName("GetFilament")
            .WithOpenApi();

        endpoints.MapGet("/order/list", async ([FromServices] IMessageBus bus) =>
            {
                var filament = await bus.InvokeAsync<IEnumerable<FilamentModel>>(new QueryFilamentList());
                return filament;
            })
            .WithName("GetFilaments")
            .WithOpenApi();
        return endpoints;
    }
}