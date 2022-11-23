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
    public class EmployeeService : IEmployeeService
    {
        private IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Delete(int Id)
        {
            _employeeRepository.Delete(Id);
        }

        public Employee Get(int Id)
        {
            return _employeeRepository.Get(Id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeRepository.GetAll();
        }

        public void Insert(Employee Entity)
        {
            _employeeRepository.Insert(Entity);
        }

        public void SaveChange()
        {
            _employeeRepository.SaveChange();
        }

        public void Update(Employee Entity)
        {
            _employeeRepository.Update(Entity);
        }
    }
}
