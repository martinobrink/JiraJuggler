using Newtonsoft.Json;

namespace JiraJuggler.Shared.Model
{
    public class ProjectData
    {
        [JsonProperty("self")]
        public string ProjectUrl { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("avatarUrls")]
        public AvatarUrls AvatarUrls { get; set; }
    }

    public class AvatarUrls
    {
        public string _16x16 { get; set; }
        public string _24x24 { get; set; }
        public string _32x32 { get; set; }
        public string _48x48 { get; set; }
    }
}