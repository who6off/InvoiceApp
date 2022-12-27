using InvoiceApp.Data.RequestParameters;

namespace InvoiceApp.Identity.RequestParameters
{
    public class UserRequestParameters : ARequestParameters
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Email { get; set; }

        public UserRequestParameters() { }

        public UserRequestParameters(
            uint pageSize, string name, string surname, string email) : base(pageSize)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }


    }
}
