using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

namespace Benchmark3
{
    public class Program3
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<ClassUnderTest>();
        }

        [Config(typeof(Config))]
        public class ClassUnderTest
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

            private string _str = "abcdefghijklmnopqrstuvwxyz";

            [Benchmark(Baseline = true)]
            public string Baseline()
            {
                char[] charArray = _str.ToCharArray();
                Array.Reverse(charArray);
                return new string(charArray);
            }

            [Benchmark]
            public string Option1()
            {
                var hiIdx = _str.Length - 1;
                var sb = new StringBuilder(_str);
                for (var i = 0; i <= hiIdx / 2; i++)
                {
                    var temp = sb[i];
                    sb[i] = _str[hiIdx - i];
                    sb[hiIdx - i] = temp;
                }
                return sb.ToString();
            }

            [Benchmark]
            public string Option2()
            {
                var sb = new StringBuilder(_str.Length);
                for (var i = _str.Length - 1; i >= 0; i--)
                {
                    sb.Append(_str[i]);
                }
                return sb.ToString();
            }

            [Benchmark]
            public string Option3()
            {
                var result = string.Empty;
                for (var i = _str.Length - 1; i >= 0; i--)
                {
                    result += _str[i];
                }
                return result;
            }
        }
    }
}
