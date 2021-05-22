﻿using BataDase.MVVM.Models.MenuVMS;
using System.Data.Entity;

namespace BataDase.Core
{
	class AppDBContext : DbContext
	{
		public AppDBContext() : base("DefaultConnection") { }
		public DbSet<CarriagesM> Carriages { get; set; }
		public DbSet<PathsM> Paths { get; set; }
		public DbSet<CitiesM> Cities { get; set; }
	}
}
