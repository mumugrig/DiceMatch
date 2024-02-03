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
        private readonly static HttpClient _httpClient = new HttpClient();
        
        private readonly static string _url = "http://localhost:5014/";

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
                await Console.Out.WriteLineAsync($"Joined in lobby with id {lobbyId}");
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
            if (await response.Content.ReadAsStringAsync() != null)
            {
                User player2 = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync())[1];
                await Console.Out.WriteLineAsync($"{player2.Username} joined the game.");
                return new Player(player2);
            }
            else return null; 
            
        }

    }
}
