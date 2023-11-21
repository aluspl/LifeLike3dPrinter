#region Using(s)

#endregion

namespace Database.Interfaces;

public interface IDatabaseInitializer
{
    public Task InitializeAsync();
}