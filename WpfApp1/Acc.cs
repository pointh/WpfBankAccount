using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WpfApp1
{
    public delegate void OnChangedBalance();
    public abstract class BankAccount
    {
        public event OnChangedBalance ChangedBalance;

        internal decimal balance { get; set; }
        internal decimal interest { get; set; } = 0.005m;
        //

        public void DefaultChangedBalance()
        {
            Debug.WriteLine("Default Changed Balance");
        }

        public void Subscribe(OnChangedBalance f)
        {
            ChangedBalance += f;
        }
        public BankAccount(decimal startBalance = 10_000)
        {
            Subscribe(DefaultChangedBalance);

            balance = startBalance;
            ChangedBalance();
        }
        public abstract void MonthlyInterest();
        public string Balance()
        {
            return $"Balance: {balance:#.##} Kč";
        }
        public void Withdraw(int amount)
        {
            balance -= amount;
            ChangedBalance();
        }
        public void Deposit(int amount)
        {
            balance += amount;
            ChangedBalance();
        }
        public virtual bool CheckWithdraw(decimal amount)
        {
            if (balance - amount > 0)
                return true;
            else
                return false;
        }
    }
    public class SavingsAccount : BankAccount
    {
        public SavingsAccount(decimal startBalance) : base(startBalance)
        {
            interest = 0.01m;
        }
        public override void MonthlyInterest()
        {
            decimal monthlyInterest = balance * interest;
            balance += monthlyInterest;
        }
    }
    public class StudentSavingsAccount : SavingsAccount
    {
        public StudentSavingsAccount(decimal startBalance) : base(startBalance)
        {

        }
        public override void MonthlyInterest()
        {
            decimal monthlyInterest = balance * interest;
            balance += monthlyInterest;
        }
    }
    public class CreditAccount : BankAccount
    {
        public CreditAccount(decimal startBalance) : base(startBalance)
        {

        }
        int negative = 5_000;
        decimal baseTax = 15;
        public override void MonthlyInterest()
        {
            decimal interestAndTax = 0;
            if (balance > 0)
            {
                interestAndTax += balance * interest;
                interestAndTax += baseTax;
            }
            else if (balance > -negative)
            {
                // rate of pain is going to decline
                interestAndTax += (negative - balance) * interest;
                interestAndTax += baseTax;
            }
            else
            {
                balance = negative; //interestAndTag still gon be 0
            }
            balance -= interestAndTax;
        }
        public override bool CheckWithdraw(decimal amount)
        {
            if (balance + negative - amount > 0)
                return true;
            else
                return false;
        }
    }



}
