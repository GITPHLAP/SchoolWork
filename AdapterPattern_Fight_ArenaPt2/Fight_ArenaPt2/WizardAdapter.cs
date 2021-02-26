using System;
using System.Collections.Generic;
using System.Text;

namespace Fight_ArenaPt2
{
    public class WizardAdapter : IFighter
    {
        Wizard adoptedwizard;

        public int DefenseLeft => adoptedwizard.DefenseLeft();

        public int Attack()
        {
            return adoptedwizard.CastFireballSpell();
        }

        public void Defend(int attack)
        {
            adoptedwizard.Shield(attack);
        }

        public bool HasEscaped()
        {
            return adoptedwizard.IsPortalOpened();
        }

        public WizardAdapter(Wizard wizard)
        {
            adoptedwizard = wizard;
        }
    }
}
