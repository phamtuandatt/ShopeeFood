using Azure.Core;
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
    public class OrderService : IOrderService
    {
        private IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Delete(int Id)
        {
            _orderRepository.Delete(Id);
        }

        public Order Get(int Id)
        {
            return _orderRepository.Get(Id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public int GetMaxId()
        {
            return _orderRepository.GetMaxIdObject(order => order.OrderId);
        }

        public int GetMaxIdOrder(int customerId, int shopId)
        {
            return _orderRepository.GetMaxIdObject(id => id.CustomerId);
        }

        public IEnumerable<Order> GetOrderByCustomer(int customerID)
        {
            return _orderRepository.GetEntity(cus => cus.CustomerId == customerID && cus.Status == 1).ToList();
        }

        public void Insert(Order Entity)
        {
            _orderRepository.Insert(Entity);
        }

        public Order OrderIsExisted(int customerId, int shopId)
        {
            return _orderRepository.CheckExisted(id => id.CustomerId == customerId && id.ShopId == shopId && id.Status == 0);
        }

        public void SaveChange()
        {
            _orderRepository.SaveChange();
        }

        public void Update(Order Entity)
        {
            _orderRepository.Update(Entity);
        }
    }
}
