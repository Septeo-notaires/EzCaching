using BenchmarkDotNet.Running;
using EzCache.Benchmarks.Benchmark;
using EzCache.Cache;

BenchmarkRunner.Run<LruCacheAddOperationWithFullCacheBench>();