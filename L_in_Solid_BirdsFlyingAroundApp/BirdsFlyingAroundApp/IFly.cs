using System;
using System.Collections.Generic;
using System.Text;

namespace BirdsFlyingAroundApp
{
    public interface IFly
    {
        //Force class who implement IFly to set altitude 
        void SetAltitude(double altitude);

    }
}
