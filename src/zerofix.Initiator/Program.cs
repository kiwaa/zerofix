using System;
using zerofix;

namespace ZeroFix.Initiator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            try
            {
                var application = new TradeApp();
                var initiator = new SessionStarter(application, new SettingsProviderMock());

                initiator.Start();
                application.Run();
                initiator.Stop();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
