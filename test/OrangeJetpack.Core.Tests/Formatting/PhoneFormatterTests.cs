using OrangeJetpack.Core.Formatting;
using Xunit;

namespace OrangeJetpack.Core.Tests.Formatting
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    public class PhoneFormatterTests
    {
        [Theory]
        [InlineData(null, "12345678", "12345678")]
        [InlineData("", "12345678", "12345678")]
        [InlineData("965", "12345678", "+965 12345678")]
        [InlineData("+965", "12345678", "+965 12345678")]
        [InlineData("+965", "1234 5678", "+965 12345678")]
        [InlineData("+965", null, "")]
        [InlineData("+965", "", "")]
        [InlineData("+965", "I am not a phone number", "")]
        public void Format_VaryingInputs_ReturnsCorrectPhoneNumber(string countryCode, string localNumber, string expected)
        {
            var actual = PhoneFormatter.Format(countryCode, localNumber);

            Assert.Equal(expected, actual);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}