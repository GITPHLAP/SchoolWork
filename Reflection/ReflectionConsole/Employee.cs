using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrivateMethods
{
    internal abstract class Employee
    {
        protected EmployeeType empType = EmployeeType.BAD;
        public string Name { get; set; }
        protected abstract string PrintInfo();

        protected Employee(string name)
        {
            this.Name = name;
            PrintInfo();
        }

        public bool ContainsIllegalChars()
        {
            if (this.Name.Contains("$"))
            {
                return true;
            }
            return false;
        }
    }

    internal class Manager : Employee
    {

        public Manager(string name) : base(name)
        {
            if (Name == "Bob")
            {
                this.empType = EmployeeType.BAD;
            }
            else if (Name == "Pete")
            {
                this.empType = EmployeeType.OK;
            }
            else if (Name.IndexOfAny("AaBbCcDdEeFfGg".ToCharArray()) != -1)
            {
                this.empType = EmployeeType.GOOD;
            }
            else
            {
                this.empType = EmployeeType.DICTATOR;
            }
        }

        protected override string PrintInfo()
        {
            string ret = $"I'm Manager {Name} . I'm ";
            switch (empType)
            {
                case EmployeeType.BAD:
                    ret += " serious badass";
                    break;
                case EmployeeType.OK:
                    ret += " pretty ok";
                    break;
                case EmployeeType.GOOD:
                    ret += " really nice";
                    break;
                default:
                    throw new ArgumentException("really not a type we like!");
            }

            return ret;
        }
    }

    internal class DeliveryManager : Employee
    {
        public DeliveryManager(string name) : base(name)
        {
            empType = EmployeeType.OK;
        }
        protected override string PrintInfo()
        {
            return $"I'm Delivery Manager {Name} ";
        }
    }

    internal enum EmployeeType
    {
        DICTATOR = 0, BAD = 1, OK = 2, GOOD = 3
    }
}
