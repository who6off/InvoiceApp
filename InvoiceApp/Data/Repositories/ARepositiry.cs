using System.Data;

namespace InvoiceApp.Data.Repositories
{
    public abstract class ARepositiry
    {
        protected readonly DapperContext _context;


        public ARepositiry(DapperContext context)
        {
            _context = context;
        }


        protected IDbConnection CreateConnection() => _context.CreateConnection();
    }
}
