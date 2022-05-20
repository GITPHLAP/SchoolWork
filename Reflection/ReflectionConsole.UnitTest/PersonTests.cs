using NUnit.Framework;
using ReflectionConsole.ReflectionLogic;
using System;
using System.Reflection;

namespace ReflectionConsole.UnitTest
{
    public class PersonTests
    {

        MyReflection myReflection;
        [SetUp]
        public void Setup()
        {
            myReflection = new MyReflection();


        }

        [Test]
        [TestCase(20, "Men")]
        [TestCase(10, "Jens")]
        [TestCase(-1, "Men")]
        [TestCase(50, "Hej med dig")]
        public void IsOld_NotOld_ReturnFalse(int age, string gender)
        {
            //Arrange
            Type personType = myReflection.GetType("Person", "PrivateMethods", "ReflectionConsole.dll");

            object person = myReflection.CreateInstanceOfType(personType, personType.Name, new object[] { age, gender });

            
            //Act
            var result = myReflection.InvokeMethod<bool>(personType, "IsOld", null, person);

            //Assert
            Assert.IsFalse(result);
            //age is expected

        }

        [Test]
        [TestCase(200, "Men")]
        [TestCase(900000000, "Jens")]
        [TestCase(51, "Hej med dig")]
        //[TestCase(50, "Hej med dig")]

        public void IsOld_Old_ReturnTrue(int age, string gender)
        {
            //Arrange
            Type personType = myReflection.GetType("Person", "PrivateMethods", "ReflectionConsole.dll");

            object person = myReflection.CreateInstanceOfType(personType, personType.Name, new object[] { age, gender });


            //Act
            var result = myReflection.InvokeMethod<bool>(personType, "IsOld", null, person);

            //Assert
            Assert.IsTrue(result);
            //age is expected

        }
    }
}