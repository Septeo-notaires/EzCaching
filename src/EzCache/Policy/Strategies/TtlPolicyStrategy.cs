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

    public bool GetPolicy()
        => !_policy.Expires;

    public void RemovePolicy()
    {
        
    }
}