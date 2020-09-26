using System;
using zerofix;
using zerofix.Messages;

namespace ZeroFix.TradeClient
{
    internal class TradeApp : IApplication
    {
        public void OnMessage(Message msg)
        {
            Console.WriteLine(">" + msg.ToString());
        }

        public void Run()
        {
            var msg = new NewOrderSingle();
            Console.ReadLine();
        }
    }
}