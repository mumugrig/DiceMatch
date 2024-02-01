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
        static void PrintAll(GameTable gameTable)
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            PrintGrid(gameTable.player1.Board);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            PrintGrid(gameTable.player2.Board);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }
        static void UseAbility(GameTable gameTable)
        {
            if (!gameTable.CurrentPlayer.Character.OnCooldown) 
            {
                Console.Write("Will you place die/0/ or use ability/1/: ");
                bool check = int.Parse(Console.ReadLine()) == 1 ? true : false;
                if (check)
                {
                    Console.Write("Input board, row and column of target: ");
                    int[] target = Console.ReadLine().Split().Select(int.Parse).ToArray();
                    gameTable.UseAbility(target);
                }
            }
            else
            {
                Console.WriteLine("Ability Cooldown - {0}", gameTable.CurrentPlayer.Character.Cooldown);
            }
        }
        
        static void PlacementInput(GameTable gameTable)
        {
            Console.Write("Input row and column: ");
            int[] input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            gameTable.Place(input[0], input[1]);
        }
        static void GameTest()
        {
            GameTable gameTable = new GameTable();
            gameTable.player1.Name = "Player1";
            gameTable.player1.Character = new Ash();
            gameTable.player2.Name = "Player2";
            gameTable.player2.Character = new Ash();

            while (!gameTable.IsBoardFull())
            {
                PrintAll(gameTable);
                gameTable.Roll();
                Console.WriteLine("Player1 rolled {0}", gameTable.Die);
                UseAbility(gameTable);
                PlacementInput(gameTable);
                Console.WriteLine("Player1 score - {0}", gameTable.player1.Score);
                if (gameTable.IsBoardFull()) break;
                PrintAll(gameTable);
                gameTable.Roll();
                Console.WriteLine("Player2 rolled {0}", gameTable.Die);
                UseAbility(gameTable);
                PlacementInput(gameTable);
                Console.WriteLine("Player2 score - {0}", gameTable.player2.Score);
            }
            Player winner = gameTable.GetWinner();
            Console.WriteLine("{0} won!!!", winner.Name);
        }

        static void AshAbilityTest()
        {
            GameTable gameTable = new GameTable();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    gameTable.player2.Board[x,y] = x+1;
                }
            }
            Ash ash = new Ash();
            ash.Ability(gameTable, new int[] { 1, 1, 0 });
            PrintGrid(gameTable.player2.Board);
        }
        static void Main(string[] args)
        {
            GameTest();
        }
    }
}