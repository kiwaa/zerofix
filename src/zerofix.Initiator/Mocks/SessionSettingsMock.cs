using zerofix;

namespace ZeroFix.Initiator
{
    internal class SessionSettingsMock : ISessionSettings
    {
        public SessionSettingsMock()
        {
        }

        public string Get(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Has(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}