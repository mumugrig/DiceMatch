using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class Lobby
    {
        public static List<Lobby> Lobbies = new List<Lobby>();
        public User[] Users;
        public User User1 { get { return Users[0]; }  set { Users[0] = value; } }
        public User User2 { get { return Users[1]; } set { Users[1] = value; } }
        public GameTable gameTable;
        public Lobby(User user1) 
        {
            Users = new User[2];
            Users[0] = user1;
            Lobbies.Add(this);
        } 
        public GameTable StartGame()
        {
            if (Users[0] != null && Users[1] != null)
            {
                gameTable = new GameTable(Users[0], Users[1]);
                return gameTable;
            }
            else return null;
        }
        public void JoinLobby(User user2)
        {
            if (User1 == null)
            {
                User1 = user2;
            }
            else if (User2 == null)
            {
                User2 = user2;
            }
        }
        public void LeaveLobby(User user)
        {
            if (User1 == user)
            {                 
                User1 = User2;
                User2 = null;
            }
            else if (User2 == user)
            {
                User2 = null;
            }
            if (User1 == null)
            {
                RemoveLobby();
            }
        }
        public void RemoveLobby()
        {
            Lobbies.Remove(this);
        }
    }
}
