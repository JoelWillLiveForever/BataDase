using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class CitiesVM
    {
        public BindingList<CitiesM> SourceList { get; set; }

		private AppDBContext dbContext;

		public CitiesVM()
        {
			dbContext = AppDBContext.GetInstance();
			dbContext.CitiesMs.Load();

			SourceList = dbContext.CitiesMs.Local.ToBindingList();
		}
	}
}
