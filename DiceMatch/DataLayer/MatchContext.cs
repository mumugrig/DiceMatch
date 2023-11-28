using BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class MatchContext : IDb<Match, int>
    {
        private readonly DiceMatchDbContext dbContext;

        public MatchContext(DiceMatchDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Match item)
        {
            try
            {
                dbContext.client.Set("Matches/" + item.Id, item);
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
                dbContext.client.Delete("Matches/" + key);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Match Read(int key)
        {
            try
            {
                return dbContext.client.Get("Matches/" + key).ResultAs<Match>();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public ICollection<Match> ReadAll()
        {
            try
            {
                try
                {
                    DiceMatchDbContext db = new DiceMatchDbContext();
                    List<Match> players = JsonConvert.DeserializeObject<List<Match>>(db.client.Get("Matches/").Body.ToString());
                    return players.Where(x => x != null).ToList();
                }
                catch (Exception)
                {

                    DiceMatchDbContext db = new DiceMatchDbContext();
                    Dictionary<string, Match> players = JsonConvert.DeserializeObject<Dictionary<string, Match>>(db.client.Get("Matches/").Body.ToString());
                    return players.Values.Where(x => x != null).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(Match item)
        {
            try
            {
                dbContext.client.Update("Matches/" + item.Id, item);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
