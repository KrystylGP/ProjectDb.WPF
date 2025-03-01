using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Factories;

// Skapar en instans av 'DataContext' under "design time" (dvs. när man kör migrationer).
public class DataContextFactory : IDesignTimeDbContextFactory<DataContext> 
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite(@"Data Source=C:\\Users\\Criss\\source\\repos\\Data\\Data\\database.db");

        return new DataContext(optionsBuilder.Options);
    }
}

