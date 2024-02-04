using BusinessLayer;
using Newtonsoft.Json;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_Layer
{
    internal static class ServerConnection
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        public static void Initiate()
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
        }
        
        private readonly static HttpClient _httpClient = new HttpClient(clientHandler);

        private readonly static string _url = "http://87.97.237.44:5014/";

        private async static void PrintResponseAsync(HttpResponseMessage responseMessage)
        {
            if (responseMessage.IsSuccessStatusCode)
            {
                // Read and print the content of the response
                string responseBody = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            else
            {
                // Print the status code if the request was not successful
                Console.WriteLine($"Error: {responseMessage.StatusCode} - {responseMessage.ReasonPhrase}");
            }
        }
        public static async Task<List<Lobby>> GetLobbiesAsync()
        {
            var response = await _httpClient.GetAsync(_url + $"lobby");
            //PrintResponseAsync(response);
            return JsonConvert.DeserializeObject<List<Lobby>>(await response.Content.ReadAsStringAsync());
        }
        public static async Task CreateLobbyAsync(User user)
        {
            try 
            {
                var json = JsonConvert.SerializeObject(user.Id);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url + $"lobby", data);
                await Console.Out.WriteLineAsync("Lobby created.");
                //PrintResponseAsync(response);

            }
            catch (HttpRequestException e)
            {
                // Handle exceptions related to the HTTP request
                Console.WriteLine($"Request error: {e.Message}");
            }
                
        }

        public static async void JoinLobbyAsync(User user, int lobbyId)
        {
            try
            {
                var json = JsonConvert.SerializeObject(user.Id);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url + $"lobby/{lobbyId}", data);
                //PrintResponseAsync(response);
            }
            catch (HttpRequestException e)
            {
                // Handle exceptions related to the HTTP request
                Console.WriteLine($"Request error: {e.Message}");
            }

        }

        public async static Task<Player> GetPlayersAsync(int lobbyId)
        {
            var response = await _httpClient.GetAsync(_url + $"lobby/{lobbyId}");
            //PrintResponseAsync(response);
            if (JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync())[1] != null)
            {
                User player2 = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync())[1];
                //await Console.Out.WriteLineAsync($"{player2.Username} joined the game.");
                return new Player(player2);
            }
            else return null; 
            
        }

        public async static Task<GameTable> GetGameAsync(int lobbyId)
        {
            var response = await _httpClient.GetAsync(_url + $"game/{lobbyId}");
            GameTable game = JsonConvert.DeserializeObject<GameTable>(await response.Content.ReadAsStringAsync());
            if (game != null)
            {
                game.CurrentPlayer.Character = game.CurrentPlayer.DetectCharacter();
            }            
            return game;
        }

        public static async Task PostGameAsync(GameTable gameTable, int lobbyId)
        {
            try
            {
                var json = JsonConvert.SerializeObject(gameTable);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_url + $"game/{lobbyId}", data);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
        }

    }
}
