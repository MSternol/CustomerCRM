using CustomerCRM.App.Helpers;
using CustomerCRM.App.LoginManagement;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.SaveFileToPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.Services
{
    public class RegisterCustomerServices
    {
        private bool registrationCancelled;
        private readonly RegistrationData registrationData;

        public RegisterCustomerServices(RegistrationData data)
        {
            registrationData = data;
        }
        public void RegisterCustomer()
        {
            List<ModelCustomer> modelRegisterCustomerList = new List<ModelCustomer>();
            string firstName;
            do
            {
                Console.Write("Podaj Imie: ");
                firstName = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidateName(firstName));

            string lastName;
            do
            {
                Console.Write("Podaj Nazwisko: ");
                lastName = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidateName(lastName));
            string country;
            do
            {
                Console.Write("Podaj Państwo: ");
                country = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidateText(country));

            string city;
            do
            {
                Console.Write("Podaj Miasto: ");
                city = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidateText(city));

            string adress;
            do
            {
                Console.WriteLine("Ulica,Numer Domu i Mieszkania - Przykład 44/1");
                Console.Write("Podaj Adres: ");
                adress = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidateAddress(adress));

            string postalCode;
            do
            {
                Console.WriteLine("Przykładowy Kod pocztowy - 12-002");
                Console.Write("Podaj kod pocztowy: ");
                postalCode = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidatePostalCode(postalCode));
            string phoneNumber;
            do
            {
                Console.WriteLine("Przykładowy telefon 454-343-334");
                Console.Write("Podaj Numer telefonu: ");
                phoneNumber = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidatePhoneNumber(phoneNumber));

            ModelCustomer customer = new ModelCustomer(
                registrationData.Username,
                registrationData.Password,
                firstName,
                lastName,
                country,
                registrationData.Role,
                city,
                adress,
                postalCode,
                phoneNumber,
                registrationData.Email,
                registrationData.ID
                );
            modelRegisterCustomerList.Add(customer);
            Console.WriteLine("Rejestracja Zakończona !");
            string expectedPosition = null;
            SavingService.SaveCustomer(customer);
            LoginSystem loginSystem = new LoginSystem();
            loginSystem.Logging();





        }
    }
}
