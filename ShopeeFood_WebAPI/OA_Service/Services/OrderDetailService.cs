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

        public void Delete(int Id)
        {
            _orderDetailRepository.Delete(Id);
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
    }
}
