using System;

namespace OrangeJetpack.Core.Formatting
{
    public class DateTimeFormatter
    {
        public enum Format
        {
            Sortable,
            Full
        }
        
        public static string ToLocalTime(DateTime? dateTime, Format format = Format.Sortable)
        {
            if (!dateTime.HasValue)
            {
                return "";
            }

            var localTime = dateTime.Value.AddHours(3);

            switch (format)
            {
                case Format.Sortable:
                    return $"{localTime:yyyy-MM-dd HH:mm} AST";
                case Format.Full:
                    return $"{localTime:dd MMM yyyy HH:mm} AST";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
