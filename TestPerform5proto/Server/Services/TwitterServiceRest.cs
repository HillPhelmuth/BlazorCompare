using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using TestPerform5proto.Shared;

namespace TestPerform5proto.Server.Services
{
    public class TwitterServiceRest
    {
        private readonly TweeterDetails _tweeterDetails;

        public TwitterServiceRest()
        {
            var detailsJson = ResultFromJsonFile();
            _tweeterDetails = JsonSerializer.Deserialize<TweeterDetails>(detailsJson);
        }
        // Weird compiler error requires adding back this method. For some reason it's still seeing the reference to this old method in the controller.
        [Obsolete("This should be unnecessary")]
        public async Task<Tweets> GetAllTweets(){return new Tweets();}
        public Task<TweeterDetails> GetAllDetails(int count)
        {
            count = count <= 7000 ? count : 7000;
            string result = ResultFromJsonFile();
            var deets = _tweeterDetails;
            var tweeters = deets.Tweeters.GetRange(0, count);
            deets.Tweeters = tweeters;
            return Task.FromResult(deets);
        }



        private static string ResultFromJsonFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var tweetJsonFile = assembly.GetManifestResourceNames()
                .SingleOrDefault(s => s.EndsWith("TrumpTweets.json"));
            string result = "";

            using var stream = assembly.GetManifestResourceStream(tweetJsonFile);
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }
    }
}
