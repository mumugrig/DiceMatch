namespace BusinessLayer
{
    public class Character
    {
        public Character()
        {

        }
        public Character(int id, string name, int wins, int allGames, int picks)
        {
            this.Id = id;
            this.Name = name;
            this.WinRate = (wins/allGames)*100;
            this.PickRate = (picks/allGames)*100;
            this.Wins = wins;
            this.AllGames = allGames;
            this.Picks = picks;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public double WinRate { get; set; }

        public double PickRate { get; set; }

        public int Wins { get; set; }

        public int AllGames { get; set; }

        public int Picks { get; set; }
    }
}