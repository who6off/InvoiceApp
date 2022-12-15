namespace InvoiceApp.Data.DbViews
{
    public class InvoiceDbView
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string OwnerName { get; set; }

        public decimal Amount { get; set; }

        public DateTime Month { get; set; }

        public string? CreatorId { get; set; }

        public DateTime CreationDate { get; set; }

        public string Status { get; set; }

        public string LastUpdateAction { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public string? LastUpdateAuthorId { get; set; }
    }
}
