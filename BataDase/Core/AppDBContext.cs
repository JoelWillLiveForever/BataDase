using BataDase.MVVM.Models.MenuVMS;
using System.Data.Entity;

namespace BataDase.Core
{
	public class AppDBContext : DbContext
	{
		// dbContext
		public AppDBContext() : base("DefaultConnection") { }

		// Entities
		public DbSet<BodiesM> BodiesMs { get; set; }
		public DbSet<CarriagesM> CarriagesMs { get; set; }
		public DbSet<CitiesM> CitiesMs { get; set; }
		public DbSet<HeadsM> HeadsMs { get; set; }
		public DbSet<LocomotivesM> LocomotivesMs { get; set; }
		public DbSet<PathsM> PathsMs { get; set; }
		public DbSet<RoutesM> RoutesMs { get; set; }
		public DbSet<TicketsM> TicketsMs { get; set; }
		public DbSet<TrainsM> TrainsMs { get; set; }
		public DbSet<TrajectoriesM> TrajectoriesMs { get; set; }
		public DbSet<UsersM> UsersMs { get; set; }
	}
}
