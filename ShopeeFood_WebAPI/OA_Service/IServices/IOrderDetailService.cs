using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IOrderDetailService
    {
        IEnumerable<OrderDetail> GetAll();

        OrderDetail Get(int Id);

        void Insert(OrderDetail Entity);

        void Delete(int Id);

        void Update(OrderDetail Entity);

        void SaveChange();
    }
}
