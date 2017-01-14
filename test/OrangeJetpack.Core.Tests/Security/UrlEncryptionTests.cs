using System;
using System.Collections.Generic;
using System.Linq;
using OrangeJetpack.Core.Security;
using OrangeJetpack.Core.Security.Exceptions;
using OrangeJetpack.Core.Tests.Providers;
using Xunit;

namespace OrangeJetpack.Core.Tests.Security
{
    public class UrlEncryptionTests
    {
        private readonly DateTime _timestamp = new DateTime(2000, 1, 1);

        private readonly Dictionary<string, object> _parameters = new Dictionary<string, object>
        {
            { "a", Constants.AnyString },
            { "b", Constants.AnyInt },
            { "c", Constants.AnyBool }
        };

        private const string Token = "H2FpMxLSYWJkcBz8a%2Fo0e6j2p22Z7fXgG8v8pthDRLc%3D";

        private UrlSecurity GetUrlEncryption()
        {
            var timeProvider = new FakeTimeProvider(_timestamp);
            return new UrlSecurity(timeProvider);
        }

        [Fact]
        public void GenerateSecureUrl_Test()
        {
            var result = GetUrlEncryption().GenerateSecureUrl(Constants.AnyUrl, _parameters);
            var expected = $"{Constants.AnyUrl}/?a=abc&b=123&c=True&timestamp={_timestamp.Ticks}&hash={Token}";

            Assert.Equal(expected, result.ToString());
        }

        [Fact]
        public void ValidateSecureUrl_ValidUrlWithDictionary_ReturnsTrue()
        {
            var valid = GetUrlEncryption().ValidateSecureUrl(new UrlToken(_timestamp, Token), _parameters);

            Assert.True(valid);
        }

        [Fact]
        public void ValidateSecureUrl_ValidUrlWithParameters_ReturnsTrue()
        {
            var parameters = _parameters.Select(i => i.Value).ToArray();

            var valid = GetUrlEncryption().ValidateSecureUrl(new UrlToken(_timestamp, Token), parameters);

            Assert.True(valid);
        }

        [Fact]
        public void ValidateSecureUrl_InvalidTimestamp_ThrowsException()
        {
            Action validateSecureUrl = () => GetUrlEncryption().ValidateSecureUrl(new UrlToken(0, Token), _parameters);

            Assert.Throws<MissingParametersException>(validateSecureUrl);
        }

        [Fact]
        public void ValidateSecureUrl_MissingToken_ThrowsException()
        {
            Action validateSecureUrl = () => GetUrlEncryption().ValidateSecureUrl(new UrlToken(_timestamp, null), _parameters);

            Assert.Throws<MissingParametersException>(validateSecureUrl);
        }

        [Fact]
        public void ValidateSecureUrl_InvalidToken_ThrowsException()
        {
            Action validateSecureUrl = () => GetUrlEncryption().ValidateSecureUrl(new UrlToken(_timestamp, "INVALID TOKEN"), _parameters);

            Assert.Throws<InvalidTokenException>(validateSecureUrl);
        }

        [Fact]
        public void ValidateSecureUrl_TamperedParameters_ThrowsException()
        {
            var parameters = _parameters.ToDictionary(i => i.Key, i => i.Value);
            parameters["a"] = "SOME OTHER VALUE";

            Action validateSecureUrl = () => GetUrlEncryption().ValidateSecureUrl(new UrlToken(_timestamp, Token), parameters);

            Assert.Throws<InvalidTokenException>(validateSecureUrl);
        }

        [Fact]
        public void ValidateSecureUrl_ExpiredTimestamp_ThrowsException()
        {
            var timestamp = _timestamp.AddSeconds(-UrlSecurity.DefaultUrlExpiration).AddMilliseconds(-1);

            Action validateSecureUrl = () => GetUrlEncryption().ValidateSecureUrl(new UrlToken(timestamp, Token), _parameters);

            Assert.Throws<ExpiredTimestampException>(validateSecureUrl);
        }
    }
}
