using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quack_api.Interfaces;
using quack_api.Objects;

namespace quack_api.RecommenderAccessLayer
{
    public class RecommenderService : IRecommenderService
    {
        Task<DataResponse> IRecommenderService.GetPlaylist(string accessToken, int qlt)
        {
            throw new NotImplementedException();
        }
    }
}
