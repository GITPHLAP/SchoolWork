using System;
using System.Collections.Generic;
using System.Text;

namespace BirdsFlyingAroundApp
{
    public class Albatross : Bird, IFly
    {
        public override void Draw()
        {
            //Draw Bird
            throw new NotImplementedException();
        }
        public override void SetLocation(double longitude, double latitude)
        {
            //implement set location code
            throw new NotImplementedException();
        }

        public void SetAltitude(double altitude)
        {
            //implement set Altitude code
            throw new NotImplementedException();
        }

    }
}
