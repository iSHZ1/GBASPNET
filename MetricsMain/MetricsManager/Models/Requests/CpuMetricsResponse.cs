using System.Text.Json.Serialization;

namespace MetricsManager.Models.Requests
{
    public class CpuMetricsResponse
    {
        public int AgentId { get; set; }

        [JsonPropertyName("metrics")]
        public CpuMetric[] Metrics { get; set; }


        //public class Rootobject
        //{
        //    public Class1[] Property1 { get; set; }
        //}

        //public class Class1
        //{
        //    public int value { get; set; }
        //    public int time { get; set; }
        //}




    }
}
