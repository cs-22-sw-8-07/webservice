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

namespace quack_api.RecommenderAccessLayer
{
    public class RecommenderService : IRecommenderService
    {
        public async Task<DataResponse<PlaylistDTO>> GetPlaylist(RecommenderSettings recommenderSettings, string accessToken, string location)
        {
            return await RecommenderServiceUtil.GetResponse(async () =>
            {
                string[] args = { recommenderSettings.RecommenderPath + @"\src\main.py", accessToken, location };
                string cmd = recommenderSettings.PythonPath;

                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = cmd;
                start.Arguments = string.Join(" ", args);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                Process process;
                string result = "";

                try
                {
                    process = Process.Start(start);
                }
                catch (Exception)
                {
                    return new DataResponse<PlaylistDTO>((int)ResponseErrors.PathToPythonExeNotFound);
                }

                using (StreamReader reader = process.StandardOutput)
                {
                    result = reader.ReadToEnd();
                }
                await process.WaitForExitAsync();
                var response = JsonSerializer.Deserialize<RecommenderResponse>(result);
                return new DataResponse<PlaylistDTO>(response.Result);
            });
        }
    }
}
