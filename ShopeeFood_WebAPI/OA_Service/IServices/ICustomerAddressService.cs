using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface ICustomerAddressService
    {
        IEnumerable<CustomerAddress> GetAll();

        CustomerAddress Get(int Id);

        void Insert(CustomerAddress Entity);

        void Delete(int Id);

        void Update(CustomerAddress Entity);

        void SaveChange();
    }
}
