using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class UsersVM
    {
        public BindingList<UsersM> SourceList { get; set; }
        private AppDBContext dbContext;

        public UsersVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.UsersMs.Load();

            SourceList = dbContext.UsersMs.Local.ToBindingList();
        }
    }
}
