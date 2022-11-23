using Microsoft.EntityFrameworkCore;
using OA_Data.Entities;
using OA_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Repository.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CityRepository (ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Delete(int Id)
        {
            City city = dbContext.Cities.Find(Id);
            dbContext.Cities.Remove(city);
        }

        public City Get(int Id)
        {
            return  dbContext.Cities.Find(Id);
        }

        public IEnumerable<City> GetCities()
        {
            return dbContext.Cities.ToList();
        }

        public void Insert(City Entity)
        {
            dbContext.Cities.Add(Entity);
        }

        public void SaveChange()
        {
            dbContext.SaveChanges();
        }

        public void Update(City Entity)
        {
            dbContext.Entry(Entity).State = EntityState.Modified;
        }
    }
}
