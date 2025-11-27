using System;

namespace ShopStock
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("ТЕСТИРОВАНИЕ СИСТЕМЫ ТОВАРОВ И СКЛАДА\n");

            try
            {
                Console.WriteLine("1. ПРОСТОЙ ТОВАР:");
                var simpleProduct = new SimpleProduct("Книга", 1000);
                Console.WriteLine($"До скидки: {simpleProduct.Price} руб.");
                simpleProduct.ApplyDiscount(20);
                Console.WriteLine($"После 20% скидки: {simpleProduct.Price} руб.");
                Console.WriteLine();

                Console.WriteLine("2. СКЛАДСКОЙ ТОВАР (ТЕСТ СКИДОК):");
                var warehouseItem = new WarehouseItem("Смартфон", 50000, 10, 40000);
                Console.WriteLine($"До скидки: {warehouseItem.Price} руб. (мин. цена: {warehouseItem.MinPrice} руб.)");
                warehouseItem.ApplyDiscount(10);
                Console.WriteLine($"После 10% скидки: {warehouseItem.Price} руб.");
                warehouseItem.ApplyDiscount(50);
                Console.WriteLine($"После 50% скидки (не ниже минимума): {warehouseItem.Price} руб.");
                Console.WriteLine();

                Console.WriteLine("3. СКЛАДСКОЙ ТОВАР (ТЕСТ РЕЗЕРВИРОВАНИЯ):");
                Console.WriteLine($"Начальный запас: {warehouseItem.Stock} шт.");
                
                bool result1 = warehouseItem.Reserve(3);
                Console.WriteLine($"Резервирование 3 шт.: {(result1 ? "УСПЕХ" : "НЕУДАЧА")}, остаток: {warehouseItem.Stock} шт.");
                
                bool result2 = warehouseItem.Reserve(8);
                Console.WriteLine($"Резервирование 8 шт.: {(result2 ? "УСПЕХ" : "НЕУДАЧА")}, остаток: {warehouseItem.Stock} шт.");
                
                bool result3 = warehouseItem.Reserve(5);
                Console.WriteLine($"Резервирование 5 шт.: {(result3 ? "УСПЕХ" : "НЕУДАЧА")}, остаток: {warehouseItem.Stock} шт.");
                Console.WriteLine();

                Console.WriteLine("4. ПОЛИМОРФИЗМ ЧЕРЕЗ ИНТЕРФЕЙСЫ:");
                IProduct[] products = {
                    new SimpleProduct("Ручка", 50),
                    new WarehouseItem("Ноутбук", 100000, 5, 80000)
                };

                foreach (var product in products)
                {
                    Console.WriteLine($"Товар: {product.Name}, цена до: {product.Price} руб.");
                    product.ApplyDiscount(30);
                    Console.WriteLine($"Цена после 30% скидки: {product.Price} руб.");
                    
                    if (product is IStockItem stockItem)
                    {
                        Console.WriteLine($"Запас на складе: {stockItem.Stock} шт.");
                    }
                    Console.WriteLine();
                }

                Console.WriteLine("ТЕСТИРОВАНИЕ ЗАВЕРШЕНО");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
            }
        }
    }
}