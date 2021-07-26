using System.ComponentModel.DataAnnotations;

namespace DogSkill.Api.Communications
{
    public class AuthenticateRequest
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
