namespace OrangeJetpack.Core.Formatting
{
    public static class NameFormatter
    {
        /// <summary>
        /// Gets a full name formatted as [first name] [last initial].
        /// </summary>
        /// <example>
        /// John Smith would return John S.
        /// </example>
        public static string GetFirstNameLastInitial(string firstName, string lastName)
        {
            TrimNames(ref firstName, ref lastName);

            if (string.IsNullOrEmpty(lastName))
            {
                return firstName;
            }

            return string.Format("{0} {1}.", firstName, lastName[0]).Trim();
        }

        /// <summary>
        /// Gets a full name formatted as [first initial]. [last name].
        /// </summary>
        /// <example>
        /// John Smith would return J. Smith
        /// </example>>
        public static string GetFirstInitialLastName(string firstName, string lastName)
        {
            TrimNames(ref firstName, ref lastName);

            if (string.IsNullOrEmpty(firstName))
            {
                return lastName;
            }

            return string.Format("{0}. {1}", firstName[0], lastName).Trim();
        }

        /// <summary>
        /// Gets a full name formatted as [last name], [first name].
        /// </summary>
        /// <example>
        /// John Smith would return Smith, John
        /// </example>
        public static string GetLastNameCommaFirstName(string firstName, string lastName)
        {
            TrimNames(ref firstName, ref lastName);

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return string.Concat(lastName, firstName);
            }

            return string.Format("{0}, {1}", lastName, firstName);
        }

        /// <summary>
        /// Gets a full name formatted as [first name] [last name].
        /// </summary>
        /// <example>
        /// John Smith would return John Smith
        /// </example>
        public static string GetFullName(string firstName, string lastName)
        {
            TrimNames(ref firstName, ref lastName);

            return string.Format("{0} {1}", firstName, lastName).Trim();
        }

        private static void TrimNames(ref string firstName, ref string lastName)
        {
            firstName = TryTrim(firstName);
            lastName = TryTrim(lastName);
        }

        private static string TryTrim(string str)
        {
            return str != null ? str.Trim() : "";
        }
    }
}
