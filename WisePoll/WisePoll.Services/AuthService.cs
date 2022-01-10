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

namespace WisePoll.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUsersRepository _repo;
        public AuthService(IUsersRepository db)
        {            
            _repo = db;
        }

        public async Task RegisterAsync(AuthRegisterViewModel model)
        {

            var user = new Users()
            {
                Pseudo = model.Pseudo,
                Email = model.Email,
                Password = model.Password,
            };

            await _repo.RegisterAsync(user);

        }


        public bool VerifyUniqueEmail(AuthRegisterViewModel model)
        {
            var user = new Users()
            {
                Pseudo = model.Pseudo,
                Email = model.Email,
                Password = model.Password,
            };

            Users result = _repo.FindUserByEmail(user);

            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
