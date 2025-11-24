using System;
using PaymentSystem;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ ПЛАТЕЖНОЙ СИСТЕМЫ ===\n");

        try
        {
            var observer = new PaymentObserver();

            Console.WriteLine("1. БАЗОВЫЙ КЛАСС:");
            var baseGateway = new PaymentGateway("Stripe");
            Console.WriteLine(baseGateway.Info());
            baseGateway.EnableSandbox(true);
            
            Console.WriteLine("Перегруженные методы:");
            Console.WriteLine($"- Process(): {baseGateway.Process()}");
            Console.WriteLine($"- Process(описание): {baseGateway.Process("тестовый платеж")}");
            Console.WriteLine($"- Process(сумма, валюта): {baseGateway.Process(200.00m, "USD")}");
            Console.WriteLine($"- Process(основной): {baseGateway.Process(100.50m)}");
            
            observer.PrintPaymentInfo(baseGateway, 300.00m);
            Console.WriteLine();

            Console.WriteLine("2. КАРТОЧНЫЙ ШЛЮЗ:");
            var cardGateway = new CardGateway("Visa");
            cardGateway.SetMaskedPan("1234567812345678");
            
            Console.WriteLine("Перегруженные методы CardGateway:");
            Console.WriteLine($"- Process(): {cardGateway.Process()}");
            Console.WriteLine($"- Process(тип, сумма): {cardGateway.Process("кредитную", 150.25m)}");
            Console.WriteLine($"- Process(основной): {cardGateway.Process(200.75m)}");
            
            observer.PrintPaymentInfo(cardGateway, 500.00m);
            Console.WriteLine();

            Console.WriteLine("3. КОШЕЛЬКОВЫЙ ШЛЮЗ:");
            var walletGateway = new WalletGateway("PayPal");
            walletGateway.Link("user12345");
            
            Console.WriteLine("WalletGateway НЕ переопределяет Process:");
            Console.WriteLine($"Process (базовый): {walletGateway.Process(150.25m)}");
            
            observer.PrintAllInfo(walletGateway);
            Console.WriteLine();

            Console.WriteLine("4. КРИПТО-КОШЕЛЕК:");
            var cryptoWallet = new CryptoWallet("Binance");
            cryptoWallet.Link("crypto_wallet_789");
            cryptoWallet.SwitchNetwork("BTC");
            
            Console.WriteLine("Перегруженные методы CryptoWallet:");
            Console.WriteLine($"- Process(быстрая): {cryptoWallet.Process(100.00m, true)}");
            Console.WriteLine($"- Process(стандартная): {cryptoWallet.Process(100.00m, false)}");
            Console.WriteLine($"- Process(основной): {cryptoWallet.Process(500.00m)}");
            
            observer.PrintAllInfo(cryptoWallet);
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

            Console.WriteLine("=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ОШИБКА: {ex.Message}");
        }
    }
}