using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpotifyApi.App;
using SpotifyApi.App.Spotify;

var serviceProvider = SetupServices();

Console.WriteLine("enter location");
var location = Console.ReadLine();

var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
var cancellationToken = cts.Token;

using var service = serviceProvider.GetRequiredService<ISpotifyService>();

var events = await service.GetEventsByLocationAsync(location?.Trim(), cancellationToken);
foreach (var @event in events)
{
    var result = $"Title: {@event.Title}, Date: {@event.Date}, Venue: {@event.Venue}, Artists: {@event.ArtistsCount}, Concerts: {@event.ConcertsCount}";
            
    Console.WriteLine(result);
    Console.WriteLine("-----------------------------------------------------------------");
}

return;

ServiceProvider SetupServices()
{
    var builder = new ConfigurationBuilder();
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("settings.json", optional: false, reloadOnChange: true);

    IConfiguration configuration = builder.Build();
    
    var settings = new Settings();
    configuration.Bind(nameof(Settings), settings);

    return new ServiceCollection()
        .AddHttpClient()
        .Configure<Settings>(configuration.GetSection(nameof(settings)))
        .AddSingleton<ISpotifyService, SpotifyService>()
        .BuildServiceProvider();
}