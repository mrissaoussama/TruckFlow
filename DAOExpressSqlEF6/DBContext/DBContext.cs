using System.Data.Entity;
using TruckFlowDomain;

namespace DAOExpressSqlEF6.DBContextNS
{
    public class DBContext : DbContext
    {
        public DBContext() : base()
        {
            Database.SetInitializer<DBContext>(new DropCreateDatabaseIfModelChanges<DBContext>());

        }
        public DbSet<Event> Events { get; set; }
  
    }
}