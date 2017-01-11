using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNetSandbox.Tests
{
    [Config(typeof(Config))]
    public class AggregateVsSum
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.LegacyJitX64);
                Add(Job.RyuJitX64);
                Add(MarkdownExporter.StackOverflow);
            }
        }

        private List<int> _list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        [Benchmark(Baseline = true)]
        public int ForeachLoop()
        {
            var result = 0;
            foreach (var item in _list)
                result += item;
            return result;
        }

        [Benchmark]
        public int LinqSum()
        {
            return _list.Sum();
        }

        [Benchmark]
        public int LinqAggregate()
        {
            return _list.Aggregate(0, (x, y) => x + y);
        }
    }
}
