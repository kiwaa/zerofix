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

            Assert.AreEqual("FIX.4.2", msg.Header[Tags.BeginString].AsString());
            Assert.AreEqual("A", msg.Header[Tags.MsgType].AsString());
            Assert.AreEqual("EXECUTOR", msg.Header[Tags.SenderCompID].AsString());
            Assert.AreEqual("CLIENT1", msg.Header[Tags.TargetCompID].AsString());
            Assert.AreEqual("1", msg[Tags.MsgSeqNum].AsString());
            Assert.AreEqual("20200925-11:05:13.797", msg[Tags.SendingTime].AsString());
        }

        [Test]
        public void ParseHeartbeat()
        {
            var str = "8=FIX.4.2\u00019=58\u000135=0\u000134=2\u000149=EXECUTOR\u000152=20200926-12:29:26.068\u000156=CLIENT1\u000110=064\u0001";
            var incoming = Encoding.UTF8.GetBytes(str);

            var fixture = new Fixture();
            var target = fixture.CreateSut();

            var msg = target.Parse(incoming.AsSpan());

            Assert.AreEqual("FIX.4.2", msg.Header[Tags.BeginString].AsString());
            Assert.AreEqual("0", msg.Header[Tags.MsgType].AsString());
            Assert.AreEqual("EXECUTOR", msg.Header[Tags.SenderCompID].AsString());
            Assert.AreEqual("CLIENT1", msg.Header[Tags.TargetCompID].AsString());
            Assert.AreEqual("2", msg[Tags.MsgSeqNum].AsString());
            Assert.AreEqual("20200926-12:29:26.068", msg[Tags.SendingTime].AsString());
        }

        [Test]
        public void ParseNewOrderSingle()
        {
            var str = "8=FIX.4.2\u00019=217\u000135=D\u000134=52\u000149=T4Example\u000156=T4\u000150=TraderName\u000152=20121211-20:16:17.874\u00011=Account1\u000111=fn-634908321778744001\u000148=CME_20121200_ESZ2\u000155=ES\u0001207=CME_Eq\u000154=1\u000138=1\u000140=2\u000144=141400\u000159=0\u0001167=FUT\u000121=1\u000160=20121211-20:16:17.874\u0001204=0\u0001";
            var incoming = Encoding.UTF8.GetBytes(str);

            var fixture = new Fixture();
            var target = fixture.CreateSut();

            var msg = target.Parse(incoming.AsSpan());
        }

        [Test]
        public void ParseBusinessMessageReject()
        {
            var str = "8=FIX.4.2\u00019=115\u000135=j\u000134=2\u000149=EXECUTOR\u000152=20200927-01:00:34.832\u000156=CLIENT1\u000145=2\u000158=Conditionally Required Field Missing\u0001372=D\u0001380=5\u000110=254\u0001";
            var incoming = Encoding.UTF8.GetBytes(str);

            var fixture = new Fixture();
            var target = fixture.CreateSut();

            var msg = target.Parse(incoming.AsSpan());

            Assert.AreEqual("FIX.4.2", msg.Header[Tags.BeginString].AsString());
            Assert.AreEqual("j", msg.Header[Tags.MsgType].AsString());
            Assert.AreEqual("EXECUTOR", msg.Header[Tags.SenderCompID].AsString());
            Assert.AreEqual("CLIENT1", msg.Header[Tags.TargetCompID].AsString());
            Assert.AreEqual("2", msg[Tags.MsgSeqNum].AsString());
            Assert.AreEqual("20200927-01:00:34.832", msg[Tags.SendingTime].AsString());
        }

        private class Fixture
        {
            public FixMessageReader CreateSut()
            {
                return new FixMessageReader();
            }
        }
    }
}
