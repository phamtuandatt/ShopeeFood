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
    public class ShopService : IShopService
    {
        private IRepository<Shop> _shopRepository;

        public ShopService(IRepository<Shop> shopRepository)
        {
            _shopRepository = shopRepository;
        }

        public void Delete(int Id)
        {
            _shopRepository.Delete(Id);
        }

        public Shop Get(int Id)
        {
            return _shopRepository.Get(Id);
        }

        public IEnumerable<Shop> GetAll()
        {
            return _shopRepository.GetAll();
        }

        public IEnumerable<Shop> GetShopCityBussinessType(int cityId, int bussinessId)
        {
            return _shopRepository.GetEntity(shop => shop.CityId == cityId && shop.BusinessId == bussinessId).ToList();
        }

        public IEnumerable<Shop> GetShopByCityId(int cityId)
        {
            return _shopRepository.GetEntity(shop => shop.CityId == cityId).ToList();
        }

        public IEnumerable<Shop> GetShopByCityIdCityDistrictId(int cityId, int cityDistrictId)
        {
            return _shopRepository.GetEntity(shop => shop.CityId == cityId && shop.CityDistricId == cityDistrictId).ToList();
        }

        public void Insert(Shop Entity)
        {
            _shopRepository.Insert(Entity);
        }

        public void SaveChange()
        {
            _shopRepository.SaveChange();
        }

        public void Update(Shop Entity)
        {
            _shopRepository.Update(Entity);
        }
    }
}
