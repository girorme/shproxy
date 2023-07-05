using System;
using System.Diagnostics;
using System.Threading;

namespace via_proxy
{
    internal class ProcessManager
    {
        public void start_socks_via_ssh()
        {
            // Start the SSH process
            Process sshProcess = new Process();
            sshProcess.StartInfo.FileName = "calc.exe"; 
            sshProcess.Start();

            // Store the PID of the SSH process
            int sshProcessId = sshProcess.Id;
            Console.WriteLine($"SSH process started with PID: {sshProcessId}");

            // At some point, terminate the SSH process
            Console.WriteLine("Terminating SSH process...");
            Process sshProcessToKill = Process.GetProcessById(sshProcessId);
            sshProcessToKill.Kill();
            sshProcessToKill.WaitForExit();

            Console.WriteLine("SSH process terminated.");
        }
    }
}
