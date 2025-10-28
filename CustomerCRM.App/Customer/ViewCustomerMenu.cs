using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerCRM.App.WarehouseApp;
using CustomerCRM.Domain.Models;
using CustomerCRM.App.WarehouseApp;

namespace CustomerCRM.App.Customer
{
    public class ViewCustomerMenu
    {
        private string userLogin;
        private WarehouseViewApp wva;
        private ViewChangePersonal settingsCustomer;
        private ShoppingCart cart;
        private ModelCustomer user;
        private bool isRunning = true;

        public ViewCustomerMenu(ModelCustomer user)
        {
            this.user = user;
            wva = new WarehouseViewApp(user);
            settingsCustomer = new ViewChangePersonal();
            cart = new ShoppingCart(user);
        }

        public bool ViewMenu(RegistrationData registrationData)
        {
            while (isRunning)
            {
                Console.WriteLine("1.Dostępne Produkty");
                Console.WriteLine("2.Wyszukaj Produkt");
                Console.WriteLine("3.Koszyk");
                Console.WriteLine("4.Ustawienia");
                Console.WriteLine("0.Wyjście");
                var operation = Console.ReadLine();
                ViewMenuCustomer(operation);
                if (operation == "0")
                {
                    isRunning = false;
                }
            }

            return isRunning;
        }

        private void ViewMenuCustomer(string operation)
        {
            switch (operation)
            {
                case "1":
                    Console.Clear();
                    wva.DisplayListProduct();
                    break;

                case "2":
                    Console.Clear();
                    wva.SearchProduct();
                    break;

                case "3":
                    Console.Clear();
                    cart.DisplayCart();
                    break;

                case "4":
                    Console.Clear();
                    settingsCustomer.ViewChangePersonalData();
                    break;

                case "0":
                    Console.Clear();
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Nieprawidłowa operacja! Spróbuj ponownie!!");
                    break;
            }
        }
    }
}
