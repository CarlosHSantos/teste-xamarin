using Newtonsoft.Json;

namespace TheosHero.Model
{
    public class MarvelItem
    {
        [JsonProperty("resourceURI")]
        public string ResourceURI { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
