using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using Renci.SshNet;

namespace sh_proxy
{
    class ProxyManager
    {
        private SshClient sshClient;
        private ForwardedPortDynamic socksProxy;

        public void StartSshAndSocksProxy()
        {
            StartSshClient();
            CreateSocksFromSsh();
        }

        public void StopSshAndSocksProxy()
        {
            StopSocks();
            StopSshClient();
        }

        public void StartSshClient()
        {
            string sshHost = "localhost";
            int sshPort = 22;
            string sshUsername = "ubuntu";
            string sshPassword = "admin";

            sshClient = new SshClient(sshHost, sshPort, sshUsername, sshPassword);
            sshClient.Connect();
        }

        public void CreateSocksFromSsh()
        {
            socksProxy = new ForwardedPortDynamic("127.0.0.1", 1337);
            sshClient.AddForwardedPort(socksProxy);
            socksProxy.Start();
        }

        public void StopSocks() => socksProxy.Stop();
        public void StopSshClient() => sshClient.Disconnect();
    }
}
