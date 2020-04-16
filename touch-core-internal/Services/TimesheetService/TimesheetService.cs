using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using touch_core_internal.Dtos.TimeSheet;
using touch_core_internal.Models;

namespace touch_core_internal.Services.TimesheetService
{
    public class TimesheetService : ITimeSheetService
    {
        public TimesheetService(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetTimeSheetDto>>> AddNewTimeSheetAsync(AddTimeSheetDto newTimeSheet)
        {
            var serviceResponse = new ServiceResponse<List<GetTimeSheetDto>>();
            var timesheet = this.Mapper.Map<TimeSheet>(newTimeSheet);
            await this.DataContext.TimeSheets.AddAsync(timesheet);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.TimeSheets
                .Include(x => x.Employee)
                .Select(e => this.Mapper.Map<GetTimeSheetDto>(e))
                .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTimeSheetDto>>> DeleteTimeSheetAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetTimeSheetDto>>();
            try
            {
                var timesheet = await this.DataContext.TimeSheets
                    .FirstAsync(x => x.TimeSheetId == id);

                this.DataContext.TimeSheets.Remove(timesheet);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.TimeSheets
                    .Select(x => this.Mapper.Map<GetTimeSheetDto>(x))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Timesheet does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTimeSheetDto>>> GetAllTimesheetsAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetTimeSheetDto>>();

            var dbTimeSheets = await DataContext.TimeSheets
                .Include(x => x.Employee)
                .ToListAsync();
            serviceResponse.Data = dbTimeSheets.Select(c => this.Mapper.Map<GetTimeSheetDto>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Employees: {serviceResponse.Data.Count}");
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTimeSheetDto>> GetTimeSheetByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetTimeSheetDto>();
            var dbTimeSheet = await DataContext.TimeSheets
                .Include(x => x.Employee)
                .FirstOrDefaultAsync(x => x.TimeSheetId == id);

            serviceResponse.Data = this.Mapper.Map<GetTimeSheetDto>(dbTimeSheet);
            return serviceResponse;
        }
    }
}