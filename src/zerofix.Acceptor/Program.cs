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
                IApplication app = new AcceptorApp();
                var acceptor = new SessionStarter(app, new SettingsProviderMock());

                acceptor.Start();
                Console.WriteLine("press <enter> to quit");
                Console.Read();
                acceptor.Stop();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
