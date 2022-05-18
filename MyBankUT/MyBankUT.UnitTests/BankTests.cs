using Autofac.Extras.Moq;
using MyBankUT.MockUps;
using NUnit.Framework;
using System.Collections.Generic;

namespace MyBankUT.UnitTests
{
    public class BankTests
    {
        Bank bank;
        [SetUp]
        public void Setup()
        {
            //Arrange
            bank = new Bank("PhilipsBank", new MockDAL(), new List<IATM> { new MockATM() } );
        }

        #region ValidateCard

        [Test]
        [TestCase("2000", 1234)]
        [TestCase("4015-334-45312-543", 1234)]
        [TestCase("4015-50453-45312-543", 4321)]
        [TestCase("3478-34242-234324-23", 1234)]
        [TestCase("4015-5047-4531-5433", 88888888)]
        public void ValidateCard_Wrong_ReturnFalse(string cardNumber, int pin)
        {


            // Act 
            var result = bank.ValidateCard(cardNumber, pin);

            // Assert
            Assert.IsFalse(result);

        }

        [Test]
        [TestCase("4015-5047-4531-5433", 1234)]
        [TestCase("0001-5047-4531-5433", 0001)]
        [TestCase("7015-5047-4531-0000", 1570)]
        [TestCase("1010-5047-4531-5433", 1010)]
        public void ValidateCard_Right_ReturnTrue(string cardNumber, int pin)
        {


            // Act 
            var result = bank.ValidateCard(cardNumber, pin);

            // Assert
            Assert.IsTrue(result);

        }

        #endregion


        #region ProcessAction

        [Test]
        [TestCase(ProcessTypes.WithDraw, "4015-5047-4531-5433", 100)]
        [TestCase(ProcessTypes.WithDraw, "5015-5047-4531-5433", 100)]
        [TestCase(ProcessTypes.WithDraw, "7015-5047-4531-0000", 2.23)]
        [TestCase(ProcessTypes.WithDraw, "9999-5047-4531-5433", 10000)]
        [TestCase(ProcessTypes.WithDraw, "0001-5047-4531-5433", 0.01)]
        public void ProcessAction_WithdrawSuccesfully_ReturnTrue(ProcessTypes process, string cardnumber, double amount)
        {
            // Act 
            var result = bank.ProcessAction(process, cardnumber, amount);

            // Assert
            Assert.IsTrue(result);

        }

        [Test]
        [TestCase(ProcessTypes.WithDraw, "4015-5047-4531-5433", 5000)]
        [TestCase(ProcessTypes.WithDraw, "5015-5047-4531-5433", -900)]
        [TestCase(ProcessTypes.WithDraw, "7015-5047-4531-0000", 5051)]
        [TestCase(ProcessTypes.WithDraw, "9999-5047-4531-5433", 9000000)]
        [TestCase(ProcessTypes.WithDraw, "0001-5047-4531-5433", 20.203001)]
        [TestCase(ProcessTypes.WithDraw, "1010-5047-4531-5433", -10.2)]
        [TestCase(ProcessTypes.WithDraw, "0000-0000-0000-0000", 0.01)]

        public void ProcessAction_WithdrawFailed_ReturnFalse(ProcessTypes process, string cardnumber, double amount)
        {
            // Act 
            var result = bank.ProcessAction(process, cardnumber, amount);

            // Assert
            Assert.IsFalse(result);

        }
        #endregion

    }
}