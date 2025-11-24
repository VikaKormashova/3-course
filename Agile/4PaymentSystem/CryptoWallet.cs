using System;
using System.Linq;

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
                string[] allowedNetworks = { "BTC", "ETH", "LTC", "BCH", "XRP" };
                if (string.IsNullOrWhiteSpace(value) || !allowedNetworks.Contains(value.ToUpper()))
                    throw new ArgumentException($"Недопустимая сеть. Разрешены: {string.Join(", ", allowedNetworks)}");
                _network = value.ToUpper();
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
            Amount = amount;

            if (string.IsNullOrEmpty(WalletId))
                throw new InvalidOperationException("Не привязан кошелек");

            if (string.IsNullOrEmpty(Network))
                throw new InvalidOperationException("Не выбрана сеть");

            return $"Обработано {Amount} в сети {Network} через крипто-кошелек {WalletId}";
        }

        public string Process(decimal amount, bool fastTransaction)
        {
            Amount = amount;
            string speed = fastTransaction ? "быструю" : "стандартную";
            return $"Обработано {Amount} в сети {Network} ({speed} транзакцию)";
        }
    }
}