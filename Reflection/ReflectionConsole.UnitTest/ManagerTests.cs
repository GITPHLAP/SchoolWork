using MyCustomReflection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ReflectionConsole.UnitTest
{
    public class ManagerTests : EmployeeTests
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
            myType = myReflection.GetType("Manager", "PrivateMethods", "ReflectionConsole.dll");
            return myReflection.CreateInstanceOfType(myType, "Manager", new object[] {name });
        }


        [Test]
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
        public void PrintInfo_UnvalidInfo_ReturnNull(string name)
        {
            //Arrange 
            object obj = CreateEmployee(name);


            //Act
             //myReflection.InvokeMethod(myType, "PrintInfo", null, obj);



            //Act and Assert
            Assert.Throws<System.Reflection.TargetInvocationException>(delegate {
                myReflection.InvokeMethod<string>(myType, "PrintInfo", null, obj); });
        }


        [Test]
        [TestCase("Abe")]
        [TestCase("Søren")]
        [TestCase("Jens")]
        [TestCase("Bob")]
        [TestCase("Åse")]
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
