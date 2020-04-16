using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using touch_core_internal.Dtos.Badge;
using touch_core_internal.Models;

namespace touch_core_internal.Services.BadgeService
{
    public class BadgeService : IBadgeService
    {
        public BadgeService(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<List<GetBadgeDto>>> AddNewBadgeAsync(AddBadgeDto newBadge)
        {
            var serviceResponse = new ServiceResponse<List<GetBadgeDto>>();
            var badge = this.Mapper.Map<Badge>(newBadge);
            await this.DataContext.Badges.AddAsync(badge);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = await this.DataContext.Badges
                .Include(x => x.Category)
                .Select(e => this.Mapper.Map<GetBadgeDto>(e))
                .ToListAsync();

            serviceResponse.UpdateResponseStatus("New badge added successfully");
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetBadgeDto>>> DeleteBadgeAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<List<GetBadgeDto>>();
            try
            {
                var badge = await this.DataContext.Badges
                    .FirstAsync(x => x.BadgeId == id);

                this.DataContext.Badges.Remove(badge);
                await this.DataContext.SaveChangesAsync();

                serviceResponse.Data = await this.DataContext.Badges
                    .Include(x => x.Category)
                    .Select(x => this.Mapper.Map<GetBadgeDto>(x))
                    .ToListAsync();

                serviceResponse.UpdateResponseStatus("Badge deleted successfully");
            }
            catch (Exception ex)
            {
                serviceResponse.UpdateResponseStatus("Badge does not exist", false);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetBadgeDto>>> GetAllBadgesAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetBadgeDto>>();

            var dbBadges = await DataContext.Badges
                .Include(x => x.Category)
                .ToListAsync();

            serviceResponse.Data = dbBadges.Select(c => this.Mapper.Map<GetBadgeDto>(c)).ToList();
            serviceResponse.UpdateResponseStatus($"Count of Badges: {serviceResponse.Data.Count}");

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetBadgeDto>> GetBadgeByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetBadgeDto>();
            var dbBadge = await DataContext.Badges
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.BadgeId == id);

            serviceResponse.Data = this.Mapper.Map<GetBadgeDto>(dbBadge);

            serviceResponse.UpdateResponseStatus("Badge exist");
            return serviceResponse;
        }
    }
}