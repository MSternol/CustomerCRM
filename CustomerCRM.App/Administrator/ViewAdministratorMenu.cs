using DataStorage.Raport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.Domain.Models;

namespace CustomerCRM.App.Administrator
{
    public class ViewAdministratorMenu
    {
        private ModelCustomer user;
        private bool isRunning = true;

        public bool ViewMenu(RegistrationData registrationData)
        {
            while (isRunning)
            {
                Console.WriteLine("1.Zarządzanie Klientami");
                Console.WriteLine("2.Zarządzanie Dostawcami");
                Console.WriteLine("3.Zarządzanie Magazynem");
                Console.WriteLine("4.Raport");
                Console.WriteLine("0. Wyjście");
                var operation = Console.ReadLine();
                isRunning = ViewAdminMenu(operation);
            }

            return isRunning;
        }

        private bool ViewAdminMenu(string operation)
        {
            ReportGenerator reportGenerator = new ReportGenerator();
            switch (operation)
            {
                case "1":
                    Console.Clear();
                    ManageCusotmersMenu manageCusotmersMenu = new ManageCusotmersMenu();
                    manageCusotmersMenu.ViewManageCustomers();
                    break;

                case "2":
                    Console.Clear();
                    ManageSuppliersMenu manageSuppliersMenu = new ManageSuppliersMenu();
                    manageSuppliersMenu.ViewManageSuppliers();
                    break;

                case "3":
                    Console.Clear();
                    WarehouseApp.WarehouseViewApp warehouse = new WarehouseApp.WarehouseViewApp(user);
                    warehouse.WarehouseView();
                    break;

                case "4":
                    Console.Clear();
                    reportGenerator.GenerateReport();
                    break;

                case "0":
                    Console.Clear();
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie!!");
                    break;
            }

            return isRunning;
        }
    }
}
