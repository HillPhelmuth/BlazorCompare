using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using TestPerform5proto.Server.Protos;
using TestPerform5proto.Shared;


namespace TestPerform5proto.Server.Services
{
    public class TweeterService : Protos.TweeterService.TweeterServiceBase
    {
        private readonly TweeterDetails _tweeterDetails;

        public TweeterService()
        {
            var detailsJson = ResultFromJsonFile();
            _tweeterDetails = JsonSerializer.Deserialize<TweeterDetails>(detailsJson);
        }

        public override Task<GetAllTweetDetailsResponse> GetAllTweetDetails(GetTweetRequest request, ServerCallContext context)
        {
            var countRequest = request.Count <= 7000 ? request.Count : 7000;
            string result = ResultFromJsonFile();
            var deets = _tweeterDetails;
            int count = countRequest;
            var proDeets = deets.Tweeters.GetRange(0, countRequest).Select(deet => new GetAllTweetDetailsResponse.Types.TweetDeets
                {
                    Text = deet.Text,
                    CreatedAt = deet.CreatedAt,
                    RetweetCount = deet.RetweetCount,
                    FavoriteCount = deet.FavoriteCount,
                    IdStr = deet.IdStr
                })
                .ToList();
            var results = new GetAllTweetDetailsResponse { Id = 1, Count = count };
            results.TweetDeets.AddRange(proDeets);
            return Task.FromResult(results);


        }
        private static string ResultFromJsonFile()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var tweetJsonFile = assembly.GetManifestResourceNames()
                .SingleOrDefault(s => s.EndsWith("TrumpTweets.json"));
            string result = "";
           
            using var stream = assembly.GetManifestResourceStream(tweetJsonFile);
            using var reader = new StreamReader(stream);
            result = reader.ReadToEnd();

            return result;
        }
    }
}
