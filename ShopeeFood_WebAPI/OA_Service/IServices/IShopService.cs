using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IShopService
    {
        IEnumerable<Shop> GetAll();

        Shop Get(int Id);

        void Insert(Shop Entity);

        void Delete(int Id);

        void Update(Shop Entity);

        void SaveChange();

        IEnumerable<Shop> GetShopCityBussinessType(int cityId, int bussinessId);

        IEnumerable<Shop> GetShopByCityId(int cityId);
        
        IEnumerable<Shop> GetShopByCityIdCityDistrictId(int cityId, int cityDistrictId);

    }
}
