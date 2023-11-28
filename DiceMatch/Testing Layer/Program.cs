using BusinessLayer;
using System.Threading.Channels;
using System;
using DataLayer;
using FireSharp.Response;
using Newtonsoft.Json;

namespace Testing_Layer
{
    internal class Program
    {
        static void CreateTest()
        {
            DiceMatchDbContext db = new DiceMatchDbContext();
            CharacterContext gameContext = new CharacterContext(db);
            Character character = new Character(1, "ash",20,30,5);

            gameContext.Create(character);
            if (db.client != null)
            {
                Console.WriteLine("ti si");
            }
        }
        static void Main(string[] args)
        {
            CreateTest();
        }
    }
}