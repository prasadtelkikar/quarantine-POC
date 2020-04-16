using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using touch_core_internal.Dtos.TimeSheet;
using touch_core_internal.Services;
using touch_core_internal.Services.TimesheetService;

namespace touch_core_internal.Controllers
{
    [ApiController]
    [Route("api/timesheet")]
    public class TimesheetController : ControllerBase
    {
        public TimesheetController(ITimeSheetService timeSheetService)
        {
            this.TimeSheetService = timeSheetService;
        }

        public ITimeSheetService TimeSheetService { get; set; }

        [Route("{id:guid}"), HttpDelete]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.TimeSheetService.DeleteTimeSheetAsync(id);

            if (serviceResponse.Data == null)
                return this.NotFound("Timesheet not found");

            return this.Ok(serviceResponse);
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            var serviceResponse = await this.TimeSheetService.GetAllTimesheetsAsync();
            return this.Ok(serviceResponse);
        }

        [Route("{id:guid?}"), HttpGet]
        public virtual async Task<IActionResult> GetByIdAsync(Guid? id)
        {
            var serviceResponse = new ServiceResponse<GetTimeSheetDto>();
            if (id.HasValue)
            {
                serviceResponse = await this.TimeSheetService.GetTimeSheetByIdAsync(id.Value);
                if (serviceResponse.Data == null)
                {
                    serviceResponse.UpdateResponseStatus($"Timesheet does not exist", false);
                    return this.NotFound(serviceResponse);
                }
                serviceResponse.UpdateResponseStatus($"Timesheet exist");
                return this.Ok(serviceResponse);
            }
            else
            {
                serviceResponse.UpdateResponseStatus($"Timesheet does not exist", false);
                return this.NotFound("");
            }
        }

        [Route("upsert"), HttpPost]
        public virtual async Task<IActionResult> UpsertTimeSheet(AddTimeSheetDto newTimeSheet)
        {
            if (!ModelState.IsValid)
                return this.BadRequest(ModelState);

            var serviceResponse = await this.TimeSheetService.AddNewTimeSheetAsync(newTimeSheet);
            return this.Ok(serviceResponse);
        }
    }
}