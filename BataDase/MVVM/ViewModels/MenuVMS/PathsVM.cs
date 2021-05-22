using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.Collections.Generic;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
	public class PathsVM
	{
        public List<PathsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public PathsVM()
        {
			//SourceList = new List<PathsM>();
			//dbContext = AppDBContext.GetInstance();
			//dbContext.Paths.Load();

			//var temp = dbContext.Paths.Local.ToBindingList();

			//for (int i = 0; i < temp.Count; i++)
			//{
			//	PathsM tempObj = new PathsM();
			//	tempObj._is_path = temp[i]._is_path;
			//	SourceList.Add(tempObj);
			//}
		}
    }
}
