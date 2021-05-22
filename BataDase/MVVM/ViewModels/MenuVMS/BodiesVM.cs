using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class BodiesVM
    {
        public BindingList<BodiesM> SourceList { get; set; }
        private AppDBContext dbContext;

        public BodiesVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.BodiesMs.Load();

            SourceList = dbContext.BodiesMs.Local.ToBindingList();
        }
    }
}
