using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class LocomotivesVM : ObjectModel
    {
        public BindingList<LocomotivesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public LocomotivesVM()
        {
            dbContext = new AppDBContext();
            dbContext.LocomotivesMs.Load();

            SourceList = dbContext.LocomotivesMs.Local.ToBindingList();
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
            dbContext.LocomotivesMs.Load();
        }
    }
}
