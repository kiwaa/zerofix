using NUnit.Framework;
using System;
using System.Text;

namespace zerofix.Tests
{
    [TestFixture]
    public class MessageParserTests
    {
        [Test]
        public void ParseLogon()
        {
            var str = "8=FIX.4.2\u00019=70\u000135=A\u000134=1\u000149=EXECUTOR\u000152=20200925-11:05:13.797\u000156=CLIENT1\u000198=0\u0001108=30\u000110=096\u0001";
            var incoming = Encoding.UTF8.GetBytes(str);

            var fixture = new Fixture();
            var target = fixture.CreateSut();

            var msg = target.Parse(incoming.AsSpan());

            Assert.AreEqual('A', msg.Type);
        }

        [Test]
        public void ParseHeartbeat()
        {
            var str = "8=FIX.4.2\u00019=58\u000135=0\u000134=2\u000149=EXECUTOR\u000152=20200926-12:29:26.068\u000156=CLIENT1\u000110=064\u0001";
            var incoming = Encoding.UTF8.GetBytes(str);

            var fixture = new Fixture();
            var target = fixture.CreateSut();

            var msg = target.Parse(incoming.AsSpan());

            Assert.AreEqual('0', msg.Type);
        }

        private class Fixture
        {
            public MessageParser CreateSut()
            {
                return new MessageParser();
            }
        }
    }
}
