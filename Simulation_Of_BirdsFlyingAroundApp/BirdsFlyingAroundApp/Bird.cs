using System;
using System.Collections.Generic;
using System.Text;

namespace BirdsFlyingAroundApp
{
    public abstract class Bird
    {
        public abstract void SetLocation(double longitude, double latitude);
        public abstract void Draw();
    }
}
