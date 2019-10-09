namespace ReserbizAPP.LIB.DbContext
{
    public class ReserbizDataContext
    {
        public ReserbizDataContext(DbContextOptions<ReserbizDataContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }
    }
}