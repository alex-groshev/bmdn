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

        public decimal Money;

        [Setup]
        public void SetupData()
        {
            Money = 0;
        }

        [Benchmark(Baseline = true)]
        public decimal NoLock()
        {
            return ++Money;
        }

        [Benchmark]
        public decimal Lock()
        {
            lock (_o)
            {
                return ++Money;
            }
        }
    }
}
