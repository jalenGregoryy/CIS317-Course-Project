using System;
using System.Collections.Generic;

namespace GameStoreApp
{
    public class Stock
    {
        public List<Game> GameList { get; set; }

        public Stock()
        {
            GameList = new List<Game>();
        }

        // Adds a game to the stock
        public void Add_game(Game game)
        {
            GameList.Add(game);
            Console.WriteLine($"{game.GameName} added to stock.");
        }

        // Updates stock by game id
        public void Update_stock(int gameId, int newStock)
        {
            Game game = Get_game_by_id(gameId);
            if (game != null)
            {
                game.AmountInStock = newStock;
                Console.WriteLine($"Stock updated for {game.GameName}. New stock: {newStock}");
            }
            else
            {
                Console.WriteLine("Game not found in stock.");
            }
        }

        // Deletes a game from the stock by game ID
        public void Delete_game(int gameId)
        {
            Game game = Get_game_by_id(gameId);
            if (game != null)
            {
                GameList.Remove(game);
                Console.WriteLine($"Game {game.GameName} has been deleted from stock.");
            }
            else
            {
                Console.WriteLine("Game not found in stock.");
            }
        }

        // Updates various properties of a game (such as price, genre)
        public void Update_game(int gameId, string name = null, string genre = null, float? price = null)
        {
            Game game = Get_game_by_id(gameId);
            if (game != null)
            {
                if (name != null) game.GameName = name;
                if (genre != null) game.GameGenre = genre;
                if (price.HasValue) game.GamePrice = price.Value;

                Console.WriteLine($"Game {game.GameName} updated successfully.");
            }
            else
            {
                Console.WriteLine("Game not found in stock.");
            }
        }

        // Retrieves the game by id
        public Game Get_game_by_id(int gameId)
        {
            return GameList.Find(g => g.GameId == gameId);
        }

        // Lists available games
        public void Display_games()
        {
            Console.WriteLine("Available Games in Stock:");
            foreach (var game in GameList)
            {
                Console.WriteLine(game);
            }
        }
    }
}
