﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using OrangeJetpack.Core.Providers;
using OrangeJetpack.Core.Security.Exceptions;

namespace OrangeJetpack.Core.Security
{
    public class UrlSecurity
    {
        /// <summary>
        /// The default amount of time in milliseconds that URLs are valid for.
        /// </summary>
        public const int DefaultUrlExpiration = 172800000; // 48 hours

        private const string Salt = "QoPTbqmsUCcw6BSbpRlz";
        private const string TimestampParam = "timestamp";
        private const string HashParam = "hash";

        private readonly ITimeProvider _timeProvider;
        private readonly int _urlExpiration;

        public UrlSecurity(ITimeProvider timeProvider = null, int? urlExpiration = null)
        {
            _timeProvider = timeProvider ?? new UtcTimeProvider();
            _urlExpiration = urlExpiration ?? DefaultUrlExpiration;
        }

        /// <summary>
        /// Gets a secure URL containing the specified querystring parameters, a timestamp, and a hash token.
        /// </summary>
        /// <param name="baseUrl">An absolute URL for the requested resource.</param>
        /// <param name="parameters">A parameter collection to be used for the URL's querystring.</param>
        public Uri GenerateSecureUrl(string baseUrl, Dictionary<string, object> parameters)
        {
            parameters = parameters ?? new Dictionary<string, object>();
            var timestamp = _timeProvider.Now().Ticks;

            var input = GetDelimitedParameters(parameters, timestamp);
            var hash = GetHash(input);

            var uriBuilder = new UriBuilder(baseUrl)
            {
                Query = GetQueryString(parameters, timestamp, hash)
            };

            return uriBuilder.Uri;
        }

        private static string GetDelimitedParameters(IReadOnlyDictionary<string, object> parameters, long timestamp)
        {
            var values = parameters.Values.ToList();
            values.Add(timestamp);
            values.Add(Salt);
            
            return string.Join("|", values);
        }

        private static string GetQueryString(IDictionary<string, object> parameters, long? timestamp, string token)
        {
            var values = parameters.ToDictionary(i => i.Key, i => i.Value);
            values.Add(TimestampParam, timestamp);
            values.Add(HashParam, token);

            return string.Join("&", values.Select(i => $"{i.Key}={WebUtility.UrlEncode(i.Value.ToString())}"));
        }

        private static string GetHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Returns whether or not a secure URL is valid.
        /// </summary>
        public bool ValidateSecureUrl(UrlToken urlToken, params object[] parameters)
        {
            var dictionary = new Dictionary<string, object>();
            for (var i = 0; i < parameters.Length; i++)
            {
                dictionary.Add(i.ToString(), parameters[i]);
            }

            return ValidateSecureUrl(urlToken, dictionary);
        }

        /// <summary>
        /// Returns whether or not a secure URL is valid.
        /// </summary>
        public bool ValidateSecureUrl(UrlToken urlToken, Dictionary<string, object> parameters)
        {
            if (!ValidateParameters(urlToken))
            {
                throw new MissingParametersException();
            }

            if (!ValidateTimestamp(urlToken.Timestamp))
            {
                throw new ExpiredTimestampException();
            }

            if (!ValidateToken(urlToken, parameters))
            {
                throw new InvalidTokenException();
            }

            return true;
        }

        private static bool ValidateParameters(UrlToken urlToken)
        {
            return urlToken.Timestamp > DateTime.MinValue.Ticks && !string.IsNullOrWhiteSpace(urlToken.Hash);
        }

        private bool ValidateTimestamp(long timestamp)
        {
            try
            {
                var dt = new DateTime(timestamp);
                var now = _timeProvider.Now();
                return now.Subtract(dt).TotalMilliseconds <= _urlExpiration;
            }
            catch
            {
                return false;
            }
        }

        private static bool ValidateToken(UrlToken urlToken, IReadOnlyDictionary<string, object> parameters)
        {
            var token = WebUtility.UrlDecode(urlToken.Hash);
            var input = GetDelimitedParameters(parameters, urlToken.Timestamp);
            var hash = GetHash(input);

            return hash.Equals(token);
        }
    }
}
