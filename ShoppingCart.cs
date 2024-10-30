using System;
using System.Collections.Generic;

namespace GameStoreApp
{
    public class ShoppingCart
    {
        public Dictionary<Game, int> CartItems { get; set; } 

        public ShoppingCart()
        {
            CartItems = new Dictionary<Game, int>();
        }

        // Adds a game to the cart
        public void Add_item(Game game, int quantity)
        {
            if (quantity <= 0)
            {
                Console.WriteLine("Quantity must be greater than zero.");
                return;
            }

            // Check if the game is available in stock
            if (!game.IsAvailable())
            {
                Console.WriteLine($"{game.GameName} is out of stock.");
                return;
            }

            // Checks if the quantity the user wants is available
            if (quantity > game.AmountInStock)
            {
                Console.WriteLine($"Only {game.AmountInStock} of {game.GameName} available. Adding {game.AmountInStock} to the cart.");
                quantity = game.AmountInStock;
            }

            // Add the game to the cart or increase its quantity if already in the cart
            if (CartItems.ContainsKey(game))
            {
                CartItems[game] += quantity;
            }
            else
            {
                CartItems.Add(game, quantity);
            }

            //
            game.AmountInStock -= quantity;

            Console.WriteLine($"{quantity} copies of {game.GameName} added to the cart.");
        }

        // Removes a game from the cart and updates its stock
        public void Remove_item(Game game, int quantity)
        {
            if (CartItems.ContainsKey(game))
            {
                if (quantity >= CartItems[game])
                {
                    // Remove the game entirely if quantity to remove is greater or equal to what's in the cart
                    game.AmountInStock += CartItems[game];
                    CartItems.Remove(game);
                    Console.WriteLine($"{game.GameName} removed from the cart.");
                }
                else
                {
                    // Reduce the quantity in the cart
                    CartItems[game] -= quantity;
                    game.AmountInStock += quantity;
                    Console.WriteLine($"{quantity} copies of {game.GameName} removed from the cart.");
                }
            }
            else
            {
                Console.WriteLine($"{game.GameName} is not in the cart.");
            }
        }

        // Calculates the total price
        public float Calculate_total()
        {
            float total = 0;
            foreach (var item in CartItems)
            {
                total += item.Key.GamePrice * item.Value; // Multiply game price by quantity
            }
            return total;
        }

        // Lists games in cart
        public void Display_cart()
        {
            if (CartItems.Count == 0)
            {
                Console.WriteLine("Your cart is empty.");
                return;
            }

            Console.WriteLine("Items in your cart:");
            foreach (var item in CartItems)
            {
                Console.WriteLine($"{item.Key.GameName} - {item.Value} copies @ ${item.Key.GamePrice} each");
            }

            Console.WriteLine($"Total: ${Calculate_total():F2}");
        }
    }
}
