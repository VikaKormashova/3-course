using System;

namespace CarProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Демонстрация работы класса Автомобиль ===\n");

            Car car1 = new Car("Toyota", "Camry", 5000);
            Car car2 = new Car("BMW", "X5");
            Car car3 = new Car("Lada", "Vesta", 10000);

            Console.WriteLine("НАЧАЛЬНОЕ СОСТОЯНИЕ:");
            Console.WriteLine(car1);
            Console.WriteLine(car2);
            Console.WriteLine(car3);
            Console.WriteLine();

            Console.WriteLine("ПОЕЗДКИ:");
            car1.Drive(150);
            car2.Drive(200);
            car3.Drive(300);
            car1.Drive(50);
            car2.Drive(-100); 
            Console.WriteLine();

            Console.WriteLine("КОНЕЧНОЕ СОСТОЯНИЕ:");
            Console.WriteLine(car1);
            Console.WriteLine(car2);
            Console.WriteLine(car3);

            Console.WriteLine($"\nОбщий пробег {car1.Brand} {car1.Model}: {car1.Mileage} км");
            Console.WriteLine($"Общий пробег {car2.Brand} {car2.Model}: {car2.Mileage} км");
            Console.WriteLine($"Общий пробег {car3.Brand} {car3.Model}: {car3.Mileage} км");

            Console.WriteLine("\n=== Программа завершена ===");
        }
    }
}