using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using quack_api.Enums;
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
        public RecommenderController(IOptions<RecommenderSettings> recommendersettingsAccessor)
        {
            recommenderSettings = recommendersettingsAccessor.Value;
            RecommenderService = new RecommenderService();
        }
        private readonly RecommenderSettings recommenderSettings;
        protected IRecommenderService RecommenderService { get; set; }

        [HttpGet]
        public async Task<ActionResult<QuackResponse<PlaylistDTO>>> GetPlaylist(string accessToken, QuackLocationType location)
        {
            return await ControllerUtil.GetResponse(
                async () => await RecommenderService.GetPlaylist(recommenderSettings, accessToken, location),
                (serviceResponse) => new QuackResponse<PlaylistDTO>(serviceResponse.Result));
        }
    }
}