using BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserContext : IDb<User, int>
    {
        private readonly DiceMatchDbContext dbContext;

        public UserContext(DiceMatchDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(User item)
        {
            try
            {
                dbContext.client.Set("Users/" + item.Id, item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(int key)
        {
            try
            {
                dbContext.client.Delete("Users/" + key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public User Read(int key)
        {
            try
            {
                return dbContext.client.Get("Users/" + key).ResultAs<User>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<User> ReadAll()
        {
            try
            {
                try
                {
                    DiceMatchDbContext db = new DiceMatchDbContext();
                    List<User> players = JsonConvert.DeserializeObject<List<User>>(db.client.Get("Users/").Body.ToString());
                    return players.Where(x => x != null).ToList();
                }
                catch (Exception)
                {

                    DiceMatchDbContext db = new DiceMatchDbContext();
                    Dictionary<string, User> players = JsonConvert.DeserializeObject<Dictionary<string, User>>(db.client.Get("Users/").Body.ToString());
                    return players.Values.Where(x => x != null).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(User item)
        {
            try
            {
                dbContext.client.Update("Users/" + item.Id, item);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
