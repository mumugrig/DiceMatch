using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public List<Match> Matches { get; set; }

        public List<User> Friends { get; set; }

        public StatusSetting Status { get; set; }

        public double WinRate { get; set; }//????

        public List<double> CharacterPickRates { get; set; }//??

        public List<double> CharacterWinRate { get; set; }

        public Character Character { get; set; }//??

        public int Wins { get; set; }

        public List<int> CharacterWins { get; set; }

        public List<int> CharacterPicks { get; set; }
    }
}