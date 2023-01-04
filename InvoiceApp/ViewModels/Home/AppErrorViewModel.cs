namespace InvoiceApp.ViewModels.Home
{
    public class AppErrorViewModel
    {
        public int ErrorCode { get; set; }

        public string ErrorDetails { get; set; } = String.Empty;
        public string Message { get; set; } = String.Empty;
    }
}
