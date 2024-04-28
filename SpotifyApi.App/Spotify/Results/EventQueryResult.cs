namespace SpotifyApi.App.Spotify.Results;

public class EventQueryResult
{
    public string Title { get; set; }
    public DateOnly Date { get; set; }
    public string Venue { get; set; }
    public int ArtistsCount { get; set; }
    public int ConcertsCount { get; set; }
}