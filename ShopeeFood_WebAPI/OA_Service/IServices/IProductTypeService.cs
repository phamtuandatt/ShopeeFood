using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IProductTypeService
    {
        IEnumerable<ProductType> GetAll();

        ProductType Get(int Id);

        void Insert(ProductType Entity);

        void Delete(int Id);

        void Update(ProductType Entity);

        void SaveChange();
    }
}
