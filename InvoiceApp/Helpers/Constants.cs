using System.Collections.Immutable;

namespace InvoiceApp.Helpers
{
    public static class Constants
    {
        public static readonly ImmutableArray<string> Months = ImmutableArray.Create<string>(
            new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
    }
}
