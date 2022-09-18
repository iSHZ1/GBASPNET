using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class RamMetric
    {
        public int Id { get; set; }

        public int AgentId { get; set; }

        [JsonPropertyName("time")]
        public TimeSpan Time { get; set; }

        [JsonPropertyName("value")]
        public int Value { get; set; }
    }
}
