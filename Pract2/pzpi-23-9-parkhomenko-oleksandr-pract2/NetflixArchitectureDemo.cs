using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetflixArchitectureDemo
{
    public class GraphQlRequest
    {
        [JsonPropertyName("query")]
        public string Query { get; set; } = string.Empty;
    }

    public class GraphQlResponse<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }

    public class HomePageData
    {
        [JsonPropertyName("profile")]
        public Profile? Profile { get; set; }

        [JsonPropertyName("recommendations")]
        public List<Recommendation>? Recommendations { get; set; }

        [JsonPropertyName("continueWatching")]
        public List<ContinueWatchingItem>? ContinueWatching { get; set; }
    }

    public class Profile
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    public class Recommendation
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("genre")]
        public string? Genre { get; set; }

        [JsonPropertyName("matchScore")]
        public int MatchScore { get; set; }
    }

    public class ContinueWatchingItem
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("progress")]
        public int Progress { get; set; }
    }

    public class NetflixApiClient
    {
        private readonly HttpClient _httpClient;

        public NetflixApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HomePageData?> GetHomePageDataAsync()
        {
            var request = new GraphQlRequest
            {
                Query = @"
                query HomePageData {
                  profile {
                    id
                    name
                  }
                  recommendations {
                    title
                    genre
                    matchScore
                  }
                  continueWatching {
                    title
                    progress
                  }
                }"
            };

            using var response = await _httpClient.PostAsJsonAsync("/graphql", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GraphQlResponse<HomePageData>>();
            return result?.Data;
        }
    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.netflix.example")
            };

            var client = new NetflixApiClient(httpClient);
            var homePageData = await client.GetHomePageDataAsync();

            if (homePageData?.Profile != null)
            {
                Console.WriteLine($"User: {homePageData.Profile.Name}");
            }

            if (homePageData?.Recommendations != null)
            {
                Console.WriteLine("Recommendations:");
                foreach (var item in homePageData.Recommendations)
                {
                    Console.WriteLine($"- {item.Title} ({item.Genre}), score: {item.MatchScore}");
                }
            }

            if (homePageData?.ContinueWatching != null)
            {
                Console.WriteLine("Continue watching:");
                foreach (var item in homePageData.ContinueWatching)
                {
                    Console.WriteLine($"- {item.Title}, progress: {item.Progress}%");
                }
            }
        }
    }
}