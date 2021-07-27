using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Communications;
using DogSkill.Api.Data.Entities;
using DogSkill.Api.Repositories;
using DogSkill.Api.Services.Interfaces;

namespace DogSkill.Api.Services
{
    public class UserActivityService : IUserActivityService
    {
        private readonly IUserActivityRepository _activityRepository;

        public UserActivityService(IUserActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<List<UserActivityGetResponse>> GetTopTenAsync()
        {
            var topScoreUsers = (await _activityRepository.GetTopTenAsync())
                .Select(x => new UserActivityGetResponse(x))
                .ToList();

            return topScoreUsers;
        }

        public async Task UpsertActivityAsync(UserActivityUpsertRequest request)
        {
            var userActivity = await _activityRepository.GetByUserIdAsync(request.UserId);

            if (userActivity == null)
            {
                var entity = new UserActivity
                {
                    UserId = request.UserId,
                    Score = request.Score,
                    LastScore = request.Score
                };
                entity.ModifiedAt = entity.CreatedAt;

                await _activityRepository.CreateAsync(entity);
                return;
            }

            await _activityRepository.UpdateScoreAsync(request.Score, userActivity);
        }
    }
}
