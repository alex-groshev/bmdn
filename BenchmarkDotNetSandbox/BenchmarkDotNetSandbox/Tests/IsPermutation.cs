using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;

namespace BenchmarkDotNetSandbox.Tests
{
    [Config(typeof(Config))]
    public class IsPermutation
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.RyuJitX64);
                Add(MarkdownExporter.StackOverflow);
            }
        }

        private readonly int[] arr = {1, 2, 3, 4, 5, 6, 7, 8, 9, 0};

        [Benchmark(Baseline = true)]
        public bool Option1()
        {
            for (var i = 0; i < arr.Length; i++)
            {
                for (var j = arr.Length - 1; j >= i; j--)
                {
                    if (arr[i] > arr[j])
                    {
                        var temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }

            for (var i = 1; i < arr.Length; i++)
            {
                if (arr[i] - arr[i - 1] > 1)
                    return false;
            }

            return true;
        }

        [Benchmark]
        public bool Option2()
        {
            Array.Sort(arr);

            for (var i = 1; i < arr.Length; i++)
            {
                if (arr[i] - arr[i - 1] > 1)
                    return false;
            }

            return true;
        }
    }
}
