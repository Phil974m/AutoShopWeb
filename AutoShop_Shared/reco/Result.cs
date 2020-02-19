using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShop_Shared.reco
{

        public class Result
        {

            [JsonProperty("predictions")]
            public List<Prediction> Predictions { get; set; }
        }

        public class Prediction
        {
            [JsonProperty("probability")]
            public float Probability { get; set; }

            [JsonProperty("tagId")]
            public string TagId { get; set; }

            [JsonProperty("tagName")]
            public string TagName { get; set; }
        }

    }


