using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using touch_core_internal.Dtos.Employee;
using touch_core_internal.Models;

namespace touch_core_internal.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> AddNewEmployeeAsync(AddEmployeeDto newEmployee)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            var employee = this.Mapper.Map<Employee>(newEmployee);
            await this.DataContext.Employees.AddAsync(employee);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.Employees
                .Include(x => x.TimeSheets)
                .Select(e => this.Mapper.Map<GetEmployeeDto>(e))
                .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> DeleteEmployeeAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            try
            {
                var employee = await this.DataContext.Employees.FirstAsync(x => x.EmployeeId == id);
                this.DataContext.Employees.Remove(employee);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.Employees
                    .Select(x => this.Mapper.Map<GetEmployeeDto>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Employee does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEmployeeDto>>> GetAllEmployeesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetEmployeeDto>>();
            var dbEmployees = await DataContext.Employees
                .Include(x => x.TimeSheets)
                .ToListAsync();
            serviceResponse.Data = dbEmployees.Select(c => this.Mapper.Map<GetEmployeeDto>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Employees: {serviceResponse.Data.Count}");
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetEmployeeByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            var dbEmployee = await DataContext.Employees
                .Include(x => x.TimeSheets)
                .FirstOrDefaultAsync(x => x.EmployeeId == id);

            serviceResponse.Data = this.Mapper.Map<GetEmployeeDto>(dbEmployee);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> GetEmployeeByNameAsync(string name)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            var dbEmployee = await DataContext.Employees.FirstOrDefaultAsync(x => x.Name == name);

            serviceResponse.Data = this.Mapper.Map<GetEmployeeDto>(dbEmployee);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEmployeeDto>> UpdateEmployeeAsync(UpdateEmployeeDto updateEmployee)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            try
            {
                var employee = await this.DataContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == updateEmployee.EmployeeId);
                employee.Designation = updateEmployee.Designation;
                employee.Email = updateEmployee.Email;
                employee.Identifier = updateEmployee.Identifier;
                employee.Name = updateEmployee.Name;

                this.DataContext.Employees.Update(employee);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = this.Mapper.Map<GetEmployeeDto>(employee);
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus($"Employee {updateEmployee.Name} not found", false);
            }
            return serviceResponse;
        }
    }
}