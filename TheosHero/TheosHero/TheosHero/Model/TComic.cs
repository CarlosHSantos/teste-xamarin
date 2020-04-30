using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TheosHero.Model
{
    public class TComic
    {
        [JsonProperty("available")]
        public int Available { get; set; }

        [JsonProperty("collectionURI")]
        public string CollectionURI { get; set; }

        [JsonProperty("items")]
        public ICollection<MarvelItem> Items { get; set; }
    }
}
