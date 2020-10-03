using System;

namespace zerofix.Transport
{
    internal class ZerofixSession : TcpSession, ITransport
    {
        private Session _session;
        private FixMessageReader _parser;

        public ZerofixSession(ZerofixAcceptor acceptor, Func<Session> sessionFact) : base(acceptor)
        {
            _session = sessionFact();
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

        public bool DisconnectAsync()
        {
            Disconnect();
            return true;
        }
    }
}