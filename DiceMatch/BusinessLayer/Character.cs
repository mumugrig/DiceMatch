namespace BusinessLayer
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public double WinRate { get; set; }

        public double PickRate { get; set; }

        public int Wins { get; set; }

        public int AllGames { get; set; }

        public int Picks { get; set; }
    }
}