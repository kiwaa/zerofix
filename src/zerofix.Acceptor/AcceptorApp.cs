using System;
using zerofix;

namespace ZeroFix.Acceptor
{
    internal class AcceptorApp : IApplication
    {
        private Session _session;
        private int _orderId = 1;
        private int _execId = 1;

        public void OnCreate(Session session)
        {
            _session = session;
        }

        public void OnMessage(FixMessage msg)
        {
            Console.WriteLine(">" + msg.ToString());
            var msgType = msg.Header[Tags.MsgType] as FixField<char>;
            switch (msgType.Value)
            {
                case 'D':
                    var accept = new FixMessage(_session.RevertID, '8');
                    accept.Header.Add(52, DateTime.UtcNow.ToString(FixSettings.DateTimeFormat));
                    accept.Add(6, 0);
                    accept.Add(14, 0);
                    accept.Add(17, _execId++);
                    accept.Add(37, _orderId++);
                    accept.Add(msg[11]);
                    accept.Add(39, 2);
                    accept.Add(msg[54]);
                    accept.Add(msg[55]);
                    accept.Add(150, 0);
                    accept.Add(151, 0);
                    _session.SendAsync(accept);
                    return;

            }
        }
    }
}