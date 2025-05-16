using System;

namespace EzCache.Policy;

internal class TtlPolicyStrategy : ICachePolicyStrategy
{
    #region Private Fields
    private readonly TtlPolicy _policy;
    #endregion
    internal TtlPolicyStrategy(ICachePolicy policy)
    {
        _policy = (TtlPolicy)policy;
    }
    
    public void GetPolicy()
    {
        throw new System.NotImplementedException();
    }

    public void RemovePolicy()
    {
        throw new System.NotImplementedException();
    }
}