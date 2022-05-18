using MyBankUT.MockUps;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBankUT.UnitTests
{
    public class AccountTests
    {
        List<Account> mockAccounts;
        [SetUp]
        public void Setup()
        {
            //Arrange
            mockAccounts = GetMockAccounts();
        }

        #region Withdraw

        [Test]
        [TestCase(1234)]
        [TestCase(234)]
        [TestCase( 4321)]
        [TestCase( 1234)]
        [TestCase(88888888)]
        [TestCase(-88888888)]
        public void Withdraw_ValidCall(double amount)
        {


            // Act 
            foreach (var account in mockAccounts)
            {
                double balanceBefore = account.Balance;
                var result = account.Withdraw(amount);
                double balanceAfter = account.Balance;

                Assert.AreEqual((balanceBefore - result), balanceAfter);
            }

        }

        #endregion


        #region CanWithdraw

        [Test]
        [TestCase(0)]
        [TestCase(null)]
        [TestCase(-0.4f)]
        [TestCase(-122131231231232133)]
        [TestCase(-88888888)]
        [TestCase(20)]
        [TestCase(1234)]
        [TestCase(234)]
        [TestCase(4321)]
        [TestCase(1234)]
        [TestCase(88888888)]
        public void CanWithdraw_ValidCall(double amount)
        {


            // Act 
            foreach (var account in mockAccounts)
            {
                var result = account.CanWithdraw(amount);

                bool canWithdraw = amount <= 0 ? false:  account.Balance >= amount;

                Assert.AreEqual(canWithdraw, result);
            }

        }

        #endregion


        public List<Account> GetMockAccounts()
        {
            List<Account> accounts = new List<Account>();
            accounts.Add(new Account(500,
                new Card("4015-5047-4531-5433", 1234)));

            accounts.Add(new Account(5050,
    new Card("5015-5047-4531-5433", 4321)));

            accounts.Add(new Account(1000,
    new Card("7015-5047-4531-0000", 1570)));

            accounts.Add(new Account(10000,
    new Card("9999-5047-4531-5433", 8080)));

            accounts.Add(new Account(20.203,
    new Card("0001-5047-4531-5433", 0001)));

            accounts.Add(new Account(0,
    new Card("1010-5047-4531-5433", 1010)));

            return accounts;

        }
    }
}
