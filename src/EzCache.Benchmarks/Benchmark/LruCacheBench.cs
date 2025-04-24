using System.Runtime.CompilerServices;
using System.Xml;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using EzCache.Cache;

namespace EzCache.Benchmarks.Benchmark;

[SimpleJob(RunStrategy.Monitoring, iterationCount: 50)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class LruCacheEmptyCache
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

[SimpleJob(RunStrategy.Monitoring, iterationCount: 50)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class LruCacheFullCache
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

[SimpleJob(RunStrategy.Monitoring, iterationCount: 50)]
[MinColumn, MaxColumn, MeanColumn, MedianColumn]
public class LruCacheAddOperationCache
{
    private LruCache _cache;
    
    [Params(true, false)] public bool Empty { get; set; }
    
    [IterationSetup()]
    public void Setup()
    {
        const int capacity = 100_000;
        _cache = new LruCache(capacity);
        if (!Empty)
        {
            for (int i = 0; i < capacity; i++)
                _cache.Add($"ref_{i}", "value");
        }
    }
    
    [Benchmark(OperationsPerInvoke = 10)]
    public void AddOperation_10()
    {
        ExecuteOperation(10);
    }
    
    [Benchmark(OperationsPerInvoke = 100)]
    public void AddOperation_100()
    {
        ExecuteOperation(100);
    }
    
    [Benchmark(OperationsPerInvoke = 1000)]
    public void AddOperation_1000()
    {
        ExecuteOperation(1000);
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
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void ExecuteOperation(int nbElement)
    {
        for (int i = 0; i < nbElement; i++)
            _cache.Add($"key_{i}", "value");
    }
}