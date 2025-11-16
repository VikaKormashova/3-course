using System;

namespace ComputerProject
{
    public class Memory
    {
        public int Capacity { get; set; }       
        public string MemoryType { get; set; }  

        public Memory(int capacity, string memoryType)
        {
            Capacity = capacity;
            MemoryType = memoryType;
        }

        public override string ToString()
        {
            return $"Память: {Capacity}GB {MemoryType}";
        }
    }
}