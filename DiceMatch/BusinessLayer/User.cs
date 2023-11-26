using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class User
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public IEnumerable<Match> Matches { get; set; }

        public IEnumerable<User> Friends { get; set; }

        public StatusSetting Status { get; set; }

        public IEnumerable<Match> WinRate { get; set; }//????

        public Character PickRates { get; set; }//??

        public Character Character { get; set; }//??
    }
}