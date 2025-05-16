using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EzCache.Policy;

namespace EzCache.Cache;

public class Cache
{
    #region Private Variables
    private readonly LruCache _lruCache;
    private const int DEFAULT_SIZE = 128;
    #endregion Private Variables

    #region  Properties
    public int Capacity => _lruCache.Capacity;
    public float Filled => ((float)_lruCache.Length / (float)_lruCache.Capacity) * 100;
    #endregion Properties

    public Cache() =>
        _lruCache = new LruCache(DEFAULT_SIZE);
    
    public Cache(int capacity) =>
        _lruCache = new LruCache(capacity);

    public object? TryGetElement(string key, Func<object> func, ICachePolicy? policy = null)
    {
        bool result = _lruCache.TryGetElement(key, out ObjectValueCache? cachedObject);
        if (!result || cachedObject == null)
            return ExecuteFuncAndAdd(key, func, policy);

        if (cachedObject is { CacheStrategy: not null } cache)
        {
            bool resultPolicy = cache.CacheStrategy.GetPolicy();
            if (resultPolicy) return cachedObject.Value;
        }

        return ExecuteFuncAndAdd(key, func, policy);
    }

    /*Add cache stampede*/
    public async Task<object?> TryGetElementAsync(string key, Func<Task<object>> funcAsync, ICachePolicy? policy = null)
    {
        bool result = _lruCache.TryGetElement(key, out ObjectValueCache? cachedObject);
        if (!result || cachedObject == null)
            return await ExecuteFuncAndAddAsync(key, funcAsync, policy);

        if (cachedObject is { CacheStrategy: not null } cache)
        {
            bool resultPolicy = cache.CacheStrategy.GetPolicy();
            if (resultPolicy) return cachedObject.Value;
        }

        return await ExecuteFuncAndAddAsync(key, funcAsync, policy);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Remove(string key) =>
        _lruCache.Remove(key);

    private ICachePolicyStrategy? ReturnCachePolicyStrategy(ICachePolicy? policy) => policy switch
    {
        TtlPolicy => new TtlPolicyStrategy(policy),
        HitPolicy => new HitPolicyStrategy(policy),
        GroupingPolicy => new GroupingPolicyStrategy(policy),
        null => null,
        _ => throw new System.NotImplementedException()
    };

    private object ExecuteFuncAndAdd(string key, Func<object> func, ICachePolicy? policy = null)
    {
        object resultFunc = func();
        _lruCache.Add(key, new ObjectValueCache(key, resultFunc, ReturnCachePolicyStrategy(policy)));
        return resultFunc;
    }
    
    private async Task<object> ExecuteFuncAndAddAsync(string key, Func<Task<object>> func, ICachePolicy? policy = null)
    {
        object resultFunc = await func()
            .ConfigureAwait(false);
        
        _lruCache.Add(key, new ObjectValueCache(key, resultFunc, ReturnCachePolicyStrategy(policy)));
        return resultFunc;
    }
}