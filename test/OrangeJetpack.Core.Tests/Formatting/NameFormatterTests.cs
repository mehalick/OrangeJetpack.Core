using OrangeJetpack.Core.Formatting;
using Xunit;

namespace OrangeJetpack.Core.Tests.Formatting
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable PossibleNullReferenceException

    public class NameFormatterTests
    {
        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData(null, null, "")]
        [InlineData("", "S", "S.")]
        [InlineData(" ", "S", "S.")]
        [InlineData(null, "S", "S.")]
        [InlineData("", "Smith", "S.")]
        [InlineData(" ", "Smith", "S.")]
        [InlineData(null, "Smith", "S.")]
        [InlineData("Jim", "", "Jim")]
        [InlineData("Jim", " ", "Jim")]
        [InlineData("Jim", null, "Jim")]
        [InlineData("Jim", "Smith", "Jim S.")]
        [InlineData(" Jim ", " Smith ", "Jim S.")]
        public void GetFirstNameLastInitial_VaryingInputs_ReturnsCorrectOutput(string firstName, string lastName, string expected)
        {
            var matchName = NameFormatter.GetFirstNameLastInitial(firstName, lastName);

            Assert.Equal(expected, matchName);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData(null, null, "")]
        [InlineData("", "S", "S")]
        [InlineData(" ", "S", "S")]
        [InlineData(null, "S", "S")]
        [InlineData("", "Smith", "Smith")]
        [InlineData(" ", "Smith", "Smith")]
        [InlineData(null, "Smith", "Smith")]
        [InlineData("Jim", "", "J.")]
        [InlineData("Jim", " ", "J.")]
        [InlineData("Jim", null, "J.")]
        [InlineData("Jim", "Smith", "J. Smith")]
        [InlineData(" Jim ", " Smith ", "J. Smith")]
        public void GetFirstInitalLastName_VaryingInputs_ReturnsCorrectOutput(string firstName, string lastName, string expected)
        {
            var matchName = NameFormatter.GetFirstInitialLastName(firstName, lastName);

            Assert.Equal(expected, matchName);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData(null, null, "")]
        [InlineData("Jim", "", "Jim")]
        [InlineData("Jim", " ", "Jim")]
        [InlineData("Jim", null, "Jim")]
        [InlineData("", "Smith", "Smith")]
        [InlineData(" ", "Smith", "Smith")]
        [InlineData(null, "Smith", "Smith")]
        [InlineData("Jim", "Smith", "Smith, Jim")]
        [InlineData(" Jim ", " Smith ", "Smith, Jim")]
        public void GetLastNameCommaFirstName_VaryingInputs_ReturnsCorrectOutput(string firstName, string lastName, string expected)
        {
            var matchName = NameFormatter.GetLastNameCommaFirstName(firstName, lastName);

            Assert.Equal(expected, matchName);
        }

        [Theory]
        [InlineData("", "", "")]
        [InlineData(" ", " ", "")]
        [InlineData(null, null, "")]
        [InlineData("Jim", "", "Jim")]
        [InlineData("Jim", " ", "Jim")]
        [InlineData("Jim", null, "Jim")]
        [InlineData("", "Smith", "Smith")]
        [InlineData(" ", "Smith", "Smith")]
        [InlineData(null, "Smith", "Smith")]
        [InlineData("Jim", "Smith", "Jim Smith")]
        [InlineData(" Jim ", " Smith ", "Jim Smith")]
        public void GetFirstNameSpaceLastName_VaryingInputs_ReturnsCorrectOutput(string firstName, string lastName, string expected)
        {
            var matchName = NameFormatter.GetFullName(firstName, lastName);

            Assert.Equal(expected, matchName);
        }
    }

    // ReSharper restore InconsistentNaming
    // ReSharper restore PossibleNullReferenceException
}