using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBotTest.MemeGenerator
{
    class resp
    {
        public bool success { get; set; }
        public data data { get; set; }
    }
    class data
    {
      
        public List<memes> memes { get; set; }
    }
    class memes
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
