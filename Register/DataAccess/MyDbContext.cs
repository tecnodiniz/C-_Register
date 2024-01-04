using Register.Model;
using System.Data.Entity;

namespace Register.DataAccess
{
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("name=MydbContext")
        {
            Database.SetInitializer<MyDbContext>(null);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
