using OA_Data.Entities;
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

        void Insert(Customer Entity);

        void Delete(int Id);

        void Update(Customer Entity);

        void SaveChange();

        Customer CheckLogin(string email, string password);
    }
}
