using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotTest.Games
{
    class Dice
    {
        public static int roll()
        {
            Random rand = new Random();
            return rand.Next(1, 6);
        }
    }
}
