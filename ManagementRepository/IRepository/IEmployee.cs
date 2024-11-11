using ManagementDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementRepository.IRepository
{
    public interface IEmployee
    {
        public List<EmployeeDTO> GetEmployees();
        public List<StateDTO> GetStates();
        public List<CityDTO> GetCity(int id);

        public void AddEmployee(EmployeeDTO employeeDTO);
    }
}
