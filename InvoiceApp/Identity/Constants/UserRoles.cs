namespace InvoiceApp.Identity.Constants
{
    public static class UserRoles
    {
        public const string Accountant = "Accountant";
        public const string Manager = "Manager";
        public const string Admin = "Admin";

        public static string[] GetAll() => new string[] { Accountant, Manager, Admin };
    }
}
