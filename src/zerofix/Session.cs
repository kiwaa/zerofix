using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zerofix.Messages;

namespace zerofix
{
    public class Session
    {
        private IApplication _application;
        private ITransport _transport;

        public Session(IApplication application)
        {
            _application = application;
        }

        internal void OnConnected()
        {
            Console.WriteLine("connected");

            var msg = new Logon();

            SendAsync(msg);
        }

        private void SendAsync(Message msg)
        {
            Console.WriteLine("OUT: " + msg.ToString());
            _transport.SendAsync(msg.GetBytes());
        }

        internal void SetTransport(ITransport transport)
        {
            _transport = transport;
        }

        internal void OnMessage(Message msg)
        {
            Console.WriteLine("IN: " + msg.ToString());
            _application.OnMessage(msg);
        }

        internal void OnDisconnected()
        {
            Console.WriteLine("disconnected");
        }
    }
}
