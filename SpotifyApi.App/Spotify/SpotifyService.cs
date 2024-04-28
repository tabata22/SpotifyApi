using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SpotifyApi.App.Spotify.Results;

namespace SpotifyApi.App.Spotify;

public class SpotifyService : ISpotifyService
{
    private readonly HttpClient _httpClient;

    public SpotifyService(IHttpClientFactory httpClientFactory, IOptions<Settings> settingsOptions)
    {
        var settings = settingsOptions.Value;
        
        _httpClient = httpClientFactory.CreateClient("consoleApp");
        _httpClient.BaseAddress = new Uri(settings.ApiBaseUrl);
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", settings.ApiKey);
        _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", settings.ApiHost);
    }

    public async Task<IReadOnlyList<EventQueryResult>> GetEventsByLocationAsync(string? location, CancellationToken cancellationToken)
    {
        var json = await _httpClient.GetStringAsync($"concerts/?gl={location}", cancellationToken);
        var concertResult = JsonConvert.DeserializeObject<ConcertResult>(json);
        if (concertResult is null)
            return Array.Empty<EventQueryResult>();

        var events = concertResult.Events.Where(x => x.OpeningDate.Date == x.ClosingDate.Date)
            .OrderBy(x => x.Venue)
            .ThenBy(x => x.OpeningDate.Date);

        return events.Select(x => new EventQueryResult
        {
            Title = x.Venue,
            Date = DateOnly.Parse(x.OpeningDate.ToShortDateString()),
            Venue = x.Venue,
            ArtistsCount = x.Artists.Length,
            ConcertsCount = x.Concerts.Length
        }).ToList();
    }

    public void Dispose()
    {
        _httpClient.Dispose();
        GC.SuppressFinalize(this);
    }
}