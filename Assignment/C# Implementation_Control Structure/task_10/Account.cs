namespace Banking_System
{
    public class task10_Account
    {
        public long AccountNumber { get; set; }
        public string AccountType { get; set; }
        public float Balance { get; set; }
        public task10_Customer Customer { get; set; }

        public task10_Account() { }

        public task10_Account(long accNo, string type, float balance, task10_Customer customer)
        {
            AccountNumber = accNo;
            AccountType = type;
            Balance = balance;
            Customer = customer;
        }

        public void PrintAccountInfo()
        {
            Console.WriteLine($"\nAccount Number: {AccountNumber}, Type: {AccountType}, Balance: {Balance}");
            Customer.PrintCustomerInfo();
        }
    }
}
