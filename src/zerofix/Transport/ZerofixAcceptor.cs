using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace zerofix.Transport
{
    public class ZerofixAcceptor  : TcpServer
    {
        private Func<Session> _sessionFact;
        public ZerofixAcceptor(IPAddress address, int port, Func<Session> sessionFact) : base(address, port)
        {
            _sessionFact = sessionFact;
        }

        protected override TcpSession CreateSession() => new ZerofixSession(this, _sessionFact);

    }
}
