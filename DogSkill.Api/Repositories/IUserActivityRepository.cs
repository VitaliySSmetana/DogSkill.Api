using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Data.Entities;

namespace DogSkill.Api.Repositories
{
    public interface IUserActivityRepository
    {
        Task<List<UserActivity>> GetTopTenAsync();
        Task<UserActivity> GetByUserIdAsync(Guid userId);
        Task CreateAsync(UserActivity entity);
        Task UpdateScoreAsync(int score, UserActivity entity);
    }
}
