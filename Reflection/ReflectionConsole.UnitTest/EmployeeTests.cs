using NUnit.Framework;
using ReflectionConsole.ReflectionLogic;
using System;
using System.Reflection;
using System.Xml.Linq;

namespace ReflectionConsole.UnitTest
{
    public abstract class EmployeeTests
    {
        public abstract object CreateEmployee(string name);

        [Test]
        public abstract void PrintInfo_ValidInfo_ReturnString(string name);

        [Test]
        [TestCase("Hej$")]
        [TestCase("@£$£@£$€€€{{[]]}[]")]
        public void ContainsIllegalChars_IllegalChars_ReturnsTrue(string name)
        {
            //Arrange 
            object obj = CreateEmployee(name);


            //Act
            var result = (bool)obj.GetType().GetMethod("ContainsIllegalChars").Invoke(obj, null);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase("Hej")]
        [TestCase("@")]
        [TestCase("£")]
        [TestCase("!")]
        [TestCase("€")]
        [TestCase("&")]
        [TestCase("{")]
        [TestCase("}")]
        [TestCase("[")]
        [TestCase("]")]
        [TestCase("?")]
        [TestCase("+")]
        public void ContainsIllegalChars_NotIllegalChars_ReturnsFalse(string name)
        {
            //Arrange 
            object obj = CreateEmployee(name);


            //Act
            var result = (bool)obj.GetType().GetMethod("ContainsIllegalChars").Invoke(obj, null);

            //Assert
            Assert.IsFalse(result);
        }
    }
}
