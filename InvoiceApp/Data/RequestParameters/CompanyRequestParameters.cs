namespace InvoiceApp.Data.RequestParameters
{

    public class CompanyRequestParameters : ARequestParameters
    {
        public string? Name { get; set; }

        public CompanyRequestParameters() : base() { }
    }
}
