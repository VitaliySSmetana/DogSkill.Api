using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DogSkill.Api.Communications;
using DogSkill.Api.Data.Entities;
using DogSkill.Api.Helpers;
using DogSkill.Api.Repositories;
using DogSkill.Api.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DogSkill.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        { 
            var user = await _userRepository.GetForAuthenticationAsync(model.Email);

            if (user == null || user.Password != GetHashedPassword(model.Password, user.Salt))
                return null;

            var token = GenerateToken(user);

            return new AuthenticateResponse(user, token);
        }

        public User GetById(int id)
            => _userRepository.GetByIdAsync(id).Result;

        public async Task CreateUserAsync(UserCreateRequest request)
        {
            var salt = Guid.NewGuid().ToString();

            var user = new User
            {
                UserName = request.UserName,
                Phone = request.Phone,
                Email = request.Email,
                Salt = salt,
                Password = GetHashedPassword(request.Password, salt)
            };

            await _userRepository.CreateUserAsync(user);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var saltByte = Encoding.ASCII.GetBytes(salt);
            
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(password, saltByte, KeyDerivationPrf.HMACSHA1, 10000, 256 / 8));
        }
    }
}
