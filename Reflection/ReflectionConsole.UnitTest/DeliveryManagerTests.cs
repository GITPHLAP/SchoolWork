using NUnit.Framework;
using ReflectionConsole.ReflectionLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionConsole.UnitTest
{
    public class DeliveryManagerTests : EmployeeTests
    {
        MyReflection myReflection;
        Type myType;
        [SetUp]
        public void Setup()
        {
            myReflection = new MyReflection();


        }
        public override object CreateEmployee(string name)
        {
            myType = myReflection.GetType("DeliveryManager", "PrivateMethods", "ReflectionConsole.dll");
            return myReflection.CreateInstanceOfType(myType, "DeliveryManager", new object[] { name });
        }


        [Test]
        [TestCase("Abe")]
        [TestCase("Søren")]
        [TestCase("Jens")]
        [TestCase("Åse")]
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
        public override void PrintInfo_ValidInfo_ReturnString(string name)
        {
            //Arrange 
            object obj = CreateEmployee(name);


            //Act
            var result = myReflection.InvokeMethod<string>(myType, "PrintInfo", null, obj);

            //Assert
            Assert.NotNull(result);
        }
    }
}
