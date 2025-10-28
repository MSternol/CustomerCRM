using CustomerCRM.App.Administrator;
using CustomerCRM.App.Customer;
using CustomerCRM.App.LoginManagement.Interface;
using CustomerCRM.App.Supplier;
using CustomerCRM.Domain.Models;

namespace CustomerCRM.App.ViewMenuUser
{
    public class AdministratorMenu : IUserMenu
    {
        public void Show(RegistrationData authenticatedUser)
        {
            bool isRunning = true;

            while (isRunning)
            {
                ViewAdministratorMenu viewAdministratorMenu = new ViewAdministratorMenu();
                isRunning = viewAdministratorMenu.ViewMenu(authenticatedUser);
            }
        }
    }

    public class CustomerMenu : IUserMenu
    {
        private ModelCustomer user;


        public void Show(RegistrationData authenticatedUser)
        {
            bool isRunning = true;

            while (isRunning)
            {
                ViewCustomerMenu viewCustomerMenu = new ViewCustomerMenu(user);
                isRunning = viewCustomerMenu.ViewMenu(authenticatedUser);
            }
        }
    }

    public class SupplierMenu : IUserMenu
    {
        public void Show(RegistrationData authenticatedUser)
        {
            bool isRunning = true;

            while (isRunning)
            {
                ViewSupplierMenu viewSupplierMenu = new ViewSupplierMenu();
                isRunning = viewSupplierMenu.ViewMenu(authenticatedUser);
            }
        }
    }

}