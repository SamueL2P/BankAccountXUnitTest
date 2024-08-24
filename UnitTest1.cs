using BankAccountXUnitTest.Models;

namespace BankAccountXUnitTest
{
    public class UnitTest1
    {

        BankAccount _account;

        public UnitTest1()
        {
            _account = new BankAccount();
        }
        [Theory]
        [InlineData(100)]
        [InlineData(-100)]
        public void Deposit_ValidAmount_IncreasesBalance(double amount)
        {
            double initialBalance = _account.GetBalance();

            _account.Deposit(amount);

            Assert.Equal(initialBalance + amount, _account.GetBalance());
        }

        [Theory]
        [InlineData(-50)]
        [InlineData(100)]
        public void Does_InvalidAmount_Deposited_ThrowsArgumentException(double amount)
        {
            Assert.Throws<ArgumentException>(() => _account.Deposit(amount));
        }

        [Theory]
        [InlineData(100, 50, 50)]
        [InlineData(200, 100, 200)]
        public void Withdraw_ValidAmount_DecreasesBalance(double initialDeposit, double withdrawAmount, double expectedBalance)
        {
            _account.Deposit(initialDeposit);
            _account.Withdraw(withdrawAmount);

            Assert.Equal(expectedBalance, _account.GetBalance());
        }

        [Theory]
        [InlineData(100, 150)]
        [InlineData(200, 100)]
        public void Withdraw_AmountExceedingBalance_ThrowsInvalidOperationException(double initialDeposit, double withdrawAmount)
        {
            _account.Deposit(initialDeposit);

            var caughtException = Assert.Throws<InvalidOperationException>(() => _account.Withdraw(withdrawAmount));
            Assert.Equal(caughtException.Message, "Insufficient Funds");
        }

        [Theory]
        [InlineData(-50)]
        [InlineData(100)]
        public void Withdraw_InvalidAmount_ThrowsArgumentException(double amount)
        {
            Assert.Throws<ArgumentException>(() => _account.Withdraw(amount));
        }

        [Theory]
        [InlineData(200, 100, 150, 150)]
        [InlineData(200, 50 , 150, 50)]
        public void Transfer_ValidAccount_ReducesSourceBalance_And_IncreasesTargetBalance(double initialDeposit, double transferAmount, double expectedSourceBalance, double expectedTargetBalance)
        {
            var toAccount = new BankAccount();
            _account.Deposit(initialDeposit);
            _account.Transfer(toAccount, transferAmount);

            Assert.Equal(expectedSourceBalance, _account.GetBalance()); 
            Assert.Equal(expectedTargetBalance, toAccount.GetBalance());
        }

        [Theory]
        [InlineData(200, 100)]
      
        public void Transfer_TO_NullAccount_ThrowsArgumentNullException(double initialDeposit, double transferAmount)
        {
            _account.Deposit(initialDeposit);
            BankAccount toAccount = null;
            Assert.Throws<ArgumentNullException>(() => _account.Transfer(toAccount, transferAmount));
        }
    }
}