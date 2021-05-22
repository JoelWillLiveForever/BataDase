using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class HeadsVM
    {
        public BindingList<HeadsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public HeadsVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.HeadsMs.Load();

            SourceList = dbContext.HeadsMs.Local.ToBindingList();
        }
    }
}
