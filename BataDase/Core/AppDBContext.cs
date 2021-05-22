using BataDase.MVVM.Models.MenuVMS;
using System.Data.Entity;

namespace BataDase.Core
{
	class AppDBContext : DbContext
	{
		// Singleton
		private static AppDBContext instance = null;

		public static AppDBContext GetInstance()
        {
			if (instance == null)
				instance = new AppDBContext();
			return instance;
        }

		// dbContext
		private AppDBContext() : base("DefaultConnection") { }

		// Entities
		public DbSet<CarriagesM> Carriages { get; set; }
		public DbSet<PathsM> Paths { get; set; }
		public DbSet<CitiesM> Cities { get; set; }
	}
}
