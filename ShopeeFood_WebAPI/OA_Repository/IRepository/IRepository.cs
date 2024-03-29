﻿using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(int Id);

        void Insert(T Entity);

        void Delete(int Id);

        void Remove(T Entity);

        void Update(T Entity);

        void SaveChange();

        T CheckLogin(Func<T, bool> entity);

        IEnumerable<T> GetEntity(Func<T, bool> entity);

        T CheckExisted(Func<T, bool> entity);

        T GetObject(Func<T, bool> entity);

        int GetMaxIdObject(Func<T, int> entity);
    }
}
