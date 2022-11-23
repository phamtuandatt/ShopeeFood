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
    public class DeliveryService : IDeliveryService
    {
        private IRepository<Delivery> _deliveryRepository;

        public DeliveryService(IRepository<Delivery> deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public void Delete(int Id)
        {
            _deliveryRepository.Delete(Id);
        }

        public Delivery Get(int Id)
        {
            return _deliveryRepository.Get(Id);
        }

        public IEnumerable<Delivery> GetAll()
        {
            return _deliveryRepository.GetAll();
        }

        public void Insert(Delivery Entity)
        {
            _deliveryRepository.Insert(Entity); 
        }

        public void SaveChange()
        {
            _deliveryRepository.SaveChange();
        }

        public void Update(Delivery Entity)
        {
            _deliveryRepository.Update(Entity);
        }
    }
}
