using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateMethods
{
    internal class Person
    {
        private bool old;
        private int age;
        public Person(int age, string gender)
        {
            this.age = age >= 0 ? age : 0;
            this.old = IsOld();
            Gender = gender;
        }
        public string Gender { get; set; }
        private bool IsOld()
        {
            return age > 50 ? true : false;
        }
    }
}
