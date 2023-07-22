using System.Threading.Tasks;

namespace sh_proxy
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class CommandRunner
    {
        private CancellationTokenSource? cancellationTokenSource;
        private Process? process;

        public void StartCommand(string command)
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            process = new Process
            {
                StartInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                }
            };

            process.OutputDataReceived += (sender, args) => Console.WriteLine(args.Data);
            process.ErrorDataReceived += (sender, args) => Console.WriteLine(args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Task.Run(() =>
            {
                try
                {
                    process.WaitForExit();
                }
                catch (Exception ex)
                {
                    // Handle process exit error if needed
                    Console.WriteLine("Error waiting for process exit: " + ex.Message);
                }
            }, token);
        }

        public void StopCommand()
        {
            Process[] processes = Process.GetProcessesByName("sthp");

            foreach (Process process in processes)
            {
                try
                {
                    process.Kill();
                    process.WaitForExit(); // Wait for the process to exit gracefully (optional)
                }
                catch (Exception ex)
                {
                    // Handle process kill error if needed
                    Console.WriteLine($"Error killing process {process.ProcessName}: {ex.Message}");
                }
            }
        }
    }
}
