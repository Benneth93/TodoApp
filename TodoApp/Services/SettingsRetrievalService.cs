using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TodoApp.Services;

public class ConnectionStrings
{
    public string TodoDbConnectionString { get; set; }
}

public static class SettingsRetrievalService
{
    private static IConfiguration _configuration;
    public static ConnectionStrings _connectionStrings { get; private set; }

    public static void SettingsRetrievalServiceConfigure(IConfiguration config)
    {
        _configuration = config;
        MapConnectionStirngs();
    }

    private static void MapConnectionStirngs()
    {
        _connectionStrings.TodoDbConnectionString = _configuration.GetConnectionString("TododbConnectionString");
    }
}