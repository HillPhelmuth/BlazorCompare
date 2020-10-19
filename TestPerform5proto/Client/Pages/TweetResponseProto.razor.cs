using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components;
using Google.Protobuf.WellKnownTypes;
using TestPerform5proto.Client.Protos;
using TestPerform5proto.Shared;

namespace TestPerform5proto.Client.Pages
{
    public partial class TweetResponseProto
    {
      
        [Inject]
        public TweeterService.TweeterServiceClient Client { get; set; }
        [Inject]
        public HttpClient Http { get; set; }
        private GetAllTweetDetailsResponse _tweetDetails = new GetAllTweetDetailsResponse();
       private TweeterDetails _restDetails = new TweeterDetails();
       private string _deetTime = "";
       private string _restDeetsTime = "";
        private int _protoRequest = 2000;
        private int _restRequest = 2000;
        private bool protoBusy;
        private bool restBusy;

        private void GetgRpc()
        {
            protoBusy = true;
            StateHasChanged();
            _ = GetDetailsRPC();
        }
        private async Task GetDetailsRPC()
        {
            
            var sw = new Stopwatch();
            sw.Start();
            _tweetDetails = await Client.GetAllTweetDetailsAsync(new GetTweetRequest{Count = _protoRequest});
            sw.Stop();
            Console.WriteLine($"proto tweet details took {sw.ElapsedMilliseconds}ms");
            _deetTime = $" {sw.ElapsedMilliseconds}ms";
            protoBusy = false;
            await InvokeAsync(StateHasChanged);
        }

        private void GetRest()
        {
            restBusy = true;
            StateHasChanged();
            _ = GetDetailsRest();
        }
       
        private async Task GetDetailsRest()
        {
            
            var sw = new Stopwatch();
            sw.Start();
            _restDetails = await Http.GetFromJsonAsync<TweeterDetails>($"api/tweeterRest/details/{_restRequest}");
            sw.Stop();
            Console.WriteLine($"Rest tweet Details took {sw.ElapsedMilliseconds}ms");
            _restDeetsTime = $" {sw.ElapsedMilliseconds}ms";
            restBusy = false;
            await InvokeAsync(StateHasChanged);
        }
    }
}
