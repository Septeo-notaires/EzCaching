using System;

namespace EzCache.Policy;

public interface ICachePolicyStrategy
{
    bool GetPolicy();
    void RemovePolicy();
}
