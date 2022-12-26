namespace InvoiceApp.Data.RequestParameters
{
    public abstract class ARequestParameters
    {
        public const uint MAX_PAGE_SIZE = 50;

        public uint Page { get; set; }

        private uint _pageSize = 10;
        public uint PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAX_PAGE_SIZE) ? MAX_PAGE_SIZE : value;
        }

        public ARequestParameters() { }

        public ARequestParameters(uint pageSize)
        {
            PageSize = pageSize;
        }

        public Dictionary<string, string> ToDictionary()
        {
            var dictionary = this.GetType()
                .GetProperties()
                .Where(property => property.GetValue(this) is not null)
                .ToDictionary(property => property.Name, property => property.GetValue(this)?.ToString());

            return dictionary;
        }

        //protected Dictionary<string, string> ToDictionary(object parameters)
        //{
        //    return parameters.GetType().GetProperties().ToDictionary(
        //        property => property.Name,
        //        property => property.GetValue(parameters).ToString());
        //}

        //public abstract Dictionary<string, string> ToDictionary();
    };
}
