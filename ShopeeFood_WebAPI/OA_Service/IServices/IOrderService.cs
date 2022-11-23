using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();

        Order Get(int Id);

        void Insert(Order Entity);

        void Delete(int Id);

        void Update(Order Entity);

        void SaveChange();
    }
}
