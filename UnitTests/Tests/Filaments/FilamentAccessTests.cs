using Commons.Enums;
using JasperFx.Core.Reflection;
using Microsoft.EntityFrameworkCore;
using PrinterService.Models.Filament;

namespace UnitTests.Tests;

public class FilamentAccessTests
{
    public FilamentAccessTests()
    {
        OaktonEnvironment.AutoStartHost = true;
    }
    
    [Fact]
    public async Task run_through_the_handler()
    {
        var name= Guid.NewGuid().ToString();
        await using var host = await AlbaHost.For<Program>();

        var tracked = await host.InvokeMessageAndWaitAsync(new CreateFilament(name, FilamentType.PLA,
            MaterialType.Basic, "red", 1000, 20, DateTime.UtcNow));
        var result = tracked.FindSingleTrackedMessageOfType<FilamentCreated>()
            .ShouldNotBeNull();

        await using var nested = host.Services.As<IContainer>().GetNestedContainer();
        var context = nested.GetInstance<EFContext>();

        var item = await context.Filaments.FirstOrDefaultAsync(x => x.Id == result.Id);
        item.ShouldNotBeNull();
    }

    [Fact]
    public async Task run_through_controller()
    {
        await using var host = await AlbaHost.For<Program>();
        var name= Guid.NewGuid().ToString();
        var tracked = await host.ExecuteAndWaitAsync(async () =>
        {
            await host.Scenario(x =>
            {
                var command = new CreateFilament(name, FilamentType.PLA, MaterialType.Basic, "red", 1000, 20, DateTime.UtcNow);
                x.Post.Json(command).ToUrl("/filament/create");
            });
        });

        var response = tracked.FindSingleTrackedMessageOfType<FilamentCreated>()
            .ShouldNotBeNull();

        await using var nested = host.Services.As<IContainer>().GetNestedContainer();
        var context = nested.GetInstance<EFContext>();

        var item = await context.Filaments.FirstOrDefaultAsync(x => x.Id == response.Id);
        item.ShouldNotBeNull();
    }

    [Fact]
    public async Task execute_through_wolverine_http()
    {
        await using var host = await AlbaHost.For<Program>();
        string name= Guid.NewGuid().ToString();
        var tracked = await host.ExecuteAndWaitAsync(async () =>
        {
            await host.Scenario(x =>
            {
                var command = new CreateFilament(name, FilamentType.PLA, MaterialType.Basic, "red", 1000, 20, DateTime.UtcNow);
                x.Post.Json(command).ToUrl("/filament/create");
                x.StatusCodeShouldBe(200);
            });
        });

        var response = tracked.FindSingleTrackedMessageOfType<FilamentCreated>()
            .ShouldNotBeNull();

        await using var nested = host.Services.As<IContainer>().GetNestedContainer();
        var context = nested.GetInstance<EFContext>();

        var item = await context.Filaments.FirstOrDefaultAsync(x => x.Id == response.Id);
        item.ShouldNotBeNull();
    }
}