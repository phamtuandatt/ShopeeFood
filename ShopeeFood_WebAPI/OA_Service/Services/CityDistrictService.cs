using Azure.Core;
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
    public class CityDistrictService : ICityDistrictService
    {
        private IRepository<CityDistrict> _cityDistrictRepository;

        public CityDistrictService(IRepository<CityDistrict> cityDistrictRepository)
        {
            _cityDistrictRepository = cityDistrictRepository;
        }

        public void Delete(int Id)
        {
            _cityDistrictRepository.Delete(Id);
        }

        public CityDistrict Get(int Id)
        {
            return _cityDistrictRepository.Get(Id);
        }

        public IEnumerable<CityDistrict> GetAll()
        {
            return _cityDistrictRepository.GetAll();
        }

        public void Insert(CityDistrict Entity)
        {
            _cityDistrictRepository.Insert(Entity);
        }

        public void SaveChange()
        {
            _cityDistrictRepository.SaveChange();
        }

        public void Update(CityDistrict Entity)
        {
            _cityDistrictRepository.Update(Entity);
        }
    }
}
