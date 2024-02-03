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

        [HttpPost()]
        public void CreateLobby([FromBody]int userId)
        {
            Lobby lobby = new Lobby(_userContext.Read(userId));
            Console.WriteLine("Lobby with id {0} was created", Lobby.Lobbies.IndexOf(lobby));
        }

        [HttpGet("{id}")]
        public List<User> GetLobby([FromRoute]int lobbyId)
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
    }
}
