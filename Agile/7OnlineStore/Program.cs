using System;

namespace OnlineStoreOrderProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Обработка заказа в интернет-магазине\n");

            var pipeline = new OrderPipeline();
            
            static void CheckStock(OrderContext context)
            {
                Console.WriteLine("Проверка наличия товара");
                context.HasStock = true;
            }

            static void CalculateDelivery(OrderContext context)
            {
                Console.WriteLine("Расчет стоимости доставки");
                context.DeliveryCost = 200;
            }

            OrderStep sendConfirmation = (context) =>
            {
                Console.WriteLine("Отправка подтверждения");
                context.IsConfirmed = true;
            };

            Console.WriteLine("ДОБАВЛЕНИЕ ОБРАБОТЧИКОВ");
            pipeline.Pipeline += CheckStock;
            pipeline.Pipeline += CalculateDelivery;
            pipeline.Pipeline += sendConfirmation;

            var order1 = new OrderContext();
            pipeline.Run(order1);
            
            Console.WriteLine($"Результат: HasStock={order1.HasStock}, " +
                            $"DeliveryCost={order1.DeliveryCost}, " +
                            $"IsConfirmed={order1.IsConfirmed}");

            Console.WriteLine("\nУДАЛЕНИЕ ОБРАБОТЧИКА");
            pipeline.Pipeline -= sendConfirmation;
            Console.WriteLine("Удален обработчик отправки подтверждения");

            var order2 = new OrderContext();
            pipeline.Run(order2);
            
            Console.WriteLine($"Результат после удаления: HasStock={order2.HasStock}, " +
                            $"DeliveryCost={order2.DeliveryCost}, " +
                            $"IsConfirmed={order2.IsConfirmed}");
            Console.WriteLine("IsConfirmed=False - подтверждение НЕ отправлено!");

            Console.WriteLine("\nДОБАВЛЕНИЕ НОВОГО ОБРАБОТЧИКА");
            OrderStep applyDiscount = (context) =>
            {
                Console.WriteLine("Применение скидки 10%");
                context.DeliveryCost = (int)(context.DeliveryCost * 0.9);
            };
            
            pipeline.Pipeline += applyDiscount;

            var order3 = new OrderContext();
            pipeline.Run(order3);
            
            Console.WriteLine($"Результат со скидкой: HasStock={order3.HasStock}, " +
                            $"DeliveryCost={order3.DeliveryCost}, " +
                            $"IsConfirmed={order3.IsConfirmed}");

        }
    }
}