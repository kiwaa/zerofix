using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix
{
    public class InitiatorSession : Session
    {
        public InitiatorSession(IApplication application, SessionIdentity id) : base(application)
        {
            ID = id;
        }

        internal override void OnConnected()
        {
            Console.WriteLine("connected");
            base.OnConnected();

            var msg = new FixMessage(ID, 'A');
            msg.Header.Add(52, DateTime.UtcNow.ToString(FixSettings.DateTimeFormat));
            msg.Add(98, "0");
            msg.Add(108, "30");

            SendAsync(msg);
        }
    }
}
