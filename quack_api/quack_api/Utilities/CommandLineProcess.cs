using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace quack_api.Utilities
{ 
    public class PythonPathNotFoundException : Exception
    {
        public PythonPathNotFoundException() { }
    }
    public class PythonPathNullException : Exception
    {
        public PythonPathNullException() { }
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
            Path = path ?? throw new PythonPathNullException();
            if (!File.Exists(path)) throw new PythonPathNotFoundException();
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
