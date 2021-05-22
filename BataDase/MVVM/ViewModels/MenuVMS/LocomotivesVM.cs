using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class LocomotivesVM
    {
        public BindingList<LocomotivesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public LocomotivesVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.LocomotivesMs.Load();

            SourceList = dbContext.LocomotivesMs.Local.ToBindingList();
        }
    }
}
