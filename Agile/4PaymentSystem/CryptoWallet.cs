using System;

namespace PaymentSystem
{
    public class CryptoWallet : WalletGateway
    {
        private string _network = "";

        public string Network
        {
            get { return _network; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название сети не может быть пустым");
                _network = value;
            }
        }

        public CryptoWallet(string providerName, bool sandbox = false) 
            : base(providerName, sandbox)
        {
        }

        public void SwitchNetwork(string network)
        {
            Network = network;
            Console.WriteLine($"Сеть изменена на: {Network}");
        }

        public override string Process(decimal amount)
        {
            if (string.IsNullOrEmpty(WalletId))
                throw new InvalidOperationException("Не привязан кошелек");

            if (string.IsNullOrEmpty(Network))
                throw new InvalidOperationException("Не выбрана сеть");

            return $"Processed {amount} via {Network} network wallet {WalletId}";
        }
    }
}