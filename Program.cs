using System;

namespace GameStoreApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Stock stock = new Stock();
            ShoppingCart cart = new ShoppingCart();
            InitializeStock(stock); 

            while (true)
            {
                Console.WriteLine("\nWelcome to my game store!");
                Console.WriteLine("1. View Games in Stock");
                Console.WriteLine("2. Add Game to Stock");
                Console.WriteLine("3. Edit Game in Stock");
                Console.WriteLine("4. Delete Game from Stock");
                Console.WriteLine("5. Add Game to Cart");
                Console.WriteLine("6. Remove Game from Cart");
                Console.WriteLine("7. View Cart");
                Console.WriteLine("8. Checkout");
                Console.WriteLine("9. Exit");
                Console.Write("Choose an option (1-9): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            stock.Display_games();
                            break;
                        case 2:
                            AddGameToStock(stock);
                            break;
                        case 3:
                            EditGameInStock(stock);
                            break;
                        case 4:
                            DeleteGameFromStock(stock);
                            break;
                        case 5:
                            AddGameToCart(cart, stock);
                            break;
                        case 6:
                            RemoveGameFromCart(cart);
                            break;
                        case 7:
                            cart.Display_cart();
                            break;
                        case 8:
                            Checkout(cart);
                            break;
                        case 9:
                            Console.WriteLine("Thank you for visiting the Game Store!");
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 9.");
                }
            }
        }

        private static void InitializeStock(Stock stock)
        {
            // Games ins tock
            stock.Add_game(new Game(1, "Elden Ring", "Action/Adventure, Souls-like", 59.99f, 10));
            stock.Add_game(new Game(2, "Forza Horizon 5", "Racing", 39.99f, 5));
            stock.Add_game(new Game(3, "Minecraft", "Sandbox, Survival", 29.99f, 20));
            stock.Add_game(new Game(4, "Rainbow Six Siege", "Tactical Shooter", 19.99f, 15));
            stock.Add_game(new Game(5, "Red Dead Redemption 2", "Action/Adventure", 49.99f, 8));
            stock.Add_game(new Game(6, "Baldur's Gate 3", "Role-Playing", 59.99f, 12));
            stock.Add_game(new Game(7, "GTA V", "Action/Adventure", 29.99f, 20));
            stock.Add_game(new Game(8, "Black Myth: Wukong", "Action/Adventure", 59.99f, 5));
            stock.Add_game(new Game(9, "Rocket League", "Sports", 19.99f, 25));
        }

        private static void AddGameToStock(Stock stock)
        {
            Console.Write("Enter Game ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter Game Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Game Genre: ");
            string genre = Console.ReadLine();
            Console.Write("Enter Game Price: ");
            float price = float.Parse(Console.ReadLine());
            Console.Write("Enter Amount in Stock: ");
            int stockAmount = int.Parse(Console.ReadLine());

            try
            {
                Game newGame = new Game(id, name, genre, price, stockAmount);
                stock.Add_game(newGame);
                Console.WriteLine($"{name} has been added to stock.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void EditGameInStock(Stock stock)
        {
            Console.Write("Enter Game ID to edit: ");
            int id = int.Parse(Console.ReadLine());
            Game gameToEdit = stock.Get_game_by_id(id);
            if (gameToEdit != null)
            {
                Console.Write("Enter new Game Name (or press Enter to keep current): ");
                string newName = Console.ReadLine();
                Console.Write("Enter new Game Genre (or press Enter to keep current): ");
                string newGenre = Console.ReadLine();
                Console.Write("Enter new Game Price (or press Enter to keep current): ");
                string priceInput = Console.ReadLine();
                float? newPrice = string.IsNullOrEmpty(priceInput) ? (float?)null : float.Parse(priceInput);

                stock.Update_game(id, newName, newGenre, newPrice);
            }
            else
            {
                Console.WriteLine("Game not found.");
            }
        }

        private static void DeleteGameFromStock(Stock stock)
        {
            Console.Write("Enter Game ID to delete: ");
            int id = int.Parse(Console.ReadLine());
            stock.Delete_game(id);
        }

        private static void AddGameToCart(ShoppingCart cart, Stock stock)
        {
            Console.Write("Enter Game ID to add to cart: ");
            int id = int.Parse(Console.ReadLine());
            Game gameToAdd = stock.Get_game_by_id(id);
            if (gameToAdd != null)
            {
                Console.Write("Enter quantity: ");
                int quantity = int.Parse(Console.ReadLine());
                cart.Add_item(gameToAdd, quantity);
            }
            else
            {
                Console.WriteLine("Game not found.");
            }
        }

        private static void RemoveGameFromCart(ShoppingCart cart)
        {
            Console.Write("Enter Game ID to remove from cart: ");
            int id = int.Parse(Console.ReadLine());
            Game gameToRemove = cart.CartItems.Keys.FirstOrDefault(g => g.GameId == id);
            if (gameToRemove != null)
            {
                Console.Write("Enter quantity to remove: ");
                int quantity = int.Parse(Console.ReadLine());
                cart.Remove_item(gameToRemove, quantity);
            }
            else
            {
                Console.WriteLine("Game not found in cart.");
            }
        }

        private static void Checkout(ShoppingCart cart)
        {
            if (cart.CartItems.Count == 0)
            {
                Console.WriteLine("Your cart is empty. Please add games before checking out.");
                return;
            }

            Console.WriteLine("Checking out the following items:");
            cart.Display_cart();
            Console.WriteLine("Thank you for your purchase!");
            cart.CartItems.Clear(); // Clears cart
        }
    }
}
