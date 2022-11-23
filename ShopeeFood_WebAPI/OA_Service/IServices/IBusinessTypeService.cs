using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IBusinessTypeService
    {
        IEnumerable<BusinessType> GetAll();

        BusinessType Get(int Id);

        void Insert(BusinessType Entity);

        void Delete(int Id);

        void Update(BusinessType Entity);

        void SaveChange();
    }
}
