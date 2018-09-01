using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNetSandbox.Tests
{
    [MemoryDiagnoser]
    [LegacyJitX86Job, LegacyJitX64Job, RyuJitX64Job]
    public class ArrayCopyVsForLoop
    {   
        private readonly int[] _source = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        private readonly int[] _destination = new int[10];
        
        [GlobalSetup]
        public void Setup()
        {
            for (var i = 0; i < _destination.Length; i++)
            {
                _destination[i] = 0;
            }
        }

        [Benchmark(Baseline = true)]
        public int ArrayCopy()
        {
            Array.Copy(_source, _destination, 10);
            return _destination[9];
        }

        [Benchmark]
        public int ForLoop()
        {
            for (int i = 0; i < _source.Length; i++)
            {
                _destination[i] = _source[i];
            }
            return _destination[9];
        }
    }
}