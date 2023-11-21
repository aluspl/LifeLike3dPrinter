using Database.Context;
using Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Seeding;

public class DatabaseInitializer : IDatabaseInitializer
{
    private readonly EFContext _context;

    public DatabaseInitializer(EFContext context)
    {
        _context = context;
    }
    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();
        await _context.SaveChangesAsync();
    }
}