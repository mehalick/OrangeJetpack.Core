using System;
using OrangeJetpack.Core.Providers;

namespace OrangeJetpack.Core.Tests.Providers
{
    internal class FakeTimeProvider : ITimeProvider
    {
        private readonly DateTime _now;

        public FakeTimeProvider(DateTime now)
        {
            _now = now;
        }

        public DateTime Now()
        {
            return _now;
        }
    }
}