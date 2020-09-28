using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    internal class ZeroFixClient : TcpClient, ITransport
    {
        private Session _session;
        private FixMessageReader _parser;
        public ZeroFixClient(Session session, string address, int port) : base(address, port)
        {
            _session = session;
            _session.SetTransport(this);
            _parser = new FixMessageReader();
        }

        public ZeroFixClient(Session session, IPAddress address, int port) : base(address, port)
        {
            _session = session;
            _session.SetTransport(this);
            _parser = new FixMessageReader();
        }

        protected override void OnConnected()
        {
            _session.OnConnected();
        }

        protected override void OnDisconnected()
        {
            _session.OnDisconnected();
        }

        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            var msg = _parser.Parse(buffer.AsSpan((int)offset, (int)size));
            _session.OnMessage(msg);
        }
    }
}
