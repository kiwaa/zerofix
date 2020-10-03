using System;
using System.Collections.Generic;
using System.Net;
using zerofix.Transport;

namespace zerofix
{
    // naive implementation
    public class SessionStarter
    {
        private readonly IApplication _application;
        private readonly ISessionSettingsProvider _settings;

        private List<ZerofixInitiator> _initiators;
        private List<ZerofixAcceptor> _acceptors;
        
        public SessionStarter(IApplication application, ISessionSettingsProvider settings)
        {
            _initiators = new List<ZerofixInitiator>();
            _acceptors = new List<ZerofixAcceptor>();

            _application = application;
            _settings = settings;
        }

        public void Start()
        {
            foreach (var settings in _settings.GetAll())
            {
                if (settings.Get(SessionSettings.CONNECTION_TYPE) == SessionSettings.CONNECTION_TYPE_INITIATOR)
                {
                    var session = new InitiatorSession(_application, new SessionIdentity("FIX.4.2", "CLIENT1", "EXECUTOR"));
                    var client = new ZerofixInitiator(session, IPAddress.Loopback, 5001);
                    client.ConnectAsync();
                }
                else
                {
                    var server = new ZerofixAcceptor(IPAddress.Loopback, 5001, () => new AcceptorSession(_application));
                    server.Start();
                }
            }
        }

        public void Stop()
        {
            foreach (var session in _initiators)
                session.DisconnectAsync();
            foreach (var acceptor in _acceptors)
                acceptor.Stop();
        }
    }
}