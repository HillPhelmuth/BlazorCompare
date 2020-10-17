using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPerform5proto.Server.Services;
using TestPerform5proto.Shared;

namespace TestPerform5proto.Server.Controllers
{
    [Route("api/tweeterRest")]
    [ApiController]
    public class TweeterRestController : ControllerBase
    {
        private TwitterServiceRest _twitterService;

        public TweeterRestController(TwitterServiceRest twitterService)
        {
            _twitterService = twitterService;
        }

        [HttpGet("text")]
        public async Task<Tweets> GetTweets()
        {
            return await _twitterService.GetAllTweets();
        }

        [HttpGet("details/{count}")]
        public async Task<TweeterDetails> GetDetails(int count)
        {
            return await _twitterService.GetAllDetails(count);
        }
    }
}
