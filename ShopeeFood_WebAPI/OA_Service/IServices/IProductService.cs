using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        Product Get(int Id);

        void Insert(Product Entity);

        void Delete(int Id);

        void Update(Product Entity);

        void SaveChange();
    }
}
