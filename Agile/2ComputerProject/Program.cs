using System;

namespace ComputerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ДЕМОНСТРАЦИЯ РАБОТЫ КЛАССОВ КОМПЬЮТЕРА ===\n");

            CPU cpu1 = new CPU(3.5, 8);
            CPU cpu2 = new CPU(2.8, 4);

            Memory memory1 = new Memory(16, "DDR4");
            Memory memory2 = new Memory(8, "DDR4");
            Memory memory3 = new Memory(32, "DDR5");

            Computer computer1 = new Computer("SN001", "Windows 11", "ASUS ROG Strix", cpu1);
            Computer computer2 = new Computer("SN002", "Linux Ubuntu", "Gigabyte AORUS", cpu2);

            Console.WriteLine("ДОБАВЛЕНИЕ МОДУЛЕЙ ПАМЯТИ:");
            computer1.AddMemoryModule(memory1);
            computer1.AddMemoryModule(memory2);
            computer2.AddMemoryModule(memory3);
            Console.WriteLine();

            computer1.DisplayComputerInfo();
            computer2.DisplayComputerInfo();

            Console.WriteLine("ДЕМОНСТРАЦИЯ ToString():");
            Console.WriteLine(computer1);
            Console.WriteLine(computer2);
            Console.WriteLine(cpu1);
            Console.WriteLine(memory1);
            Console.WriteLine();

            CPU cpu3 = new CPU(4.2, 12);
            Computer computer3 = new Computer("SN003", "Windows 10", "MSI MPG", cpu3);
            computer3.AddMemoryModule(new Memory(64, "DDR5"));
            computer3.AddMemoryModule(new Memory(64, "DDR5"));
            Console.WriteLine();
            computer3.DisplayComputerInfo();

            Console.WriteLine("=== ПРОГРАММА ЗАВЕРШЕНА ===");
        }
    }
}