using BusinessLayer;
using System.Threading.Channels;
using System;
using DataLayer;
using FireSharp.Response;
using Newtonsoft.Json;
using ServiceLayer;
using System.Text;

namespace Testing_Layer
{
    internal class Program
    {
        static async Task PrintLobbies()
        {
            List<Lobby> Lobbies = await ServerConnection.GetLobbiesAsync();
            try
            {
                foreach (Lobby lobby in Lobbies)
                {
                    Console.WriteLine($"{Lobbies.IndexOf(lobby)}. {lobby.User1.Username}\'s lobby");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("No lobbies available");
                
            }           
        }
        static async Task LobbyActions(User user)
        {
            Console.Clear();
            Console.WriteLine("Would you like to create/0/ or join/1/ a lobby: ");
            int input = int.Parse(Console.ReadLine());
            switch (input)
            {
                case 0:
                    Task.Run(() => ServerConnection.CreateLobbyAsync(user)).GetAwaiter().GetResult();
                    await WaitForPlayer();
                    break;
                case 1:
                    try
                    {
                        PrintLobbies();
                        Console.WriteLine("Enter lobby id: ");
                        int lobbyId = int.Parse(Console.ReadLine());
                        ServerConnection.JoinLobbyAsync(user, lobbyId);
                    }
                    catch
                    {
                        await LobbyActions(user);
                    }
                    
                    break;
            }
        }
        static async Task WaitForPlayer()
        {
            Player player2 = await ServerConnection.GetPlayersAsync(0);
            Console.WriteLine("Waiting for player...");
            while (player2 == null)
            {
                player2 = await ServerConnection.GetPlayersAsync(0);
            }
            Console.WriteLine($"{player2.Name} has joined");
        }
        static async Task<GameTable> StartGame()
        {
            Console.WriteLine("Press any key to start the game.");
            GameTable gameTable = await ServerConnection.GetGameAsync(0);
            while (gameTable == null)
            {
                gameTable = await ServerConnection.GetGameAsync(0);
            }
            Console.ReadKey();
            return gameTable;
        }
        static async Task HttpClientTest()
        {
            ServerConnection.Initiate();
            Console.WriteLine("Log in as Player1 or Player2(1/2):");
            int input = int.Parse(Console.ReadLine());
            UserContext userContext = ContextGenerator.GetUsersContext();
            User user = userContext.Read(input);
            await LobbyActions(user);           
            GameTable gameTable = await StartGame();
            while (!gameTable.IsBoardFull())
            {
                if (user.Username == gameTable.CurrentPlayer.Name)
                {                    
                    gameTable.Roll();
                    PrintAll(gameTable);
                    UseAbility(gameTable);
                    PlacementInput(gameTable);
                    PrintAll(gameTable);
                    Task.Run(() => ServerConnection.PostGameAsync(gameTable, 0)).GetAwaiter().GetResult();
                    if (gameTable.IsBoardFull()) break;
                    await Console.Out.WriteLineAsync("Waiting for other player...");
                }
                else
                {
                    gameTable = await ServerConnection.GetGameAsync(0);
                }
            }
            Player winner = gameTable.GetWinner();
            Console.WriteLine("{0} won!!!", winner.Name);
        }
        #region Game and DB tests
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

        static GameTable CreateGameTable()
        {
            UserContext userContext = ContextGenerator.GetUsersContext();
            GameTable gameTable = new GameTable(userContext.Read(1), userContext.Read(2));

            return gameTable;
        }

        static void UserCreateTest()
        {
            DiceMatchDbContext db = new DiceMatchDbContext();
            UserContext gameContext = new UserContext(db);
            CharacterContext characterContext = new CharacterContext(db);
            User user = new User();
            user.Id = 2;
            user.Username = "Player2";
            user.Character = characterContext.Read(1);


            gameContext.Create(user);
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
            Console.Clear();
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Player1 score - {0}", gameTable.player1.Score);
            Console.WriteLine(gameTable.player1.Character.Name);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            PrintGrid(gameTable.player1.Board);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Player2 score - {0}", gameTable.player2.Score);
            Console.WriteLine(gameTable.player2.Character.Name);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            PrintGrid(gameTable.player2.Board);
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("{1} rolled {0}", gameTable.Die, gameTable.CurrentPlayer.Name);
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
                    PrintAll(gameTable);
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

            GameTable gameTable = CreateGameTable();

            while (!gameTable.IsBoardFull())
            {       
                gameTable.Roll();
                PrintAll(gameTable);
                UseAbility(gameTable);
                PlacementInput(gameTable);    
                if (gameTable.IsBoardFull()) break;       
                gameTable.Roll();
                PrintAll(gameTable);
                UseAbility(gameTable);
                PlacementInput(gameTable);
                
            }
            Player winner = gameTable.GetWinner();
            Console.WriteLine("{0} won!!!", winner.Name);
        }

        static void AshAbilityTest()
        {

            GameTable gameTable = CreateGameTable();
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
        #endregion
        static void Main(string[] args)
        {
            Task.Run(() => HttpClientTest()).GetAwaiter().GetResult();
        }
    }
}