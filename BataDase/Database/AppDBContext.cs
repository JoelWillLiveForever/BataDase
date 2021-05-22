using BataDase.MVVM.Models.MenuVMS;
using System.Data.Entity;

namespace BataDase.Database
{
	class AppDBContext : DbContext
	{
		public AppDBContext() : base("DefaultConnection") { }
		public DbSet<CarriagesM> Carriages { get; set; }
		public DbSet<PathsM> Paths { get; set; }

	}
}
