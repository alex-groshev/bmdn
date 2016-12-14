using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNetSandbox.Tests
{
    [Config(typeof(Config))]
    public class MissingElementInPermutation
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.RyuJitX64);
                Add(MarkdownExporter.StackOverflow);
            }
        }

        private readonly int[] arr = {2, 1, 3, 4, 6, 5, 8, 9, 0};

        [Benchmark(Baseline = true)]
        public int Option1()
        {
            var expected = arr.Length;
            var actual = 0;
            for (var i = 0; i < arr.Length; i++)
            {
                expected += i + 1;
                actual += arr[i];
            }
            return expected - actual + 1;
        }

        [Benchmark]
        public int Option2()
        {
            var actualLen = arr.Length;
            var expected = actualLen;
            var actual = 0;
            for (var i = 0; i < actualLen; i++)
            {
                expected += i + 1;
                actual += arr[i];
            }
            return expected - actual + 1;
        }

        [Benchmark]
        public int Option3()
        {
            return Enumerable.Range(1, arr.Length + 1).Sum() - arr.Sum();
        }
    }
}
