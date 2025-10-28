using CustomerCRM.Domain.Models;
using CustomerCRM.Domain.Services.Warehouse.Abstract;
using DataStorage;
using CustomerCRM.Domain.Services.Warehouse;

namespace CustomerCRM.App.Customer
{
    public class ShoppingCart
    {
        private List<CartItem> cartItems = new List<CartItem>();
        private string shoppingCartFilePath;
        private ModelCustomer userLogin;

        public List<ProductBase> Products => cartItems.ConvertAll(item => item.Product);
        public int nextProductId { get; set; }

        public ShoppingCart(ModelCustomer userLogin)
        {
            this.userLogin = userLogin;
            shoppingCartFilePath = FileLocations.GetShoppingCartFilePath();
            LoadFromFile();
        }

        public void AddToCart(ProductBase product, int quantity)
        {
            var existingCartItem = cartItems.Find(item => item.Product.Id == product.Id);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                cartItems.Add(new CartItem { Product = product, Quantity = quantity });
            }

            Console.WriteLine("Produkt dodany do koszyka!");
            SaveToFile();
        }

        public void RemoveFromCart(ProductBase product, int quantity)
        {
            var existingCartItem = cartItems.Find(item => item.Product.Id == product.Id);

            if (existingCartItem != null)
            {
                if (existingCartItem.Quantity > quantity)
                {
                    existingCartItem.Quantity -= quantity;
                }
                else
                {
                    cartItems.Remove(existingCartItem);
                }

                SaveToFile();
            }
        }

        public void DisplayCart()
        {
            foreach (var cartItem in cartItems)
            {
                var product = cartItem.Product;
                var quantity = cartItem.Quantity;
                var totalPrice = quantity * product.Price;

                Console.WriteLine($"Nazwa: {product.Name}, Cena: {product.Price}, Ilość: {quantity}, Całkowita cena: {totalPrice}");
            }
        }

        public List<CartItem> GetCartItems()
        {
            return cartItems;
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (var cartItem in cartItems)
            {
                totalPrice += cartItem.Product.Price * cartItem.Quantity;
            }
            return totalPrice;
        }

        public void SaveToFile()
        {
            try
            {
                string[] lines = cartItems.Select(cartItem => $"{cartItem.Product.Id}|{cartItem.Product.Name}|{cartItem.Product.Category}|{cartItem.Quantity}|{cartItem.Product.Price}").ToArray();
                File.WriteAllLines(shoppingCartFilePath, lines);
                Console.WriteLine("Zapisano koszyk do pliku.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu koszyka do pliku: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            if (File.Exists(shoppingCartFilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(shoppingCartFilePath);
                    cartItems = lines.Select(line =>
                    {
                        string[] parts = line.Split('|');
                        if (parts.Length == 5 && int.TryParse(parts[0], out int productId) && decimal.TryParse(parts[4], out decimal price))
                        {
                            string name = parts[1];
                            string category = parts[2];
                            int quantity = int.Parse(parts[3]);

                            // Wczytaj produkt i ilość i dodaj do koszyka
                            ProductBase product = CreateProduct(productId, name, category, quantity, price);
                            return new CartItem { Product = product, Quantity = quantity };
                        }
                        return null;
                    }).Where(item => item != null).ToList();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Błąd podczas wczytywania koszyka z pliku: {ex.Message}");
                }
            }
        }

        public ProductBase CreateProduct(int id, string name, string category, int quantity, decimal price)
        {
            switch (category.ToLower())
            {
                case "owoce":
                    return new FruitProduct
                    {
                        Id = id,
                        Name = name,
                        Category = category,
                        Quantity = quantity,
                        Price = price
                    };

                case "warzywa":
                    return new VegetableProduct
                    {
                        Id = id,
                        Name = name,
                        Category = category,
                        Quantity = quantity,
                        Price = price
                    };

                case "elektronika":
                    return new ElectronicProduct
                    {
                        Id = id,
                        Name = name,
                        Category = category,
                        Quantity = quantity,
                        Price = price
                    };

                case "chemia":
                    return new ChemicalProduct
                    {
                        Id = id,
                        Name = name,
                        Category = category,
                        Quantity = quantity,
                        Price = price
                    };

                default:
                    throw new ArgumentException("Nieprawidłowa kategoria produktu.");
            }
        }
    }
}
