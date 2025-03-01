using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace ProjectDb;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection(); // Anv�nds f�r att registrera tj�nster, dvs DI.
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        base.OnStartup(e);
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // L�gg till DataContext i DI-container med SQLite.
        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(@"Data Source=C:\\Users\\Criss\\source\\repos\\Data\\Data\\database.db"));

        
    }
}
