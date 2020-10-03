using System.Collections.Generic;
using zerofix;

namespace ZeroFix.Acceptor
{
    internal class SessionSettingsMock : ISessionSettings
    {
        private Dictionary<string, string> _settings;
        public SessionSettingsMock()
        {
            _settings = new Dictionary<string, string>();
            _settings.Add(SessionSettings.CONNECTION_TYPE, SessionSettings.CONNECTION_TYPE_ACCEPTOR);
        }

        public string Get(string key)
        {
            return _settings[key];
        }

        public bool Has(string key)
        {
            return _settings.ContainsKey(key);
        }
    }
}