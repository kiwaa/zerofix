using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Linq;
using System.Net.Sockets;
using System.IO;

namespace zerofix
{
    // naive implementation
    public class SocketInitiator
    {
        private readonly IApplication _application;
        private readonly ISessionSettingsProvider _settings;
        
        private TcpClient _client;

        public SocketInitiator(IApplication application, ISessionSettingsProvider settings)
        {
            _application = application;
            _settings = settings;
        }

        public void Start()
        {
            var session = new Session(_application);
            _client = new ZeroFixClient(session, IPAddress.Loopback, 5001);
            _client.ConnectAsync();
        }

        public void Stop()
        {
            _client.Disconnect();
        }
    }
}