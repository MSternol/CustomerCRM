using System;
using System.Security.Cryptography.X509Certificates;
using DataStorage;
using CustomerCRM.App.LoginManagement;
using CustomerCRM.App.Registration;
using CustomerCRM.Domain.Services.Security;
using DataStorage.Raport;

namespace CustomerCRM
{
    internal class Program
    {

        static void Main(string[] args)
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("====== Witaj! w systemie CRM Dla Klientów ======" + "\n");
                Console.WriteLine("1.Zaloguj się");
                Console.WriteLine("2.Zarejestruj się");
                Console.WriteLine("0.Wyjdź" + "\n");
                string operation = Console.ReadLine();

                switch (operation)
                {
                    case "1":
                        Console.Clear();
                        LoginSystem loginSystem = new LoginSystem();
                        loginSystem.Logging();
                        break;
                    case "2":
                        Console.Clear();
                        Registration registrationService = new Registration();
                        registrationService.Register();
                        break;
                    case "0":
                        Console.WriteLine("Do Zobaczenia!");
                        isRunning = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie");
                        break;
                }
            }
        }

    }
}