namespace ReserbizAPP.LIB.DbContext
{
    public class ReserbizClientDataContext: DbContext
    {
        public ReserbizClientDataContext(DbContextOptions<ReserbizClientDataContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}