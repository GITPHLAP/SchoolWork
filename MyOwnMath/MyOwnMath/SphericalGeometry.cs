using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOwnMath
{
    public class SphericalGeometry
    {
        public static double CalculateDistanceBetweenTwoLongAndLatitude(double longA, double latiA, double longB, double latiB)
        {

            // formula for distance: dist(A,B) = 2* pi*r n/360degree
            // A and B is point on earth 
            // n is angel between sphere distance between a and b 

            int r = 6370; //Earth radius in km

            double n = (MyMath.SinN(latiA) * MyMath.SinN(latiB)) + (MyMath.CosN(latiA) * MyMath.CosN(latiB) * MyMath.CosN(longA - longB));

            double dist = 2 * Math.PI * r * (MyMath.AcosN(n) / 360);

            return dist;
        }
    }
}
