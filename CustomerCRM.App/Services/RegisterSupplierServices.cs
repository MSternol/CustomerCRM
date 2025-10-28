using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.App.Helpers;
using CustomerCRM.App.Registration;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.SaveFileToPath;

namespace CustomerCRM.App.Services
{
    public class RegisterSupplierServices
    {
        private bool registrationCancelled;
        private readonly RegistrationData registrationData;
        public RegisterSupplierServices(RegistrationData Data)
        {
            registrationData = Data;
            registrationData.ID = IDGenerator.GenerateUniqueID();
        }

        public void RegisterSupplier()
        {
            List<ModelSupplier> modelRegisterSuppliers = new List<ModelSupplier>();
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

            string phoneNumber;
            do
            {
                Console.Write("Podaj numer telefonu: ");
                phoneNumber = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidatePhoneNumber(phoneNumber));

            string business;
            do
            {
                Console.Write("Podaj Firme: ");
                business = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.ValidateText(business));

            ModelSupplier supplier = new ModelSupplier(
                registrationData.Username,
                registrationData.Password,
                firstName,
                lastName,
                business,
                registrationData.Role,
                phoneNumber,
                registrationData.Email,
                registrationData.ID
                );
            modelRegisterSuppliers.Add(supplier);
            Console.WriteLine("Rejestracja Zakończona!");
            SavingService.SaveSupplier(supplier);
        }

    }
}
