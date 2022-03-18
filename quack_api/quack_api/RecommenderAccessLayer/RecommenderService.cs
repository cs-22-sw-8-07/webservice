using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using quack_api.Interfaces;
using quack_api.Objects;
using quack_api.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using Quack.Utilities;
using quack_api.Enums;
using quack_api.Utilities;
using System.Text;

namespace quack_api.RecommenderAccessLayer
{
    public class RecommenderService : IRecommenderService
    {
        public async Task<DataResponse<PlaylistDTO>> GetPlaylist(RecommenderSettings recommenderSettings, string accessToken, string location)
        {
            return await RecommenderServiceUtil.GetResponse(async () =>
            {
                string[] args = { recommenderSettings.RecommenderPath, accessToken, location };
                string pythonPath = recommenderSettings.PythonPath;

                string arguments = string.Join(" ", args);

                string result = string.Empty;

                using (CommandLineProcess cmd = new CommandLineProcess(pythonPath, arguments))
                {
                    int exitCode = cmd.Run(out result, out string processError);
                }

                var response = JsonSerializer.Deserialize<RecommenderResponse>(result);
                return new DataResponse<PlaylistDTO>(response.Result);
            });
        }
    }
}
