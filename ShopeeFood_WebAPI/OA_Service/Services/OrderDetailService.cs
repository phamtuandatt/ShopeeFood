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
    public class OrderDetailService : IOrderDetailService
    {
        private IRepository<OrderDetail> _orderDetailRepository;

        public OrderDetailService(IRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public void Remove(OrderDetail entity)
        {
            _orderDetailRepository.Remove(entity);
        }

        public OrderDetail Get(int Id)
        {
            return _orderDetailRepository.Get(Id);
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _orderDetailRepository.GetAll();
        }

        public void Insert(OrderDetail Entity)
        {
            _orderDetailRepository.Insert(Entity);
        }

        public void SaveChange()
        {
            _orderDetailRepository.SaveChange();
        }

        public void Update(OrderDetail Entity)
        {
            _orderDetailRepository.Update(Entity);
        }

        public IEnumerable<OrderDetail> GetOrderDetails(int Id)
        {
            return _orderDetailRepository.GetEntity(or => or.OrderId == Id);
        }

        public OrderDetail GetOrderDetail(int orderId, int productId)
        {
            return _orderDetailRepository.GetObject(ord => ord.OrderId == orderId && ord.ProductId == productId);
        }
    }
}
