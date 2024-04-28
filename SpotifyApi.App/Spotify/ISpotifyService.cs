using SpotifyApi.App.Spotify.Results;

namespace SpotifyApi.App.Spotify;

public interface ISpotifyService : IDisposable
{
    Task<IReadOnlyList<EventQueryResult>> GetEventsByLocationAsync(string? location, CancellationToken cancellationToken);
}