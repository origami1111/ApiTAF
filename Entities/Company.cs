﻿using Newtonsoft.Json;

namespace ApiTAF.Entities
{
    public class Company
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("catchPhrase")]
        public string CatchPhrase { get; set; }

        [JsonProperty("bs")]
        public string Bs {  get; set; }
    }
}