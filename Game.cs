using System;

namespace GameStoreApp
{
    public class Game
    {
        private float gamePrice;
        private int amountInStock;

        public int GameId { get; set; }
        public string GameName { get; set; }
        public string GameGenre { get; set; }

        public float GamePrice
        {
            get { return gamePrice; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Game price cannot be negative.");
                }
                gamePrice = value;
            }
        }

        public int AmountInStock
        {
            get { return amountInStock; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Amount in stock cannot be negative.");
                }
                amountInStock = value;
            }
        }

        // Constructor
        public Game(int id, string name, string genre, float price, int stock)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Game name cannot be empty.");

            if (string.IsNullOrEmpty(genre))
                throw new ArgumentException("Game genre cannot be empty.");

            GameId = id;
            GameName = name;
            GameGenre = genre;
            GamePrice = price; 
            AmountInStock = stock; 
        }

        // Override information displayed
        public override string ToString()
        {
            return $"{GameName} ({GameGenre}) - ${GamePrice:F2} | In Stock: {AmountInStock}";
        }

        // Check if the game is available in stock
        public bool IsAvailable()
        {
            return AmountInStock > 0;
        }
    }
}
