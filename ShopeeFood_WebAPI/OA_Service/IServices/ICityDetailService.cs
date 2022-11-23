using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface ICityDetailService
    {
        IEnumerable<CityDetail> GetAll();

        CityDetail Get(int Id);

        void Insert(CityDetail Entity);

        void Delete(int Id);

        void Update(CityDetail Entity);

        void SaveChange();
    }
}
