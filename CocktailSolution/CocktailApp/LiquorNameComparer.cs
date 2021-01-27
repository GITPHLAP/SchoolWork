using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocktailApp
{
    class LiquorNameComparer : IEqualityComparer<Liquor>
    {

        public bool Equals(Liquor x, Liquor y)
        {
            if (string.Equals(x.LiquorName, y.LiquorName, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Liquor obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
