using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace DataLayer
{
    public class DiceMatchDbContext
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "XfuXLFzYtjRCMgqTxxP3QWY6ldBYdX9CIXPTXTLU",
            BasePath = "https://dice-match-6d219-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        public IFirebaseClient client;
        public DiceMatchDbContext()
        {
            client = new FirebaseClient(config);
        }
    }
}