using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Match
    {
        public User Player1 { get; set; }

        public User Player2 { get; set; }
        public int Player1Points { get; set; }

        public int Player2Points { get; set; }

        public Character Player1Character { get; set; }

        public Character Player2Character { get; set; }
    }
}
