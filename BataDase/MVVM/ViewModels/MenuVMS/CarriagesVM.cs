using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
	public class CarriagesVM : ObjectModel
	{
        public BindingList<CarriagesM> SourceList { get; set; }
		private AppDBContext dbContext;

        public CarriagesVM()
        {
		 	dbContext = AppDBContext.GetInstance();
			dbContext.CarriagesMs.Load();

			SourceList = dbContext.CarriagesMs.Local.ToBindingList();
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
			dbContext = AppDBContext.GetInstance();
			dbContext.CarriagesMs.Load();
		}

        public void Request()
        {
            throw new System.NotImplementedException();
        }

        public void Add()
        {
            throw new System.NotImplementedException();
        }

        public void Edit()
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
