using System;
using System.Collections.Generic;
using System.Text;

namespace GameCharacter
{
    public interface ICharacter
    {
        void Heal();
        void Die();
        void Fight();
    }
}
