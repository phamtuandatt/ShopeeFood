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
    public class ProductTypeService : IProductTypeService
    {
        private IRepository<ProductType> _productTypeRepository;

        public ProductTypeService(IRepository<ProductType> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public void Delete(int Id)
        {
            _productTypeRepository.Delete(Id);
        }

        public ProductType Get(int Id)
        {
            return _productTypeRepository.Get(Id);
        }

        public IEnumerable<ProductType> GetAll()
        {
            return _productTypeRepository.GetAll();
        }

        public void Insert(ProductType Entity)
        {
            _productTypeRepository.Insert(Entity); 
        }

        public void SaveChange()
        {
            _productTypeRepository.SaveChange();
        }

        public void Update(ProductType Entity)
        {
            _productTypeRepository.Update(Entity);
        }
    }
}
