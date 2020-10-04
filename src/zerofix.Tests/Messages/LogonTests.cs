using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace zerofix.Tests.Messages
{
    [TestFixture]
    public class LogonTests
    {
        [Test]
        public void ToString()
        {
            var sessionId = new SessionIdentity("FIX.4.2", "EXECUTOR", "CLIENT1");
            var target = new FixMessage(sessionId, 'A');
            target.Header.Add(34, "1");
            target.Header.Add(52, "20200925-11:05:13.797");
            target.Add(98, "0");
            target.Add(108, "30");

            var str = target.ToString();

            Assert.AreEqual("8=FIX.4.2\u00019=70\u000135=A\u000149=EXECUTOR\u000156=CLIENT1\u000134=1\u000152=20200925-11:05:13.797\u000198=0\u0001108=30\u000110=096\u0001", str);
        }
    }
}
