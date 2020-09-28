using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zerofix;

namespace ZeroFix.TradeClient
{
    class SettingsProviderMock : ISessionSettingsProvider
    {
        public IEnumerable<SessionIdentity> GetSessions()
        {
            return new[]
            {
                new SessionIdentity("FIX.4.2", "CLIENT1", "EXCHANGE")
            };
        }
        public ISessionSettings Get(SessionIdentity sessionId)
        {
            return new SessionSettingsMock();
        }
    }
}
