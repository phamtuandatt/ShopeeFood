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
    public class CustomerService : ICustomerService
    {
        private IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer IsCustomerExisted(string email)
        {
            return _customerRepository.Check_Existed(c => c.Email == email);
        }

        public Customer CheckLogin(string email, string password)
        {
            return _customerRepository.CheckLogin(c => c.Email == email && c.Password == password);
        }

        public void Delete(int Id)
        {
            _customerRepository.Delete(Id);
        }

        public Customer Get(int Id)
        {
            return _customerRepository.Get(Id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCusByEmail(string email)
        {
            return _customerRepository.GetObject(c => c.Email == email);
        }

        public void Insert(Customer Entity)
        {
            _customerRepository.Insert(Entity); 
        }

        public void SaveChange()
        {
            _customerRepository.SaveChange();
        }

        public void Update(Customer Entity)
        {
            _customerRepository.Update(Entity);
        }
    }
}
