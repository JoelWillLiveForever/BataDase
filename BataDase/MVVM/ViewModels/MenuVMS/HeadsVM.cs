﻿using BataDase.Core;
using BataDase.MVVM.Models.MenuVMS;
using System.ComponentModel;
using System.Data.Entity;

namespace BataDase.MVVM.ViewModels.MenuVMS
{
    public class HeadsVM : ObjectModel
    {
        public BindingList<HeadsM> SourceList { get; set; }
        private AppDBContext dbContext;

        public HeadsVM()
        {
            dbContext = AppDBContext.GetInstance();
            dbContext.HeadsMs.Load();

            SourceList = dbContext.HeadsMs.Local.ToBindingList();
        }

        public void Save()
        {
            if (dbContext != null)
                dbContext.SaveChanges();
        }

        public void Close()
        {
            if (dbContext != null) dbContext = null;
        }

        public void Connect()
        {
            if (dbContext != null) return;
            dbContext = AppDBContext.GetInstance();
            dbContext.HeadsMs.Load();
        }

        public void Request()
        {
            throw new System.NotImplementedException();
        }

        public void Add()
        {
            throw new System.NotImplementedException();
        }

        public void Edit()
        {
            throw new System.NotImplementedException();
        }

        public void Delete()
        {
            throw new System.NotImplementedException();
        }
    }
}
