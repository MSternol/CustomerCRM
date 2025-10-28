using CustomerCRM.App.Helpers;
using CustomerCRM.App.Services;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services;
using DataStorage.LoggApp;
using CustomerCRM.Domain.Services.Security;
using System;

namespace CustomerCRM.App.Registration
{
    public class Registration
    {
        private bool registrationCancelled;
        private string id = IDGenerator.GenerateUniqueID();

        public void Register()
        {
            RegisterMenu();
        }
        private void RegisterMenu()
        {
            Console.WriteLine("==== Rejestracja ====" + "\n");
            Console.WriteLine("Anulowanie - ESC" + "\n");
            string username;
            try
            {
                do
                {
                    Console.WriteLine("\n" + "Login musi się składać z 4-14 znaków i conajmniej 1 cyfry");
                    Console.Write("Podaj Login: ");
                    username = CheckForEscKey.ReadInput(ref registrationCancelled);
                    if (registrationCancelled)
                        return;
                } while (!ValidationHelper.ValidateUsername(username));

                string password;
                do
                {
                    Console.WriteLine("\n" + "Hasło musi się składać z 8-16 znaków i conajmniej 1 cyfry i znaku specjalnego");
                    Console.Write("Podaj Hasło: ");
                    password = Domain.Services.Security.SecurePasswordInput.GetPassword(ref registrationCancelled);
                    if (registrationCancelled)
                        return;
                } while (!ValidationHelper.ValidatePassword(password));

                string email;
                do
                {
                    Console.WriteLine("\n" + "Przykładowy Email: Przyklad@Email.org");
                    Console.Write("Podaj Email: ");
                    email = CheckForEscKey.ReadInput(ref registrationCancelled);
                    if (registrationCancelled)
                        return;
                } while (!ValidationHelper.ValidateEmail(email));

                string role = "";

                Console.WriteLine("\n" + "Wybierz Rolę:" + "\n" + "1.Klient" + "\n" + "2.Dostawca" + "\n");
                role = CheckForEscKey.ReadInput(ref registrationCancelled);

                if (registrationCancelled)
                {
                    return;
                }

                switch (role)
                {
                    case "Klient":
                    case "1":
                        RegistrationData registrationCustomerData = new RegistrationData(username, password, email, id, role);
                        RegisterCustomerServices registerCustomerServices = new RegisterCustomerServices(registrationCustomerData);
                        registerCustomerServices.RegisterCustomer();
                        LogToFileMessage.LogSuccess($"Zarejestrowano klienta o nazwie użytkownika: {username}", "Registration.Register");
                        Console.WriteLine("Rejestracja zakończona pomyślnie.");
                        break;
                    case "Dostawca":
                    case "2":
                        RegistrationData registrationSupplierData = new RegistrationData(username, password, email, id, role);
                        RegisterSupplierServices registerSupplierServices = new RegisterSupplierServices(registrationSupplierData);
                        registerSupplierServices.RegisterSupplier();
                        LogToFileMessage.LogSuccess($"Zarejestrowano dostawcę o nazwie użytkownika: {username}", "Registration.Register");
                        Console.WriteLine("Rejestracja zakończona pomyślnie.");
                        break;
                    case "Administrator":
                    case "3":
                        RegistrationData registrationAdminData = new RegistrationData(username, password, email, id, role);
                        RegisterAdminServices registerAdminServices = new RegisterAdminServices(registrationAdminData);
                        registerAdminServices.RegisterAdmin();
                        LogToFileMessage.LogSuccess($"Zarejestrowano administratora o nazwie użytkownika: {username}", "Registration.Register");
                        Console.WriteLine("Rejestracja zakończona pomyślnie.");
                        break;
                    default:
                        if (!registrationCancelled)
                        {
                            Console.WriteLine("Nieprawidłowa Rola");
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas rejestracji: {ex.Message}");
                LogToFileMessage.LogError($"Błąd rejestracji: {ex.Message}", "Registration.Register");
            }
        }
    }
}
