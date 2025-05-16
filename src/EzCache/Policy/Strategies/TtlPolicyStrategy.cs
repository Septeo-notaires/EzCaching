using System;

namespace EzCache.Policy;

internal class TtlPolicyStrategy : ICachePolicyStrategy
{
    void ICachePolicyStrategy.GetPolicy()
    {
        throw new System.NotImplementedException();
    }

    void ICachePolicyStrategy.RemovePolicy()
    {
        throw new System.NotImplementedException();
    }

    internal TtlPolicyStrategy(ICachePolicy policy)
    {
        TtlPolicy pol = (TtlPolicy)policy;
    }
}