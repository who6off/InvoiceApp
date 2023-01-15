using System.Text.Json;

namespace InvoiceApp.Data.RequestParameters
{
    public class ARequestParameters
    {
        private const uint MAX_PAGE_SIZE = 50;

        private uint _pageSize = 5;

        public uint Page { get; set; }

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


        public Task<ARequestParameters> Clone() => Task.Run(() =>
        {
            var json = JsonSerializer.Serialize(this);
            var obj = JsonSerializer.Deserialize<ARequestParameters>(json);
            return obj;
        });
    };
}
