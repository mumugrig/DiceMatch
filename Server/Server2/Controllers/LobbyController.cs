using BusinessLayer;
using DataLayer;
using ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace Server2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LobbyController : Controller
    {
        UserContext _userContext;
        public LobbyController(DiceMatchDbContext dbContext)
        {
            _userContext = new UserContext(dbContext);
        }

        [HttpGet]
        public ICollection<Lobby> Get()
        {
            return Lobby.Lobbies;
        }

        [HttpPost]
        public int CreateLobby([FromBody] int userId)
        {
            Lobby lobby = new Lobby(_userContext.Read(userId));
            int lobbyId = Lobby.Lobbies.IndexOf(lobby);
            Console.WriteLine("Lobby with id {0} was created", lobbyId);
            return lobbyId;
        }

        [HttpGet("{id}")]
        public List<User> GetLobby([FromRoute] int lobbyId)
        {
            return Lobby.Lobbies[lobbyId].Users.ToList();
        }

        [HttpPost("{id}")]
        public void JoinLobby([FromRoute] int lobbyId, [FromBody] int userId)
        {
            User user = _userContext.Read(userId);
            if (Lobby.Lobbies[lobbyId].User1 == null)
            {
                Console.WriteLine("{0} has joined the game as player1", user.Username);
                Lobby.Lobbies[lobbyId].User1 = user;
            }
            else if (Lobby.Lobbies[lobbyId].User2 == null)
            {
                Console.WriteLine("{0} has joined the game as player2", user.Username);
                Lobby.Lobbies[lobbyId].JoinLobby(user);
            }
        }
        [HttpDelete("{id}")]
        public void RemoveLobby(int lobbyId)
        {
            Lobby.Lobbies[lobbyId].RemoveLobby();
        }
    }
}
