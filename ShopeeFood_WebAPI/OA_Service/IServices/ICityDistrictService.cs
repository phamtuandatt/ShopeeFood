using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface ICityDistrictService
    {
        IEnumerable<CityDistrict> GetAll();

        CityDistrict Get(int Id);

        void Insert(CityDistrict Entity);

        void Delete(int Id);

        void Update(CityDistrict Entity);

        void SaveChange();
    }
}
