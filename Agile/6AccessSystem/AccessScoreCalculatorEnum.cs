using System;

namespace AccessSystem
{
    public static class AccessScoreCalculatorEnum
    {
        private const int TwoFactorBonus = 1;
        private const int TrustedIPBonus = 1;
        private const int SuspiciousPenalty = 2;
        
        public static int CalculateAccessScore(UserRole role, AccessContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
                
            int baseScore = GetBaseScore(role);
            
            if (role == UserRole.GuestLimited)
                return baseScore;
                
            int modifiedScore = ApplyModifiers(baseScore, context);
            return Math.Max(0, modifiedScore);
        }
        
        private static int GetBaseScore(UserRole role)
        {
            return role switch
            {
                UserRole.Viewer => 1,
                UserRole.Editor => 3,
                UserRole.Manager => 5,
                UserRole.Admin => 7,
                UserRole.GuestLimited => 1,
                _ => throw new ArgumentException($"Unknown role: {role}")
            };
        }
        
        private static int ApplyModifiers(int score, AccessContext context)
        {
            if (context.HasTwoFactor)
                score += TwoFactorBonus;
                
            if (context.IsFromTrustedIP)
                score += TrustedIPBonus;
                
            if (context.IsSuspicious)
                score -= SuspiciousPenalty;
                
            return score;
        }
    }
}