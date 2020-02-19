using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoShop_Shared.Models
{
    public class Article
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }
        public string Label { get; set; }
        public float Prix { get; set; }
        public string Tag { get; set; }
        public string Photo { get; set; }
    }
}
