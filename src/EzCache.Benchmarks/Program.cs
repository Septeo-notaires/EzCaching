using BenchmarkDotNet.Running;
using EzCache.Benchmarks.Benchmark;
using EzCache.Cache;

BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
    .Run();