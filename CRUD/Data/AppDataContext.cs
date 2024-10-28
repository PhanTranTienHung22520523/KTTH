using CRUD.Models;
namespace CRUD.Data
{
	public class AppDataContext: DbContext
	{
		public AppDataContext(DbContextOptions options):base(options) 
		{

		}

		public DbSet<Product> Products { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
