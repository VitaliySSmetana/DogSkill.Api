using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogSkill.Api.Data.Entities;

namespace DogSkill.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetForAuthenticationAsync(string email);
        Task<User> GetByIdAsync(int id);
        Task CreateUserAsync(User entity);
    }
}
