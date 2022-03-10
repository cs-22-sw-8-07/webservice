using Microsoft.AspNetCore.Mvc;
using quack_api.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quack_api.Interfaces
{
    public interface IRecommenderService
    {
        /// <summary>
        /// Takes a string accessToken and a int qlt and creates a playlist based on the qlt
        /// </summary>
        /// <param name="accessToken">string of a spotify acces token</param>
        /// <param name="qlt">Int representation of Quack location type</param>
        /// <returns></returns>
        public Task<DataResponse> GetPlaylist(string accessToken, int qlt);

    }
}