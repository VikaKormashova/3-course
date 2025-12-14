using System;

namespace AccessSystem
{
    public static class AccessScoreCalculatorOop
    {
        public static int CalculateAccessScore(Role role, AccessContext context)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));
            if (context == null)
                throw new ArgumentNullException(nameof(context));
                
            return role.GetAccess(context);
        }
    }
}