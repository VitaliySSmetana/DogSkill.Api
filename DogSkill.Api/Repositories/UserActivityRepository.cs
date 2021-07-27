using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Data;
using DogSkill.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogSkill.Api.Repositories
{
    public class UserActivityRepository : IUserActivityRepository
    {
        private readonly DSContext _context;

        public UserActivityRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<List<UserActivity>> GetTopTenAsync()
        {
            return await _context.UserActivities
                .Include(x => x.User)
                .OrderByDescending(x => x.Score)
                .Take(10)
                .ToListAsync();
        }

        public async Task<UserActivity> GetByUserIdAsync(Guid userId)
        {
            return await _context.UserActivities.AsTracking().FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task CreateAsync(UserActivity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateScoreAsync(int score, UserActivity entity)
        {
            if (entity.Score < score)
            {
                entity.Score = score;
            }

            entity.LastScore = score;
            entity.ModifiedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}
