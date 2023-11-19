using Newtonsoft.Json;

namespace ApiTAF.Entities
{
    public class ResponsePostUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
