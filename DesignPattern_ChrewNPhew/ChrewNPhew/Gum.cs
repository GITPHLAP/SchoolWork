using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ChrewNPhew
{
    public enum GumColor { Blue = 25, Purple = 12, Yellow = 20, Orange = 19, Red = 14, Green = 10 }




    /// <summary>
    /// MAX 55 gums
    /// Blue 25%
    /// Purple 12%
    /// Yellow 20%
    /// Orange 19%
    /// Red 14%
    /// Green 10%
    /// Blue = "Blåbær", Purple = "Brombær", Yellow = "Tutti frutti", Orange = "Appelsin", Red = "Jordbær", Green = "Æble"
    /// </summary>

    //Blue 25%
    //
    public class Gum
    {

        public GumColor Gumcolor { get; } 
        public Gum(GumColor gumColor)
        {
            this.Gumcolor = gumColor;
        }
        
    }


    
}
