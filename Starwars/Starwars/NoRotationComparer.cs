using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starwars
{
    class NoRotationComparer : IEqualityComparer<Planet>
    {

        public bool Equals(Planet x, Planet y)
        {
            if (int.Equals(x.RotationPeriod, y.RotationPeriod))
            {
                return true;
            }
            return false;
        }

        public int GetHashCode(Planet obj)
        {
            return obj.RotationPeriod.GetHashCode();
        }
    }
}
