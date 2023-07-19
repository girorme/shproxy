using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sh_proxy
{
    class ConfigManager
    {
        public string? SshServer { get; set; }
        public string? SshUsername{ get; set; }
        public string? SshPassword { get; set; }
        public uint ProxyPortSocks { get; set; }
        public uint ProxyPortHttp { get; set; }
    }
}
