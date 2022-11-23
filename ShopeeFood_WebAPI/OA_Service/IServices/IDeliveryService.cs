using OA_Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IDeliveryService
    {
        IEnumerable<Delivery> GetAll();

        Delivery Get(int Id);

        void Insert(Delivery Entity);

        void Delete(int Id);

        void Update(Delivery Entity);

        void SaveChange();
    }
}
