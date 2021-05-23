using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class TicketsVM : ObjectModel
    {
        public BindingList<TicketsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public TicketsVM()
        {
            dbContext = new AppDBContext();
            dbContext.TicketsMs.Load();

            SourceList = dbContext.TicketsMs.Local.ToBindingList();
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
            dbContext.TicketsMs.Load();
        }
    }
}
