using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace VittighedsMaskinen
{
    public class JokeComparer : IEqualityComparer<Joke>
    {
        public bool Equals(Joke x, Joke y)
        {
            if (x.Id == y.Id)
            {
                return true;
            }

            return false;
        }

        public int GetHashCode(Joke obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
