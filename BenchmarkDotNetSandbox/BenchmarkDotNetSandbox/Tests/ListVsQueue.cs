using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNetSandbox.Tests
{
    [Config(typeof(Config))]
    public class ListVsQueue
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

        private readonly Queue<int> _queue = new Queue<int>(10);
        private readonly List<int> _list = new List<int>(10);

        [GlobalSetup]
        public void Setup()
        {
            _queue.Enqueue(2);
            _queue.Enqueue(5);
            _queue.Enqueue(6);
            _queue.Enqueue(3);
            _queue.Enqueue(0);
            _queue.Enqueue(7);
            _queue.Enqueue(1);
            _queue.Enqueue(8);
            _queue.Enqueue(9);
            _queue.Enqueue(4);

            _list.Add(2);
            _list.Add(5);
            _list.Add(6);
            _list.Add(3);
            _list.Add(0);
            _list.Add(7);
            _list.Add(1);
            _list.Add(8);
            _list.Add(9);
            _list.Add(4);
        }

        [Benchmark(Baseline = true)]
        public int QueueLinq()
        {
            return _queue.Count(x => x > 5);
        }

        [Benchmark]
        public int ListLinq()
        {
            return _list.Count(x => x > 5);
        }
    }
}
