using System;
using System.Collections.Generic;
using EzCache.Cache;

namespace EzCache.Policy;
internal class HitPolicyStrategy : ICachePolicyStrategy
{
    #region Private Variables
    private int _nbHits = 0;
    private readonly int _maxHit;
    private readonly HitPolicy _policy;
    #endregion Private Variables

    internal HitPolicyStrategy(ICachePolicy policy)
    {
        HitPolicy pol = (HitPolicy)policy;
    }
    
    public bool GetPolicy()
        => _nbHits++ < _maxHit;

    public void RemovePolicy()
    {
        throw new System.NotImplementedException();
    }

}