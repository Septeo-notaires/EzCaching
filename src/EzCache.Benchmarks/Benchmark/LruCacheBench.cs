using System.Runtime.CompilerServices;
using System.Xml;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using EzCache.Cache;

namespace EzCache.Benchmarks.Benchmark;

[SimpleJob(RunStrategy.Monitoring, iterationCount: 100)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class LruCacheEmptyCacheBench
{
    private const string referenceKey = "referenceKey";
    private const string referenceValue = "referenceValue";
    
    private LruCache _cache;
    [Params(10, 100, 1000, 10_000 )] public int Capacity { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _cache = new LruCache(Capacity);
        _cache.Add(referenceKey, referenceValue);
    }

    [Benchmark]
    public bool GetElementOperation()
    {
        return _cache.TryGetElement(referenceKey, out object _);
    }
}

[SimpleJob(RunStrategy.Monitoring, iterationCount: 100)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class LruCacheFullCacheBench
{
    private LruCache _cache;
    
    [Params(10, 100, 1000, 10_000)] public int Capacity { get; set; }
    [Params("ref5", "ref50", "ref250", "ref6000")] public string ReferenceKey { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _cache = new LruCache(Capacity);
        Enumerable.Range(0, Capacity)
            .ToList()
            .ForEach(i => _cache.Add($"ref_{i}", "value"));
    }

    [Benchmark]
    public bool GetElementOperation()
    {
        return _cache.TryGetElement(ReferenceKey, out object _);
    }
}

[SimpleJob(RunStrategy.Monitoring, iterationCount: 100, launchCount:10, warmupCount:1)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class LruCacheAddOperationWithFullCacheBench
{
    private LruCache _cache;
    [GlobalSetup]
    public void Setup()
    {
        _cache = new LruCache(100_000_000);
    }

    [Benchmark(OperationsPerInvoke = 10_000)]
    public void AddOperation_10_000()
    {
        ExecuteOperation(10_000);
    }
    
    [Benchmark(OperationsPerInvoke = 100_000)]
    public void AddOperation_100_000()
    {
        ExecuteOperation(100_000);
    }
    
    [Benchmark(OperationsPerInvoke = 1_000_000)]
    public void AddOperation_1_000_000()
    {
        ExecuteOperation(1_000_000);
    }
    
    [Benchmark(OperationsPerInvoke = 100_000_000)]
    public void AddOperation_100_000_000()
    {
        ExecuteOperation(10_000);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ExecuteOperation(int nbElement)
    {
        for (int i = 0; i < nbElement; i++)
            _cache.Add($"key_{i}", "value");
    }
}