using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlazorStrap;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.JSInterop;
using TestPerform5proto.Client.Protos;

namespace TestPerform5proto.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton(services =>
            {
               
                var config = services.GetRequiredService<IConfiguration>();
                var backendUrl = config["BackendUrl"] ?? builder.HostEnvironment.BaseAddress;
                var channel = GrpcChannel.ForAddress(backendUrl, new GrpcChannelOptions
                {
                    HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())
                });
                var client = new TweeterService.TweeterServiceClient(channel);
                return client;
               
            });
            builder.Services.AddSingleton(serviceProvider => (IJSInProcessRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            builder.Services.AddSingleton(serviceProvider => (IJSUnmarshalledRuntime)serviceProvider.GetRequiredService<IJSRuntime>());
            builder.Services.AddBootstrapCss();
            await builder.Build().RunAsync();
        }
    }
}
