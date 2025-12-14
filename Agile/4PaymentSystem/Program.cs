using System;
using PaymentSystem;  

class Program
{
    static void Main()
    {
        Console.WriteLine(" ДЕМОНСТРАЦИЯ ПЛАТЕЖНОЙ СИСТЕМЫ \n");

        try
        {
            Console.WriteLine("1. БАЗОВЫЙ КЛАСС:");
            var baseGateway = new PaymentGateway("Stripe");
            Console.WriteLine(baseGateway.Info());
            baseGateway.EnableSandbox(true);
            Console.WriteLine($"Process: {baseGateway.Process(100.50m)}");
            Console.WriteLine();

            Console.WriteLine("2. КАРТОЧНЫЙ ШЛЮЗ:");
            var cardGateway = new CardGateway("Visa");
            cardGateway.SetMaskedPan("1234567812345678");
            Console.WriteLine($"Process: {cardGateway.Process(200.75m)}");
            Console.WriteLine();

            Console.WriteLine("3. КОШЕЛЬКОВЫЙ ШЛЮЗ:");
            var walletGateway = new WalletGateway("PayPal");
            walletGateway.Link("user12345");
            Console.WriteLine($"Process (базовый): {walletGateway.Process(150.25m)}");
            Console.WriteLine();

            Console.WriteLine("4. КРИПТО-КОШЕЛЕК:");
            var cryptoWallet = new CryptoWallet("Binance");
            cryptoWallet.Link("crypto_wallet_789");
            cryptoWallet.SwitchNetwork("BTC");
            Console.WriteLine($"Process: {cryptoWallet.Process(500.00m)}");
            Console.WriteLine();

            Console.WriteLine("5. ПОЛИМОРФИЗМ ЧЕРЕЗ МАССИВ:");
            PaymentGateway[] gateways = {
                new PaymentGateway("BaseProvider"),
                new CardGateway("MasterCard"),
                new WalletGateway("WebMoney"),
                new CryptoWallet("Coinbase")
            };

            ((CardGateway)gateways[1]).SetMaskedPan("8765432187654321");
            ((WalletGateway)gateways[2]).Link("webmoney123");
            ((CryptoWallet)gateways[3]).Link("coinbase456");
            ((CryptoWallet)gateways[3]).SwitchNetwork("ETH");

            foreach (var gateway in gateways)
            {
                Console.WriteLine($"=== {gateway.GetType().Name} ===");
                Console.WriteLine($"Инфо: {gateway.Info()}");
                Console.WriteLine($"Процесс: {gateway.Process(99.99m)}");
                Console.WriteLine();
            }

            Console.WriteLine(" ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА: {ex.Message}");
        }
    }
}