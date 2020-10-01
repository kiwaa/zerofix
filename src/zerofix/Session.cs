using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    public class Session
    {
        private HashSet<char> _sessionLevel = new HashSet<char>(new[] { '0', '1', '2', '3', '4', '5', 'A' });

        private readonly IApplication _application;
        private ITransport _transport;

        private int _seqNum = 1;

        public SessionIdentity ID { get; }

        public Session(IApplication application)
        {
            _application = application;
            ID = new SessionIdentity("FIX.4.2", "CLIENT1", "EXECUTOR");
        }

        internal void OnConnected()
        {
            Console.WriteLine("connected");

            _application.OnCreate(this);
            var msg = new FixMessage(ID, 'A');
            msg.Add(52, DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff"));
            msg.Add(98, "0");
            msg.Add(108, "30");

            SendAsync(msg);
        }

        public void SendAsync(FixMessage msg)
        {
            Console.WriteLine("OUT: " + msg.ToString());
            msg.Header.Add(Tags.MsgSeqNum, _seqNum++);
            _transport.SendAsync(msg.ToByteArray());
        }

        internal void SetTransport(ITransport transport)
        {
            _transport = transport;
        }

        internal void OnMessage(FixMessage msg)
        {
            Console.WriteLine("IN: " + msg.ToString());
            if (IsSessionLevelMessage(msg))
                Process(msg);
            else
            {
                _application.OnMessage(msg);
            }
        }

        private void Process(FixMessage msg)
        {
            var field = msg.Header[Tags.MsgType] as FixField<char>;

            switch (field.Value)
            {
                case '0':
                    var hb = new FixMessage(ID, '0');
                    hb.Add(52, DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff"));
                    SendAsync(hb);
                    break;
                default:
                    break;
            }
        }

        private bool IsSessionLevelMessage(FixMessage msg)
        {
            var field = msg.Header[Tags.MsgType] as FixField<char>;
            return _sessionLevel.Contains(field.Value);
        }

        internal void OnDisconnected()
        {
            Console.WriteLine("disconnected");
            // todo: connect again
        }
    }
}
