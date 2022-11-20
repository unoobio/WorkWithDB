// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog;
using System.Data;
using WorkWithDB.AppLogic;
using WorkWithDB.DataAccess.EntityFramework;
using WorkWithDB.DataAccess.EntityFramework.Initializer;
using WorkWithDB.DataAccess.EntityFramework.Repository;
using WorkWithDB.DataAccess.EntityFramework.Repository.Interfaces;
using WorkWithDB.DataAccess.Npgsql;
using WorkWithDB.DataAccess.Npgsql.Interfaces;
using WorkWithDB.UI;

internal class Program
{
    private static void Main(string[] args)
    {
        var host = AppStartup();

        // Для тестирования нужны БД EBay и EBayEF

        // EBay
        // 1. Создание таблиц (скрипты в файле SlqScripts.cs либо Scripts.txt)
        ITableCreator dbCreator = host.Services.GetRequiredService<ITableCreator>();
        dbCreator.CreateTables();
        // 2. Заполнение таблиц
        ITablesPlaceholder tablesPlaceholder = host.Services.GetRequiredService<ITablesPlaceholder>();
        tablesPlaceholder.InserDefaultRows();
        // 3. Вывод содержимого таблиц
        ITableReader tableReader = host.Services.GetRequiredService<ITableReader>();
        tableReader.ReadTable("users");
        tableReader.ReadTable("markets");
        tableReader.ReadTable("products");

        // EBayEF
        // 4. Добавление в таблицу на выбор (использовал EF, поэтому таблицы в другой БД) 
        IDBInitializer tableInitializer = host.Services.GetRequiredService<IDBInitializer>();
        tableInitializer.Initialize();
        InsertDialog insertDialog = host.Services.GetRequiredService<InsertDialog>();
        insertDialog.Run();
    }

    static void BuildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
    }

    static IHost AppStartup()
    {
        var builder = new ConfigurationBuilder();
        BuildConfig(builder);

        Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Build())
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .CreateLogger();

        var host = Host.CreateDefaultBuilder()
                    .ConfigureServices((context, services) => {
                        services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(builder.Build().GetConnectionString("PostgreEBay")))
                        .AddTransient<ITableCreator, TableCreator>()
                        .AddTransient<ITablesPlaceholder, TablesPlaceholder>()
                        .AddTransient<ISqlScriptExecutor, SqlScriptExecutor>()
                        .AddTransient<ITableReader, TableReader>()
                        .AddDbContext<EBayMarketContext>(options =>
                            options.UseNpgsql(new NpgsqlConnection(builder.Build().GetConnectionString("PostgreEBayEF")))
                            .UseSnakeCaseNamingConvention())
                        .AddTransient<IUserRepository, UserRepository>()
                        .AddTransient<IMarketRepository, MarketRepository>()
                        .AddTransient<IProductRepository, ProductRepository>()
                        .AddTransient<ITableService, TableService>()
                        .AddTransient<InsertDialog>()
                        .AddTransient<IDBInitializer, DBInitializer>();
                        
                    })
                    .UseSerilog()
                    .Build();

        return host;
    }
}