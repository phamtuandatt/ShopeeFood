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
    public class ProductService : IProductService
    {
        private IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public void Delete(int Id)
        {
            _productRepository.Delete(Id); 
        }

        public Product Get(int Id)
        {
            return _productRepository.Get(Id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public void Insert(Product Entity)
        {
            _productRepository.Insert(Entity); 
        }

        public void SaveChange()
        {
            _productRepository.SaveChange();
        }

        public void Update(Product Entity)
        {
            _productRepository.Update(Entity);
        }
    }
}
