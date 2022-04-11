using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using quack_api.Enums;
using quack_api.Models;
using quack_api.Interfaces;
using quack_api.Objects;
using quack_api.Test.Mocks;
using Quack.Utilities;
using quack_api.Utilities;

namespace quack_api.Test.Mocks
{
    public class MockRecommenderService : IRecommenderService
    {
        public static string LoadJson(string asset)
        {
            using (StreamReader r = new(asset))
            {
                return r.ReadToEnd();
            }
        }
        public Task<ServiceResponse<PlaylistDTO>> GetPlaylist(RecommenderSettings recommenderSettings, string accessToken, QuackLocationType location)
        {
            if (accessToken == "ProcessCouldNotStart")
            {
                return RecommenderServiceUtil.GetResponse<ServiceResponse<PlaylistDTO>>(() =>
                {
                    throw new ProcessCouldNotStartException();
                });
            }

            if (accessToken == "AnExceptionOccurredInTheSAL")
            {
                return RecommenderServiceUtil.GetResponse<ServiceResponse<PlaylistDTO>>(() =>
                {
                    throw new Exception();
                });
            }

            if (accessToken == "AnExceptionOccurredInAController")
            {
                throw new Exception();
            }

            var response = JsonSerializer.Deserialize<ServiceResponse<PlaylistDTO>>(LoadJson("RecommenderMockResponse.json"));
            return Task.FromResult(new ServiceResponse<PlaylistDTO>(response.Result));
        }
    }
}
