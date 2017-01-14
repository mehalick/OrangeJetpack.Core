using OrangeJetpack.Core.Formatting;
using Xunit;

namespace OrangeJetpack.Core.Tests.Formatting
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    public class AddressFormatterTests
    {
        [Theory]
        [InlineData("", "", "", "", "", "", "", "", "<br/>")]
        [InlineData("11", "66", "49", "", "Abu Al Hasaniya", "", "", "KW", "Abu Al Hasaniya<br/>Block 11 Street 66 Building 49")]
        public void MultiLine_CountryIsKuwait_FormatsCorrectly(string addressLine1, string addressLine2, string addressLine3, string addressLine4, string cityArea, string stateProvince, string postalCode, string country, string expected)
        {
            var actual = AddressFormatter.ToHtml(addressLine1, addressLine2, addressLine3, addressLine4, cityArea, stateProvince, postalCode, country);

            Assert.Equal($"<address>{expected}</address>", actual);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}