using WisePoll.Services.ViewModels;
using WisePoll.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WisePoll.Data.Models;
using Identity.PasswordHasher;

namespace WisePoll.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUsersRepository _repo;
        private readonly HttpContext _httpContext;
        public AuthService(IUsersRepository db, IHttpContextAccessor contextAccessor)
        {            
            _repo = db;
            _httpContext = contextAccessor.HttpContext;
        }

        public async Task RegisterAsync(Users user)
        {
            //Password hash with Identity.PasswordHash
            var passwordHasher = new PasswordHasher();
            string PasswordHash = passwordHasher.HashPassword(user.Password);

            user.Password = PasswordHash;

            await _repo.RegisterAsync(user);

        }


        public Users CheckSingleEmail(Users user)
        {
            Users result = _repo.FindUserByEmail(user);

            return result;
        }


        public async Task<bool> AuthenticateAsync(Users users, bool StayLog)
        {
            var FoundUser = CheckSingleEmail(users);

            // Search a user with the same Email
            if (FoundUser != null)
            {

                //Password hash with Identity.PasswordHash
                var passwordHasher = new PasswordHasher();

                // Compare passwords
                if (passwordHasher.VerifyHashedPassword(FoundUser.Password, users.Password))
                {

                    var claims = new List<Claim>()
                    {
                        new Claim("Name", users.Pseudo),
                        new Claim("Email", users.Email),
                        new Claim("Role", "User"),
                    };

                    var identity = new ClaimsIdentity(claims, "Cookies");
                    var principal = new ClaimsPrincipal(identity);

                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = StayLog
                    };

                    await _httpContext.SignInAsync(
                        "Cookies", principal, properties);
                    return true;
                }
            }

            return false;
        }
    }
}
