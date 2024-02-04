using BusinessLayer;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer
{
    [JsonDerivedType(typeof(GameCharacter), typeDiscriminator:"character")]
    [JsonDerivedType(typeof(Ash), typeDiscriminator:"ash")]
    public class GameCharacter
    {
        public GameCharacter()
        {
            Cooldown = 0;
        }
        public GameCharacter(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            Cooldown = 0;
        }
        public int Id;
        public string Name;
        public string Description;
        public string AbilityDesctiption;
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
        public virtual void Ability(GameTable gameTable, int[] input = null)
        {
        }
        
    }
}
