using System;

namespace EzCache.Policy;

public interface ICachePolicyStrategy
{
    void GetPolicy();
    void RemovePolicy();
}
