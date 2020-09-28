using BenchmarkDotNet.Running;
using System;

namespace zerofix.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var switcher = new BenchmarkSwitcher(new[]
            {
                typeof(ParseMessageBenchmark)
            });
            switcher.Run(args);
        }
    }
}
