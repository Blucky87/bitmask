using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    class Program
    {
        public enum Benchmark
        {
            EnumFlags = 0,
            BitMask = 1,
        }

        static void Main(string[] args)
        {
            var iterations = 1000;

            var benchmarkImpl = new BenchmarkImplementation();

            var times = new Dictionary<Benchmark, TimeSpan[]>();
            times[Benchmark.EnumFlags] = new TimeSpan[iterations];
            times[Benchmark.BitMask] = new TimeSpan[iterations];

            for (int i = 0; i < iterations; ++i)
            {
                /* EnumFlags benchmark */
                {
                    var stopwatch = Stopwatch.StartNew();
                    benchmarkImpl.BenchmarkEnumFlags();
                    stopwatch.Stop();

                    times[Benchmark.EnumFlags][i] = stopwatch.Elapsed;
                }

                /* BitMask benchmark */
                {
                    var stopwatch = Stopwatch.StartNew();
                    benchmarkImpl.BenchmarkBitMask();
                    stopwatch.Stop();

                    times[Benchmark.BitMask][i] = stopwatch.Elapsed;
                }

                Console.WriteLine($"Iteration {i} done.");
            }

            foreach (Benchmark benchmark in Enum.GetValues(typeof(Benchmark)))
            {
                var benchmarkTimes = times[benchmark];

                var slowestTime = TimeSpan.FromTicks(benchmarkTimes.Max(ts => ts.Ticks));
                var fastestTime = TimeSpan.FromTicks(benchmarkTimes.Min(ts => ts.Ticks));
                var averageTime = TimeSpan.FromTicks((long)benchmarkTimes.Average(ts => ts.Ticks));

                Console.WriteLine($"{benchmark.ToString()} ran for {iterations} iterations.");
                Console.WriteLine($"Slowest: {slowestTime}");
                Console.WriteLine($"Fastest: {fastestTime}");
                Console.WriteLine($"Average: {averageTime}");
            }

            Console.ReadKey();
        }
    }
}