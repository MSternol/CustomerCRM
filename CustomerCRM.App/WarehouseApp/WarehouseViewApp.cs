using CustomerCRM.App.Customer;
using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.Warehouse;
using CustomerCRM.Domain.Services.Warehouse.Abstract;
using DataStorage.LoggApp;
using System;
using CustomerCRM.Domain.Services.Warehouse.Interface;

namespace CustomerCRM.App.WarehouseApp
{
    public class WarehouseViewApp
    {
        private readonly WarehouseService warehouse = new WarehouseService();
        private readonly ModelCustomer user;
        private readonly ShoppingCart shoppingCart;
        private bool isRunning = true;

        public WarehouseViewApp(ModelCustomer user)
        {
            this.user = user;
            shoppingCart = new ShoppingCart(user);
        }


        public void WarehouseView()
        {
            while (isRunning)
            {
                DisplayMenuOptions();
                string choice = Console.ReadLine();
                ProcessUserChoice(choice);
            }
        }

        private void DisplayMenuOptions()
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Dodaj produkt");
            Console.WriteLine("2. Usuń produkt");
            Console.WriteLine("3. Zmodyfikuj ilość produktu");
            Console.WriteLine("4. Zmodyfikuj cenę produktu");
            Console.WriteLine("5. Wyszukaj produkt");
            Console.WriteLine("6. Wyświetl pełną listę produktów");
            Console.WriteLine("7. Wyświetl koszyk");
            Console.WriteLine("0. Wyjdź");
        }

        private void ProcessUserChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    AddProduct();
                    break;

                case "2":
                    Console.Clear();
                    RemoveProduct();
                    break;

                case "3":
                    Console.Clear();
                    ModifyQuantityProduct();
                    break;

                case "4":
                    Console.Clear();
                    ModifyPriceProduct();
                    break;

                case "5":
                    Console.Clear();
                    SearchProduct();
                    break;

                case "6":
                    Console.Clear();
                    DisplayListProduct();
                    break;

                case "7":
                    Console.Clear();
                    DisplayShoppingCart();
                    break;

                case "0":
                    Console.Clear();
                    isRunning = false;
                    break;

                default:
                    Console.WriteLine("Nieprawidłowy wybór!");
                    break;
            }
        }

        public void AddProduct()
        {
            Console.Write("Nazwa produktu: ");
            string name = Console.ReadLine();
            Console.Write("Kategoria (Owoce, Warzywa, Elektronika, Chemia): ");
            string category = Console.ReadLine();
            Console.Write("Ilość: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Cena: ");
            decimal price = decimal.Parse(Console.ReadLine());

            ProductBase newProduct = (ProductBase)warehouse.CreateProduct(0, name, category, quantity, price);
            if (newProduct != null)
            {
                warehouse.AddProduct(newProduct);
                Console.WriteLine("Produkt dodany!");

                LogToFileMessage.LogSuccess("Dodano nowy produkt: " + name, "WarehouseViewApp - AddProduct");
            }
            else
            {
                Console.WriteLine("Nieprawidłowa kategoria produktu.");

                LogToFileMessage.LogError("Nie udało się dodać nowego produktu: " + name, "WarehouseViewApp - AddProduct");
            }
        }

        public void RemoveProduct()
        {
            Console.Write("ID produktu do usunięcia: ");
            int productIdToRemove = int.Parse(Console.ReadLine());
            warehouse.RemoveProduct(productIdToRemove);
            Console.WriteLine("Produkt usunięty!");

            LogToFileMessage.LogSuccess("Usunięto produkt o ID: " + productIdToRemove, "WarehouseViewApp - RemoveProduct");
        }

        public void ModifyQuantityProduct()
        {
            Console.Write("ID produktu do zmodyfikowania ilości: ");
            int productIdToModifyQuantity = int.Parse(Console.ReadLine());
            Console.Write("Nowa ilość: ");
            int newQuantity = int.Parse(Console.ReadLine());
            warehouse.ModifyProductQuantity(productIdToModifyQuantity, newQuantity);
            Console.WriteLine("Ilość produktu zmodyfikowana!");

            LogToFileMessage.LogSuccess("Zmieniono ilość produktu o ID: " + productIdToModifyQuantity, "WarehouseViewApp - ModifyQuantityProduct");
        }

        public void ModifyPriceProduct()
        {
            Console.Write("ID produktu do zmodyfikowania ceny: ");
            int productIdToModifyPrice = int.Parse(Console.ReadLine());
            Console.Write("Nowa cena: ");
            decimal newPrice = decimal.Parse(Console.ReadLine());
            warehouse.ModifyProductPrice(productIdToModifyPrice, newPrice);
            Console.WriteLine("Cena produktu zmodyfikowana!");

            LogToFileMessage.LogSuccess("Zmieniono cenę produktu o ID: " + productIdToModifyPrice, "WarehouseViewApp - ModifyPriceProduct");
        }

        public void SearchProduct()
        {
            Console.WriteLine("Wyszukiwanie produktu:");
            Console.WriteLine("1. Wyszukaj po ID");
            Console.WriteLine("2. Wyszukaj po nazwie");
            Console.WriteLine("3. Wyszukaj po kategorii");
            Console.WriteLine("4. Powrót");

            string searchChoice = Console.ReadLine();

            switch (searchChoice)
            {
                case "1":
                    Console.Write("Podaj ID produktu: ");
                    int productId = int.Parse(Console.ReadLine());
                    ProductBase foundProductById = (ProductBase)warehouse.GetProductById(productId);
                    if (foundProductById != null)
                    {
                        Console.WriteLine("Znaleziony produkt:");
                        Console.WriteLine(foundProductById.GetProductInfo());
                        LogToFileMessage.LogSuccess("Znaleziono produkt o ID: " + productId, "WarehouseViewApp - SearchProduct");
                    }
                    else
                    {
                        Console.WriteLine("Produkt o podanym ID nie został znaleziony.");

                        LogToFileMessage.LogError("Nie znaleziono produktu o ID: " + productId, "WarehouseViewApp - SearchProduct");
                    }
                    break;

                case "2":
                    Console.Write("Podaj nazwę produktu: ");
                    string productName = Console.ReadLine();
                    var foundProductsByName = warehouse.GetProductsByName(productName);
                    if (foundProductsByName.Count > 0)
                    {
                        Console.WriteLine("Znalezione produkty:");
                        foreach (var product in foundProductsByName)
                        {
                            Console.WriteLine(product.GetProductInfo());
                        }

                        LogToFileMessage.LogSuccess("Znaleziono produkty o nazwie: " + productName, "WarehouseViewApp - SearchProduct");
                    }
                    else
                    {
                        Console.WriteLine("Produkty o podanej nazwie nie zostały znalezione.");

                        LogToFileMessage.LogError("Nie znaleziono produktów o nazwie: " + productName, "WarehouseViewApp - SearchProduct");
                    }
                    break;

                case "3":
                    Console.Write("Podaj kategorię produktu: ");
                    string productCategory = Console.ReadLine();
                    var foundProductsByCategory = warehouse.GetProductsByCategory(productCategory);
                    if (foundProductsByCategory.Count > 0)
                    {
                        Console.WriteLine("Znalezione produkty:");
                        foreach (var product in foundProductsByCategory)
                        {
                            Console.WriteLine(product.GetProductInfo());
                        }

                        LogToFileMessage.LogSuccess("Znaleziono produkty w kategorii: " + productCategory, "WarehouseViewApp - SearchProduct");
                    }
                    else
                    {
                        Console.WriteLine("Produkty o podanej kategorii nie zostały znalezione.");

                        LogToFileMessage.LogError("Nie znaleziono produktów w kategorii: " + productCategory, "WarehouseViewApp - SearchProduct");
                    }
                    break;

                case "4":
                    break;

                default:
                    Console.WriteLine("Nieprawidłowy wybór!");
                    break;
            }
        }

        public void DisplayListProduct()
        {
            warehouse.ListProductsDisplay();

            Console.WriteLine("Wybierz produkt do dodania do koszyka (podaj numer lub 0 aby wrócić):");
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int selectedProductIndex))
            {
                if (selectedProductIndex == 0)
                {
                    return;
                }

                if (selectedProductIndex >= 1 && selectedProductIndex <= warehouse.Products.Count)
                {
                    ProductBase selectedProduct = (ProductBase)warehouse.Products[selectedProductIndex - 1];
                    AddProductToCart(selectedProduct);
                }
                else
                {
                    Console.WriteLine("Nieprawidłowy wybór!");
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy wybór!");
            }
        }

        public void AddProductToCart(ProductBase product)
        {
            while (true)
            {
                if (product != null)
                {
                    Console.WriteLine($"Czy chcesz dodać produkt '{product.Name}' do koszyka? (T/N): ");
                    string addToCartChoice = Console.ReadLine();
                    if (addToCartChoice.ToUpper() == "T")
                    {
                        Console.Write("Podaj ilość produktu: ");
                        int quantityToAdd;
                        if (int.TryParse(Console.ReadLine(), out quantityToAdd))
                        {
                            if (quantityToAdd <= product.Quantity)
                            {
                                shoppingCart.AddToCart(product, quantityToAdd);
                                Console.WriteLine($"Dodano {quantityToAdd} sztuk produktu '{product.Name}' do koszyka!");
                                shoppingCart.DisplayCart();
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Nieprawidłowa ilość produktu. Dostępna ilość: {product.Quantity}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Podano nieprawidłową ilość produktu.");
                        }
                    }
                    else if (addToCartChoice.ToUpper() == "N")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Nieprawidłowa odpowiedź. Wpisz 'T' lub 'N'.");
                    }
                }
                else
                {
                    Console.WriteLine("Podano nieprawidłowy produkt.");
                    break;
                }
            }
        }

        public void DisplayShoppingCart()
        {
            Console.WriteLine("Zawartość koszyka:");
            shoppingCart.DisplayCart();
            Console.WriteLine($"Łączna cena koszyka: {shoppingCart.GetTotalPrice()} zł");

        }

    }
}
