namespace OrangeJetpack.Core.Formatting
{
    public class PhoneFormatter
    {
        public static string Format(string countryCode, string localNumber)
        {
            countryCode = StringFormatter.StripNonDigits(countryCode);
            localNumber = StringFormatter.StripNonDigits(localNumber);

            if (string.IsNullOrWhiteSpace(localNumber))
            {
                return string.Empty;
            }

            return string.IsNullOrWhiteSpace(countryCode) ? localNumber : $"+{countryCode} {localNumber}";
        }
    }
}
