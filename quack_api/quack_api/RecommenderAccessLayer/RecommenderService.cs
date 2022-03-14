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
using Newtonsoft.Json.Linq;
using System.Text.Json;
using WASP.Utilities;

namespace quack_api.RecommenderAccessLayer
{
    public class RecommenderService : IRecommenderService
    {
        public async Task<DataResponse<PlaylistDTO>> GetPlaylist(string recommenderConnection, string accessToken, int qlt)
        {
            return await RecommenderServiceUtil.GetResponse(async () =>
            {
                string[] args = { recommenderConnection + @"\src\main.py", "test", "test2", "test3" };
                string cmd = recommenderConnection + @"\venv\Scripts\python.exe";

                ProcessStartInfo start = new ProcessStartInfo();
                start.FileName = cmd;
                start.Arguments = string.Join(" ", args);
                start.UseShellExecute = false;
                start.RedirectStandardOutput = true;
                using (Process process = Process.Start(start))
                {
                    string result = "";
                    using (StreamReader reader = process.StandardOutput)
                    {
                        result = reader.ReadToEnd();
                    }
                    await process.WaitForExitAsync();
                    var response = JsonSerializer.Deserialize<RecommenderResponse>(result);
                    return new DataResponse<PlaylistDTO>(response.Result);
                }
            });

            
            
        }
    }
}
