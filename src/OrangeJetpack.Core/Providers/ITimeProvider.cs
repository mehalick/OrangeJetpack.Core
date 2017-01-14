using System;

namespace OrangeJetpack.Core.Providers
{
    public interface ITimeProvider
    {
        DateTime Now();
    }
}