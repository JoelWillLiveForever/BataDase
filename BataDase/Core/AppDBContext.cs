using BataDase.MVVM.Models.MenuVMS;
using System.Data.Entity;

namespace BataDase.Core
{
	public class AppDBContext : DbContext
	{
		private static AppDBContext instance = null;

		// dbContext
		private AppDBContext() : base("DefaultConnection") { }

		public static AppDBContext GetInstance()
		{
			if (instance == null)
				instance = new AppDBContext();
			return instance;
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			// RoutesMs entities
			modelBuilder.Entity<RoutesM>()
				.HasRequired(m => m.CitiesM_Start)
				.WithMany(t => t.RoutesMs_Start)
				.HasForeignKey(m => m._start_city_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<RoutesM>()
				.HasRequired(m => m.CitiesM_Finish)
				.WithMany(t => t.RoutesMs_Finish)
				.HasForeignKey(m => m._finish_city_id)
				.WillCascadeOnDelete(false);

			// TrainsMs entities
			modelBuilder.Entity<TrainsM>()
				.HasRequired(m => m.CarriagesM_First)
				.WithMany(t => t.TrainsMs_First)
				.HasForeignKey(m => m._first_carriage_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TrainsM>()
				.HasRequired(m => m.CarriagesM_Second)
				.WithMany(t => t.TrainsMs_Second)
				.HasForeignKey(m => m._second_carriage_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TrainsM>()
				.HasRequired(m => m.CarriagesM_Third)
				.WithMany(t => t.TrainsMs_Third)
				.HasForeignKey(m => m._third_carriage_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TrainsM>()
				.HasRequired(m => m.CarriagesM_Fourth)
				.WithMany(t => t.TrainsMs_Fourth)
				.HasForeignKey(m => m._fourth_carriage_id)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<TrainsM>()
				.HasRequired(m => m.CarriagesM_Fifth)
				.WithMany(t => t.TrainsMs_Fifth)
				.HasForeignKey(m => m._fifth_carriage_id)
				.WillCascadeOnDelete(false);
		}

        // Entities
        public virtual DbSet<CarriagesM> CarriagesMs { get; set; }
		public virtual DbSet<CitiesM> CitiesMs { get; set; }
		public virtual DbSet<LocomotivesM> LocomotivesMs { get; set; }
		public virtual DbSet<RoutesM> RoutesMs { get; set; }
		public virtual DbSet<SchedulesM> SchedulesMs { get; set; }
		public virtual DbSet<TicketsM> TicketsMs { get; set; }
		public virtual DbSet<TrainsM> TrainsMs { get; set; }
		public virtual DbSet<UsersM> UsersMs { get; set; }
	}
}
