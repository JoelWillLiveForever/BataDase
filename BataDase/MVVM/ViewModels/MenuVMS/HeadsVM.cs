using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class HeadsVM : ObjectModel
    {
        public BindingList<HeadsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public HeadsVM()
        {
            dbContext = new AppDBContext();
            dbContext.HeadsMs.Load();

            SourceList = dbContext.HeadsMs.Local.ToBindingList();
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
            dbContext.HeadsMs.Load();
        }
    }
}
