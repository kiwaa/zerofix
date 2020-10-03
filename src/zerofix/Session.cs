using System;

namespace zerofix
{
    public abstract class Session
    {
        private readonly IApplication _application;
        private ITransport _transport;

        private int _seqNum = 1;

        public SessionIdentity ID { get; protected set; }
        public SessionIdentity RevertID { get; protected set; }

        public int HeartbeatInterval { get; protected set; }

        public Session(IApplication application)
        {
            _application = application;
        }

        internal virtual void OnConnected()
        {
            Console.WriteLine("connected");
            _application.OnCreate(this);
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
            OnMessageInternal(msg);
        }

        internal void OnDisconnected()
        {
            Console.WriteLine("disconnected");
            // todo: connect again
        }

        private void OnMessageInternal(FixMessage msg)
        {
            var field = msg.Header[Tags.MsgType] as FixField<char>;

            switch (field.Value)
            {
                case '0':
                    var hb = new FixMessage(ID, '0');
                    hb.Add(52, DateTime.UtcNow.ToString(FixSettings.DateTimeFormat));
                    SendAsync(hb);
                    break;
                case '1':
                    var testAck = new FixMessage(ID, '0');
                    testAck.Add(52, DateTime.UtcNow.ToString(FixSettings.DateTimeFormat));
                    testAck.Add(msg[Tags.TestReqID]);
                    SendAsync(testAck);
                    break;
                case '2':
                    throw new NotImplementedException();
                case '3':
                    throw new NotImplementedException();
                case '4':
                    throw new NotImplementedException();
                case '5':
                    _transport.DisconnectAsync();
                    break;
                case 'A':
                    ID = new SessionIdentity(msg.Header[Tags.BeginString].AsString(), msg.Header[Tags.SenderCompID].AsString(), msg.Header[Tags.TargetCompID].AsString());
                    RevertID = new SessionIdentity(msg.Header[Tags.BeginString].AsString(), msg.Header[Tags.TargetCompID].AsString(), msg.Header[Tags.SenderCompID].AsString());
                    //HeartbeatInterval = msg[Tag]

                    var logon = new FixMessage(RevertID, 'A');
                    logon.Add(52, DateTime.UtcNow.ToString(FixSettings.DateTimeFormat));
                    logon.Add(98, "0");
                    logon.Add(108, "30");
                    SendAsync(logon);
                    break;
                default:
                    // non session level message
                    _application.OnMessage(msg);
                    break;
            }
        }

    }
}
