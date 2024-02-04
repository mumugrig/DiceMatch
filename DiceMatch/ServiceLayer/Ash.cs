using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class Ash : GameCharacter
    {
        public Ash() 
        {
            Id = 1;
            Name = "Ash";
            Description = "idkbro";
            AbilityDesctiption = "Destroy a chosen die from any player's board";
            Cooldown = 0;
        }
        public Ash(Character character) : base(character) 
        {
            Description = "idkbro";
            AbilityDesctiption = "Destroy a chosen die from any player's board";
            Cooldown = 0;
        }
        public int[] TargetCell;
        public override void Ability(GameTable gameTable, int[] targetCell)//always assing targetCell before calling ability
        {
            TargetCell = targetCell;                                        //index 0 - player Board; index 1 - row; index 2 - column
            CooldownCheck();
            int[,] board;
            if (targetCell[0] == 0) board = gameTable.CurrentPlayer.Board;
            else  board = gameTable.OpponentPlayer.Board;               
            board[targetCell[1], targetCell[2]] = 0;
            Cooldown += 6;      
        }
    }
}
