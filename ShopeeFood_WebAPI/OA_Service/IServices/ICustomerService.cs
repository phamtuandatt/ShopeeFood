using OA_Data.Entities;
using OA_Service.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();

        Customer Get(int Id);

        void Insert(CreateCustomerRequest Entity);

        void Delete(int Id);

        void Update(Customer Entity);

        void SaveChange();

        Customer CheckLogin(string email, string password);

        Customer IsCustomerExisted(string email);

        Customer GetCusByEmail(string email);
    }
}
