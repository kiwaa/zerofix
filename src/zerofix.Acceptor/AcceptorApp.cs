using System;
using zerofix;

namespace ZeroFix.Acceptor
{
    internal class AcceptorApp : IApplication
    {
        private Session _session;

        public void OnCreate(Session session)
        {
            _session = session;
        }

        public void OnMessage(FixMessage msg)
        {
            Console.WriteLine(">" + msg.ToString());

        }
    }
}