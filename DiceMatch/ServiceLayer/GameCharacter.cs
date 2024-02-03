using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public abstract class GameCharacter
    {
        public GameCharacter() { }
        public GameCharacter(Character character)
        {
            Id = character.Id;
            Name = character.Name;
        }
        public static int Id;
        public static string Name;
        public static string Description;
        public static string AbilityDesctiption;
        public int Cooldown;
        public bool OnCooldown
        {
            get { return Cooldown > 0; }
        }
        public void CooldownCheck()
        {
            if (OnCooldown)
            {
                throw new ArgumentException("Ability is on cooldown");
            }
        }
        public abstract void Ability(GameTable gameTable, int[] input = null);
        
    }
}
