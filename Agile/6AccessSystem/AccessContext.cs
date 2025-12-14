using System;

namespace AccessSystem
{
    public class AccessContext
    {
        public bool HasTwoFactor { get; set; }
        public bool IsFromTrustedIP { get; set; }
        public bool IsSuspicious { get; set; }
        
        public AccessContext(bool hasTwoFactor = false, bool isFromTrustedIP = false, bool isSuspicious = false)
        {
            HasTwoFactor = hasTwoFactor;
            IsFromTrustedIP = isFromTrustedIP;
            IsSuspicious = isSuspicious;
        }
    }
}