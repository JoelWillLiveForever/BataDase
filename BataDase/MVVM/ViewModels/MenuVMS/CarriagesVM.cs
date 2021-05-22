﻿using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.Collections.Generic;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
	public class CarriagesVM
	{
        public List<CarriagesM> SourceList { get; set; }
		private AppDBContext dbContext;

        public CarriagesVM()
        {
			SourceList = new List<CarriagesM>();
			dbContext = AppDBContext.GetInstance();
			dbContext.Carriages.Load();

			var temp = dbContext.Carriages.Local.ToBindingList();

			for (int i = 0; i < temp.Count; i++)
			{
				CarriagesM tempObj = new CarriagesM();
				tempObj._cargo_weight = temp[i]._cargo_weight;
				tempObj._max_seats = temp[i]._max_seats;
				tempObj._model = temp[i]._model;
				tempObj._pmw = temp[i]._pmw;
				tempObj._weight = temp[i]._weight;
				tempObj._type = temp[i]._type;
				SourceList.Add(tempObj);
			}
		}
    }
}
