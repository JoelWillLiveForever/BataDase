using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class TrainsVM : ObjectModel
    {
        public BindingList<TrainsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public TrainsVM()
        {
            dbContext = new AppDBContext();
            dbContext.TrainsMs.Load();

            SourceList = dbContext.TrainsMs.Local.ToBindingList();
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
            dbContext.TrainsMs.Load();
        }
    }
}
