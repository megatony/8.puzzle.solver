using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntToAI_TermProject.Classes
{
    public class State
    {
        public string name { get; set; }
        public int[,] value { get; set; }
        public int Fvalue { get; set; }
        public State parent {get; set;}
        public int movesCount { get; set; }
    }
}
