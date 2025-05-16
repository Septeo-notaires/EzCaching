using System;
using System.Collections.Generic;
using EzCache.Cache;

namespace EzCache.Policy;
internal class HitPolicyStrategy : ICachePolicyStrategy
{
    #region Private Variables
    private int _nbHits = 0;
    private readonly int _maxHit;
    #endregion Private Variables

    internal HitPolicyStrategy(ICachePolicy policy)
    {
        HitPolicy pol = (HitPolicy)policy;
    }
    
    internal HitPolicyStrategy(int maxHit) =>
        _maxHit = maxHit;
    
    public void GetPolicy()
    {
        throw new NotImplementedException();
    }

    public void RemovePolicy()
    {
        throw new System.NotImplementedException();
    }

}