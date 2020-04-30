using Newtonsoft.Json;
using System.Collections.Generic;
using TheosHero.Helper;

namespace TheosHero.Model
{
    public class Hero : ObservableObject
    {
        private int id;
        [JsonProperty("id")]
        public int Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        private string name;
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string description;
        [JsonProperty("description")]
        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        private ImageUrl thumbnail;
        [JsonProperty("thumbnail")]
        public ImageUrl Thumbnail
        {
            get
            {
                return thumbnail;
            }
            private set
            {
                thumbnail = value;
                OnPropertyChanged();
            }
        }

        private TComic comics;
        [JsonProperty("comics")]
        public TComic Comics
        {
            get
            {
                return comics;
            }
            private set
            {
                comics = value;
                OnPropertyChanged();
            }
        }

        private TComic series;
        [JsonProperty("series")]
        public TComic Series
        {
            get
            {
                return series;
            }
            private set
            {
                series = value;
                OnPropertyChanged();
            }
        }

        private TComic stories;
        [JsonProperty("stories")]
        public TComic Stories
        {
            get
            {
                return stories;
            }
            private set
            {
                stories = value;
                OnPropertyChanged();
            }
        }

        private TComic events;
        [JsonProperty("events")]
        public TComic Events
        {
            get
            {
                return events;
            }
            private set
            {
                events = value;
                OnPropertyChanged();
            }
        }
    }
}
