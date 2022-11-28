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
    public class CustomerAddressService : ICustomerAddressService
    {
        private IRepository<CustomerAddress> _customerAddressRepository;

        public CustomerAddressService(IRepository<CustomerAddress> customerAddressRepository)
        {
            _customerAddressRepository = customerAddressRepository;
        }

        public void Delete(int Id)
        {
            _customerAddressRepository.Delete(Id);
        }

        public CustomerAddress Get(int Id)
        {
            return _customerAddressRepository.Get(Id);
        }

        public IEnumerable<CustomerAddress> GetAll()
        {
            return _customerAddressRepository.GetAll();
        }

        public void Insert(CustomerAddress Entity)
        {
            _customerAddressRepository.Insert(Entity);
        }

        public void SaveChange()
        {
            _customerAddressRepository.SaveChange();
        }

        public void Update(CustomerAddress Entity)
        {
            _customerAddressRepository.Update(Entity);
        }
    }
}
