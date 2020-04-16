using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using touch_core_internal.Dtos.Reward;
using touch_core_internal.Models;

namespace touch_core_internal.Services.RewardService
{
    public class RewardService : IRewardService
    {
        public RewardService(IMapper mapper, DataContext dataContext)
        {
            this.Mapper = mapper;
            this.DataContext = dataContext;
        }

        public DataContext DataContext { get; private set; }

        public IMapper Mapper { get; private set; }

        public async Task<ServiceResponse<Guid>> AddNewRewardAsync(AddRewardDto newReward)
        {
            var serviceResponse = new ServiceResponse<Guid>();
            var reward = this.Mapper.Map<Reward>(newReward);
            await this.DataContext.Rewards.AddAsync(reward);
            await this.DataContext.SaveChangesAsync();

            serviceResponse.Data = reward.RewardId;
            serviceResponse.UpdateResponseStatus("Added new reward successfully");
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetRewardDto>>> GetAllRewardsAsync()
        {
            var serviceResponse = new ServiceResponse<List<GetRewardDto>>();

            var dbRewards = await DataContext.Rewards
                .Include(x => x.Employee).ThenInclude(e => e.TimeSheets)
                .Include(x => x.Badge).ThenInclude(b => b.Category)
                .ToListAsync();

            serviceResponse.Data = dbRewards.Select(c => this.Mapper.Map<GetRewardDto>(c)).ToList();

            serviceResponse.UpdateResponseStatus($"Count of Rewards: {serviceResponse.Data.Count}");
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetRewardDto>> GetRewardByIdAsync(Guid id)
        {
            var serviceResponse = new ServiceResponse<GetRewardDto>();
            var dbReward = await DataContext.Rewards
                .Include(x => x.Employee).ThenInclude(e => e.TimeSheets)
                .Include(x => x.Badge).ThenInclude(b => b.Category)
                .FirstOrDefaultAsync(x => x.RewardId == id);

            serviceResponse.Data = this.Mapper.Map<GetRewardDto>(dbReward);
            return serviceResponse;
        }
    }
}