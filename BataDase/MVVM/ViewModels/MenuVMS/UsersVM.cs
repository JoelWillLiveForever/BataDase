using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class UsersVM : ObjectModel
    {
        public BindingList<UsersM> SourceList { get; set; }
        private AppDBContext dbContext;

        public UsersVM()
        {
            dbContext = new AppDBContext();
            dbContext.UsersMs.Load();

            SourceList = dbContext.UsersMs.Local.ToBindingList();
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
            dbContext.UsersMs.Load();
        }
    }
}
