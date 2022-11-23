using OA_Data.Entities;
using OA_Repository.IRepository;
using OA_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.Services
{
    public class CityDetailService : ICityDetailService
    {
        private IRepository<CityDetail> _cityDetailReposiory;

        public CityDetailService(IRepository<CityDetail> cityDetailReposiory)
        {
            _cityDetailReposiory = cityDetailReposiory;
        }

        public void Delete(int Id)
        {
            _cityDetailReposiory.Delete(Id);
        }

        public CityDetail Get(int Id)
        {
            return _cityDetailReposiory.Get(Id);
        }

        public IEnumerable<CityDetail> GetAll()
        {
            return _cityDetailReposiory.GetAll();
        }

        public void Insert(CityDetail Entity)
        {
            _cityDetailReposiory.Insert(Entity);
        }

        public void SaveChange()
        {
            _cityDetailReposiory.SaveChange();
        }

        public void Update(CityDetail Entity)
        {
            _cityDetailReposiory.Update(Entity);
        }
    }
}
