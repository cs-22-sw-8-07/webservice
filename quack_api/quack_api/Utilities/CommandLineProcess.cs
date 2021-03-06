using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace quack_api.Utilities
{ 
    public class PathNotFoundException : Exception
    {
        public PathNotFoundException() { }
    }
    public class PathNullException : Exception
    {
        public PathNullException() { }
    }
    public class ProcessCouldNotStartException : Exception
    {
        public ProcessCouldNotStartException() { }
    }

    public sealed class CommandLineProcess : IDisposable
    {
        public string Path { get; }
        public string Arguments { get; }
        public int? ExitCode { get; private set; }

        private Process Process;

        public CommandLineProcess(string path, string arguments)
        {
            Path = path ?? throw new PathNullException();
            if (!File.Exists(path)) throw new PathNotFoundException();
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

            if (!Process.Start()) throw new ProcessCouldNotStartException();
            var output = await Process.StandardOutput.ReadToEndAsync();
            await Process.WaitForExitAsync();
            Process.Refresh();
            ExitCode = Process.ExitCode;
            return new (ExitCode.Value, output);
        }

        public void Dispose()
        {
            try { Process?.Dispose(); }
            catch { }
        }
    }
}
