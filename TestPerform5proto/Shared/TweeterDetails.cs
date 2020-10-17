using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestPerform5proto.Shared
{

    public partial class TweeterDetails
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }

        [JsonPropertyName("tweeters")]
        public List<Tweeter> Tweeters { get; set; }
    }

    public partial class Tweeter
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("created_at")]
        public string CreatedAt { get; set; }

        [JsonPropertyName("retweet_count")]
        public long RetweetCount { get; set; }

        [JsonPropertyName("favorite_count")]
        public long FavoriteCount { get; set; }

        [JsonPropertyName("id_str")]
        public string IdStr { get; set; }
    }

    public class Tweets
    {
        [JsonPropertyName("Id")]
        public long Id { get; set; }

        [JsonPropertyName("count")]
        public long Count { get; set; }
        [JsonPropertyName("texts")]
        public List<string> Texts { get; set; }
    }

}
