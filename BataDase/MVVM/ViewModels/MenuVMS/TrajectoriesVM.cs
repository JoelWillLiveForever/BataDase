using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class TrajectoriesVM
    {
        public BindingList<TrajectoriesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public TrajectoriesVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.TrajectoriesMs.Load();

            SourceList = dbContext.TrajectoriesMs.Local.ToBindingList();
        }
    }
}
