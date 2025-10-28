using CustomerCRM.App.Helpers;
using CustomerCRM.App.Registration;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.SaveFileToPath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerCRM.App.Services
{
    public class RegisterAdminServices
    {
        private bool registrationCancelled;
        private readonly RegistrationData registrationData;

        public RegisterAdminServices(RegistrationData data)
        {
            registrationData = data;
            registrationData.ID = IDGenerator.GenerateUniqueID();
        }

        public void RegisterAdmin()
        {
            List<ModelAdministrator> modelRegisterAdminList = new List<ModelAdministrator>();

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

            string position;
            do
            {
                Console.WriteLine("Administrator/Kierownik/Manager/Asystent/Specjalista");
                Console.Write("Podaj stanowisko: ");
                position = CheckForEscKey.ReadInput(ref registrationCancelled);
                if (registrationCancelled)
                    return;
            } while (!ValidationHelper.IsPositionValid(position));

            ModelAdministrator admin = new ModelAdministrator(
                registrationData.Username,
                registrationData.Password,
                firstName,
                lastName,
                registrationData.Email,
                position,
                registrationData.Role,
                registrationData.ID
                );


            modelRegisterAdminList.Add(admin);

            Console.WriteLine("Rejestracja Zakończona!");

            SavingService.SaveAdmin(admin);
        }
    }
}
