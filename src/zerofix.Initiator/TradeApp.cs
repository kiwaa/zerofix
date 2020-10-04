using System;
using System.Collections;
using System.Collections.Generic;
using zerofix;

namespace ZeroFix.Initiator
{
    internal class TradeApp : IApplication
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

        public void Run()
        {
            Console.ReadLine();
            Console.WriteLine("sending");
            var msg = new FixMessage(_session.ID, 'D');
            msg.Header.Add(52, DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff"));
            msg.Add(60, DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff"));
            msg.Add(1, "test");
            msg.Add(11, 1);
            msg.Add(21, 1);
            msg.Add(54, 1);
            msg.Add(55, "APPL");
            msg.Add(40, 1);
            _session.SendAsync(msg);
            Console.ReadLine();
        }
    }
}