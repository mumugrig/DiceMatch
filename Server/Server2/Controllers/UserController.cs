using BusinessLayer;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Server.Services;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly UserContext _userContext;
        public UserController(IPlayerService playerService, DiceMatchDbContext dbContext) 
        {
            _playerService = playerService;
            _userContext = new UserContext(dbContext);
        }
        [HttpGet("{id}")]
        public User Get([FromRoute] int id)
        {
            var user = _userContext.Read(id);
            _playerService.DoSomething();
            return user;
        }
        [HttpPost]
        public User Post(User user)
        {
            _userContext.Create(user);
            Console.WriteLine("Player has been added to the DB");
            return user;
        }
        [HttpPut]
        public User Put([FromBody]User user)
        {
            _userContext.Update(user);
            return user;
        }
    }
}
