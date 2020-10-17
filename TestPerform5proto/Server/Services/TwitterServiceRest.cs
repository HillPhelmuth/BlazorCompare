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
        public Task<Tweets> GetAllTweets()
        {
            string result = ResultFromJsonFile();

            var deets = JsonSerializer.Deserialize<TweeterDetails>(result);
            var texts = deets.Tweeters.Select(x => x.Text).ToList();
            var count = texts.Count;
            return Task.FromResult(new Tweets {Count = count, Id = 1, Texts = texts});
        }

        public Task<TweeterDetails> GetAllDetails(int count)
        {
            count = count <= 7000 ? count : 7000;
            string result = ResultFromJsonFile();
            var deets = JsonSerializer.Deserialize<TweeterDetails>(result);
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
