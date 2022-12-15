using System.Globalization;

namespace InvoiceApp.Helpers
{
    public static class FormatExtensions
    {
        private static readonly CultureInfo _cultureInfo = new CultureInfo("en-US");

        public static string ToSeparationString(this decimal number)
        {
            return string.Format(_cultureInfo, "{0:0,0.00}", number);
        }


        public static string ToMonthString(this DateTime date)
        {
            return date.ToString("MMM yyyy", _cultureInfo);
        }


        public static string ToDateWithTimeString(this DateTime date)
        {
            return date.ToString("hh:mm MM/dd/yyyy", _cultureInfo);
        }
    }
}
