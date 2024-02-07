using BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class Gogin : GameCharacter
    {
        public Gogin()
        {
            Id = 4;
            Name = "Gogin";
            Description = "idkbro";
            AbilityDesctiption = "Reroll your die";
            Cooldown = 0;
        }
        public Gogin(Character character) : base(character)
        {
            Description = "idkbro";
            AbilityDesctiption = "Reroll your die";
            Cooldown = 0;
        }
        public Gogin(GameCharacter character)
        {
            Id = 4;
            Name = "Gogin";
            Description = "idkbro";
            AbilityDesctiption = "Reroll your die";
            Cooldown = character.Cooldown;
        }
        public override void Ability(GameTable gameTable, int[] targetCell = null)
        {                                             
            CooldownCheck();
            gameTable.HasRolled = false;
            gameTable.Roll();
            Cooldown += 5;//4 in-game
        }
    }
}
