using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class User
    {
        public User()
        {
            Matches = new List<Match>();
            Friends = new List<User>();
            CharacterPickRates = new List<double>();
            CharacterWinRates = new List<double>();
            CharacterWins = new List<int>();
            CharacterPicks = new List<int>();
        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
            Matches = new List<Match>();
            Friends = new List<User>();
            CharacterPickRates = new List<double>();
            CharacterWinRates = new List<double>();
            CharacterWins = new List<int>();
            CharacterPicks = new List<int>();
        }
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public List<Match> Matches { get; set; }

        public List<User> Friends { get; set; }

        public StatusSetting Status { get; set; }

        public double WinRate { get; set; }//????

        public List<double> CharacterPickRates { get; set; }//??

        public List<double> CharacterWinRates { get; set; }

        public Character Character { get; set; }//??

        public int Wins { get; set; }

        public List<int> CharacterWins { get; set; }

        public List<int> CharacterPicks { get; set; }
    }
}