using System.Text.Json.Serialization;

namespace MetricsManager.Models
{
    public class CpuMetric
    {
        public int Id { get; set; }

        public int AgentId { get; set; }

        [JsonPropertyName("value")]
        public int value { get; set; }

        [JsonPropertyName("time")]
        public long time { get; set; }


    }
}
