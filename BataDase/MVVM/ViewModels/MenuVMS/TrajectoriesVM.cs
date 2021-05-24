using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class TrajectoriesVM : ObjectModel
    {
        public BindingList<TrajectoriesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public TrajectoriesVM()
        {
            dbContext = new AppDBContext();
            dbContext.TrajectoriesMs.Load();

            SourceList = dbContext.TrajectoriesMs.Local.ToBindingList();
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
            dbContext.TrajectoriesMs.Load();
        }
    }
}
