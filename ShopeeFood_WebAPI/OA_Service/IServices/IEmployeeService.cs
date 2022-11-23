using OA_Data.Entities;
using OA_Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA_Service.IServices
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();

        Employee Get(int Id);

        void Insert(Employee Entity);

        void Delete(int Id);

        void Update(Employee Entity);

        void SaveChange();


    }
}
