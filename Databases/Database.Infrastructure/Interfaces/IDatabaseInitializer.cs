#region Using(s)

#endregion

namespace Database.Infrastructure.Interfaces;

public interface IDatabaseInitializer
{
    public Task InitializeAsync();
}