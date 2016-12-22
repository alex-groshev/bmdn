using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNetSandbox.Tests
{
    [Config(typeof(Config))]
    public class NullOrEmpty
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

        private List<int> _list = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};

        [Benchmark(Baseline = true)]
        public bool NullCountProperty()
        {
            return _list == null || _list.Count == 0;
        }

        [Benchmark]
        public bool NullCountLinq()
        {
            return _list == null || _list.Count() == 0;
        }

        [Benchmark]
        public bool NullAny()
        {
            return _list == null || !_list.Any();
        }
    }
}
