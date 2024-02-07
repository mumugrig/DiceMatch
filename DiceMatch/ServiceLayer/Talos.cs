using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class Talos : GameCharacter
    {
        public Talos()
        {
            Id = 2;
            Name = "Talos";
            Description = "idkbro";
            AbilityDesctiption = "Swap a random die from your board with a random die from the opponent's board";
            Cooldown = 0;
        }
        public Talos(Character character) : base(character)
        {
            Description = "idkbro";
            AbilityDesctiption = "Swap a random die from your board with a random die from the opponent's board";
            Cooldown = 0;
        }
        public Talos(GameCharacter character)
        {
            Id = 2;
            Name = "Talos";
            Description = "idkbro";
            AbilityDesctiption = "Swap a random die from your board with a random die from the opponent's board";
            Cooldown = character.Cooldown;
        }

        public override void Ability(GameTable gameTable, int[] input=null)
        {
            CooldownCheck();
            int[,] player1Board = gameTable.player1.Board;
            int[,] player2Board = gameTable.player2.Board;
            List<int[]> ValidPositions1 = new List<int[]>();
            List<int[]> ValidPositions2 = new List<int[]>();
            for(int row = 0; row < 3; row++)
            {
                for(int col = 0; col < 3; col++)
                {
                    if (player1Board[row, col] != 0)
                    {
                        ValidPositions1.Add(new int[] { row, col });
                    }
                    if (player2Board[row, col] != 0)
                    {
                        ValidPositions2.Add(new int[] {row, col});
                    }
                }
            }
            if(ValidPositions1.Count==0 || ValidPositions2.Count == 0)
            {
                throw new ArgumentException("Not enough dice to swap.");
            }
            Random rng = new Random();
            int randomPosition1 = rng.Next(0, ValidPositions1.Count);
            int randomPosition2 = rng.Next(0, ValidPositions2.Count);
            int temp = player1Board[ValidPositions1[randomPosition1][0], ValidPositions1[randomPosition1][1]];
            player1Board[ValidPositions1[randomPosition1][0], ValidPositions1[randomPosition1][1]] = player2Board[ValidPositions2[randomPosition2][0], ValidPositions2[randomPosition2][1]];
            player2Board[ValidPositions2[randomPosition2][0], ValidPositions2[randomPosition2][1]] = temp;
            Cooldown += 3;//2 in-game
        }
    }
}
