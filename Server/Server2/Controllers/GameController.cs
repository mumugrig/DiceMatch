using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace Server2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : Controller
    {
        public GameController() { }

        [HttpPost("{lobbyId}")]
        public void PostGame([FromBody]GameTable gameTable, [FromRoute]int lobbyId)
        {
            Lobby.Lobbies[lobbyId].gameTable = gameTable;
        }

        [HttpGet("{lobbyId}")]
        public GameTable GetGame(int lobbyId)
        {
            try
            {
                if (Lobby.Lobbies[lobbyId].gameTable == null)
                {
                    Lobby.Lobbies[lobbyId].StartGame();
                }
                return Lobby.Lobbies[lobbyId].gameTable;
            }
            catch
            {
                return null;
            }
            
        }
    }
}
