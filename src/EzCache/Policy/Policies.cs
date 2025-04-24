/*
 * Cache Policy configuration
 */

using System;

namespace EzCache.Policy
{
    public interface ICachePolicy { }
    public class HitPolicy : ICachePolicy
    {
        #region Private Variables
        
        private readonly int _maxHit;
        
        #endregion Private Variables

        internal int MaxHit => _maxHit;

        public HitPolicy(int maxHit) 
            => _maxHit = maxHit;
    }

    public class TtlPolicy : ICachePolicy
    {
        #region Private Variables
        private readonly TimeSpan _ttl;
        #endregion Private Variables
        
        internal TimeSpan Ttl => _ttl;
        
        public TtlPolicy(TimeSpan ttl) 
            => _ttl = ttl;
    }

    public class GroupingPolicy : ICachePolicy
    {
        #region Private Variables
        
        private readonly string _groupName;
        
        #endregion Private Variables
        
        internal string GroupName => _groupName;
        
        public GroupingPolicy(string groupName)
            => _groupName = groupName;
    }
}