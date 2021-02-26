using System;
using System.Collections.Generic;
using System.Text;

namespace Fight_ArenaPt2
{
    public class DragonAdapter : IFighter
    {
        bool fireattack = false;
        Dragon adopteddragon;

        public DragonAdapter(Dragon dragon)
        {
            adopteddragon = dragon;
        }

        public int DefenseLeft => adopteddragon.Defense();

        public int Attack()
        {
            if (fireattack == false)
            {
                fireattack = true;
                return adopteddragon.TaleSlash();
            }
            else
            {
                fireattack = false;
                return adopteddragon.BreatheFire();
            }
            
        }

        public void Defend(int attack)
        {
            adopteddragon.Defend(attack);
        }

        public bool HasEscaped()
        {
            return adopteddragon.IsFlyingAway();
        }
    }
}
