using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleMachine
{
    public class Bottle
    {
        string name;

        public Bottle(string name)
        {
            Name = name;
        }

        public string Name { get => name; set => name = value; }
    }
}
