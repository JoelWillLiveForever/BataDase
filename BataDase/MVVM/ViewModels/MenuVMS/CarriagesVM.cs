using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
	public class CarriagesVM
	{
        public BindingList<CarriagesM> SourceList { get; set; }
		private AppDBContext dbContext;

        public CarriagesVM()
        {
			dbContext = AppDBContext.GetInstance();
			dbContext.CarriagesMs.Load();

			SourceList = dbContext.CarriagesMs.Local.ToBindingList();
		}
    }
}
