using System;

namespace PaymentSystem
{
    public class WalletGateway : PaymentGateway
    {
        private string _walletId = "";

        public string WalletId
        {
            get { return _walletId; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("ID кошелька не может быть пустым");
                _walletId = value;
            }
        }

        public WalletGateway(string providerName, bool sandbox = false) 
            : base(providerName, sandbox)
        {
        }

        public void Link(string id)
        {
            WalletId = id;
            Console.WriteLine($"Кошелек привязан: {WalletId}");
        }
    }
}