using Commons.Enums;
using JasperFx.Core.Reflection;
using Microsoft.EntityFrameworkCore;
using PrinterService.Models.Filament;

namespace UnitTests.Tests.Filaments;

public class FilamentCreateTests
{
    public FilamentCreateTests()
    {
        OaktonEnvironment.AutoStartHost = true;
    }
    
    [Fact]
    public async Task Should_Create_Filament()
    {
        var name= Guid.NewGuid().ToString();
        await using var host = await AlbaHost.For<Program>();
        ClearDB(host);

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
    public async Task Should_Create_And_Refill_Filament()
    {
        var name= Guid.NewGuid().ToString();
        await using var host = await AlbaHost.For<Program>();
        ClearDB(host);
        
        var tracked = await host.InvokeMessageAndWaitAsync(new CreateFilament(name, FilamentType.PLA,
            MaterialType.Basic, "red", 1000, 20, DateTime.UtcNow));
        var result = tracked.FindSingleTrackedMessageOfType<FilamentCreated>()
            .ShouldNotBeNull();

        await using var nested = host.Services.As<IContainer>().GetNestedContainer();
        var context = nested.GetInstance<EFContext>();

        var item = await context.Filaments.FirstOrDefaultAsync(x => x.Id == result.Id);
        item.ShouldNotBeNull();
        
        tracked = await host.InvokeMessageAndWaitAsync(new RefillFilament(result.Id, 1000, 60, DateTime.UtcNow));
        var response = tracked.FindSingleTrackedMessageOfType<FilamentRefilled>()
            .ShouldNotBeNull();
        
        response.id.ShouldNotBe(Guid.Empty);
        response.updated.ShouldNotBeNull();
        response.updated.Value.Day.ShouldBe(DateTime.UtcNow.Day);
        response.weight.ShouldBe(2000);
    }

    [Fact]
    public async Task Should_Create_And_Use_Filament()
    {
        var name= Guid.NewGuid().ToString();
        await using var host = await AlbaHost.For<Program>();
        ClearDB(host);

        var tracked = await host.InvokeMessageAndWaitAsync(new CreateFilament(name, FilamentType.PLA,
            MaterialType.Basic, "red", 1000, 20, DateTime.UtcNow));
        var result = tracked.FindSingleTrackedMessageOfType<FilamentCreated>()
            .ShouldNotBeNull();

        await using var nested = host.Services.As<IContainer>().GetNestedContainer();
        var context = nested.GetInstance<EFContext>();

        var item = await context.Filaments.FirstOrDefaultAsync(x => x.Id == result.Id);
        item.ShouldNotBeNull();
        
        tracked = await host.InvokeMessageAndWaitAsync(new UseFilament(result.Id, 1000, Guid.NewGuid()));
        var response = tracked.FindSingleTrackedMessageOfType<FilamentUsed>()
            .ShouldNotBeNull();
        
        response.id.ShouldNotBe(Guid.Empty);
        response.updated.ShouldNotBeNull();
        response.updated.Value.Day.ShouldBe(DateTime.UtcNow.Day);
        response.left.ShouldBe(0);
    }
    
    private static void ClearDB(IAlbaHost host)
    {
        host.BeforeEachAsync(async (_) =>
        {
            await using var nested = host.Services.As<IContainer>().GetNestedContainer();
            var context = nested.GetInstance<EFContext>();
            context.Filaments.RemoveRange(context.Filaments);
            await context.SaveChangesAsync();
        });
    }
}