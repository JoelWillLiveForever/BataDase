using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class TrainsVM
    {
        public BindingList<TrainsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public TrainsVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.TrainsMs.Load();

            SourceList = dbContext.TrainsMs.Local.ToBindingList();
        }
    }
}
