using System;
using zerofix;

namespace ZeroFix.Acceptor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            try
            {
                //IApplication app = new AcceptorApp();
                //IAcceptor acceptor = new ThreadedSocketAcceptor(app, new SessionSettingsMock());

                //acceptor.Start();
                //Console.WriteLine("press <enter> to quit");
                //Console.Read();
                //acceptor.Stop();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("==FATAL ERROR==");
                Console.WriteLine(e.ToString());
            }
        }
    }
}
