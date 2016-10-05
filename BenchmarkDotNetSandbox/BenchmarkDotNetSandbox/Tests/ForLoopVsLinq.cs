using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNetSandbox.Tests
{
    struct Foo
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    [Config(typeof(Config))]
    public class ForLoopVsLinq
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.LegacyJitX64);
                Add(MarkdownExporter.StackOverflow);
            }
        }

        List<Foo> _foos;

        [Setup]
        public void SetupData()
        {
            _foos = new List<Foo>
            {
                new Foo {X = 0, Y = 0},
                new Foo {X = 1, Y = 1},
                new Foo {X = 0, Y = 1},
                new Foo {X = 1, Y = 0},
                new Foo {X = 1, Y = 0}
            };
        }

        [Benchmark(Baseline = true)]
        public int Option1()
        {
            int total = 0;
            for (var i = 0; i < _foos.Count; i++)
            {
                if (_foos[i].X == 1 && _foos[i].Y == 0)
                {
                    total++;
                }
            }
            return total;
        }

        [Benchmark]
        public int Option2()
        {
            int total = 0;
            foreach (var foo in _foos)
            {
                if (foo.X == 1 && foo.Y == 0)
                {
                    total++;
                }
            }
            return total;
        }

        [Benchmark]
        public int Option3()
        {
            return _foos.Count(f => f.X == 1 && f.Y == 0);
        }

        [Benchmark]
        public int Option4()
        {
            return (from f in _foos where f.X == 1 && f.Y == 0 select f).Count();
        }
    }
}
