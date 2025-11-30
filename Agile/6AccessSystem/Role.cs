using System;

namespace AccessSystem
{
    public abstract class Role
    {
        private readonly int _basePoints;
        
        public int BasePoints => _basePoints;
        
        protected Role(int basePoints)
        {
            _basePoints = basePoints;
        }
        
        public virtual int GetAccess(AccessContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
                
            int score = _basePoints;
            
            if (context.HasTwoFactor)
                score += 1;
                
            if (context.IsFromTrustedIP)
                score += 1;
                
            if (context.IsSuspicious)
                score -= 2;
                
            return Math.Max(0, score);
        }
    }
}