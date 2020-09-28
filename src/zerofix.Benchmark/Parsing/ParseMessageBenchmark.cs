using BenchmarkDotNet.Attributes;
using Microsoft.Diagnostics.Runtime.DacInterface;
using QuickFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zerofix.Benchmark
{
    [MemoryDiagnoser]
    public class ParseMessageBenchmark
    {
        private const string NewOrderSingle = "8=FIX.4.2\u00019=217\u000135=D\u000134=52\u000149=T4Example\u000156=T4\u000150=TraderName\u000152=20121211-20:16:17.874\u00011=Account1\u000111=fn-634908321778744001\u000148=CME_20121200_ESZ2\u000155=ES\u0001207=CME_Eq\u000154=1\u000138=1\u000140=2\u000144=141400\u000159=0\u0001167=FUT\u000121=1\u000160=20121211-20:16:17.874\u0001204=0\u0001";
        private FixMessageReader _zerofix;
        private Parser _quickfixn;
        private Message _msg;
        private byte[] _byte;

        [GlobalSetup]
        public void Prepare()
        {
            _byte = Encoding.UTF8.GetBytes(NewOrderSingle);
            _zerofix = new FixMessageReader();
            _quickfixn = new Parser();
            _msg = new Message();
        }

        [Benchmark(OperationsPerInvoke = 1_000_000)]
        public void zerofix()
        {
            _zerofix.Parse(_byte.AsSpan());
        }

        [Benchmark(OperationsPerInvoke = 1_000_000)]
        public void quickfixn()
        {
            //_quickfixn.AddToStream(ref _byte, _byte.Length);
            //_quickfixn.ReadFixMessage(out string msg);
            var msg = NewOrderSingle;
            _msg.FromString(msg, false, null, null);
        }
    }
}
