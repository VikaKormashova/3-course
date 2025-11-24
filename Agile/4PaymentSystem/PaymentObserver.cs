using System;

namespace PaymentSystem
{
    public class PaymentObserver
    {
        public void PrintPaymentInfo(PaymentGateway gateway, decimal amount)
        {
            Console.WriteLine("=== ИНФОРМАЦИЯ О ПЛАТЕЖЕ ===");
            Console.WriteLine($"Провайдер: {gateway.ProviderName}");
            Console.WriteLine($"Статус: {gateway.Process(amount)}");
            Console.WriteLine($"Доп. инфо: {gateway.Info()}");
            Console.WriteLine();
        }

        public void PrintAllInfo(PaymentGateway gateway)
        {
            Console.WriteLine("=== ПОЛНАЯ ИНФОРМАЦИЯ ===");
            Console.WriteLine(gateway.Info());
            Console.WriteLine($"Процесс по умолчанию: {gateway.Process()}");
            Console.WriteLine($"Процесс с описанием: {gateway.Process("тестовый")}");
            Console.WriteLine($"Процесс с валютой: {gateway.Process(75.50m, "EUR")}");
            Console.WriteLine();
        }
    }
}