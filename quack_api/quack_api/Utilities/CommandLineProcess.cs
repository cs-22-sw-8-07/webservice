using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace quack_api.Utilities
{
    public sealed class CommandLineProcess : IDisposable
    {
        public string Path { get; }
        public string Arguments { get; }
        public int? ExitCode { get; private set; }

        private Process Process;

        public CommandLineProcess(string path, string arguments)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            if (!File.Exists(path)) throw new ArgumentException($"Executable not found: {path}");
            Arguments = arguments;
        }

        public async Task<Tuple<int, string>> Run()
        {
            Process = new Process()
            {
                EnableRaisingEvents = true,
                StartInfo = new ProcessStartInfo()
                {
                    FileName = Path,
                    Arguments = Arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                },
            };

            if (!Process.Start()) throw new Exception("Process could not be started");
            var output = await Process.StandardOutput.ReadToEndAsync();
            await Process.WaitForExitAsync();
            try { Process.Refresh(); } catch { }
            return new ((ExitCode = Process.ExitCode).Value, output);
        }

        public void Dispose()
        {
            try { Process?.Dispose(); }
            catch { }
        }
    }
}
