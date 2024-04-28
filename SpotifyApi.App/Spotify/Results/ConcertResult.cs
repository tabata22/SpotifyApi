using System.Text.Json.Serialization;

namespace SpotifyApi.App.Spotify.Results;

public class ConcertResult
{
    [JsonPropertyName("events")]
    public Event[] Events { get; set; }
}

public class Event
{
    [JsonPropertyName("artists")]
    public string[] Artists { get; set; }
    
    [JsonPropertyName("venue")]
    public string Venue { get; set; }
    
    [JsonPropertyName("concerts")]
    public Concert[] Concerts { get; set; }
    
    [JsonPropertyName("openingDate")]
    public DateTime OpeningDate { get; set; }
    
    [JsonPropertyName("closingDate")]
    public DateTime ClosingDate { get; set; }
}

public class Concert
{
    [JsonPropertyName("artists")]
    public Artist[] Artists { get; set; }
}

public class Artist
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}