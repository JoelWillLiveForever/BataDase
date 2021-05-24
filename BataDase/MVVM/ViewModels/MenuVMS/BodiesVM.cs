using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class BodiesVM : ObjectModel
    {
        public BindingList<BodiesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public BodiesVM()
        {
            dbContext = new AppDBContext();
            dbContext.BodiesMs.Load();

            SourceList = dbContext.BodiesMs.Local.ToBindingList();
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
            dbContext.BodiesMs.Load();
        }
    }
}
