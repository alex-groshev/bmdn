using BenchmarkDotNet.Running;
using BenchmarkDotNetSandbox.Tests;

namespace BenchmarkDotNetSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<MissingElementInPermutation>();
        }
    }
}
