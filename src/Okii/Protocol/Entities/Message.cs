using Newtonsoft.Json;

namespace Okii.Protocol
{
    public class Message
    {
        [JsonProperty("author", Required = Required.AllowNull)]
        public string Author { get; init; }

        [JsonProperty("content", Required = Required.Always)]
        public string Content { get; init; }
    }
}
