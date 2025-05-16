using EzCache.Policy;

namespace EzCache.Cache;

public struct ObjectValueCache(string key, object value, ICachePolicyStrategy? cacheStrategy = null)
{
    public ICachePolicyStrategy? CacheStrategy { get; private set; } = cacheStrategy;
    public string Key { get; private set; } = key;
    public object Value { get; private set; } = value;
}
