using BusinessLayer;
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
        public GameCharacter Character;
        public int Score;
        private User user;
        public string Name;

        public Player() 
        {
            //Name = user.Username;
            //Character = user.Character;
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
            Board[row, column] = die;
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

