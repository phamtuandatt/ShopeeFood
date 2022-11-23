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
    public class BusinessTypeService : IBusinessTypeService
    {
        private IRepository<BusinessType> _businessTypeRepository;

        public BusinessTypeService(IRepository<BusinessType> businessTypeRepository)
        {
            _businessTypeRepository = businessTypeRepository;
        }

        public void Delete(int Id)
        {
            _businessTypeRepository.Delete(Id);
        }

        public BusinessType Get(int Id)
        {
            return _businessTypeRepository.Get(Id);
        }

        public IEnumerable<BusinessType> GetAll()
        {
            return _businessTypeRepository.GetAll();
        }

        public void Insert(BusinessType Entity)
        {
            _businessTypeRepository.Insert(Entity);
        }

        public void SaveChange()
        {
            _businessTypeRepository.SaveChange();
        }

        public void Update(BusinessType Entity)
        {
            _businessTypeRepository.Update(Entity);
        }
    }
}
