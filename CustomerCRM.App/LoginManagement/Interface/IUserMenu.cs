using CustomerCRM.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.LoginManagement.Interface
{
    public interface IUserMenu
    {
        void Show(RegistrationData authenticatedUser);
    }
}
