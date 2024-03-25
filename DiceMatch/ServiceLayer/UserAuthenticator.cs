using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class UserAuthenticator
    {
        private readonly DbManager<BusinessLayer.User, int> userManager;
        private readonly DbManager<BusinessLayer.Match, int> matchManager;
        public UserAuthenticator()
        {
            userManager = new DbManager<User, int>(ContextGenerator.GetUsersContext());
            matchManager = new DbManager<Match, int>(ContextGenerator.GetMatchesContext());
            LoggedIn = false;
            Player = userManager.Read(0);
            LocalGames = matchManager.ReadAll().Where(x => x.Player1.Id == Player.Id).ToList();
            
        }
        public User Player 
        {
            get;set;
        }

        public ICollection<Match> LocalGames { get; set; }
        public bool LoggedIn { get; set; }
        

        public void LogIn(string username, string password)
        {
            if(userManager.ReadAll().Any(x => x!=null && x.Username == username && x.Password == password))
            {
                LoggedIn = true;
                Player = userManager.ReadAll().FirstOrDefault(x => x.Username==username);
                LocalGames = userManager.ReadAll().Where(x => x!=null && x.Id == Player.Id).FirstOrDefault().Matches.ToList();
            }
            else
            {
                throw new ArgumentException("Account does not exist!");
            }
        }
        public void LogOut()
        {
            LoggedIn = false;
            Player=userManager.Read(0);
            LocalGames = matchManager.ReadAll().Where(x => x.Player1.Id == Player.Id).ToList();
        }
        public void SignIn(string username, string password)
        {
            if(userManager.ReadAll().Any(x => x.Username == username))
            {
                throw new ArgumentException("Username exists");
            }
            else
            {
                userManager.Create(new User(username, password));
            }
            
        }
    }
}
