using System;
using System.Collections.Generic;
using System.Text;

namespace MyBanker
{
    public class Account
    {
        //TODO: Not all should be public public get private set.

        long accountNum;

        string owner;

        double dailyUse = 0;

        double monthlyUse = 0;

        double balance = 500; //Default balance to 500
        public double DailyUse { get => dailyUse; set => dailyUse = value; }
        public double MonthlyUse { get => monthlyUse; set => monthlyUse = value; }
        public double Balance { get => balance; set => balance = value; }
        public long AccountNum { get => accountNum; set => accountNum = value; }
        public string Owner { get => owner; set => owner = value; }

        public Account(long accountNum, string owner)
        {
            this.accountNum = accountNum;
            this.owner = owner;
        }

        //implement methodes to change balance 
    }
}
