using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zerofix;

namespace ZeroFix.Acceptor
{
    class SettingsProviderMock : ISessionSettingsProvider
    {
        public IEnumerable<ISessionSettings> GetAll()
        {
            return new[]
            {
                new SessionSettingsMock()
            };
        }
    }
}