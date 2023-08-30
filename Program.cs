using AccountApp.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp
{
    internal class Program
    {
        static string filePath;
        static void Main(string[] args)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Account account;

            filePath = ConfigurationManager.AppSettings["filePath"];

            if (File.Exists(filePath))
            {
                // File exists, deserialize account
                using (FileStream stream = new FileStream(filePath, FileMode.Open))
                {
                    account = (Account)formatter.Deserialize(stream);
                }
                Console.WriteLine("Welcome back! Account balance is: {0}", account.CheckBalance());
            }
            else
            {
                Console.WriteLine("Welcome! Enter account details to create a new account:");

                Console.Write("Account Number: ");
                int accountNumber = int.Parse(Console.ReadLine());

                Console.Write("Account Holder Name: ");
                string accountHolderName = Console.ReadLine();

                Console.Write("Bank Name: ");
                string bankName = Console.ReadLine();

                Console.Write("Opening Balance: ");
                double balance = double.Parse(Console.ReadLine());

                account = new Account(accountNumber, accountHolderName, bankName, balance);


                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    formatter.Serialize(stream, account);
                }

                Console.WriteLine("Account created successfully!");
            }

            while (true)
            {
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("1. Deposit");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Display Balance");
                Console.WriteLine("4. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter amount to deposit: ");
                        double depositAmount = double.Parse(Console.ReadLine());
                        account.Deposit(depositAmount);
                        SaveAccount(account, formatter);
                        break;

                    case 2:
                        Console.Write("Enter amount to withdraw: ");
                        double withdrawAmount = double.Parse(Console.ReadLine());
                        account.Withdraw(withdrawAmount);
                        SaveAccount(account, formatter);
                        break;

                    case 3:
                        Console.WriteLine("Balance: {0}", account.CheckBalance());
                        break;

                    case 4:
                        return;
                }
            }
        }

        static void SaveAccount(Account account, BinaryFormatter formatter)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, account);
            }
        }
    }

}





