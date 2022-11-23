using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Repository.IRepository
{
    public interface ICityRepository
    {
        IEnumerable<City> GetCities();

        City Get(int Id);

        void Insert(City Entity);

        void Delete(int Id);

        void Update(City Entity);

        void SaveChange();
    }
}
