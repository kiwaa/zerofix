using System.Collections.Generic;
using zerofix;

namespace ZeroFix.TradeClient
{
    internal class SessionSettingsMock : ISessionSettings
    {
        private readonly Dictionary<string, string> _dict = new ();

        public SessionSettingsMock()
        {
        }

        public bool Has(string key)
        {
            return false;
        }

        public bool GetLong(string key)
        {
            throw new System.NotImplementedException();
        }

        public string GetString(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}