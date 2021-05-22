using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class RoutesVM
    {
        public BindingList<RoutesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public RoutesVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.RoutesMs.Load();

            SourceList = dbContext.RoutesMs.Local.ToBindingList();
        }
    }
}
