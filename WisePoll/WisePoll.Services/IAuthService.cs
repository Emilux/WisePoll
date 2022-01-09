using WisePoll.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WisePoll.Data.Models;

namespace WisePoll.Services
{
    public interface IAuthService
    {
        bool VerifyUniqueEmail(AuthRegisterViewModel model);
        Task RegisterAsync(AuthRegisterViewModel model);
    }
}
