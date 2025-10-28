using CustomerCRM.App.LoginManagement.Interface;
using CustomerCRM.App.ViewMenuUser;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.Security;
using DataStorage;
using DataStorage.LoggApp;
using System;
namespace CustomerCRM.App.LoginManagement
{
    public class LoginSystem
    {
        private ModelCustomer user;
        private bool registrationCancelled;

        public void Logging()
        {

            Console.WriteLine("Witaj! Wybierz, jako kogo chcesz się zalogować:");
            Console.WriteLine("1. Klient");
            Console.WriteLine("2. Dostawca");
            Console.WriteLine("3. Administrator");
            Console.Write("Wybierz opcję (1/2/3): ");
            string option = Console.ReadLine();

            string accountType = GetAccountType(option);

            if (accountType != null)
            {
                Console.WriteLine($"Witaj jako {accountType}!");

                Console.Write("Login: ");
                string login = Console.ReadLine();

                Console.Write("Hasło: ");
                string password = Domain.Services.Security.SecurePasswordInput.GetPassword(ref registrationCancelled);

                string filePath = GetFilePath(accountType);

                if (filePath != null)
                {
                    try
                    {
                        RegistrationData authenticatedUser = Domain.Services.Authentication.AuthenticateUser(filePath, login, password, accountType);

                        if (authenticatedUser != null)
                        {
                            IUserMenu userMenu = GetUserMenu(accountType);
                            if (userMenu != null)
                            {
                                userMenu.Show(authenticatedUser);
                                LogToFileMessage.LogSuccess($"Zalogowano jako {accountType} o nazwie użytkownika: {login}", "LoginSystem.Logging");
                            }
                        }
                        else
                        {
                            LogToFileMessage.LogError("Nieprawidłowy login lub hasło.", "LoginSystem.Logging");
                        }
                    }
                    catch (Exception ex)
                    {
                        string location = "LoginSystem.cs";
                        LogToFileMessage.LogError($"Błąd: {ex.Message}", location);
                    }
                }
                else
                {
                    Console.WriteLine("Błąd: Nie można znaleźć pliku z danymi użytkowników.");
                    LogToFileMessage.LogError("Nie można znaleźć pliku z danymi użytkowników.", "LoginSystem.Logging");
                }
            }

            Console.ReadLine();
        }

        private string GetAccountType(string option)
        {
            return option switch
            {
                "1" => "Klient",
                "2" => "Dostawca",
                "3" => "Administrator",
                _ => null
            };
        }

        private string GetFilePath(string accountType)
        {
            return accountType switch
            {
                "Administrator" => FileLocations.GetAdminFilePath(),
                "Klient" => FileLocations.GetCustomerFilePath(),
                "Dostawca" => FileLocations.GetSupplierFilePath(),
                _ => null
            };
        }

        private IUserMenu GetUserMenu(string accountType)
        {
            switch (accountType)
            {
                case "Administrator":
                    return new AdministratorMenu();

                case "Klient":
                    return new CustomerMenu();

                case "Dostawca":
                    return new SupplierMenu();

                default:
                    return null;
            }
        }
    }
}
