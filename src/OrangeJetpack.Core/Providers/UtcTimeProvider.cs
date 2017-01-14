using System;

namespace OrangeJetpack.Core.Providers
{
    public class UtcTimeProvider : ITimeProvider
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }
    }
}