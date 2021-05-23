using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class CitiesVM : ObjectModel
    {
        public BindingList<CitiesM> SourceList { get; set; }
		private AppDBContext dbContext;

		public CitiesVM()
        {
			dbContext = new AppDBContext();
			dbContext.CitiesMs.Load();

			SourceList = dbContext.CitiesMs.Local.ToBindingList();
		}

		public void Save()
        {
			if (dbContext != null)
				dbContext.SaveChanges();
		}

		public void Close()
        {
			if (dbContext != null) dbContext = null;
		}

		public void Connect()
        {
			if (dbContext != null) return;
			dbContext = new AppDBContext();
			dbContext.CitiesMs.Load();
		}
	}
}
