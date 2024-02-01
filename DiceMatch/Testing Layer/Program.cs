using BusinessLayer;
using System.Threading.Channels;
using System;
using DataLayer;
using FireSharp.Response;
using Newtonsoft.Json;
using ServiceLayer;

namespace Testing_Layer
{
    internal class Program
    {
        static void CreateTest()
        {
            DiceMatchDbContext db = new DiceMatchDbContext();
            CharacterContext gameContext = new CharacterContext(db);
            Character character = new Character(1, "ash",20,30,5);

            gameContext.Create(character);
            if (db.client != null)
            {
                Console.WriteLine("ti si");
            }
        }
        static void PrintGrid(int[,] grid)
        {
            for(int y = 0; y < 3; y++)
            {
                for(int x = 0; x<3; x++)
                {
                    Console.Write(grid[y, x] + " ");
                }
                Console.WriteLine();
            }
        }
        static void GameTest()
        {
            GameTable gameTable = new GameTable();
            gameTable.player1.Name = "Player1";
            gameTable.player2.Name = "Player2";

            while (!gameTable.IsBoardFull())
            {
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                PrintGrid(gameTable.player1.Board);
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                PrintGrid(gameTable.player2.Board);
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                gameTable.Roll();
                Console.WriteLine("Player1 rolled {0}", gameTable.Die);
                Console.Write("Input row and column: ");
                int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                gameTable.Place(input[0], input[1]);
                Console.WriteLine("Player1 score - {0}", gameTable.player1.Score);
                if (gameTable.IsBoardFull()) break;
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                PrintGrid(gameTable.player1.Board);
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                PrintGrid(gameTable.player2.Board);
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                gameTable.Roll();
                Console.WriteLine("Player2 rolled {0}", gameTable.Die);
                Console.Write("Input row and column: ");
                input = Console.ReadLine().Split().Select(int.Parse).ToArray();
                gameTable.Place(input[0], input[1]);
                Console.WriteLine("Player2 score - {0}", gameTable.player2.Score);
            }
            Player winner = gameTable.GetWinner();
            Console.WriteLine("{0} won!!!", winner.Name);
        }
        static void Main(string[] args)
        {
            GameTest();
        }
    }
}