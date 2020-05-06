using System;
using System.Text.Json.Serialization;

namespace WebApiClient
{
    public class Repository
    {
        [JsonPropertyName("name")]
        public string repoName { get; set; }
        [JsonPropertyName("id")]
        public int repoId { get; set; }
        [JsonPropertyName("pushed_at")]
        public DateTime LastPushUtc { get; set; }
        public DateTime lastPush => LastPushUtc.ToLocalTime();
    }
}