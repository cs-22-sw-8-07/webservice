using Microsoft.AspNetCore.Mvc;
using quack_api.Interfaces;
using quack_api.Models;
using quack_api.Objects;
using quack_api.RecommenderAccessLayer;
using quack_api.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quack_api.Controllers
{
    [ApiController]
    [Route("Quack/Recommender/[action]")]
    public class RecommenderController : BaseController
    {
        protected IRecommenderService RecommenderService { get; set; }

        [HttpGet]
        public async Task<ActionResult<QuackResponse>> GetPlaylist(string accessToken, int qlt)
        {
            return await ControllerUtil.GetResponse(
                async () => await RecommenderService.GetPlaylist(accessToken, qlt),
                (dataResponse) => new QuackResponse<PlaylistDTO>(dataResponse.Result));
        }
    }
}