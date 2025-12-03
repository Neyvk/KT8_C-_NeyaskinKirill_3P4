using System;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        private BankAccount account;
        private Logger logger;

        public Form1()
        {
            InitializeComponent();
            account = new BankAccount();
            logger = new Logger(account);

            account.BalanceChanged += UpdateBalanceLabel;
        }

        private void UpdateBalanceLabel(decimal newBalance)
        {
            lblBalance.Text = "Баланс: " + newBalance;
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            account.Deposit(Convert.ToDecimal(txtAmount.Text));
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            account.Withdraw(Convert.ToDecimal(txtAmount.Text));
        }
    }

    class BankAccount
    {
        public decimal Balance { get; private set; }
        public event Action<decimal> BalanceChanged;

        public void Deposit(decimal amount)
        {
            Balance += amount;
            BalanceChanged(Balance);
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance)
            {
                MessageBox.Show("Недостаточно средств!");
                return;
            }
            Balance -= amount;
            BalanceChanged(Balance);
        }
    }

    class Logger
    {
        private string filePath = "D:\\balance_log.txt";

        public Logger(BankAccount account)
        {
            account.BalanceChanged += OnBalanceChanged;
        }

        private void OnBalanceChanged(decimal newBalance)
        {
            string message = $"Баланс изменен: {newBalance}";
            File.AppendAllText(filePath, message + Environment.NewLine);
        }
    }
}
