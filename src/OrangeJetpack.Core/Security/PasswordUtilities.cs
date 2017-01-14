using OrangeJetpack.Core.Security.Exceptions;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace OrangeJetpack.Core.Security
{
    public class PasswordUtilities
    {
        private const string Salt = "ZpWj6zk3qGNbkxeDPQuD";
        private const int ResetPasswordExpiration = 2880; // 48 hours

        public static string GenerateResetPasswordUrl(string resetPasswordUrl, string email)
        {
            var timestamp = DateTime.UtcNow.Ticks.ToString(CultureInfo.InvariantCulture);
            var input = string.Join("|", email, timestamp, Salt);
            var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));
            var token = WebUtility.UrlEncode(Convert.ToBase64String(hash));

            var uriBuilder = new UriBuilder(resetPasswordUrl)
            {
                Query = $"Email={email}&Timestamp={timestamp}&Token={token}"
            };

            return uriBuilder.Uri.ToString();
        }

        public static bool ValidateResetPasswordParameters(UrlTokenParameters parameters)
        {
            if (!ValidateParameters(parameters))
            {
                throw new MissingParametersException();
            }

            if (!ValidateTimestamp(parameters, ResetPasswordExpiration))
            {
                throw new ExpiredTimestampException();
            }

            if (!ValidateToken(parameters))
            {
                throw new InvalidTokenException();
            }

            return true;
        }

        private static bool ValidateParameters(UrlTokenParameters parameters)
        {
            return new[] { parameters.Email, parameters.Timestamp, parameters.Token }.All(param => !string.IsNullOrEmpty(param));
        }

        private static bool ValidateTimestamp(UrlTokenParameters parameters, int expiration)
        {
            try
            {
                var timestamp = new DateTime(Convert.ToInt64(parameters.Timestamp));
                return DateTime.Now.Subtract(timestamp).TotalMinutes <= expiration;
            }
            catch
            {
                return false;
            }
        }

        private static bool ValidateToken(UrlTokenParameters parameters)
        {
            var urlDecode = WebUtility.UrlDecode(parameters.Token);
            if (urlDecode == null)
            {
                return false;
            }

            var token = urlDecode.Replace(" ", "+");
            var input = string.Join("|", parameters.Email, parameters.Timestamp, Salt);
            var hash = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash).Equals(token);
        }
    }
}
