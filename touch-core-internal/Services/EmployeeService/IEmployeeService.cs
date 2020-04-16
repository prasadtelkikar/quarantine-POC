using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using touch_core_internal.Dtos.Employee;

namespace touch_core_internal.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<ServiceResponse<List<GetEmployeeDto>>> AddNewEmployeeAsync(AddEmployeeDto newEmployee);

        Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployeeAsync(Guid id);

        Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployeesAsync();

        Task<ServiceResponse<GetEmployeeDto>> GetEmployeeByIdAsync(Guid id);

        Task<ServiceResponse<GetEmployeeDto>> GetEmployeeByNameAsync(string name);

        Task<ServiceResponse<GetEmployeeDto>> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployee);
    }
}