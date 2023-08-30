using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Model
{
    [Serializable]
    internal class Account
    {
        public int accountNumber;
        public string accountHolderName;
        public string bankName;
        public double balance;

        public Account(int accountNumber, string accountHolderName, string bankName, double balance)
        {
            this.accountNumber = accountNumber;
            this.accountHolderName = accountHolderName;
            this.bankName = bankName;
            this.balance = balance;
        }

        public void Deposit(double amount)
        {
            balance= balance+ amount;
        }
        public void Withdraw(double amount)
        {
            if(balance - amount >= 500)
            {
                balance = balance - amount;
            }
            else
            {
                Console.WriteLine("Insufficient balance");
            }
        }
        public double CheckBalance()
        {
            return balance;
        }
    }
}
