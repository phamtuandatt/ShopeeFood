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

        IEnumerable<OrderDetail> GetOrderDetails(int Id);

        void Insert(OrderDetail Entity);

        void Remove(OrderDetail Entity);

        void Update(OrderDetail Entity);

        void SaveChange();

        OrderDetail GetOrderDetail(int orderId, int productId);
    }
}
