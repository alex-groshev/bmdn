using System.Threading;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Benchmark2
{
    class Program2
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ClassUnderTest>();
        }
    }

    [Config(typeof(Config))]
    public class ClassUnderTest
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.LegacyJitX64);
                Add(MarkdownExporter.StackOverflow);
            }
        }

        private readonly object _o = new object();

        [Params(1)]
        public decimal Money;

        [Benchmark(Baseline = true)]
        public decimal Option1()
        {
            return ++Money;
        }

        [Benchmark]
        public decimal Option2()
        {
            lock (_o)
            {
                ++Money;
            }
            return Money;
        }

        [Benchmark]
        public decimal Option3()
        {
            Monitor.Enter(_o);
            try
            {
                return ++Money;
            }
            finally
            {
                Monitor.Exit(_o);
            }
        }
    }
}
