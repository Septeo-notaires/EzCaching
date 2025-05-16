using System.Threading.Tasks;

namespace EzCache.Cache;

internal interface ICache
{
    void Add(string key, ObjectValueCache objectValue);
    void Remove(string key);
    bool TryGetElement(string key, out ObjectValueCache objectValue);
}