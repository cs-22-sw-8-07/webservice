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
        public async Task<ServiceResponse<PlaylistDTO>> GetPlaylist(RecommenderSettings recommenderSettings, string accessToken, QuackLocationType location)
        {
            return await RecommenderServiceUtil.GetResponse(async () =>
            {
                string[] args = { recommenderSettings.RecommenderPath, accessToken, ((int)location).ToString() };
                string pythonPath = recommenderSettings.PythonPath;

                string arguments = string.Join(" ", args);

                using (CommandLineProcess cmd = new CommandLineProcess(pythonPath, arguments))
                {
                    // Get result from script
                    var result = await cmd.Run();

                    // Check for errors occurred in the script
                    if (result.Item1 > 0)
                        return new ServiceResponse<PlaylistDTO>(errorNo: (int)ResponseErrors.SomethingWentWrongInTheRecommender);
                    // Parse result from script
                    var response = JsonSerializer.Deserialize<ServiceResponse<PlaylistDTO>>(result.Item2);
                    // Return response
                    return new ServiceResponse<PlaylistDTO>(response.Result);
                }
            });
        }
    }
}
