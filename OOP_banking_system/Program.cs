using System;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            BudgetAccount wallet1 = new BudgetAccount("artem", 100.0m, 50.0m, 0);
            wallet1.DisplaySummary();

            // BOTTOM OF THE SECOND HW PAGE
            bool working = true;
            while (working)
            {
                Console.WriteLine("[1] - Add expense");
                Console.WriteLine("[2] - Add funds");
                Console.WriteLine("[3] - Change monthly limit");
                Console.WriteLine("[4] - View summary");
                Console.WriteLine("[5] - Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();

                if (option == "1")
                {
                    Console.WriteLine("Enter an expense amount: ");
                    decimal expense = Convert.ToDecimal(Console.ReadLine());
                    wallet1.AddExpense(expense);
                }
                else if (option == "2")
                {
                    Console.WriteLine("Enter a deposit amount: ");
                    decimal deposit = Convert.ToDecimal(Console.ReadLine());
                    wallet1.AddFunds(deposit);
                }
                else if (option == "3")
                {
                    Console.WriteLine("What do you want to change the monthly limit to: ");
                    decimal newLimit = Convert.ToDecimal(Console.ReadLine());
                    wallet1.ChangeMonthlyLimit(newLimit);
                }
                else if (option == "4")
                {
                    wallet1.DisplaySummary();
                }
                else
                {
                    working = false;
                }
            }

            // LEFT PAGE OF THE HW + TOP OF THE RIGHT PAGE
            decimal userExpense = 0;
            decimal userDeposit = 0;

            bool valid = false;

            while (!valid)
            {
                try
                {
                    Console.Write("Enter an expense amount: ");
                    userExpense = Convert.ToDecimal(Console.ReadLine());   
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Please enter data in the right format.");
                }
            }
            while (userExpense < 0)
            {
                userExpense = Convert.ToDecimal(Console.ReadLine());
            }
            wallet1.AddExpense(userExpense);

            valid = false;
            while (!valid)
            {
                try
                {
                    Console.Write("Enter a deposit amount: ");
                    userDeposit = Convert.ToDecimal(Console.ReadLine());   
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Please enter data in the right format.");
                }
            }
            while (userDeposit < 0)
            {
                userDeposit = Convert.ToDecimal(Console.ReadLine());
            }
            wallet1.AddFunds(userDeposit);

            wallet1.DisplaySummary();
        }

        class BudgetAccount
        {
            private string ownerName;
            private decimal balance;
            private decimal monthlyLimit;
            private decimal totalSpent;

            public BudgetAccount(string anOwnerName, decimal theBalance, decimal theMonthlyLimit, decimal theTotalSpent)
            {
                ownerName = anOwnerName;
                balance = theBalance;
                monthlyLimit = theMonthlyLimit;
                totalSpent = theTotalSpent;
            }

            public string getOwnerName()
            {
                return ownerName;
            }
            public decimal getBalance()
            {
                return balance;
            }
            public decimal getMonthlyLimit()
            {
                return monthlyLimit;
            }
            public decimal getTotalSpend()
            {
                return totalSpent;
            }


            //EXTERNAL FUNCTIONS
            public void AddExpense(decimal amount)
            {
                if (amount > monthlyLimit && balance - amount >= 0)
                {
                    Console.WriteLine("Amount exceeded the monthly limit!");
                    balance -= amount;
                }
                else if (amount > monthlyLimit && balance - amount <= 0)
                {
                    Console.WriteLine("Amount exceeded the monthly limit!");
                }
                else
                {
                    balance -= amount;
                }
                totalSpent += amount;
            }
            public void AddFunds(decimal amount)
            {
                balance += amount;   
            }
            public void ResetMonthlySpendings()
            {
                totalSpent = 0; 
            }
            public void ChangeMonthlyLimit(decimal newLimit)
            {
                if (newLimit > totalSpent)
                {
                    monthlyLimit = newLimit;   
                }
                else
                {
                    Console.WriteLine($"The amount has to be > the current total spent, which is: {totalSpent}");
                }
            }
            public void DisplaySummary()
            {
                Console.WriteLine($"Name: {ownerName}, Balance: {balance}, Monthly limit: {monthlyLimit}");
            }
        }
    }
}