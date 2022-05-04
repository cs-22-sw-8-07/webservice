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
        public async Task<ServiceResponse<PlaylistDTO>> GetPlaylist(RecommenderSettings recommenderSettings, string accessToken, QuackLocationType location, int[] previousOffsets)
        {
            return await RecommenderServiceUtil.GetResponse(async () =>
            {
                // Check if RecommenderPath exists
                if (!File.Exists(recommenderSettings.RecommenderPath))
                    return new ServiceResponse<PlaylistDTO>(errorNo: (int)ResponseErrors.RecommenderPathWrong);

                // Setting up arguments for CommandlineProccess                
                string arguments = string.Join(" ", (new string[]{
                    recommenderSettings.RecommenderPath,
                    accessToken,
                    ((int)location).ToString(),
                    recommenderSettings.RecommenderType,
                    string.Join(";",previousOffsets)
                }));

                using (CommandLineProcess cmd = new CommandLineProcess(recommenderSettings.PythonPath, arguments))
                {
                    // Get result from script
                    var result = await cmd.Run();

                    // Check for errors occurred in the script
                    if (result.Item1 > 0)
                        return new ServiceResponse<PlaylistDTO>(errorNo: (int)ResponseErrors.SomethingWentWrongInTheRecommender);

                    // Check if result from CommandLineProcess is empty or null
                    if (string.IsNullOrWhiteSpace(result.Item2))
                        return new ServiceResponse<PlaylistDTO>(errorNo: (int)ResponseErrors.ResultFromCommandlineEmpty);

                    // Parse result from script and return response
                    return JsonSerializer.Deserialize<ServiceResponse<PlaylistDTO>>(result.Item2);
                }
            });
        }
    }
}
