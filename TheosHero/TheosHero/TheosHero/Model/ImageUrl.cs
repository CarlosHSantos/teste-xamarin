using Newtonsoft.Json;
using TheosHero.Helper;

namespace TheosHero.Model
{
    public class ImageUrl : ObservableObject
    {
        public ImageUrl(string extension, string path)
        {
            Extension = extension;
            Path = path;
        }

        public ImageUrl()
        {

        }


        private string _extension;
        [JsonProperty("extension")]
        public string Extension
        {
            get
            {
                return _extension;
            }
            private set
            {
                _extension = value;
                OnPropertyChanged();
            }
        }

        private string _path;
        [JsonProperty("path")]
        public string Path
        {
            get
            {
                return _path;
            }
            private set
            {
                _path = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return $"{Path}.{Extension}";
        }
    }
}
