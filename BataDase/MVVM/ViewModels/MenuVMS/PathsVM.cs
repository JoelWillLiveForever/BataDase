using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
	public class PathsVM
	{
        public BindingList<PathsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public PathsVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.PathsMs.Load();

            SourceList = dbContext.PathsMs.Local.ToBindingList();
        }
    }
}
