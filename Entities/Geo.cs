using Newtonsoft.Json;

namespace ApiTAF.Entities
{
    public class Geo
    {
        [JsonProperty("lat")]
        public string Lat {  get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }
    }
}