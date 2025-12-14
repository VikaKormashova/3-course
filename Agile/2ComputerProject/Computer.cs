using System;
using System.Collections.Generic;

namespace ComputerProject
{
    public class Computer
    {
        public string SerialNumber { get; set; }          
        public string OperatingSystem { get; set; }       
        public string Motherboard { get; set; }           
        public CPU Processor { get; set; }                
        public List<Memory> MemoryModules { get; set; }   

        public Computer(string serialNumber, string operatingSystem, string motherboard, CPU processor)
        {
            SerialNumber = serialNumber;
            OperatingSystem = operatingSystem;
            Motherboard = motherboard;
            Processor = processor;
            MemoryModules = new List<Memory>();  
        }

        public void AddMemoryModule(Memory memoryModule)
        {
            MemoryModules.Add(memoryModule);
            Console.WriteLine($"Добавлен модуль памяти: {memoryModule.Capacity}GB {memoryModule.MemoryType}");
        }

        public void DisplayComputerInfo()
        {
            Console.WriteLine("=== ИНФОРМАЦИЯ О КОМПЬЮТЕРЕ ===");
            Console.WriteLine($"Серийный номер: {SerialNumber}");
            Console.WriteLine($"Операционная система: {OperatingSystem}");
            Console.WriteLine($"Материнская плата: {Motherboard}");
            Console.WriteLine($"Процессор: {Processor.Cores} ядер, {Processor.Frequency} GHz");
            Console.WriteLine("Модули памяти:");
            
            foreach (var memory in MemoryModules)
            {
                Console.WriteLine($"  - {memory.Capacity}GB {memory.MemoryType}");
            }
            Console.WriteLine();
        }

        public override string ToString()
        {
            return $"Компьютер {SerialNumber} ({OperatingSystem})";
        }
    }
}