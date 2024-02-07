using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class Player
    {
        public int[,] Board;
        public User user;
        public GameCharacter Character;
        public int Score;
        public string Name;
        public Player(User user) 
        {
            this.user = user;
            Name = this.user.Username;
            Character = DetectCharacter();
        }
        public Player()
        {

        }


        public GameCharacter DetectCharacter()
        {
            switch (user.Character.Id)
            {
                case 1:
                    {
                        if (Character == null)
                        {
                            return new Ash();
                        }
                        return new Ash(Character);
                    }
                case 2:
                    {
                        if (Character == null)
                        {
                            return new Talos();
                        }
                        return new Talos(Character);
                    }
                case 3:
                    {
                        if (Character == null)
                        {
                            return new Columna();
                        }
                        return new Columna(Character);

                    }
                case 4:
                    {
                        if (Character == null)
                        {
                            return new Gogin();
                        }
                        return new Gogin(Character);

                    }
            }
            return null;
        }
        
        public void InitiateBoard()
        {
            double Length = Math.Sqrt(Board.Length);
            for (int rows = 0; rows < Length; rows++)
            {             
                for (int columns = 0; columns < Length; columns++)
                {
                    Board[rows, columns] = 0;
                }
            }
        }

        public int RollDie()
        {
            Random random = new Random();
            return random.Next(1, 7); // Rolling a single 6-sided die
        }

        public void PlaceDie(int row, int column, int die)
        {
            if (Board[row, column] == 0)
            {
                Board[row, column] = die;
            }
            else
            {
                throw new ArgumentException("There is already a die in that position");
            }
        }
        public int UpdateScore()
        {
            Score = 0;
            double Length = Math.Sqrt(Board.Length);
            for (int columns = 0; columns < Length; columns++)
            {
                List<int> columnList = new List<int>();
                for(int rows = 0; rows < Length; rows++)
                {
                    columnList.Add(Board[rows , columns]);
                }
                var repeats = columnList.GroupBy(x => x).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count).First();
                for(int i = 0; i < Length; i++)
                {
                    if (columnList[i] == repeats.Value)
                    {
                        columnList[i] *= repeats.Count;
                    }
                    Score += columnList[i];
                }
            }
            return Score;

            
        }
        public bool IsBoardFull()
        {
            foreach (var cell in Board)
            {
                if (cell == 0)
                {
                    return false;
                }
            }

            return true;
        }


    }
}

