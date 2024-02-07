using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class Columna : GameCharacter
    {
        public Columna()
        {
            Id = 3;
            Name = "Columna";
            Description = "idkbro";
            AbilityDesctiption = "Reduce the values of opponent dice in a selected column";
            Cooldown = 0;
        }
        public Columna(Character character) : base(character)
        {
            Description = "idkbro";
            AbilityDesctiption = "Reduce the values of opponent dice in a selected column";
            Cooldown = 0;
        }
        public Columna(GameCharacter character)
        {
            Id = 3;
            Name = "Columna";
            Description = "idkbro";
            AbilityDesctiption = "Reduce the values of opponent dice in a selected column";
            Cooldown = character.Cooldown;
        }

        public override void Ability(GameTable gameTable, int[] input = null)
        {
            int[,] opponendBoard = gameTable.OpponentPlayer.Board;
            for(int row = 0; row < 3; row++)
            {
                if (opponendBoard[row, input[0]] > 0)
                {
                    opponendBoard[row, input[0]]--;
                }                
            }
            Cooldown += 4;//3 in-game
        }

    }
}
