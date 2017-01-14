using System;
using System.Text;

namespace OrangeJetpack.Core.Formatting
{
    public static class AddressFormatter
    {
        public static string ToHtml(string addressLine1, string addressLine2, string addressLine3, string addressLine4, string cityArea, string stateProvince, string postalCode, string country)
        {
            if (country.Equals("KW", StringComparison.OrdinalIgnoreCase))
            {
                return GetKuwaitAddress(addressLine1, addressLine2, addressLine3, addressLine4, cityArea);
            }

            var result = new StringBuilder(addressLine1 + "<br/>");

            if (!string.IsNullOrWhiteSpace(addressLine2))
            {
                result.Append(addressLine2 + "<br/>");
            }

            if (!string.IsNullOrWhiteSpace(addressLine3))
            {
                result.Append(addressLine3 + "<br/>");
            }

            if (!string.IsNullOrWhiteSpace(addressLine4))
            {
                result.Append(addressLine4 + "<br/>");
            }

            if (!string.IsNullOrWhiteSpace(cityArea))
            {
                result.Append(cityArea + ", ");
            }

            if (!string.IsNullOrWhiteSpace(stateProvince))
            {
                result.Append(stateProvince + ", ");
            }

            if (!string.IsNullOrWhiteSpace(postalCode))
            {
                result.Append(postalCode + ", ");
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                result.Append(country);
            }

            return $"<address>{result}</address>";
        }

        public static string ToSingleLine(string addressLine1, string addressLine2, string addressLine3,
   string addressLine4, string cityArea, string stateProvince, string postalCode)
        {
            var result = new StringBuilder(cityArea + " ");

            if (!string.IsNullOrWhiteSpace(addressLine1))
            {
                result.Append("Block " + addressLine1 + " ");
            }

            if (!string.IsNullOrWhiteSpace(addressLine4))
            {
                result.Append("Jadda " + addressLine4 + " ");
            }

            if (!string.IsNullOrWhiteSpace(addressLine2))
            {
                result.Append("Street " + addressLine2 + " ");
            }

            if (!string.IsNullOrWhiteSpace(addressLine3))
            {
                result.Append("Building " + addressLine3);
            }

            return $"<address>{result}</address>";
        }

        private static string GetKuwaitAddress(string addressLine1, string addressLine2, string addressLine3, string addressLine4, string cityArea)
        {
            var result = new StringBuilder(cityArea + "<br/>");

            if (!string.IsNullOrWhiteSpace(addressLine1))
            {
                result.Append("Block " + addressLine1 + " ");
            }

            if (!string.IsNullOrWhiteSpace(addressLine4))
            {
                result.Append("Jadda " + addressLine4 + " ");
            }

            if (!string.IsNullOrWhiteSpace(addressLine2))
            {
                result.Append("Street " + addressLine2 + " ");
            }

            if (!string.IsNullOrWhiteSpace(addressLine3))
            {
                result.Append("Building " + addressLine3);
            }

            return $"<address>{result}</address>";
        }
    }
}
