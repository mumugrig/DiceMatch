using BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CharacterContext : IDb<Character, int>
    {
        private readonly DiceMatchDbContext dbContext;

        public CharacterContext(DiceMatchDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Character item)
        {
            try
            {
                dbContext.client.Set("Characters/" + item.Id, item);
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
                dbContext.client.Delete("Characters/" + key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Character Read(int key)
        {
            try
            {
                return dbContext.client.Get("Characters/" + key).ResultAs<Character>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<Character> ReadAll()
        {
            try
            {
                try
                {
                    DiceMatchDbContext db = new DiceMatchDbContext();
                    List<Character> players = JsonConvert.DeserializeObject<List<Character>>(db.client.Get("Characters/").Body.ToString());
                    return players.Where(x => x != null).ToList();
                }
                catch (Exception)
                {

                    DiceMatchDbContext db = new DiceMatchDbContext();
                    Dictionary<string, Character> players = JsonConvert.DeserializeObject<Dictionary<string, Character>>(db.client.Get("Characters/").Body.ToString());
                    return players.Values.Where(x => x != null).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Character item)
        {
            try
            {
                dbContext.client.Update("Characters/" + item.Id, item);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
