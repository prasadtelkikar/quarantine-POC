using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.Dtos.Employee;
using touch_core_internal.Services;
using touch_core_internal.Services.EmployeeService;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(IEmployeeService employeeService)
        {
            this.EmployeeService = employeeService;
        }

        public IEmployeeService EmployeeService { get; set; }

        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.EmployeeService.DeleteEmployeeAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Employee not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.EmployeeService.GetAllEmployeesAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetEmployeeDto>();
            if (id.HasValue)
            {
                serviceResponse = await this.EmployeeService.GetEmployeeByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Employee does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Employee exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Employee does not exist", false);
                return this.NotFound("");
            }
        }

        [Route("{username?}"), HttpGet]
        public virtual async Task<IActionResult> GetByNameAsync(string username = null)
        {
            ServiceResponse<GetEmployeeDto> serviceResponse = new ServiceResponse<GetEmployeeDto>();
            if (!string.IsNullOrEmpty(username))
            {
                serviceResponse = await EmployeeService.GetEmployeeByNameAsync(username);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Employee with {username} does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Employee { username } exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Employee does not exist", false);
                return this.NotFound("");
            }
        }

        [Route("update"), HttpPost]
        public virtual async Task<IActionResult> UpdateEmployee(UpdateEmployeeDto updateEmployee)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.EmployeeService.UpdateEmployeeAsync(updateEmployee);
            return this.Ok(serviceResponse);
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertEmployee(AddEmployeeDto newEmployee)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.EmployeeService.AddNewEmployeeAsync(newEmployee);
            return this.Ok(serviceResponse);
        }
    }
}