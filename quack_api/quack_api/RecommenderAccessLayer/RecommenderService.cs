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
        public async Task<DataResponse<PlaylistDTO>> GetPlaylist(RecommenderSettings recommenderSettings, string accessToken, QuackLocationType location)
        {
            return await RecommenderServiceUtil.GetResponse(async () =>
            {
                int numberLocation = (int)location;
                string[] args = { recommenderSettings.RecommenderPath, accessToken, numberLocation.ToString() };
                string pythonPath = recommenderSettings.PythonPath;

                string arguments = string.Join(" ", args);

                Tuple<int, string> result = new (-1, "");

                using (CommandLineProcess cmd = new CommandLineProcess(pythonPath, arguments))
                {
                    result = await cmd.Run();
                }

                int exitCode = result.Item1;
                var output = result.Item2;

                var response = JsonSerializer.Deserialize<RecommenderResponse>(output);
                return new DataResponse<PlaylistDTO>(response.Result);
            });
        }
    }
}
