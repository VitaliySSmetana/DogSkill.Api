using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Data;
using DogSkill.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DogSkill.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DSContext _context;

        public UserRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<User> GetForAuthenticationAsync(string userName, string password)
            => await _context.Users.FirstOrDefaultAsync(x => x.FirstName == userName && x.Password == password);

        public async Task<User> GetByIdAsync(int id)
            => await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }
}
