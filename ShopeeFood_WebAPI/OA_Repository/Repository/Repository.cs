﻿using Microsoft.EntityFrameworkCore;
using OA_Repository.IRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext dbContext;
        private DbSet<T> entity;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.entity = this.dbContext.Set<T>();
        }

        public T CheckLogin(Func<T, bool> _entity)
        {
            return entity.FirstOrDefault(_entity);
        }

        public void Delete(int Id)
        {
            entity.Remove(Get(Id));
        }

        public T Get(int Id)
        {
            return entity.Find(Id);
        }

        public IEnumerable<T> GetAll()
        {
            return entity.AsEnumerable();
        }

        public IEnumerable<T> GetShop_City_BussinessType(Func<T, bool> _entity)
        {
            return entity.Where(_entity).ToList();
        }

        public void Insert(T Entity)
        {
            entity.Add(Entity);
        }

        public void SaveChange()
        {
            dbContext.SaveChanges();
        }

        public void Update(T Entity)
        {
            entity.Update(Entity);
        }
    }
}
