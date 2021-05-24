using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class RoutesVM : ObjectModel
    {
        public BindingList<RoutesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public RoutesVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.RoutesMs.Load();

            SourceList = dbContext.RoutesMs.Local.ToBindingList();
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
            dbContext.RoutesMs.Load();
        }

        public void Request()
        {
            throw new System.NotImplementedException();
        }

        public void AddEdit(bool isAdd)
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
